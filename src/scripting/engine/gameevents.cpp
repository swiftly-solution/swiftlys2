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

bool Bridge_GameEvents_GetBool(void* event, const char* key)
{
    if (!event) return false;
    return ((IGameEvent*)event)->GetBool(key);
}

int Bridge_GameEvents_GetInt(void* event, const char* key)
{
    if (!event) return 0;
    return ((IGameEvent*)event)->GetInt(key);
}

uint64_t Bridge_GameEvents_GetUint64(void* event, const char* key)
{
    if (!event) return 0;
    return ((IGameEvent*)event)->GetUint64(key);
}

float Bridge_GameEvents_GetFloat(void* event, const char* key)
{
    if (!event) return 0.0f;
    return ((IGameEvent*)event)->GetFloat(key);
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

void* Bridge_GameEvents_GetPtr(void* event, const char* key)
{
    if (!event) return nullptr;
    return ((IGameEvent*)event)->GetPtr(key);
}

void* Bridge_GameEvents_GetEHandle(void* event, const char* key)
{
    if (!event) return nullptr;
    return ((IGameEvent*)event)->GetEHandle(key).Get();
}

void* Bridge_GameEvents_GetEntity(void* event, const char* key)
{
    if (!event) return nullptr;
    return ((IGameEvent*)event)->GetEntity(key);
}

int Bridge_GameEvents_GetEntityIndex(void* event, const char* key)
{
    if (!event) return -1;
    return ((IGameEvent*)event)->GetEntityIndex(key).Get();
}

int Bridge_GameEvents_GetPlayerSlot(void* event, const char* key)
{
    if (!event) return -1;
    return ((IGameEvent*)event)->GetPlayerSlot(key).Get();
}

void* Bridge_GameEvents_GetPlayerController(void* event, const char* key)
{
    if (!event) return nullptr;
    return ((IGameEvent*)event)->GetPlayerController(key);
}

void* Bridge_GameEvents_GetPlayerPawn(void* event, const char* key)
{
    if (!event) return nullptr;
    return ((IGameEvent*)event)->GetPlayerPawn(key);
}

void* Bridge_GameEvents_GetPawnEHandle(void* event, const char* key)
{
    if (!event) return nullptr;
    return ((IGameEvent*)event)->GetPawnEHandle(key).Get();
}

int Bridge_GameEvents_GetPawnEntityIndex(void* event, const char* key)
{
    if (!event) return -1;
    return ((IGameEvent*)event)->GetPawnEntityIndex(key).Get();
}

void Bridge_GameEvents_SetBool(void* event, const char* key, bool value)
{
    if (!event) return;
    ((IGameEvent*)event)->SetBool(key, value);
}

void Bridge_GameEvents_SetInt(void* event, const char* key, int value)
{
    if (!event) return;
    ((IGameEvent*)event)->SetInt(key, value);
}

void Bridge_GameEvents_SetUint64(void* event, const char* key, uint64_t value)
{
    if (!event) return;
    ((IGameEvent*)event)->SetUint64(key, value);
}

void Bridge_GameEvents_SetFloat(void* event, const char* key, float value)
{
    if (!event) return;
    ((IGameEvent*)event)->SetFloat(key, value);
}

void Bridge_GameEvents_SetString(void* event, const char* key, const char* value)
{
    if (!event) return;
    ((IGameEvent*)event)->SetString(key, value);
}

void Bridge_GameEvents_SetPtr(void* event, const char* key, void* value)
{
    if (!event) return;
    ((IGameEvent*)event)->SetPtr(key, value);
}

void Bridge_GameEvents_SetEntity(void* event, const char* key, void* value)
{
    if (!event) return;
    ((IGameEvent*)event)->SetEntity(key, (CEntityInstance*)value);
}

void Bridge_GameEvents_SetEntityIndex(void* event, const char* key, int value)
{
    if (!event) return;
    ((IGameEvent*)event)->SetEntity(key, CEntityIndex(value));
}

void Bridge_GameEvents_SetPlayerSlot(void* event, const char* key, int value)
{
    if (!event) return;
    ((IGameEvent*)event)->SetPlayer(key, CPlayerSlot(value));
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

void Bridge_GameEvents_SetListenerPreHandler(void* callback_ptr)
{
    g_pGameEventManager->SetGameEventFireHandler([callback_ptr](std::string& event_name, IGameEvent* event, bool& dont_broadcast, uint32_t& hash) -> int
        {
            typedef int (*CallbackType)(uint32_t hash, void* event, bool* dont_broadcast);
            return reinterpret_cast<CallbackType>(callback_ptr)(hash, event, &dont_broadcast);
        });
}

void Bridge_GameEvents_SetListenerPostHandler(void* callback_ptr)
{
    g_pGameEventManager->SetPostGameEventFireHandler([callback_ptr](std::string& event_name, IGameEvent* event, bool& dont_broadcast, uint32_t& hash) -> int
        {
            typedef int (*CallbackType)(uint32_t hash, void* event, bool* dont_broadcast);
            return reinterpret_cast<CallbackType>(callback_ptr)(hash, event, &dont_broadcast);
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

DEFINE_NATIVE("GameEvents.GetBool", Bridge_GameEvents_GetBool);
DEFINE_NATIVE("GameEvents.GetInt", Bridge_GameEvents_GetInt);
DEFINE_NATIVE("GameEvents.GetUint64", Bridge_GameEvents_GetUint64);
DEFINE_NATIVE("GameEvents.GetFloat", Bridge_GameEvents_GetFloat);
DEFINE_NATIVE("GameEvents.GetString", Bridge_GameEvents_GetString);
DEFINE_NATIVE("GameEvents.GetPtr", Bridge_GameEvents_GetPtr);
DEFINE_NATIVE("GameEvents.GetEHandle", Bridge_GameEvents_GetEHandle);
DEFINE_NATIVE("GameEvents.GetEntity", Bridge_GameEvents_GetEntity);
DEFINE_NATIVE("GameEvents.GetEntityIndex", Bridge_GameEvents_GetEntityIndex);
DEFINE_NATIVE("GameEvents.GetPlayerSlot", Bridge_GameEvents_GetPlayerSlot);
DEFINE_NATIVE("GameEvents.GetPlayerController", Bridge_GameEvents_GetPlayerController);
DEFINE_NATIVE("GameEvents.GetPlayerPawn", Bridge_GameEvents_GetPlayerPawn);
DEFINE_NATIVE("GameEvents.GetPawnEHandle", Bridge_GameEvents_GetPawnEHandle);
DEFINE_NATIVE("GameEvents.GetPawnEntityIndex", Bridge_GameEvents_GetPawnEntityIndex);
DEFINE_NATIVE("GameEvents.SetBool", Bridge_GameEvents_SetBool);
DEFINE_NATIVE("GameEvents.SetInt", Bridge_GameEvents_SetInt);
DEFINE_NATIVE("GameEvents.SetUint64", Bridge_GameEvents_SetUint64);
DEFINE_NATIVE("GameEvents.SetFloat", Bridge_GameEvents_SetFloat);
DEFINE_NATIVE("GameEvents.SetString", Bridge_GameEvents_SetString);
DEFINE_NATIVE("GameEvents.SetPtr", Bridge_GameEvents_SetPtr);
DEFINE_NATIVE("GameEvents.SetEntity", Bridge_GameEvents_SetEntity);
DEFINE_NATIVE("GameEvents.SetEntityIndex", Bridge_GameEvents_SetEntityIndex);
DEFINE_NATIVE("GameEvents.SetPlayerSlot", Bridge_GameEvents_SetPlayerSlot);
DEFINE_NATIVE("GameEvents.HasKey", Bridge_GameEvents_HasKey);
DEFINE_NATIVE("GameEvents.IsReliable", Bridge_GameEvents_IsReliable);
DEFINE_NATIVE("GameEvents.IsLocal", Bridge_GameEvents_IsLocal);
DEFINE_NATIVE("GameEvents.RegisterListener", Bridge_GameEvents_RegisterListener);
DEFINE_NATIVE("GameEvents.SetListenerPreHandler", Bridge_GameEvents_SetListenerPreHandler);
DEFINE_NATIVE("GameEvents.SetListenerPostHandler", Bridge_GameEvents_SetListenerPostHandler);
DEFINE_NATIVE("GameEvents.CreateEvent", Bridge_GameEvents_CreateEvent);
DEFINE_NATIVE("GameEvents.FreeEvent", Bridge_GameEvents_FreeEvent);
DEFINE_NATIVE("GameEvents.FireEvent", Bridge_GameEvents_FireEvent);
DEFINE_NATIVE("GameEvents.FireEventToClient", Bridge_GameEvents_FireEventToClient);
DEFINE_NATIVE("GameEvents.IsPlayerListeningToEventName", Bridge_GameEvents_IsPlayerListeningToEventName);
DEFINE_NATIVE("GameEvents.IsPlayerListeningToEvent", Bridge_GameEvents_IsPlayerListeningToEvent);