#include "clr_capture.h"
#include "dac_abi.h"

#include <algorithm>
#include <cerrno>
#include <cstdio>
#include <cstdlib>
#include <dlfcn.h>
#include <fcntl.h>
#include <fstream>
#include <map>
#include <set>
#include <sstream>
#include <string>
#include <unistd.h>
#include <vector>

#include "client/linux/minidump_writer/linux_dumper.h"
#include "client/linux/dump_writer_common/ucontext_reader.h"

namespace {
struct Mapping { uint64_t begin, end, offset; std::string permissions, path; };
struct Region { uint64_t begin, end; };

class Target final : public DataTarget, public RuntimeLocator, public MemoryCallback {
 public:
  Target(google_breakpad::LinuxDumper* dumper, pid_t tid,
         const ucontext_t* context) : dumper_(dumper), tid_(tid) {
    const std::string base = "/proc/" + std::to_string(tid);
    mem_ = open((base + "/mem").c_str(), O_RDONLY | O_CLOEXEC);
    std::ifstream maps(base + "/maps");
    std::string line;
    while (std::getline(maps, line)) {
      Mapping m{};
      std::string range, device;
      uint64_t inode;
      std::istringstream input(line);
      input >> range >> m.permissions >> std::hex >> m.offset >> device >> std::dec >> inode;
      const auto dash = range.find('-');
      if (dash == std::string::npos) continue;
      m.begin = std::stoull(range.substr(0, dash), nullptr, 16);
      m.end = std::stoull(range.substr(dash + 1), nullptr, 16);
      std::getline(input >> std::ws, m.path);
      if (m.path.ends_with("/libcoreclr.so") && m.offset == 0) {
        runtime_ = m.begin;
        dac_path_ = m.path.substr(0, m.path.rfind('/') + 1) + "libmscordaccore.so";
      }
      if (!m.permissions.empty() && m.permissions[0] == 'r') {
        readable_bytes_ += m.end - m.begin;
        mappings_.push_back(m);
      }
    }
    for (size_t i = 0; i < dumper_->threads().size(); ++i) {
      google_breakpad::ThreadInfo info{};
      if (!dumper_->GetThreadInfoByIndex(i, &info)) continue;
      auto& cpu = contexts_[dumper_->threads()[i]];
      info.FillCPUContext(&cpu);
      if (dumper_->threads()[i] == tid && context)
        google_breakpad::UContextReader::FillCPUContext(
            &cpu, context, reinterpret_cast<const google_breakpad::fpstate_t*>(&info.fpregs));
    }
  }
  ~Target() { if (mem_ >= 0) close(mem_); }
  bool Ready() const { return mem_ >= 0 && runtime_ && !contexts_.empty(); }
  const std::string& DacPath() const { return dac_path_; }

  HResult QueryInterface(const Guid& id, void** out) override {
    *out = nullptr;
    if (id == kUnknown || id == kDataTarget) *out = static_cast<DataTarget*>(this);
    else if (id == kRuntimeLocator) *out = static_cast<RuntimeLocator*>(this);
    else if (id == kMemoryCallback) *out = static_cast<MemoryCallback*>(this);
    return *out ? kOk : kNoInterface;
  }
  uint32_t AddRef() override { return 1; }
  uint32_t Release() override { return 1; }
  HResult GetMachineType(uint32_t* value) override { *value = 0x8664; return kOk; }
  HResult GetPointerSize(uint32_t* value) override { *value = 8; return kOk; }
  HResult GetRuntimeBase(uint64_t* value) override { *value = runtime_; return kOk; }
  HResult GetImageBase(const char16_t* name, uint64_t* value) override {
    std::string path;
    while (*name) path.push_back(char(*name++));
    for (const auto& m : mappings_) {
      if (m.path == path || m.path.substr(m.path.rfind('/') + 1) == path) {
        *value = m.begin - m.offset;
        return kOk;
      }
    }
    *value = 0;
    return kFail;
  }
  HResult ReadVirtual(uint64_t address, uint8_t* data, uint32_t size,
                      uint32_t* done) override {
    ssize_t count;
    do { count = pread(mem_, data, size, address); } while (count < 0 && errno == EINTR);
    *done = count > 0 ? uint32_t(count) : 0;
    if (count <= 0) return kFail;
    Select(address, count);
    return kOk;
  }
  HResult GetThreadContext(uint32_t id, uint32_t flags, uint32_t size,
                           uint8_t* buffer) override {
    const auto found = contexts_.find(id);
    if (found == contexts_.end() || size < sizeof(found->second)) return kFail;
    std::memset(buffer, 0, size);
    std::memcpy(buffer, &found->second, sizeof(found->second));
    reinterpret_cast<google_breakpad::RawContextCPU*>(buffer)->context_flags = flags;
    return kOk;
  }
  HResult GetCurrentThreadID(uint32_t* value) override { *value = tid_; return kOk; }
  HResult WriteVirtual(uint64_t, uint8_t*, uint32_t, uint32_t*) override { return kNotImpl; }
  HResult GetTLSValue(uint32_t, uint32_t, uint64_t*) override { return kNotImpl; }
  HResult SetTLSValue(uint32_t, uint32_t, uint64_t) override { return kNotImpl; }
  HResult SetThreadContext(uint32_t, uint32_t, uint8_t*) override { return kNotImpl; }
  HResult Request(uint32_t, uint32_t, uint8_t*, uint32_t, uint8_t*) override { return kNotImpl; }
  HResult EnumMemoryRegion(uint64_t address, uint32_t size) override {
    Select(address, size);
    return kOk;
  }

