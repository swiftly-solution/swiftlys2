// Included inside Breakpad MinidumpWriter by the collector build.
  static bool ClrMemoryRegion(void* context, uint64_t address, uint64_t size) {
    return static_cast<MinidumpWriter*>(context)->WriteClrRegion(address, size);
  }

  bool WriteClrRegion(uint64_t address, uint64_t size) {
    const uint64_t end = address + size;
    while (address < end) {
      uint64_t stop = end;
      bool saved = false;
      for (size_t i = 0; i < memory_blocks_.size(); ++i) {
        const MDMemoryDescriptor& old = memory_blocks_[i];
        const uint64_t begin = old.start_of_memory_range;
        const uint64_t old_end = begin + old.memory.data_size;
        if (begin <= address && address < old_end) {
          address = std::min(end, old_end);
          saved = true;
          break;
        }
        if (address < begin) stop = std::min(stop, begin);
      }
      if (saved) continue;
      while (address < stop) {
        const size_t wanted = std::min(uint64_t(1024 * 1024), stop - address);
        ssize_t actual;
        do { actual = sys_pread64(clr_mem_fd_, clr_buffer_, wanted, address); }
        while (actual < 0 && errno == EINTR);
        // Fail explicitly rather than silently synthesizing missing bytes.
        if (actual <= 0 || memory_blocks_.size() >= 4000 ||
            uint64_t(minidump_writer_.position()) + actual >
                uint64_t(3) * 1024 * 1024 * 1024) return false;
        UntypedMDRVA memory(&minidump_writer_);
        if (!memory.Allocate(actual) || !memory.Copy(clr_buffer_, actual)) return false;
        MDMemoryDescriptor descriptor;
        descriptor.start_of_memory_range = address;
        descriptor.memory = memory.location();
        memory_blocks_.push_back(descriptor);
        address += actual;
      }
    }
    return true;
  }

  bool WriteClrProcessMemory() {
    if (dumper_->IsPostMortem()) return false;
    char path[NAME_MAX];
    if (!dumper_->BuildProcPath(path, GetCrashThread(), "mem")) return false;
    clr_mem_fd_ = sys_open(path, O_RDONLY, 0);
    if (clr_mem_fd_ < 0) return false;
    clr_buffer_ = static_cast<uint8_t*>(dumper_->allocator()->Alloc(1024 * 1024));
    bool success = false;
    try {
      success = clr_buffer_ && CollectClrMemory(
          dumper_, GetCrashThread(), ucontext_, ClrMemoryRegion, this);
    } catch (...) {
      // Keep writing the original native streams if CLR enumeration fails.
    }
    sys_close(clr_mem_fd_);
    return success;
  }

  int clr_mem_fd_ = -1;
  uint8_t* clr_buffer_ = nullptr;
