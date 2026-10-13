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

#ifndef src_memory_hooks_function_h
#define src_memory_hooks_function_h

#include <api/memory/hooks/function.h>
#include <safetyhook/safetyhook.hpp>

#include <functional>
#include <string>

#include "managed.h"

class FunctionHook : public IFunctionHook, public IManagedHook
{
public:
    FunctionHook() = default;
    FunctionHook(void* callback, std::string signatureName);
    FunctionHook(void* callback, std::function<void*()> addressResolver, std::string name);

    ~FunctionHook();

    FunctionHook(const FunctionHook&) = delete;
    FunctionHook& operator=(const FunctionHook&) = delete;

    virtual void SetHookFunction(const std::string& functionSignature, void* callback) override;
    virtual void SetHookFunction(void* functionAddress, void* callback) override;

    virtual void Enable() override;
    virtual void Disable() override;

    virtual void* GetOriginal() override;
    virtual bool IsEnabled() override;

    virtual bool Install() override;
    virtual void Uninstall() override;
    virtual const char* GetName() const override;

private:
    SafetyHookInline m_oHook;

    std::function<void()> m_fnResolve;
    std::string m_sName;
    bool m_bManaged = false;
};

#endif
