#pragma once

// Small Linux x86-64 ABI subset from dotnet/runtime v10.0.0:
// src/coreclr/inc/{clrdata.idl,sospriv.idl,dacprivate.h} (MIT license).
// https://github.com/dotnet/runtime/tree/v10.0.0/src/coreclr/inc
#include <cstdint>
#include <cstring>

#if !defined(__linux__) || !defined(__x86_64__)
#error "Managed minidump capture requires Linux x86-64"
#endif

using HResult = int32_t;
struct Guid {
  uint32_t a;
  uint16_t b, c;
  uint8_t d[8];
  bool operator==(const Guid& other) const {
    return std::memcmp(this, &other, sizeof(*this)) == 0;
  }
};
constexpr HResult kOk = 0;
constexpr HResult kFail = HResult(0x80004005u);
constexpr HResult kNotImpl = HResult(0x80004001u);
constexpr HResult kNoInterface = HResult(0x80004002u);
constexpr Guid kUnknown{0,0,0,{0xc0,0,0,0,0,0,0,0x46}};
constexpr Guid kDataTarget{0x3e11ccee,0xd08b,0x43e5,{0xaf,1,0x32,0x71,0x7a,0x64,0xda,3}};
constexpr Guid kRuntimeLocator{0xb760bf44,0x9377,0x4597,{0x8b,0xe7,0x58,8,0x3b,0xdc,0x51,0x46}};
constexpr Guid kMemoryCallback{0xbcdd6908,0xba2d,0x4ec5,{0x96,0xcf,0xdf,0x4d,0x5c,0xdc,0xb4,0xa4}};
constexpr Guid kEnumMemory{0x471c35b4,0x7c2f,0x4ef0,{0xa9,0x45,0,0xf8,0xc3,0x80,0x56,0xf1}};
constexpr Guid kSos{0x436f00f2,0xb42a,0x4b9f,{0x87,0x0c,0xe7,0x3d,0xb6,0x6a,0xe9,0x30}};
constexpr Guid kSos8{0xc12f35a9,0xe55c,0x4520,{0xa8,0x94,0xb3,0xdc,0x51,0x65,0xdf,0xce}};

struct Unknown {
  virtual HResult QueryInterface(const Guid&, void**) = 0;
  virtual uint32_t AddRef() = 0;
  virtual uint32_t Release() = 0;
};
struct DataTarget : Unknown {
  virtual HResult GetMachineType(uint32_t*) = 0;
  virtual HResult GetPointerSize(uint32_t*) = 0;
  virtual HResult GetImageBase(const char16_t*, uint64_t*) = 0;
  virtual HResult ReadVirtual(uint64_t, uint8_t*, uint32_t, uint32_t*) = 0;
  virtual HResult WriteVirtual(uint64_t, uint8_t*, uint32_t, uint32_t*) = 0;
  virtual HResult GetTLSValue(uint32_t, uint32_t, uint64_t*) = 0;
  virtual HResult SetTLSValue(uint32_t, uint32_t, uint64_t) = 0;
  virtual HResult GetCurrentThreadID(uint32_t*) = 0;
  virtual HResult GetThreadContext(uint32_t, uint32_t, uint32_t, uint8_t*) = 0;
  virtual HResult SetThreadContext(uint32_t, uint32_t, uint8_t*) = 0;
  virtual HResult Request(uint32_t, uint32_t, uint8_t*, uint32_t, uint8_t*) = 0;
};
struct RuntimeLocator : Unknown {
  virtual HResult GetRuntimeBase(uint64_t*) = 0;
};
struct MemoryCallback : Unknown {
  virtual HResult EnumMemoryRegion(uint64_t, uint32_t) = 0;
};
struct EnumMemory : Unknown {
  virtual HResult EnumMemoryRegions(MemoryCallback*, uint32_t, uint32_t) = 0;
};
using CreateDac = HResult (*)(const Guid&, DataTarget*, void**);

struct Generation { uint64_t start_segment, allocation_start, alloc_ptr, alloc_limit; };
struct HeapDetails {
  uint64_t heap, allocated, mark_array, gc_state, next_sweep_obj;
  uint64_t saved_segment, saved_start, bg_low, bg_high;
  Generation generations[4];
  uint64_t ephemeral_segment, finalization[7], low, high, card_table;
};
struct HeapData { int32_t server, valid; uint32_t count, max_generation; };
struct DomainStore { uint64_t shared, system; int32_t count; uint32_t padding; };
struct ModuleData {
  uint64_t address, pe_assembly, il_base, metadata, metadata_size, assembly;
  int32_t reflection, pe_file;
  uint64_t base_class_index, module_id;
  uint32_t flags, padding;
  uint64_t maps[7], loader_allocator, thunk_heap, module_index;
};
struct SegmentData {
  uint64_t address, allocated, committed, reserved, used, mem, next, heap;
  uint64_t high_mark, flags, bg_allocated;
};
static_assert(sizeof(HeapDetails) == 288);
static_assert(sizeof(SegmentData) == 88);
static_assert(sizeof(ModuleData) == 160);

// Slots and signatures follow the SOS interfaces in the .NET 10 definitions.
template<size_t Slot, typename... Args>
HResult SosCall(Unknown* object, Args... args) {
  auto table = *reinterpret_cast<void***>(object);
  return reinterpret_cast<HResult (*)(Unknown*, Args...)>(table[Slot])(object, args...);
}
