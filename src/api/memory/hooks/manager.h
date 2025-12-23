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

#ifndef src_api_memory_hooks_manager_h
#define src_api_memory_hooks_manager_h

#include "function.h"
#include "vfunction.h"
#include "mfunction.h"

#define PTR_SIZE sizeof(void*)

class IHooksManager
{
public:
    virtual void Initialize() = 0;
    virtual void Shutdown() = 0;

    virtual IFunctionHook* CreateFunctionHook() = 0;
    virtual IVFunctionHook* CreateVFunctionHook() = 0;
    virtual IMFunctionHook* CreateMFunctionHook() = 0;

    virtual void DestroyFunctionHook(IFunctionHook* hook) = 0;
    virtual void DestroyVFunctionHook(IVFunctionHook* hook) = 0;
    virtual void DestroyMFunctionHook(IMFunctionHook* hook) = 0;

    // ptr CEntityIOOutput, string outputName, ptr activator, ptr caller, float delay -> int (HookResult)
    virtual uint64_t CreateEntityHookOutput(const std::string& className, const std::string& outputName, void* callback) = 0;
    virtual void DestroyEntityHookOutput(uint64_t id) = 0;
};

template<typename T, typename RetType, typename... Args>
int GetVirtualFunctionId(RetType(T::* func)(Args...)) {
#if defined(COMPILER_CLANG) || defined(COMPILER_GCC)
    struct MemFuncPtr {
        union {
            RetType(T::* func)(Args...);
            intptr_t vmt_idx_odd;
        };
        intptr_t delta;
    };

    int vmt_idx;
    auto details = (MemFuncPtr*)&func;
    if (details->vmt_idx_odd & 1) {
        vmt_idx = (details->vmt_idx_odd - 1) / PTR_SIZE;
    }
    else {
        vmt_idx = -1;
    }

    return vmt_idx;
#elif defined(COMPILER_MSVC)
    // https://www.unknowncheats.me/forum/c-and-c-/102577-vtable-index-pure-virtual-function.html

    // Check whether it's a virtual function call on x86

    // They look like this:a
    //		0:  8b 01                   mov    eax,DWORD PTR [ecx]
    //		2:  ff 60 04                jmp    DWORD PTR [eax+0x4]
    // ==OR==
    //		0:  8b 01                   mov    eax,DWORD PTR [ecx]
    //		2:  ff a0 18 03 00 00       jmp    DWORD PTR [eax+0x318]]

    // However, for vararg functions, they look like this:
    //		0:  8b 44 24 04             mov    eax,DWORD PTR [esp+0x4]
    //		4:  8b 00                   mov    eax,DWORD PTR [eax]
    //		6:  ff 60 08                jmp    DWORD PTR [eax+0x8]
    // ==OR==
    //		0:  8b 44 24 04             mov    eax,DWORD PTR [esp+0x4]
    //		4:  8b 00                   mov    eax,DWORD PTR [eax]
    //		6:  ff a0 18 03 00 00       jmp    DWORD PTR [eax+0x318]
    // With varargs, the this pointer is passed as if it was the first argument

    // On x64
    //		0:  48 8b 01                mov    rax,QWORD PTR [rcx]
    //		3:  ff 60 04                jmp    QWORD PTR [rax+0x4]
    // ==OR==
    //		0:  48 8b 01                mov    rax,QWORD PTR [rcx]
    //		3:  ff a0 18 03 00 00       jmp    QWORD PTR [rax+0x318]

    unsigned char* addr = (unsigned char*)*(void**)&func;
    if (*addr == 0xE9)		// Jmp
    {
        // May or may not be!
        // Check where it'd jump
        addr += 5 /*size of the instruction*/ + *(unsigned long*)(addr + 1);
    }

    bool ok = false;
    if (addr[0] == 0x8B && addr[1] == 0x44 && addr[2] == 0x24 && addr[3] == 0x04 &&
        addr[4] == 0x8B && addr[5] == 0x00)
    {
        addr += 6;
        ok = true;
    }
    else if (addr[0] == 0x8B && addr[1] == 0x01)
    {
        addr += 2;
        ok = true;
    }
    else if (addr[0] == 0x48 && addr[1] == 0x8B && addr[2] == 0x01)
    {
        addr += 3;
        ok = true;
    }
    if (!ok)
        return -1;

    if (*addr++ == 0xFF)
    {
        if (*addr == 0x60)
        {
            return *++addr / PTR_SIZE;
        }
        else if (*addr == 0xA0)
        {
            return *((unsigned int*)++addr) / PTR_SIZE;
        }
        else if (*addr == 0x20)
            return 0;
        else
            return -1;
    }
    return -1;
#endif
}

#endif