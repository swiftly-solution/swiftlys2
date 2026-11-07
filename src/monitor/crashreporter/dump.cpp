/************************************************************************************************
 *  SwiftlyS2 is a scripting framework for Source2-based games.
 *  Copyright (C) 2023-2026 Swiftly Solution SRL via Sava Andrei-Sebastian and it's contributors
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
 ************************************************************************************************/

#include "crashreporter.h"
#include <format>
#include <nlohmann/json.hpp>
#include <core/entrypoint.h>
#include <api/shared/profiler.h>

#include <google_breakpad/processor/stack_frame.h>
#include <google_breakpad/processor/stack_frame_cpu.h>
#include <processor/pathname_stripper.h>

void PrintModules(nlohmann::json& modules_entry, const google_breakpad::CodeModules* modules, const std::vector<const google_breakpad::CodeModule*>* modules_without_symbols, const std::vector<const google_breakpad::CodeModule*>* modules_with_corrupt_symbols);
void PrintThread(nlohmann::json& thread_entry, google_breakpad::CallStack* stack, const std::string& cpu,
    google_breakpad::MemoryRegion* memory, const google_breakpad::CodeModules* modules, google_breakpad::SourceLineResolverInterface* resolver);

std::string FormatProcessState(const google_breakpad::ProcessState& process_state, google_breakpad::SourceLineResolverInterface* resolver)
{
    PROFILE_FUNCTION();

    nlohmann::json out;

    nlohmann::json& native = out["native"];
    native["version"] = g_SwiftlyCore.GetVersion();

    nlohmann::json& machine = out["machine"];

    nlohmann::json& cpu = machine["cpu"];
    cpu["name"] = process_state.system_info()->cpu;
    cpu["count"] = process_state.system_info()->cpu_count;
    cpu["info"] = process_state.system_info()->cpu_info;

    nlohmann::json& operating_system = machine["os"];
    operating_system["name"] = process_state.system_info()->os;
    operating_system["version"] = process_state.system_info()->os_version;

    nlohmann::json& process = out["process"];
    process["crashed"] = process_state.crashed();

    nlohmann::json& process_info = process["info"];
    process_info["uptime"] = process_state.time_date_stamp() - process_state.process_create_time();

    nlohmann::json& crash = process["crash"];
    crash["reason"] = process_state.crash_reason();
    crash["address"] = std::format("0x{:016X}", process_state.crash_address());
    crash["assertion"] = process_state.assertion();
    crash["thread_id"] = process_state.requesting_thread();

    nlohmann::json& threads = process["threads"];
    for (size_t i = 0; i < process_state.threads()->size(); i++)
    {
        threads.push_back({
            {"thread_id", i},
            {"frames", nlohmann::json::array()},
            });

        nlohmann::json& thread_entry = threads.back();
        PrintThread(thread_entry, process_state.threads()->at(i), process_state.system_info()->cpu, process_state.thread_memory_regions()->at(i), process_state.modules(), resolver);
    }

    nlohmann::json& modules = process["modules"];
    PrintModules(modules, process_state.modules(), process_state.modules_without_symbols(), process_state.modules_with_corrupt_symbols());

    return out.dump(4);
}

void PrintModules(nlohmann::json& modules_entry, const google_breakpad::CodeModules* modules, const std::vector<const google_breakpad::CodeModule*>* modules_without_symbols, const std::vector<const google_breakpad::CodeModule*>* modules_with_corrupt_symbols)
{
    PROFILE_FUNCTION();

    for (size_t i = 0; i < modules->module_count(); i++)
    {
        const google_breakpad::CodeModule* module = modules->GetModuleAtSequence(i);
        if (!module) continue;

        nlohmann::json& module_entry = modules_entry[std::format("0x{:016X}", module->base_address())];
        module_entry["name"] = google_breakpad::PathnameStripper::File(module->code_file());
        module_entry["version"] = module->version();
        module_entry["debug_file"] = google_breakpad::PathnameStripper::File(module->debug_file());
        module_entry["debug_identifier"] = module->debug_identifier();
        module_entry["has_symbols"] = std::find(modules_without_symbols->begin(), modules_without_symbols->end(), module) == modules_without_symbols->end();
        module_entry["corrupt_symbols"] = std::find(modules_with_corrupt_symbols->begin(), modules_with_corrupt_symbols->end(), module) != modules_with_corrupt_symbols->end();
    }
}

