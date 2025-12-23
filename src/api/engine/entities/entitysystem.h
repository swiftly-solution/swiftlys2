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

#ifndef src_api_engine_entities_entitysystem_h
#define src_api_engine_entities_entitysystem_h

#include <variant>

#include <public/entity2/entitysystem.h>

using InputType = std::variant<int32_t, uint32_t, int64_t, uint64_t, float, double, bool, const char*>;

class IEntitySystem
{
public:
    virtual void Initialize() = 0;
    virtual void Shutdown() = 0;

    virtual void Spawn(void* pEntity, void* pKeyValues) = 0;
    virtual void Despawn(void* pEntity) = 0;

    virtual void* CreateEntityByName(const char* name) = 0;

    virtual void AcceptInput(void* pEntity, const char* input, void* activator, void* caller, InputType value, int outputID) = 0;
    virtual void AddEntityIOEvent(void* pEntity, const char* input, void* activator, void* caller, InputType value, float delay) = 0;

    virtual bool IsValidEntity(void* pEntity) = 0;

    virtual void AddEntityListener(IEntityListener* listener) = 0;
    virtual void RemoveEntityListener(IEntityListener* listener) = 0;

    virtual CEntitySystem* GetEntitySystem() = 0;

    virtual void* GetGameRules() = 0;
};

#endif