  void Select(uint64_t address, uint64_t size) {
    if (!size || size > UINT64_MAX - 4095 || address > UINT64_MAX - size - 4095) return;
    regions_.push_back({address & ~uint64_t(4095), (address + size + 4095) & ~uint64_t(4095)});
  }

  bool SelectMetadata(Unknown* sos) {
    DomainStore store{};
    if (SosCall<4>(sos, &store) < 0 || store.count < 0 || store.count > 4096) return false;
    std::vector<uint64_t> domains(store.count);
    uint32_t count = 0;
    if (SosCall<5>(sos, uint32_t(domains.size()), domains.data(), &count) < 0 || count > domains.size()) return false;
    domains.resize(count);
    std::set<uint64_t> modules_seen;
    for (auto domain : domains) {
      int32_t assemblies_needed = 0;
      if (SosCall<9>(sos, domain, int32_t(0), static_cast<uint64_t*>(nullptr), &assemblies_needed) < 0 ||
          assemblies_needed < 0 || assemblies_needed > 100000) return false;
      std::vector<uint64_t> assemblies(assemblies_needed);
      if (SosCall<9>(sos, domain, assemblies_needed, assemblies.data(), &assemblies_needed) < 0 ||
          size_t(assemblies_needed) > assemblies.size()) return false;
      for (auto assembly : assemblies) {
        uint32_t needed = 0;
        if (SosCall<15>(sos, assembly, uint32_t(0), static_cast<uint64_t*>(nullptr), &needed) < 0 || needed > 100000) return false;
        std::vector<uint64_t> modules(needed);
        if (SosCall<15>(sos, assembly, needed, modules.data(), &needed) < 0 || needed > modules.size()) return false;
        for (auto address : modules) {
          if (!modules_seen.insert(address).second) continue;
          ModuleData data{};
          if (SosCall<13>(sos, address, &data) < 0) return false;
          if (data.il_base) Select(data.il_base, 4096);
          if (data.metadata) Select(data.metadata, data.metadata_size);
        }
      }
    }
    std::fprintf(stderr, "SW2DUMP_CLR_METADATA_MODULES=%zu\n", modules_seen.size());
    return true;
  }

  bool SelectHeap(Unknown* sos) {
    HeapData data{};
    if (SosCall<46>(sos, &data) < 0 || !data.valid || data.count > 4096) return false;
    std::vector<uint64_t> heaps(data.server ? data.count : 1, 0);
    uint32_t needed = 0;
    if (data.server && SosCall<47>(sos, uint32_t(heaps.size()), heaps.data(), &needed) < 0) return false;
    Unknown* sos8 = nullptr;
    if (sos->QueryInterface(kSos8, reinterpret_cast<void**>(&sos8)) < 0) return false;
    uint32_t generations = 0;
    bool success = SosCall<3>(sos8, &generations) >= 0 && generations > 0 && generations < 32;
    std::set<uint64_t> visited;
    uint64_t heap_bytes = 0;
    for (auto heap : heaps) {
      if (!success) break;
      HeapDetails details{};
      HResult hr = data.server ? SosCall<48>(sos, heap, &details) : SosCall<49>(sos, &details);
      if (hr < 0) { success = false; break; }
      std::vector<Generation> table(generations);
      hr = data.server ? SosCall<6>(sos8, heap, generations, table.data(), &needed)
                       : SosCall<4>(sos8, generations, table.data(), &needed);
      if (hr < 0 || needed > generations) { success = false; break; }
      for (const auto& gen : table) {
        uint64_t address = gen.start_segment;
        while (address && visited.insert(address).second) {
          if (visited.size() > 100000) { success = false; break; }
          SegmentData seg{};
          if (SosCall<50>(sos, address, &seg) < 0) { success = false; break; }
          uint64_t end = seg.high_mark;
          if (!end) end = seg.address == details.ephemeral_segment ? details.allocated : seg.allocated;
          if (seg.mem && end > seg.mem) {
            Select(seg.mem, end - seg.mem);
            heap_bytes += end - seg.mem;
          }
          address = seg.next;
        }
      }
    }
    sos8->Release();
    std::fprintf(stderr, "SW2DUMP_CLR_GC_SEGMENTS=%zu\nSW2DUMP_CLR_GC_ALLOCATED_BYTES=%llu\n",
                 visited.size(), (unsigned long long)heap_bytes);
    return success;
  }

