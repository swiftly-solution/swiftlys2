/************************************************************************************************
 * SwiftlyS2 is a scripting framework for Source2-based games.
 * Copyright (C) 2023-2026 Swiftly Solution SRL via Sava Andrei-Sebastian and it's contributors
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 ************************************************************************************************/

#include <api/interfaces/interfaces.h>

#include <api/shared/profiler.h>

#include <public/entity2/entityhandle.h>

#include <scripting/scripting.h>

enum GameEventValueKind
{
    GameEventValueKind_Bool,
    GameEventValueKind_Int32,
    GameEventValueKind_UInt64,
    GameEventValueKind_Float,
    GameEventValueKind_Ptr,
    GameEventValueKind_Entity,
    GameEventValueKind_EntityIndex,
    GameEventValueKind_PlayerSlot,
    GameEventValueKind_PlayerController,
    GameEventValueKind_PlayerPawn,
    GameEventValueKind_PawnEntityIndex
};

static void WriteDefaultValue(int kind, void* out)
{
    PROFILE_FUNCTION();

    switch (kind)
    {
        case GameEventValueKind_Bool: *static_cast<bool*>(out) = false; break;
        case GameEventValueKind_Int32: *static_cast<int32_t*>(out) = 0; break;
        case GameEventValueKind_UInt64: *static_cast<uint64_t*>(out) = 0; break;
        case GameEventValueKind_Float: *static_cast<float*>(out) = 0.0f; break;
        case GameEventValueKind_Ptr:
        case GameEventValueKind_Entity:
        case GameEventValueKind_PlayerController:
        case GameEventValueKind_PlayerPawn: *static_cast<void**>(out) = nullptr; break;
        case GameEventValueKind_EntityIndex:
        case GameEventValueKind_PlayerSlot:
        case GameEventValueKind_PawnEntityIndex: *static_cast<int32_t*>(out) = -1; break;
    }
}

bool Bridge_GameEvents_GetValue(void* pevent, const char* key, int kind, void* out)
{
    PROFILE_FUNCTION();

    WriteDefaultValue(kind, out);
    if (!pevent) return false;

    auto* event = static_cast<IGameEvent*>(pevent);
    switch (kind)
    {
        case GameEventValueKind_Bool: *static_cast<bool*>(out) = event->GetBool(key); break;
        case GameEventValueKind_Int32: *static_cast<int32_t*>(out) = event->GetInt(key); break;
        case GameEventValueKind_UInt64: *static_cast<uint64_t*>(out) = event->GetUint64(key); break;
        case GameEventValueKind_Float: *static_cast<float*>(out) = event->GetFloat(key); break;
        case GameEventValueKind_Ptr: *static_cast<void**>(out) = event->GetPtr(key); break;
        case GameEventValueKind_Entity: *static_cast<void**>(out) = event->GetEntity(key); break;
        case GameEventValueKind_EntityIndex: *static_cast<int32_t*>(out) = event->GetEntityIndex(key).Get(); break;
        case GameEventValueKind_PlayerSlot: *static_cast<int32_t*>(out) = event->GetPlayerSlot(key).Get(); break;
        case GameEventValueKind_PlayerController: *static_cast<void**>(out) = event->GetPlayerController(key); break;
        case GameEventValueKind_PlayerPawn: *static_cast<void**>(out) = event->GetPlayerPawn(key); break;
        case GameEventValueKind_PawnEntityIndex: *static_cast<int32_t*>(out) = event->GetPawnEntityIndex(key).Get(); break;
        default: return false;
    }

    return true;
}

bool Bridge_GameEvents_SetValue(void* pevent, const char* key, int kind, const void* value)
{
    PROFILE_FUNCTION();

    if (!pevent) return false;

    auto* event = static_cast<IGameEvent*>(pevent);
    switch (kind)
    {
        case GameEventValueKind_Bool: event->SetBool(key, *static_cast<const bool*>(value)); break;
        case GameEventValueKind_Int32: event->SetInt(key, *static_cast<const int32_t*>(value)); break;
        case GameEventValueKind_UInt64: event->SetUint64(key, *static_cast<const uint64_t*>(value)); break;
        case GameEventValueKind_Float: event->SetFloat(key, *static_cast<const float*>(value)); break;
        case GameEventValueKind_Ptr: event->SetPtr(key, *static_cast<void* const*>(value)); break;
        case GameEventValueKind_Entity: event->SetEntity(key, *static_cast<CEntityInstance* const*>(value)); break;
        case GameEventValueKind_EntityIndex: event->SetEntity(key, CEntityHandle(*static_cast<const int32_t*>(value))); break;
        case GameEventValueKind_PlayerSlot: event->SetPlayer(key, CPlayerSlot(*static_cast<const int32_t*>(value))); break;
        default: return false;
    }

    return true;
}

char* Bridge_GameEvents_GetString(int* size, void* event, const char* key)
{
    PROFILE_FUNCTION();

    if (!event)
    {
        return Bridge_CopyString("", size);
    }

    std::string s = static_cast<IGameEvent*>(event)->GetString(key);
    return Bridge_CopyString(s, size);
}

