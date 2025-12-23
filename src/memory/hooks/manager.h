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

#ifndef src_memory_hooks_manager_h
#define src_memory_hooks_manager_h

#include <api/memory/hooks/manager.h>
#include <vector>

#include "function.h"
#include "mfunction.h"
#include "vfunction.h"

#include <entityhandle.h>
#include <public/entity2/entitysystem.h>
#include <string_t.h>

class HooksManager : public IHooksManager
{
public:
    virtual void Initialize() override;
    virtual void Shutdown() override;

    virtual IFunctionHook* CreateFunctionHook() override;
    virtual IVFunctionHook* CreateVFunctionHook() override;
    virtual IMFunctionHook* CreateMFunctionHook() override;

    virtual void DestroyFunctionHook(IFunctionHook* hook) override;
    virtual void DestroyVFunctionHook(IVFunctionHook* hook) override;
    virtual void DestroyMFunctionHook(IMFunctionHook* hook) override;

    // ptr CEntityIOOutput, string outputName, ptr activator, ptr caller, float delay -> int (HookResult)
    virtual uint64_t CreateEntityHookOutput(const std::string& className, const std::string& outputName, void* callback) override;
    virtual void DestroyEntityHookOutput(uint64_t id) override;
};

struct EntityIOConnectionDesc_t
{
    string_t m_targetDesc;
    string_t m_targetInput;
    string_t m_valueOverride;
    CEntityHandle m_hTarget;
    EntityIOTargetType_t m_nTargetType;
    int32_t m_nTimesToFire;
    float m_flDelay;
};

struct EntityIOConnection_t : EntityIOConnectionDesc_t
{
    bool m_bMarkedForRemoval;
    EntityIOConnection_t* m_pNext;
};

struct EntityIOOutputDesc_t
{
    const char* m_pName;
    uint32_t m_nFlags;
    uint32_t m_nOutputOffset;
};

class CEntityIOOutput
{
public:
    void* vtable;
    EntityIOConnection_t* m_pConnections;
    EntityIOOutputDesc_t* m_pDesc;
};

#endif