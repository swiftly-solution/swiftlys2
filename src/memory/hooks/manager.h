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

#include "function.h"
#include "mfunction.h"
#include "vfunction.h"

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
};

#endif