void Bridge_GameEvents_SetString(void* event, const char* key, const char* value)
{
    PROFILE_FUNCTION();

    if (!event) return;
    static_cast<IGameEvent*>(event)->SetString(key, value);
}

bool Bridge_GameEvents_IsReliable(void* event)
{
    PROFILE_FUNCTION();

    if (!event) return false;
    return static_cast<IGameEvent*>(event)->IsReliable();
}

bool Bridge_GameEvents_IsLocal(void* event)
{
    PROFILE_FUNCTION();

    if (!event) return false;
    return static_cast<IGameEvent*>(event)->IsLocal();
}

void Bridge_GameEvents_RegisterListener(const char* eventName)
{
    PROFILE_FUNCTION();

    g_pGameEventManager->RegisterGameEventListener(eventName);
}

using GameEventCallbackType = int (*)(uint32_t hash, void* event, bool* dont_broadcast);

void Bridge_GameEvents_UnregisterListener(const char* eventName)
{
    PROFILE_FUNCTION();

    g_pGameEventManager->UnregisterGameEventListener(eventName);
}

void Bridge_GameEvents_SetListenerPreHandler(void* callback_ptr)
{
    PROFILE_FUNCTION();

    g_pGameEventManager->SetGameEventFireHandler([callback_ptr](IGameEvent* event, bool& dont_broadcast, uint32_t hash) -> int
        {
            return reinterpret_cast<GameEventCallbackType>(callback_ptr)(hash, event, &dont_broadcast);
        });
}

void Bridge_GameEvents_SetListenerPostHandler(void* callback_ptr)
{
    PROFILE_FUNCTION();

    g_pGameEventManager->SetPostGameEventFireHandler([callback_ptr](IGameEvent* event, bool& dont_broadcast, uint32_t hash) -> int
        {
            return reinterpret_cast<GameEventCallbackType>(callback_ptr)(hash, event, &dont_broadcast);
        });
}

void* Bridge_GameEvents_CreateEvent(const char* eventName)
{
    PROFILE_FUNCTION();

    return g_pGameEventManager->GetGameEventManager()->CreateEvent(eventName);
}

void Bridge_GameEvents_FreeEvent(void* event)
{
    PROFILE_FUNCTION();

    g_pGameEventManager->GetGameEventManager()->FreeEvent(static_cast<IGameEvent*>(event));
}

void Bridge_GameEvents_FireEvent(void* event, bool dontBroadcast)
{
    PROFILE_FUNCTION();

    if (!event) return;

    g_pGameEventManager->GetGameEventManager()->FireEvent(static_cast<IGameEvent*>(event), dontBroadcast);
}

void Bridge_GameEvents_FireEventToClient(void* event, int playerid)
{
    PROFILE_FUNCTION();

    if (!event) return;
    auto player = g_pPlayerManager->GetPlayer(playerid);
    if (!player) return;
    if (!player->IsListeningGameEvent(static_cast<IGameEvent*>(event)->GetID())) return;
    player->SendGameEvent(static_cast<IGameEvent*>(event));
}

bool Bridge_GameEvents_IsPlayerListeningToEventName(int playerid, const char* eventName)
{
    PROFILE_FUNCTION();

    auto player = g_pPlayerManager->GetPlayer(playerid);
    if (!player) return false;

    return player->IsListeningGameEvent(g_pGameEventManager->GetGameEventListenerID(eventName));
}

DEFINE_NATIVE("GameEvents.GetValue", Bridge_GameEvents_GetValue);
DEFINE_NATIVE("GameEvents.SetValue", Bridge_GameEvents_SetValue);
DEFINE_NATIVE("GameEvents.GetString", Bridge_GameEvents_GetString);
DEFINE_NATIVE("GameEvents.SetString", Bridge_GameEvents_SetString);
DEFINE_NATIVE("GameEvents.IsReliable", Bridge_GameEvents_IsReliable);
DEFINE_NATIVE("GameEvents.IsLocal", Bridge_GameEvents_IsLocal);
DEFINE_NATIVE("GameEvents.RegisterListener", Bridge_GameEvents_RegisterListener);
DEFINE_NATIVE("GameEvents.UnregisterListener", Bridge_GameEvents_UnregisterListener);
DEFINE_NATIVE("GameEvents.SetListenerPreHandler", Bridge_GameEvents_SetListenerPreHandler);
DEFINE_NATIVE("GameEvents.SetListenerPostHandler", Bridge_GameEvents_SetListenerPostHandler);
DEFINE_NATIVE("GameEvents.CreateEvent", Bridge_GameEvents_CreateEvent);
DEFINE_NATIVE("GameEvents.FreeEvent", Bridge_GameEvents_FreeEvent);
DEFINE_NATIVE("GameEvents.FireEvent", Bridge_GameEvents_FireEvent);
DEFINE_NATIVE("GameEvents.FireEventToClient", Bridge_GameEvents_FireEventToClient);
DEFINE_NATIVE("GameEvents.IsPlayerListeningToEventName", Bridge_GameEvents_IsPlayerListeningToEventName);