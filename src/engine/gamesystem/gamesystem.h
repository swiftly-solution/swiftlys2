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

#ifndef src_engine_gamesystem_gamesystem_h
#define src_engine_gamesystem_gamesystem_h

#include <public/entity2/entitysystem.h>
#include <game/shared/igamesystemfactory.h>

bool InitGameSystem();
bool ShutdownGameSystem();

class CGameSystem : public CBaseGameSystem
{
public:
    GS_EVENT(BuildGameSessionManifest);

    void Shutdown() override
    {
        delete sm_Factory;
    }

    void SetGameSystemGlobalPtrs(void* pValue) override
    {
        if (sm_Factory)
            sm_Factory->SetGlobalPtr(pValue);
    }

    bool DoesGameSystemReallocate() override { return sm_Factory->ShouldAutoAdd(); }

    static IGameSystemFactory* sm_Factory;
};

abstract_class IGameSystemEventDispatcher{
    public:
        virtual ~IGameSystemEventDispatcher() {}
};

class CGameSystemEventDispatcher : public IGameSystemEventDispatcher
{
public:
    CUtlVector<CUtlVector<IGameSystem*>>* m_funcListeners;
};

struct AddedGameSystem_t
{
    IGameSystem* m_pGameSystem;
    int m_nPriority;
    int m_nInsertionOrder;
};

#endif