  bool Emit(ClrMemorySink sink, void* context) {
    std::sort(regions_.begin(), regions_.end(), [](auto a, auto b) { return a.begin < b.begin; });
    std::vector<Region> merged;
    for (auto r : regions_) {
      if (!merged.empty() && r.begin <= merged.back().end)
        merged.back().end = std::max(merged.back().end, r.end);
      else merged.push_back(r);
    }
    uint64_t selected = 0;
    size_t count = 0;
    for (auto r : merged) {
      for (const auto& m : mappings_) {
        const uint64_t start = std::max(r.begin, m.begin), end = std::min(r.end, m.end);
        if (end <= start) continue;
        if (!sink(context, start, end - start)) return false;
        selected += end - start;
        ++count;
      }
    }
    std::fprintf(stderr, "SW2DUMP_CLR_SELECTED_RANGES=%zu\nSW2DUMP_CLR_SELECTED_BYTES=%llu\nSW2DUMP_CLR_ALL_READABLE_BYTES=%llu\n",
                 count, (unsigned long long)selected, (unsigned long long)readable_bytes_);
    return true;
  }

 private:
  google_breakpad::LinuxDumper* dumper_;
  pid_t tid_;
  int mem_ = -1;
  uint64_t runtime_ = 0, readable_bytes_ = 0;
  std::string dac_path_;
  std::vector<Mapping> mappings_;
  std::vector<Region> regions_;
  std::map<pid_t, google_breakpad::RawContextCPU> contexts_;
};
}  // namespace

bool CollectClrMemory(google_breakpad::LinuxDumper* dumper, pid_t tid,
                      const ucontext_t* context, ClrMemorySink sink, void* sink_context) {
  Target target(dumper, tid, context);
  if (!target.Ready()) { std::fprintf(stderr, "SW2DUMP_CLR_TARGET_FAILED\n"); return false; }
  void* library = dlopen(target.DacPath().c_str(), RTLD_NOW | RTLD_LOCAL);
  if (!library) { std::fprintf(stderr, "SW2DUMP_CLR_DAC_LOAD_FAILED: %s\n", dlerror()); return false; }
  auto dll_main = reinterpret_cast<int32_t (*)(void*, uint32_t, void*)>(dlsym(library, "DllMain"));
  if (dll_main) {
    auto register_module = reinterpret_cast<void* (*)(const char*)>(dlsym(library, "PAL_RegisterModule"));
    if (!register_module || !dll_main(register_module(target.DacPath().c_str()), 1, nullptr)) return false;
  }
  auto create = reinterpret_cast<CreateDac>(dlsym(library, "CLRDataCreateInstance"));
  EnumMemory* enumerator = nullptr;
  HResult hr = create ? create(kEnumMemory, &target, reinterpret_cast<void**>(&enumerator)) : kFail;
  std::fprintf(stderr, "SW2DUMP_CLR_DAC_CREATE_HR=%08x\n", uint32_t(hr));
  if (hr < 0) return false;
  // Heap2 supplies CLR/loader state; allocated GC segments are added separately.
  // These selected ranges do not guarantee complete handle-table or SyncBlock
  // diagnostics. They retain object contents without including every native heap.
  hr = enumerator->EnumMemoryRegions(&target, 0x200, 3);
  std::fprintf(stderr, "SW2DUMP_CLR_ENUM_HR=%08x\n", uint32_t(hr));
  bool success = hr >= 0;
  if (success) {
    Unknown* sos = nullptr;
    hr = create(kSos, &target, reinterpret_cast<void**>(&sos));
    success = hr >= 0 && target.SelectMetadata(sos) && target.SelectHeap(sos);
    if (sos) sos->Release();
  }
  enumerator->Release();
  // The collector is short-lived; keep the initialized DAC loaded until exit.
  return success && target.Emit(sink, sink_context);
}