void PrintRegister(nlohmann::json& registers, const std::string& name, uint32_t value) {
    PROFILE_FUNCTION();

    registers[name] = std::format("0x{:016X}", value);
}

void PrintRegister64(nlohmann::json& registers, const std::string& name, uint64_t value) {
    PROFILE_FUNCTION();

    registers[name] = std::format("0x{:016X}", value);
}

void PrintThread(nlohmann::json& thread_entry, google_breakpad::CallStack* stack, const std::string& cpu,
    google_breakpad::MemoryRegion* memory, const google_breakpad::CodeModules* modules, google_breakpad::SourceLineResolverInterface* resolver)
{
    PROFILE_FUNCTION();

    nlohmann::json& frames = thread_entry["frames"];

    for (size_t i = 0; i < stack->frames()->size(); i++)
    {
        google_breakpad::StackFrame* frame = stack->frames()->at(i);
        frames.push_back({
            {"index", i},
            });

        nlohmann::json& frame_entry = frames.back();
        uint64_t return_addr = frame->ReturnAddress();

        // Header
        frame_entry["address"] = std::format("0x{:016X}", return_addr);

        if (frame->module) {
            frame_entry["module"] = google_breakpad::PathnameStripper::File(frame->module->code_file());
            frame_entry["function_offset"] = std::format("0x{:016X}", return_addr - frame->module->base_address());
            if (!frame->function_name.empty()) {
                frame_entry["function"] = frame->function_name;
                frame_entry["instruction_offset"] = std::format("0x{:X}", return_addr - frame->function_base);
                if (!frame->source_file_name.empty()) {
                    std::string source_file = google_breakpad::PathnameStripper::File(frame->source_file_name);
                    frame_entry["source_file"] = source_file;
                    frame_entry["source_line"] = frame->source_line;
                    frame_entry["source_offset"] = return_addr - frame->source_line_base;
                }
            }
        }

        // Registers
        if (frame->trust != google_breakpad::StackFrameAMD64::FRAME_TRUST_INLINE) {
            nlohmann::json& registers = frame_entry["registers"];

            if (cpu == "x86") {
                const google_breakpad::StackFrameX86* frame_x86 =
                    reinterpret_cast<const google_breakpad::StackFrameX86*>(frame);

                if (frame_x86->context_validity & google_breakpad::StackFrameX86::CONTEXT_VALID_EIP)
                    PrintRegister(registers, "eip", frame_x86->context.eip);
                if (frame_x86->context_validity & google_breakpad::StackFrameX86::CONTEXT_VALID_ESP)
                    PrintRegister(registers, "esp", frame_x86->context.esp);
                if (frame_x86->context_validity & google_breakpad::StackFrameX86::CONTEXT_VALID_EBP)
                    PrintRegister(registers, "ebp", frame_x86->context.ebp);
                if (frame_x86->context_validity & google_breakpad::StackFrameX86::CONTEXT_VALID_EBX)
                    PrintRegister(registers, "ebx", frame_x86->context.ebx);
                if (frame_x86->context_validity & google_breakpad::StackFrameX86::CONTEXT_VALID_ESI)
                    PrintRegister(registers, "esi", frame_x86->context.esi);
                if (frame_x86->context_validity & google_breakpad::StackFrameX86::CONTEXT_VALID_EDI)
                    PrintRegister(registers, "edi", frame_x86->context.edi);
                if (frame_x86->context_validity == google_breakpad::StackFrameX86::CONTEXT_VALID_ALL) {
                    PrintRegister(registers, "eax", frame_x86->context.eax);
                    PrintRegister(registers, "ecx", frame_x86->context.ecx);
                    PrintRegister(registers, "edx", frame_x86->context.edx);
                    PrintRegister(registers, "efl", frame_x86->context.eflags);
                }
            }
            else if (cpu == "ppc") {
                const google_breakpad::StackFramePPC* frame_ppc =
                    reinterpret_cast<const google_breakpad::StackFramePPC*>(frame);

                if (frame_ppc->context_validity & google_breakpad::StackFramePPC::CONTEXT_VALID_SRR0)
                    PrintRegister(registers, "srr0", frame_ppc->context.srr0);
                if (frame_ppc->context_validity & google_breakpad::StackFramePPC::CONTEXT_VALID_GPR1)
                    PrintRegister(registers, "r1", frame_ppc->context.gpr[1]);
            }
            else if (cpu == "amd64") {
                const google_breakpad::StackFrameAMD64* frame_amd64 =
                    reinterpret_cast<const google_breakpad::StackFrameAMD64*>(frame);

                if (frame_amd64->context_validity & google_breakpad::StackFrameAMD64::CONTEXT_VALID_RAX)
                    PrintRegister64(registers, "rax", frame_amd64->context.rax);
                if (frame_amd64->context_validity & google_breakpad::StackFrameAMD64::CONTEXT_VALID_RDX)
                    PrintRegister64(registers, "rdx", frame_amd64->context.rdx);
                if (frame_amd64->context_validity & google_breakpad::StackFrameAMD64::CONTEXT_VALID_RCX)
                    PrintRegister64(registers, "rcx", frame_amd64->context.rcx);
                if (frame_amd64->context_validity & google_breakpad::StackFrameAMD64::CONTEXT_VALID_RBX)
                    PrintRegister64(registers, "rbx", frame_amd64->context.rbx);
                if (frame_amd64->context_validity & google_breakpad::StackFrameAMD64::CONTEXT_VALID_RSI)
                    PrintRegister64(registers, "rsi", frame_amd64->context.rsi);
                if (frame_amd64->context_validity & google_breakpad::StackFrameAMD64::CONTEXT_VALID_RDI)
                    PrintRegister64(registers, "rdi", frame_amd64->context.rdi);
                if (frame_amd64->context_validity & google_breakpad::StackFrameAMD64::CONTEXT_VALID_RBP)
                    PrintRegister64(registers, "rbp", frame_amd64->context.rbp);
                if (frame_amd64->context_validity & google_breakpad::StackFrameAMD64::CONTEXT_VALID_RSP)
                    PrintRegister64(registers, "rsp", frame_amd64->context.rsp);
                if (frame_amd64->context_validity & google_breakpad::StackFrameAMD64::CONTEXT_VALID_R8)
                    PrintRegister64(registers, "r8", frame_amd64->context.r8);
                if (frame_amd64->context_validity & google_breakpad::StackFrameAMD64::CONTEXT_VALID_R9)
                    PrintRegister64(registers, "r9", frame_amd64->context.r9);
                if (frame_amd64->context_validity & google_breakpad::StackFrameAMD64::CONTEXT_VALID_R10)
                    PrintRegister64(registers, "r10", frame_amd64->context.r10);
                if (frame_amd64->context_validity & google_breakpad::StackFrameAMD64::CONTEXT_VALID_R11)
                    PrintRegister64(registers, "r11", frame_amd64->context.r11);
                if (frame_amd64->context_validity & google_breakpad::StackFrameAMD64::CONTEXT_VALID_R12)
                    PrintRegister64(registers, "r12", frame_amd64->context.r12);
                if (frame_amd64->context_validity & google_breakpad::StackFrameAMD64::CONTEXT_VALID_R13)
                    PrintRegister64(registers, "r13", frame_amd64->context.r13);
                if (frame_amd64->context_validity & google_breakpad::StackFrameAMD64::CONTEXT_VALID_R14)
                    PrintRegister64(registers, "r14", frame_amd64->context.r14);
                if (frame_amd64->context_validity & google_breakpad::StackFrameAMD64::CONTEXT_VALID_R15)
                    PrintRegister64(registers, "r15", frame_amd64->context.r15);
                if (frame_amd64->context_validity & google_breakpad::StackFrameAMD64::CONTEXT_VALID_RIP)
                    PrintRegister64(registers, "rip", frame_amd64->context.rip);
            }
            else if (cpu == "sparc") {
                const google_breakpad::StackFrameSPARC* frame_sparc =
                    reinterpret_cast<const google_breakpad::StackFrameSPARC*>(frame);

                if (frame_sparc->context_validity & google_breakpad::StackFrameSPARC::CONTEXT_VALID_SP)

                    PrintRegister(registers, "sp", frame_sparc->context.g_r[14]);
                if (frame_sparc->context_validity & google_breakpad::StackFrameSPARC::CONTEXT_VALID_FP)

                    PrintRegister(registers, "fp", frame_sparc->context.g_r[30]);
                if (frame_sparc->context_validity & google_breakpad::StackFrameSPARC::CONTEXT_VALID_PC)
                    PrintRegister(registers, "pc", frame_sparc->context.pc);
            }
            else if (cpu == "arm") {
                const google_breakpad::StackFrameARM* frame_arm =
                    reinterpret_cast<const google_breakpad::StackFrameARM*>(frame);

                // Argument registers (caller-saves), which will likely only be valid
                // for the youngest frame.
                if (frame_arm->context_validity & google_breakpad::StackFrameARM::CONTEXT_VALID_R0)
                    PrintRegister(registers, "r0", frame_arm->context.iregs[0]);
                if (frame_arm->context_validity & google_breakpad::StackFrameARM::CONTEXT_VALID_R1)
                    PrintRegister(registers, "r1", frame_arm->context.iregs[1]);
                if (frame_arm->context_validity & google_breakpad::StackFrameARM::CONTEXT_VALID_R2)
                    PrintRegister(registers, "r2", frame_arm->context.iregs[2]);
                if (frame_arm->context_validity & google_breakpad::StackFrameARM::CONTEXT_VALID_R3)
                    PrintRegister(registers, "r3", frame_arm->context.iregs[3]);

                // General-purpose callee-saves registers.
                if (frame_arm->context_validity & google_breakpad::StackFrameARM::CONTEXT_VALID_R4)
                    PrintRegister(registers, "r4", frame_arm->context.iregs[4]);
                if (frame_arm->context_validity & google_breakpad::StackFrameARM::CONTEXT_VALID_R5)
                    PrintRegister(registers, "r5", frame_arm->context.iregs[5]);
                if (frame_arm->context_validity & google_breakpad::StackFrameARM::CONTEXT_VALID_R6)
                    PrintRegister(registers, "r6", frame_arm->context.iregs[6]);
                if (frame_arm->context_validity & google_breakpad::StackFrameARM::CONTEXT_VALID_R7)
                    PrintRegister(registers, "r7", frame_arm->context.iregs[7]);
                if (frame_arm->context_validity & google_breakpad::StackFrameARM::CONTEXT_VALID_R8)
                    PrintRegister(registers, "r8", frame_arm->context.iregs[8]);
                if (frame_arm->context_validity & google_breakpad::StackFrameARM::CONTEXT_VALID_R9)
                    PrintRegister(registers, "r9", frame_arm->context.iregs[9]);
                if (frame_arm->context_validity & google_breakpad::StackFrameARM::CONTEXT_VALID_R10)
                    PrintRegister(registers, "r10", frame_arm->context.iregs[10]);
                if (frame_arm->context_validity & google_breakpad::StackFrameARM::CONTEXT_VALID_R12)
                    PrintRegister(registers, "r12", frame_arm->context.iregs[12]);

                // Registers with a dedicated or conventional purpose.
                if (frame_arm->context_validity & google_breakpad::StackFrameARM::CONTEXT_VALID_FP)
                    PrintRegister(registers, "fp", frame_arm->context.iregs[11]);
                if (frame_arm->context_validity & google_breakpad::StackFrameARM::CONTEXT_VALID_SP)
                    PrintRegister(registers, "sp", frame_arm->context.iregs[13]);
                if (frame_arm->context_validity & google_breakpad::StackFrameARM::CONTEXT_VALID_LR)
                    PrintRegister(registers, "lr", frame_arm->context.iregs[14]);
                if (frame_arm->context_validity & google_breakpad::StackFrameARM::CONTEXT_VALID_PC)
                    PrintRegister(registers, "pc", frame_arm->context.iregs[15]);
            }
            else if (cpu == "arm64") {
                const google_breakpad::StackFrameARM64* frame_arm64 =
                    reinterpret_cast<const google_breakpad::StackFrameARM64*>(frame);

                if (frame_arm64->context_validity & google_breakpad::StackFrameARM64::CONTEXT_VALID_X0) {
                    PrintRegister64(registers, "x0", frame_arm64->context.iregs[0]);
                }
                if (frame_arm64->context_validity & google_breakpad::StackFrameARM64::CONTEXT_VALID_X1) {
                    PrintRegister64(registers, "x1", frame_arm64->context.iregs[1]);
                }
                if (frame_arm64->context_validity & google_breakpad::StackFrameARM64::CONTEXT_VALID_X2) {
                    PrintRegister64(registers, "x2", frame_arm64->context.iregs[2]);
                }
                if (frame_arm64->context_validity & google_breakpad::StackFrameARM64::CONTEXT_VALID_X3) {
                    PrintRegister64(registers, "x3", frame_arm64->context.iregs[3]);
                }
                if (frame_arm64->context_validity & google_breakpad::StackFrameARM64::CONTEXT_VALID_X4) {
                    PrintRegister64(registers, "x4", frame_arm64->context.iregs[4]);
                }
                if (frame_arm64->context_validity & google_breakpad::StackFrameARM64::CONTEXT_VALID_X5) {
                    PrintRegister64(registers, "x5", frame_arm64->context.iregs[5]);
                }
                if (frame_arm64->context_validity & google_breakpad::StackFrameARM64::CONTEXT_VALID_X6) {
                    PrintRegister64(registers, "x6", frame_arm64->context.iregs[6]);
                }
                if (frame_arm64->context_validity & google_breakpad::StackFrameARM64::CONTEXT_VALID_X7) {
                    PrintRegister64(registers, "x7", frame_arm64->context.iregs[7]);
                }
                if (frame_arm64->context_validity & google_breakpad::StackFrameARM64::CONTEXT_VALID_X8) {
                    PrintRegister64(registers, "x8", frame_arm64->context.iregs[8]);
                }
                if (frame_arm64->context_validity & google_breakpad::StackFrameARM64::CONTEXT_VALID_X9) {
                    PrintRegister64(registers, "x9", frame_arm64->context.iregs[9]);
                }
                if (frame_arm64->context_validity &
                    google_breakpad::StackFrameARM64::CONTEXT_VALID_X10) {
                    PrintRegister64(registers, "x10", frame_arm64->context.iregs[10]);
                }
                if (frame_arm64->context_validity &
                    google_breakpad::StackFrameARM64::CONTEXT_VALID_X11) {
                    PrintRegister64(registers, "x11", frame_arm64->context.iregs[11]);
                }
                if (frame_arm64->context_validity &
                    google_breakpad::StackFrameARM64::CONTEXT_VALID_X12) {
                    PrintRegister64(registers, "x12", frame_arm64->context.iregs[12]);
                }
                if (frame_arm64->context_validity &
                    google_breakpad::StackFrameARM64::CONTEXT_VALID_X13) {
                    PrintRegister64(registers, "x13", frame_arm64->context.iregs[13]);
                }
                if (frame_arm64->context_validity &
                    google_breakpad::StackFrameARM64::CONTEXT_VALID_X14) {
                    PrintRegister64(registers, "x14", frame_arm64->context.iregs[14]);
                }
                if (frame_arm64->context_validity &
                    google_breakpad::StackFrameARM64::CONTEXT_VALID_X15) {
                    PrintRegister64(registers, "x15", frame_arm64->context.iregs[15]);
                }
                if (frame_arm64->context_validity &
                    google_breakpad::StackFrameARM64::CONTEXT_VALID_X16) {
                    PrintRegister64(registers, "x16", frame_arm64->context.iregs[16]);
                }
                if (frame_arm64->context_validity &
                    google_breakpad::StackFrameARM64::CONTEXT_VALID_X17) {
                    PrintRegister64(registers, "x17", frame_arm64->context.iregs[17]);
                }
                if (frame_arm64->context_validity &
                    google_breakpad::StackFrameARM64::CONTEXT_VALID_X18) {
                    PrintRegister64(registers, "x18", frame_arm64->context.iregs[18]);
                }
                if (frame_arm64->context_validity &
                    google_breakpad::StackFrameARM64::CONTEXT_VALID_X19) {
                    PrintRegister64(registers, "x19", frame_arm64->context.iregs[19]);
                }
                if (frame_arm64->context_validity &
                    google_breakpad::StackFrameARM64::CONTEXT_VALID_X20) {
                    PrintRegister64(registers, "x20", frame_arm64->context.iregs[20]);
                }
                if (frame_arm64->context_validity &
                    google_breakpad::StackFrameARM64::CONTEXT_VALID_X21) {
                    PrintRegister64(registers, "x21", frame_arm64->context.iregs[21]);
                }
                if (frame_arm64->context_validity &
                    google_breakpad::StackFrameARM64::CONTEXT_VALID_X22) {
                    PrintRegister64(registers, "x22", frame_arm64->context.iregs[22]);
                }
                if (frame_arm64->context_validity &
                    google_breakpad::StackFrameARM64::CONTEXT_VALID_X23) {
                    PrintRegister64(registers, "x23", frame_arm64->context.iregs[23]);
                }
                if (frame_arm64->context_validity &
                    google_breakpad::StackFrameARM64::CONTEXT_VALID_X24) {
                    PrintRegister64(registers, "x24", frame_arm64->context.iregs[24]);
                }
                if (frame_arm64->context_validity &
                    google_breakpad::StackFrameARM64::CONTEXT_VALID_X25) {
                    PrintRegister64(registers, "x25", frame_arm64->context.iregs[25]);
                }
                if (frame_arm64->context_validity &
                    google_breakpad::StackFrameARM64::CONTEXT_VALID_X26) {
                    PrintRegister64(registers, "x26", frame_arm64->context.iregs[26]);
                }
                if (frame_arm64->context_validity &
                    google_breakpad::StackFrameARM64::CONTEXT_VALID_X27) {
                    PrintRegister64(registers, "x27", frame_arm64->context.iregs[27]);
                }
                if (frame_arm64->context_validity &
                    google_breakpad::StackFrameARM64::CONTEXT_VALID_X28) {
                    PrintRegister64(registers, "x28", frame_arm64->context.iregs[28]);
                }

                // Registers with a dedicated or conventional purpose.
                if (frame_arm64->context_validity & google_breakpad::StackFrameARM64::CONTEXT_VALID_FP) {
                    PrintRegister64(registers, "fp", frame_arm64->context.iregs[29]);
                }
                if (frame_arm64->context_validity & google_breakpad::StackFrameARM64::CONTEXT_VALID_LR) {
                    PrintRegister64(registers, "lr", frame_arm64->context.iregs[30]);
                }
                if (frame_arm64->context_validity & google_breakpad::StackFrameARM64::CONTEXT_VALID_SP) {
                    PrintRegister64(registers, "sp", frame_arm64->context.iregs[31]);
                }
                if (frame_arm64->context_validity & google_breakpad::StackFrameARM64::CONTEXT_VALID_PC) {
                    PrintRegister64(registers, "pc", frame_arm64->context.iregs[32]);
                }
            }
            else if ((cpu == "mips") || (cpu == "mips64")) {
                const google_breakpad::StackFrameMIPS* frame_mips =
                    reinterpret_cast<const google_breakpad::StackFrameMIPS*>(frame);

                if (frame_mips->context_validity & google_breakpad::StackFrameMIPS::CONTEXT_VALID_GP)
                    PrintRegister64(registers,
                        "gp", frame_mips->context.iregs[MD_CONTEXT_MIPS_REG_GP]);
                if (frame_mips->context_validity & google_breakpad::StackFrameMIPS::CONTEXT_VALID_SP)
                    PrintRegister64(registers,
                        "sp", frame_mips->context.iregs[MD_CONTEXT_MIPS_REG_SP]);
                if (frame_mips->context_validity & google_breakpad::StackFrameMIPS::CONTEXT_VALID_FP)
                    PrintRegister64(registers,
                        "fp", frame_mips->context.iregs[MD_CONTEXT_MIPS_REG_FP]);
                if (frame_mips->context_validity & google_breakpad::StackFrameMIPS::CONTEXT_VALID_RA)
                    PrintRegister64(registers,
                        "ra", frame_mips->context.iregs[MD_CONTEXT_MIPS_REG_RA]);
                if (frame_mips->context_validity & google_breakpad::StackFrameMIPS::CONTEXT_VALID_PC)
                    PrintRegister64(registers, "pc", frame_mips->context.epc);

                // Save registers s0-s7
                if (frame_mips->context_validity & google_breakpad::StackFrameMIPS::CONTEXT_VALID_S0)
                    PrintRegister64(registers,
                        "s0", frame_mips->context.iregs[MD_CONTEXT_MIPS_REG_S0]);
                if (frame_mips->context_validity & google_breakpad::StackFrameMIPS::CONTEXT_VALID_S1)
                    PrintRegister64(registers,
                        "s1", frame_mips->context.iregs[MD_CONTEXT_MIPS_REG_S1]);
                if (frame_mips->context_validity & google_breakpad::StackFrameMIPS::CONTEXT_VALID_S2)
                    PrintRegister64(registers,
                        "s2", frame_mips->context.iregs[MD_CONTEXT_MIPS_REG_S2]);
                if (frame_mips->context_validity & google_breakpad::StackFrameMIPS::CONTEXT_VALID_S3)
                    PrintRegister64(registers,
                        "s3", frame_mips->context.iregs[MD_CONTEXT_MIPS_REG_S3]);
                if (frame_mips->context_validity & google_breakpad::StackFrameMIPS::CONTEXT_VALID_S4)
                    PrintRegister64(registers,
                        "s4", frame_mips->context.iregs[MD_CONTEXT_MIPS_REG_S4]);
                if (frame_mips->context_validity & google_breakpad::StackFrameMIPS::CONTEXT_VALID_S5)
                    PrintRegister64(registers,
                        "s5", frame_mips->context.iregs[MD_CONTEXT_MIPS_REG_S5]);
                if (frame_mips->context_validity & google_breakpad::StackFrameMIPS::CONTEXT_VALID_S6)
                    PrintRegister64(registers,
                        "s6", frame_mips->context.iregs[MD_CONTEXT_MIPS_REG_S6]);
                if (frame_mips->context_validity & google_breakpad::StackFrameMIPS::CONTEXT_VALID_S7)
                    PrintRegister64(registers,
                        "s7", frame_mips->context.iregs[MD_CONTEXT_MIPS_REG_S7]);
            }
            else if (cpu == "riscv") {
                const google_breakpad::StackFrameRISCV* frame_riscv =
                    reinterpret_cast<const google_breakpad::StackFrameRISCV*>(frame);

                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_PC)
                    PrintRegister(registers,
                        "pc", frame_riscv->context.pc);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_RA)
                    PrintRegister(registers,
                        "ra", frame_riscv->context.ra);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_SP)
                    PrintRegister(registers,
                        "sp", frame_riscv->context.sp);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_GP)
                    PrintRegister(registers,
                        "gp", frame_riscv->context.gp);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_TP)
                    PrintRegister(registers,
                        "tp", frame_riscv->context.tp);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_T0)
                    PrintRegister(registers,
                        "t0", frame_riscv->context.t0);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_T1)
                    PrintRegister(registers,
                        "t1", frame_riscv->context.t1);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_T2)
                    PrintRegister(registers,
                        "t2", frame_riscv->context.t2);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_S0)
                    PrintRegister(registers,
                        "s0", frame_riscv->context.s0);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_S1)
                    PrintRegister(registers,
                        "s1", frame_riscv->context.s1);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_A0)
                    PrintRegister(registers,
                        "a0", frame_riscv->context.a0);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_A1)
                    PrintRegister(registers,
                        "a1", frame_riscv->context.a1);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_A2)
                    PrintRegister(registers,
                        "a2", frame_riscv->context.a2);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_A3)
                    PrintRegister(registers,
                        "a3", frame_riscv->context.a3);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_A4)
                    PrintRegister(registers,
                        "a4", frame_riscv->context.a4);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_A5)
                    PrintRegister(registers,
                        "a5", frame_riscv->context.a5);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_A6)
                    PrintRegister(registers,
                        "a6", frame_riscv->context.a6);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_A7)
                    PrintRegister(registers,
                        "a7", frame_riscv->context.a7);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_S2)
                    PrintRegister(registers,
                        "s2", frame_riscv->context.s2);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_S3)
                    PrintRegister(registers,
                        "s3", frame_riscv->context.s3);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_S4)
                    PrintRegister(registers,
                        "s4", frame_riscv->context.s4);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_S5)
                    PrintRegister(registers,
                        "s5", frame_riscv->context.s5);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_S6)
                    PrintRegister(registers,
                        "s6", frame_riscv->context.s6);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_S7)
                    PrintRegister(registers,
                        "s7", frame_riscv->context.s7);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_S8)
                    PrintRegister(registers,
                        "s8", frame_riscv->context.s8);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_S9)
                    PrintRegister(registers,
                        "s9", frame_riscv->context.s9);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_S10)
                    PrintRegister(registers,
                        "s10", frame_riscv->context.s10);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_S11)
                    PrintRegister(registers,
                        "s11", frame_riscv->context.s11);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_T3)
                    PrintRegister(registers,
                        "t3", frame_riscv->context.t3);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_T4)
                    PrintRegister(registers,
                        "t4", frame_riscv->context.t4);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_T5)
                    PrintRegister(registers,
                        "t5", frame_riscv->context.t5);
                if (frame_riscv->context_validity &
                    google_breakpad::StackFrameRISCV::CONTEXT_VALID_T6)
                    PrintRegister(registers,
                        "t6", frame_riscv->context.t6);
            }
            else if (cpu == "riscv64") {
                const google_breakpad::StackFrameRISCV64* frame_riscv64 =
                    reinterpret_cast<const google_breakpad::StackFrameRISCV64*>(frame);

                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_PC)
                    PrintRegister64(registers,
                        "pc", frame_riscv64->context.pc);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_RA)
                    PrintRegister64(registers,
                        "ra", frame_riscv64->context.ra);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_SP)
                    PrintRegister64(registers,
                        "sp", frame_riscv64->context.sp);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_GP)
                    PrintRegister64(registers,
                        "gp", frame_riscv64->context.gp);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_TP)
                    PrintRegister64(registers,
                        "tp", frame_riscv64->context.tp);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_T0)
                    PrintRegister64(registers,
                        "t0", frame_riscv64->context.t0);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_T1)
                    PrintRegister64(registers,
                        "t1", frame_riscv64->context.t1);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_T2)
                    PrintRegister64(registers,
                        "t2", frame_riscv64->context.t2);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_S0)
                    PrintRegister64(registers,
                        "s0", frame_riscv64->context.s0);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_S1)
                    PrintRegister64(registers,
                        "s1", frame_riscv64->context.s1);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_A0)
                    PrintRegister64(registers,
                        "a0", frame_riscv64->context.a0);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_A1)
                    PrintRegister64(registers,
                        "a1", frame_riscv64->context.a1);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_A2)
                    PrintRegister64(registers,
                        "a2", frame_riscv64->context.a2);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_A3)
                    PrintRegister64(registers,
                        "a3", frame_riscv64->context.a3);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_A4)
                    PrintRegister64(registers,
                        "a4", frame_riscv64->context.a4);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_A5)
                    PrintRegister64(registers,
                        "a5", frame_riscv64->context.a5);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_A6)
                    PrintRegister64(registers,
                        "a6", frame_riscv64->context.a6);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_A7)
                    PrintRegister64(registers,
                        "a7", frame_riscv64->context.a7);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_S2)
                    PrintRegister64(registers,
                        "s2", frame_riscv64->context.s2);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_S3)
                    PrintRegister64(registers,
                        "s3", frame_riscv64->context.s3);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_S4)
                    PrintRegister64(registers,
                        "s4", frame_riscv64->context.s4);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_S5)
                    PrintRegister64(registers,
                        "s5", frame_riscv64->context.s5);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_S6)
                    PrintRegister64(registers,
                        "s6", frame_riscv64->context.s6);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_S7)
                    PrintRegister64(registers,
                        "s7", frame_riscv64->context.s7);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_S8)
                    PrintRegister64(registers,
                        "s8", frame_riscv64->context.s8);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_S9)
                    PrintRegister64(registers,
                        "s9", frame_riscv64->context.s9);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_S10)
                    PrintRegister64(registers,
                        "s10", frame_riscv64->context.s10);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_S11)
                    PrintRegister64(registers,
                        "s11", frame_riscv64->context.s11);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_T3)
                    PrintRegister64(registers,
                        "t3", frame_riscv64->context.t3);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_T4)
                    PrintRegister64(registers,
                        "t4", frame_riscv64->context.t4);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_T5)
                    PrintRegister64(registers,
                        "t5", frame_riscv64->context.t5);
                if (frame_riscv64->context_validity &
                    google_breakpad::StackFrameRISCV64::CONTEXT_VALID_T6)
                    PrintRegister64(registers,
                        "t6", frame_riscv64->context.t6);
            }
        }

        nlohmann::json& trust = frame_entry["trust"];
        trust["description"] = frame->trust_description();
    }
}