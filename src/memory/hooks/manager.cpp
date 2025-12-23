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

#include "manager.h"

#include <map>
#include <vector>

#include <api/shared/string.h>

IFunctionHook* HooksManager::CreateFunctionHook()
{
    return new FunctionHook();
}

IVFunctionHook* HooksManager::CreateVFunctionHook()
{
    return new VFunctionHook();
}

IMFunctionHook* HooksManager::CreateMFunctionHook()
{
    return new MFunctionHook();
}

void HooksManager::DestroyFunctionHook(IFunctionHook* hook)
{
    delete (FunctionHook*)hook;
}

void HooksManager::DestroyVFunctionHook(IVFunctionHook* hook)
{
    delete (VFunctionHook*)hook;
}

void HooksManager::DestroyMFunctionHook(IMFunctionHook* hook)
{
    delete (MFunctionHook*)hook;
}

IFunctionHook* g_pFireOutputHook = nullptr;
std::map<uint64_t, std::map<uint64_t, void*>> outputHooksList;

void CEntityIOOutput_FireOutputInternal_Hook(CEntityIOOutput* pThis, CEntityInstance* pActivator, CEntityInstance* pCaller, void* variantValue, float delay, void* unk01, void* unk02);

void HooksManager::Initialize()
{
    g_pFireOutputHook = CreateFunctionHook();
    g_pFireOutputHook->SetHookFunction("CEntityIOOutput::FireOutputInternal", reinterpret_cast<void*>(CEntityIOOutput_FireOutputInternal_Hook));
    g_pFireOutputHook->Enable();
}

void HooksManager::Shutdown()
{
    g_pFireOutputHook->Disable();
    DestroyFunctionHook(g_pFireOutputHook);
    g_pFireOutputHook = nullptr;
}

void CEntityIOOutput_FireOutputInternal_Hook(CEntityIOOutput* pThis, CEntityInstance* pActivator, CEntityInstance* pCaller, void* variantValue, float delay, void* unk01, void* unk02)
{
    const char* outputName = pThis->m_pDesc->m_pName;
    const char* callerClassName = pCaller ? pCaller->GetClassname() : "(null)";
    std::vector searchOutputs{ ((uint64_t)hash_32_fnv1a_const("*") << 32 | hash_32_fnv1a_const(outputName)), ((uint64_t)hash_32_fnv1a_const("*") << 32 | hash_32_fnv1a_const("*")) };

    if (pCaller)
    {
        uint64_t classHash = hash_32_fnv1a_const(callerClassName);
        uint64_t outputHash = hash_32_fnv1a_const(outputName);
        uint64_t combinedHash = (classHash << 32) | outputHash;
        searchOutputs.push_back(combinedHash);
        searchOutputs.push_back(((uint64_t)hash_32_fnv1a_const(callerClassName) << 32 | hash_32_fnv1a_const("*")));
    }

    for (auto& output : searchOutputs)
    {
        bool shouldStop = false;
        for (auto& hook : outputHooksList[output])
        {
            int result = reinterpret_cast<int (*)(CEntityIOOutput*, const char*, CEntityInstance*, CEntityInstance*, float)>(hook.second)(pThis, outputName, pActivator, pCaller, delay);
            if (result == 1)
            {
                return;
            }
            else if (result == 2)
            {
                shouldStop = true;
                break;
            }
        }
        if (shouldStop)
        {
            break;
        }
    }

    reinterpret_cast<decltype(&CEntityIOOutput_FireOutputInternal_Hook)>(g_pFireOutputHook->GetOriginal())(pThis, pActivator, pCaller, variantValue, delay, unk01, unk02);
}

uint64_t HooksManager::CreateEntityHookOutput(const std::string& className, const std::string& outputName, void* callback)
{
    static uint64_t listenerID = 0;
    uint64_t outputHash = ((uint64_t)hash_32_fnv1a_const(className.c_str()) << 32 | hash_32_fnv1a_const(outputName.c_str()));
    outputHooksList[outputHash][++listenerID] = callback;
    return listenerID;
}

void HooksManager::DestroyEntityHookOutput(uint64_t id)
{
    for (auto& output : outputHooksList)
    {
        auto it = output.second.find(id);
        if (it != output.second.end())
        {
            output.second.erase(it);
        }
    }
}
