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

#include <api/shared/string.h>

#include <fmt/format.h>

#include <game/shared/ehandle.h>

#include <scripting/scripting.h>

typedef IGameEventListener2* (*GetLegacyGameEventListener)(CPlayerSlot slot);

static char* Bridge_GameEvents_CopyString(const std::string& value, int* size)
{
    int outSize = static_cast<int>(value.size());
    *size = outSize;

    char* out = (char*)g_pMemoryAllocator->Alloc(outSize + 1);
    g_pMemoryAllocator->Copy(out, (void*)value.c_str(), outSize);
    out[outSize] = '\0';
    return out;
}

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
    switch (kind)
    {
        case GameEventValueKind_Bool: *(bool*)out = false; break;
        case GameEventValueKind_Int32: *(int32_t*)out = 0; break;
        case GameEventValueKind_UInt64: *(uint64_t*)out = 0; break;
        case GameEventValueKind_Float: *(float*)out = 0.0f; break;
        case GameEventValueKind_Ptr:
        case GameEventValueKind_Entity:
        case GameEventValueKind_PlayerController:
        case GameEventValueKind_PlayerPawn: *(void**)out = nullptr; break;
        case GameEventValueKind_EntityIndex:
        case GameEventValueKind_PlayerSlot:
        case GameEventValueKind_PawnEntityIndex: *(int32_t*)out = -1; break;
    }
}

bool Bridge_GameEvents_GetValue(void* pevent, const char* key, int kind, void* out)
{
    WriteDefaultValue(kind, out);
    if (!pevent) return false;

    IGameEvent* event = (IGameEvent*)pevent;
    switch (kind)
    {
        case GameEventValueKind_Bool: *(bool*)out = event->GetBool(key); break;
        case GameEventValueKind_Int32: *(int32_t*)out = event->GetInt(key); break;
        case GameEventValueKind_UInt64: *(uint64_t*)out = event->GetUint64(key); break;
        case GameEventValueKind_Float: *(float*)out = event->GetFloat(key); break;
        case GameEventValueKind_Ptr: *(void**)out = event->GetPtr(key); break;
        case GameEventValueKind_Entity: *(void**)out = event->GetEntity(key); break;
        case GameEventValueKind_EntityIndex: *(int32_t*)out = event->GetEntityIndex(key).Get(); break;
        case GameEventValueKind_PlayerSlot: *(int32_t*)out = event->GetPlayerSlot(key).Get(); break;
        case GameEventValueKind_PlayerController: *(void**)out = event->GetPlayerController(key); break;
        case GameEventValueKind_PlayerPawn: *(void**)out = event->GetPlayerPawn(key); break;
        case GameEventValueKind_PawnEntityIndex: *(int32_t*)out = event->GetPawnEntityIndex(key).Get(); break;
        default: return false;
    }

    return true;
}

bool Bridge_GameEvents_SetValue(void* pevent, const char* key, int kind, const void* value)
{
    if (!pevent) return false;

    IGameEvent* event = (IGameEvent*)pevent;
    switch (kind)
    {
        case GameEventValueKind_Bool: event->SetBool(key, *(const bool*)value); break;
        case GameEventValueKind_Int32: event->SetInt(key, *(const int32_t*)value); break;
        case GameEventValueKind_UInt64: event->SetUint64(key, *(const uint64_t*)value); break;
        case GameEventValueKind_Float: event->SetFloat(key, *(const float*)value); break;
        case GameEventValueKind_Ptr: event->SetPtr(key, *(void* const*)value); break;
        case GameEventValueKind_Entity: event->SetEntity(key, *(CEntityInstance* const*)value); break;
        case GameEventValueKind_EntityIndex: event->SetEntity(key, CEntityIndex(*(const int32_t*)value)); break;
        case GameEventValueKind_PlayerSlot: event->SetPlayer(key, CPlayerSlot(*(const int32_t*)value)); break;
        default: return false;
    }

    return true;
}

char* Bridge_GameEvents_GetString(int* size, void* event, const char* key)
{
    if (!event)
    {
        return Bridge_GameEvents_CopyString("", size);
    }

    std::string s = ((IGameEvent*)event)->GetString(key);
    return Bridge_GameEvents_CopyString(s, size);
}

void Bridge_GameEvents_SetString(void* event, const char* key, const char* value)
{
    if (!event) return;
    ((IGameEvent*)event)->SetString(key, value);
}

bool Bridge_GameEvents_HasKey(void* event, const char* key)
{
    if (!event) return false;
    return ((IGameEvent*)event)->HasKey(key);
}

