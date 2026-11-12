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

#ifndef src_memory_hooks_vfunction_h
#define src_memory_hooks_vfunction_h

#include <api/memory/hooks/vfunction.h>
#include <safetyhook/safetyhook.hpp>

#include <functional>
#include <string>

#include "managed.h"

class VFunctionHook : public IVFunctionHook, public IManagedHook
{
public:
    VFunctionHook() = default;
    VFunctionHook(void* callback, std::string moduleName, std::string vtableName, std::string offsetName);
    VFunctionHook(void* callback, std::string interfaceName, std::string offsetName);
    VFunctionHook(void* callback, std::function<void*()> instanceResolver, std::string offsetName);

    ~VFunctionHook();

    VFunctionHook(const VFunctionHook&) = delete;
    VFunctionHook& operator=(const VFunctionHook&) = delete;

    void SetHookFunction(void* instance, int index, void* callback, bool is_vtable) override;

    void Enable() override;
    void Disable() override;

    void* GetOriginal() override;

    bool Install() override;
    void Uninstall() override;
    const char* GetName() const override;

private:
    SafetyHookInline m_oHook;

    std::function<void()> m_fnResolve;
    std::string m_sName;
    bool m_bManaged = false;
};

#endif
