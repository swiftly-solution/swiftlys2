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

#ifndef src_engine_entities_entitysystem_h
#define src_engine_entities_entitysystem_h

#include <api/engine/entities/entitysystem.h>

class CEntSystem : public IEntitySystem
{
public:
    virtual void Initialize() override;
    virtual void Shutdown() override;

    virtual void Spawn(void* pEntity, void* pKeyValues) override;
    virtual void Despawn(void* pEntity) override;

    virtual void* CreateEntityByName(const char* name) override;

    virtual void AcceptInput(void* pEntity, const char* input, void* activator, void* caller, InputType value, int outputID) override;
    virtual void AddEntityIOEvent(void* pEntity, const char* input, void* activator, void* caller, InputType value, float delay) override;

    virtual bool IsValidEntity(void* pEntity) override;

    virtual void AddEntityListener(IEntityListener* listener) override;
    virtual void RemoveEntityListener(IEntityListener* listener) override;

    virtual CEntitySystem* GetEntitySystem() override;

    virtual void* GetGameRules() override;

    void StartupServer(const GameSessionConfiguration_t& config, ISource2WorldSession*, const char*);
};

extern void* g_pGameRules;

#endif