bool Bridge_GameEvents_IsReliable(void* event)
{
    if (!event) return false;
    return ((IGameEvent*)event)->IsReliable();
}

bool Bridge_GameEvents_IsLocal(void* event)
{
    if (!event) return false;
    return ((IGameEvent*)event)->IsLocal();
}

void Bridge_GameEvents_RegisterListener(const char* eventName)
{
    g_pGameEventManager->RegisterGameEventListener(eventName);
}

typedef int (*GameEventCallbackType)(uint32_t hash, void* event, bool* dont_broadcast);

void Bridge_GameEvents_UnregisterListener(const char* eventName)
{
    g_pGameEventManager->UnregisterGameEventListener(eventName);
}

void Bridge_GameEvents_SetListenerPreHandler(void* callback_ptr)
{
    g_pGameEventManager->SetGameEventFireHandler([callback_ptr](IGameEvent* event, bool& dont_broadcast, uint32_t hash) -> int
        {
            return reinterpret_cast<GameEventCallbackType>(callback_ptr)(hash, event, &dont_broadcast);
        });
}

void Bridge_GameEvents_SetListenerPostHandler(void* callback_ptr)
{
    g_pGameEventManager->SetPostGameEventFireHandler([callback_ptr](IGameEvent* event, bool& dont_broadcast, uint32_t hash) -> int
        {
            return reinterpret_cast<GameEventCallbackType>(callback_ptr)(hash, event, &dont_broadcast);
        });
}

void* Bridge_GameEvents_CreateEvent(const char* eventName)
{
    return g_pGameEventManager->GetGameEventManager()->CreateEvent(eventName);
}

void Bridge_GameEvents_FreeEvent(void* event)
{
    g_pGameEventManager->GetGameEventManager()->FreeEvent((IGameEvent*)event);
}

void Bridge_GameEvents_FireEvent(void* event, bool dontBroadcast)
{
    if (!event) return;

    g_pGameEventManager->GetGameEventManager()->FireEvent((IGameEvent*)event, dontBroadcast);
}

void Bridge_GameEvents_FireEventToClient(void* event, int playerid)
{
    if (!event) return;

    static auto pListenerSig = g_pGameDataManager->GetSignatures()->Fetch("LegacyGameEventListener");
    if (!pListenerSig) return;

    auto listener = reinterpret_cast<GetLegacyGameEventListener>(pListenerSig)(playerid);
    if (!listener) return;

    if (!g_pGameEventManager->GetGameEventManager()->FindListener(listener, ((IGameEvent*)event)->GetName()))
    {
        return g_pCrashReporter->ReportPreventionIncident("GameEvents", fmt::format("Tried to fire event '{}' but the client isn't listening to this event.", ((IGameEvent*)event)->GetName()));
    }

    listener->FireGameEvent((IGameEvent*)event);
}

bool Bridge_GameEvents_IsPlayerListeningToEventName(int playerid, const char* eventName)
{
    auto pListenerSig = g_pGameDataManager->GetSignatures()->Fetch("LegacyGameEventListener");
    if (!pListenerSig) return false;

    auto listener = reinterpret_cast<GetLegacyGameEventListener>(pListenerSig)(playerid);
    if (!listener) return false;

    return g_pGameEventManager->GetGameEventManager()->FindListener(listener, eventName);
}

bool Bridge_GameEvents_IsPlayerListeningToEvent(int playerid, void* event)
{
    auto pListenerSig = g_pGameDataManager->GetSignatures()->Fetch("LegacyGameEventListener");
    if (!pListenerSig) return false;

    auto listener = reinterpret_cast<GetLegacyGameEventListener>(pListenerSig)(playerid);
    if (!listener) return false;

    return g_pGameEventManager->GetGameEventManager()->FindListener(listener, ((IGameEvent*)event)->GetName());
}

DEFINE_NATIVE("GameEvents.GetValue", Bridge_GameEvents_GetValue);
DEFINE_NATIVE("GameEvents.SetValue", Bridge_GameEvents_SetValue);
DEFINE_NATIVE("GameEvents.GetString", Bridge_GameEvents_GetString);
DEFINE_NATIVE("GameEvents.SetString", Bridge_GameEvents_SetString);
DEFINE_NATIVE("GameEvents.HasKey", Bridge_GameEvents_HasKey);
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
DEFINE_NATIVE("GameEvents.IsPlayerListeningToEvent", Bridge_GameEvents_IsPlayerListeningToEvent);
