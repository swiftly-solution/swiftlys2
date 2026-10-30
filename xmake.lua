set_project("swiftlys2")
set_version("1.0.0")

add_requires("pcre2")

set_languages("cxx23")

add_rules("mode.debug", "mode.release")

option("profiler")
    set_default(false)
    set_showmenu(true)
    set_description("Enable the PROFILE_* scope profiler")
    add_defines("PROFILER")
option_end()

local GITHUB_SHA = os.getenv("GITHUB_SHA") or "Local"
local SWIFTLY_VERSION = os.getenv("SWIFTLY_VERSION") or "Local"

local sdk_path = "vendor/s2sdk"
local breakpad_path = "vendor/breakpad/src"

includes(sdk_path)

target("swiftlys2")
    set_kind("shared")
    add_options("profiler")
    add_packages("pcre2")
    add_deps("s2sdk")
    add_rules("cpp_properties")

    set_runtimes("MT", {public = true})

    add_files({
        "src/**/*.cpp",
    }, { cxxflags = "-rdynamic -g1" })

    --[[ -------------------------------- Include Section -------------------------------- ]]

    add_includedirs({
        "src",
        "vendor",

        "src/core/managed/host",
        "src/core/managed/libs/dotnet",
        "src/monitor/crashreporter",

        breakpad_path,
    })

    --[[ -------------------------------- Defines Section -------------------------------- ]]

    if(is_plat("windows")) then
        add_files({
            breakpad_path.."/client/windows/crash_generation/client_info.cc",
            breakpad_path.."/client/windows/crash_generation/crash_generation_client.cc",
            breakpad_path.."/client/windows/crash_generation/crash_generation_server.cc",
            breakpad_path.."/client/windows/crash_generation/minidump_generator.cc",
            breakpad_path.."/client/windows/handler/exception_handler.cc",
            breakpad_path.."/common/windows/guid_string.cc",
        }, { cxxflags = {"-g0", "/DUNICODE", "/D_UNICODE"} })
    else
        add_shflags("-Wl,-z,noexecstack", {force = true})

        add_defines({
            -- Breakpad config defines
            "BPLOG_MINIMUM_SEVERITY=SEVERITY_CRITICAL",
            "__STDC_FORMAT_MACROS",
            "HAVE_A_OUT_H=1",
            "HAVE_CXX17=1",
            "HAVE_GETCONTEXT=1",
            "HAVE_GETRANDOM=1",
            "HAVE_INTTYPES_H=1",
            "HAVE_MEMFD_CREATE=1",
            "HAVE_PTHREAD=1",
            "HAVE_STDINT_H=1",
            "HAVE_STDIO_H=1",
            "HAVE_STDLIB_H=1",
            "HAVE_STRINGS_H=1",
            "HAVE_STRING_H=1",
            "HAVE_SYS_MMAN_H=1",
            "HAVE_SYS_RANDOM_H=1",
            "HAVE_SYS_STAT_H=1",
            "HAVE_SYS_TYPES_H=1",
            "HAVE_UNISTD_H=1",
        })

        add_files({
            breakpad_path.."/client/linux/crash_generation/crash_generation_client.cc",
            breakpad_path.."/client/linux/crash_generation/crash_generation_server.cc",
            breakpad_path.."/client/linux/dump_writer_common/thread_info.cc",
            breakpad_path.."/client/linux/dump_writer_common/ucontext_reader.cc",
            breakpad_path.."/client/linux/handler/exception_handler.cc",
            breakpad_path.."/client/linux/handler/minidump_descriptor.cc",
            breakpad_path.."/client/linux/log/log.cc",
            breakpad_path.."/client/linux/microdump_writer/microdump_writer.cc",
            breakpad_path.."/client/linux/minidump_writer/linux_core_dumper.cc",
            breakpad_path.."/client/linux/minidump_writer/linux_dumper.cc",
            breakpad_path.."/client/linux/minidump_writer/linux_ptrace_dumper.cc",
            breakpad_path.."/client/linux/minidump_writer/minidump_writer.cc",
            breakpad_path.."/client/linux/minidump_writer/pe_file.cc",
            breakpad_path.."/common/linux/elf_core_dump.cc",
            breakpad_path.."/common/linux/elfutils.cc",
            breakpad_path.."/common/linux/file_id.cc",
            breakpad_path.."/common/linux/guid_creator.cc",
            breakpad_path.."/common/linux/linux_libc_support.cc",
            breakpad_path.."/common/linux/memory_mapped_file.cc",
            breakpad_path.."/common/linux/safe_readlink.cc",
            breakpad_path.."/common/linux/scoped_pipe.cc",
            breakpad_path.."/common/linux/scoped_tmpfile.cc",
            breakpad_path.."/processor/disassembler_objdump.cc",
            breakpad_path.."/common/linux/breakpad_getcontext.S",
            breakpad_path.."/client/minidump_file_writer.cc",
        }, { cxxflags = "-g0" })
    end

    add_defines({
        "GITHUB_SHA=\""..GITHUB_SHA.."\"",
        "SWIFTLY_VERSION=\""..SWIFTLY_VERSION.."\"",
        "HAVE_CONFIG_H",
        "SW_HOST"
    })

    --[[ -------------------------------- Vendor Section -------------------------------- ]]

    add_files({
        "vendor/safetyhook/safetyhook.cpp",
        "vendor/safetyhook/Zydis.c",
        breakpad_path.."/common/convert_UTF.cc",
        breakpad_path.."/common/md5.cc",
        breakpad_path.."/common/string_conversion.cc",
        breakpad_path.."/processor/basic_code_modules.cc",
        breakpad_path.."/processor/basic_source_line_resolver.cc",
        breakpad_path.."/processor/call_stack.cc",
        breakpad_path.."/processor/cfi_frame_info.cc",
        breakpad_path.."/processor/convert_old_arm64_context.cc",
        breakpad_path.."/processor/disassembler_x86.cc",
        breakpad_path.."/processor/dump_context.cc",
        breakpad_path.."/processor/dump_object.cc",
        breakpad_path.."/processor/exploitability.cc",
        breakpad_path.."/processor/exploitability_linux.cc",
        breakpad_path.."/processor/exploitability_win.cc",
        breakpad_path.."/processor/fast_source_line_resolver.cc",
        breakpad_path.."/processor/logging.cc",
        breakpad_path.."/processor/microdump.cc",
        breakpad_path.."/processor/microdump_processor.cc",
        breakpad_path.."/processor/minidump.cc",
        breakpad_path.."/processor/minidump_processor.cc",
        breakpad_path.."/processor/module_comparer.cc",
        breakpad_path.."/processor/module_serializer.cc",
        breakpad_path.."/processor/pathname_stripper.cc",
        breakpad_path.."/processor/process_state.cc",
        breakpad_path.."/processor/proc_maps_linux.cc",
        breakpad_path.."/processor/simple_symbol_supplier.cc",
        breakpad_path.."/processor/source_line_resolver_base.cc",
        breakpad_path.."/processor/stack_frame_cpu.cc",
        breakpad_path.."/processor/stack_frame_symbolizer.cc",
        breakpad_path.."/processor/stackwalk_common.cc",
        breakpad_path.."/processor/stackwalker.cc",
        breakpad_path.."/processor/stackwalker_amd64.cc",
        breakpad_path.."/processor/stackwalker_arm.cc",
        breakpad_path.."/processor/stackwalker_arm64.cc",
        breakpad_path.."/processor/stackwalker_address_list.cc",
        breakpad_path.."/processor/stackwalker_mips.cc",
        breakpad_path.."/processor/stackwalker_ppc.cc",
        breakpad_path.."/processor/stackwalker_ppc64.cc",
        breakpad_path.."/processor/stackwalker_riscv.cc",
        breakpad_path.."/processor/stackwalker_riscv64.cc",
        breakpad_path.."/processor/stackwalker_sparc.cc",
        breakpad_path.."/processor/stackwalker_x86.cc",
        breakpad_path.."/processor/symbolic_constants_win.cc",
        breakpad_path.."/processor/tokenize.cc",
        breakpad_path.."/third_party/libdisasm/ia32_implicit.c",
		breakpad_path.."/third_party/libdisasm/ia32_insn.c",
		breakpad_path.."/third_party/libdisasm/ia32_invariant.c",
		breakpad_path.."/third_party/libdisasm/ia32_modrm.c",
		breakpad_path.."/third_party/libdisasm/ia32_opcode_tables.c",
		breakpad_path.."/third_party/libdisasm/ia32_operand.c",
		breakpad_path.."/third_party/libdisasm/ia32_reg.c",
		breakpad_path.."/third_party/libdisasm/ia32_settings.c",
		breakpad_path.."/third_party/libdisasm/x86_disasm.c",
		breakpad_path.."/third_party/libdisasm/x86_format.c",
		breakpad_path.."/third_party/libdisasm/x86_imm.c",
		breakpad_path.."/third_party/libdisasm/x86_insn.c",
		breakpad_path.."/third_party/libdisasm/x86_misc.c",
		breakpad_path.."/third_party/libdisasm/x86_operand_list.c",
    }, { cxxflags = "-g0" })

    --[[ -------------------------------- Misc Section -------------------------------- ]]

    set_optimize("fastest")
    set_symbols("debug")
    set_strip("none")

    if is_plat("windows") then
        add_shflags("/INCREMENTAL:NO", { force = true })
    end

    after_build(function(target)
        local dist = is_plat("windows") and "win64" or "linuxsteamrt64"
        local ext = is_plat("windows") and "dll" or "so"
        local package = "build/package/addons/swiftlys2"

        os.tryrm("build/package")
        os.cp("plugin_files/", package)
        os.mkdir(package.."/bin/"..dist)
        os.cp(target:targetfile(), package.."/bin/"..dist.."/swiftlys2."..ext)
    end)
