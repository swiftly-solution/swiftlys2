local root = os.projectdir()
local breakpad = path.join(root, "vendor/breakpad/src")
local collector = path.join(root, "src/monitor/crashreporter/managed_dump")

target("sw2dump")
    set_kind("binary")
    set_optimize("fastest")
    add_includedirs(path.join(root, "vendor"), path.join(root, "src/monitor/crashreporter"), breakpad)
    add_defines("HAVE_CONFIG_H", "HAVE_GETCONTEXT=1", "HAVE_GETRANDOM=1", "HAVE_SYS_RANDOM_H=1", "HAVE_PTHREAD=1", "HAVE_CXX17=1", "__STDC_FORMAT_MACROS")
    add_links("dl", "pthread")
    add_files(path.join(collector, "*.cc"))
    add_files(
        path.join(breakpad, "client/linux/crash_generation/crash_generation_server.cc"),
        path.join(breakpad, "client/linux/dump_writer_common/thread_info.cc"),
        path.join(breakpad, "client/linux/dump_writer_common/ucontext_reader.cc"),
        path.join(breakpad, "client/linux/log/log.cc"),
        path.join(breakpad, "client/linux/minidump_writer/linux_core_dumper.cc"),
        path.join(breakpad, "client/linux/minidump_writer/linux_dumper.cc"),
        path.join(breakpad, "client/linux/minidump_writer/linux_ptrace_dumper.cc"),
        path.join(breakpad, "client/linux/minidump_writer/pe_file.cc"),
        path.join(breakpad, "client/minidump_file_writer.cc"),
        path.join(breakpad, "common/convert_UTF.cc"),
        path.join(breakpad, "common/md5.cc"),
        path.join(breakpad, "common/string_conversion.cc"),
        path.join(breakpad, "common/linux/breakpad_getcontext.S"),
        path.join(breakpad, "common/linux/elf_core_dump.cc"),
        path.join(breakpad, "common/linux/elfutils.cc"),
        path.join(breakpad, "common/linux/file_id.cc"),
        path.join(breakpad, "common/linux/guid_creator.cc"),
        path.join(breakpad, "common/linux/linux_libc_support.cc"),
        path.join(breakpad, "common/linux/memory_mapped_file.cc"),
        path.join(breakpad, "common/linux/safe_readlink.cc"))

    on_load(function(target)
        -- Keep the pinned Breakpad submodule untouched. Only the collector's
        -- writer gains a hook while target threads are still suspended.
        local source = io.readfile(path.join(breakpad, "client/linux/minidump_writer/minidump_writer.cc"))
        local function replace_once(old, replacement)
            local first, last = source:find(old, 1, true)
            assert(first and not source:find(old, last + 1, true), "Breakpad writer layout changed; update the CLR capture hook")
            source = source:sub(1, first - 1) .. replacement .. source:sub(last + 1)
        end
        local include = '#include "client/linux/dump_writer_common/ucontext_reader.h"'
        replace_once(include, include .. '\n#include "managed_dump/clr_capture.h"')
        local memory = '    if (!WriteAppMemory())\n      return false;'
        replace_once(memory, memory .. [[

    if (!WriteClrProcessMemory()) {
      static const char warning[] = "sw2dump: CLR memory capture incomplete; preserving native dump\n";
      sys_write(STDERR_FILENO, warning, sizeof(warning) - 1);
    }
]])
        local members = '  static bool ShouldIncludeMapping('
        replace_once(members, '#include "managed_dump/writer_extension.h"\n\n' .. members)
        local generated = path.absolute(path.join(target:autogendir(), "minidump_writer.cc"), root)
        os.mkdir(path.directory(generated))
        if not os.isfile(generated) or io.readfile(generated) ~= source then
            io.writefile(generated, source)
        end
        target:add("files", generated)
    end)
