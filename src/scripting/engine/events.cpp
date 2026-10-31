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

#include <scripting/scripting.h>
#include <api/interfaces/interfaces.h>
#include <api/shared/profiler.h>
#include <scripting/engine/events.h>

DEFINE_EVENT(g_pOnGameTickCallback, void, bool, bool, bool) = nullptr;
DEFINE_EVENT(g_pOnClientConnectCallback, bool, int) = nullptr;
DEFINE_EVENT(g_pOnClientDisconnectCallback, void, int, int) = nullptr;
DEFINE_EVENT(g_pOnClientKeyStateChangedCallback, void, int, uint32_t, bool) = nullptr;
DEFINE_EVENT(g_pOnClientPutInServerCallback, void, int, int) = nullptr;
DEFINE_EVENT(g_pOnClientSteamAuthorizeCallback, void, int) = nullptr;
DEFINE_EVENT(g_pOnClientSteamAuthorizeFailCallback, void, int) = nullptr;
DEFINE_EVENT(g_pOnEntityCreatedCallback, void, void*) = nullptr;
DEFINE_EVENT(g_pOnEntityDeletedCallback, void, void*) = nullptr;
DEFINE_EVENT(g_pOnEntityParentChangedCallback, void, void*, void*) = nullptr;
DEFINE_EVENT(g_pOnEntitySpawnedCallback, void, void*) = nullptr;
DEFINE_EVENT(g_pOnMapLoadCallback, void, const char*) = nullptr;
DEFINE_EVENT(g_pOnMapUnloadCallback, void, const char*) = nullptr;
DEFINE_EVENT(g_pOnPrecacheResourceCallback, void, void*) = nullptr;
DEFINE_EVENT(g_pOnPreworldUpdateCallback, void, bool) = nullptr;
DEFINE_EVENT(g_pOnStartupServerCallback, void) = nullptr;

void Bridge_Events_RegisterOnGameTickCallback(void* callback)
{
    PROFILE_FUNCTION();

    g_pOnGameTickCallback = reinterpret_cast<decltype(g_pOnGameTickCallback)>(callback);
}

void Bridge_Events_RegisterOnClientConnectCallback(void* callback)
{
    PROFILE_FUNCTION();

    g_pOnClientConnectCallback = reinterpret_cast<decltype(g_pOnClientConnectCallback)>(callback);
}

void Bridge_Events_RegisterOnClientDisconnectCallback(void* callback)
{
    PROFILE_FUNCTION();

    g_pOnClientDisconnectCallback = reinterpret_cast<decltype(g_pOnClientDisconnectCallback)>(callback);
}

void Bridge_Events_RegisterOnClientKeyStateChangedCallback(void* callback)
{
    PROFILE_FUNCTION();

    g_pOnClientKeyStateChangedCallback = reinterpret_cast<decltype(g_pOnClientKeyStateChangedCallback)>(callback);
}

void Bridge_Events_RegisterOnClientPutInServerCallback(void* callback)
{
    PROFILE_FUNCTION();

    g_pOnClientPutInServerCallback = reinterpret_cast<decltype(g_pOnClientPutInServerCallback)>(callback);
}

void Bridge_Events_RegisterOnClientSteamAuthorizeCallback(void* callback)
{
    PROFILE_FUNCTION();

    g_pOnClientSteamAuthorizeCallback = reinterpret_cast<decltype(g_pOnClientSteamAuthorizeCallback)>(callback);
}

void Bridge_Events_RegisterOnClientSteamAuthorizeFailCallback(void* callback)
{
    PROFILE_FUNCTION();

    g_pOnClientSteamAuthorizeFailCallback = reinterpret_cast<decltype(g_pOnClientSteamAuthorizeFailCallback)>(callback);
}

void Bridge_Events_RegisterOnEntityCreatedCallback(void* callback)
{
    PROFILE_FUNCTION();

    g_pOnEntityCreatedCallback = reinterpret_cast<decltype(g_pOnEntityCreatedCallback)>(callback);
}

void Bridge_Events_RegisterOnEntityDeletedCallback(void* callback)
{
    PROFILE_FUNCTION();

    g_pOnEntityDeletedCallback = reinterpret_cast<decltype(g_pOnEntityDeletedCallback)>(callback);
}

void Bridge_Events_RegisterOnEntityParentChangedCallback(void* callback)
{
    PROFILE_FUNCTION();

    g_pOnEntityParentChangedCallback = reinterpret_cast<decltype(g_pOnEntityParentChangedCallback)>(callback);
}

void Bridge_Events_RegisterOnEntitySpawnedCallback(void* callback)
{
    PROFILE_FUNCTION();

    g_pOnEntitySpawnedCallback = reinterpret_cast<decltype(g_pOnEntitySpawnedCallback)>(callback);
}

void Bridge_Events_RegisterOnMapLoadCallback(void* callback)
{
    PROFILE_FUNCTION();

    g_pOnMapLoadCallback = reinterpret_cast<decltype(g_pOnMapLoadCallback)>(callback);
}

void Bridge_Events_RegisterOnMapUnloadCallback(void* callback)
{
    PROFILE_FUNCTION();

    g_pOnMapUnloadCallback = reinterpret_cast<decltype(g_pOnMapUnloadCallback)>(callback);
}

void Bridge_Events_RegisterOnPrecacheResourceCallback(void* callback)
{
    PROFILE_FUNCTION();

    g_pOnPrecacheResourceCallback = reinterpret_cast<decltype(g_pOnPrecacheResourceCallback)>(callback);
}

void Bridge_Events_RegisterOnPreworldUpdateCallback(void* callback)
{
    PROFILE_FUNCTION();

    g_pOnPreworldUpdateCallback = reinterpret_cast<decltype(g_pOnPreworldUpdateCallback)>(callback);
}

void Bridge_Events_RegisterOnStartupServerCallback(void* callback)
{
    PROFILE_FUNCTION();

    g_pOnStartupServerCallback = reinterpret_cast<decltype(g_pOnStartupServerCallback)>(callback);
}

DEFINE_NATIVE("Events.RegisterOnGameTickCallback", Bridge_Events_RegisterOnGameTickCallback);
DEFINE_NATIVE("Events.RegisterOnClientConnectCallback", Bridge_Events_RegisterOnClientConnectCallback);
DEFINE_NATIVE("Events.RegisterOnClientDisconnectCallback", Bridge_Events_RegisterOnClientDisconnectCallback);
DEFINE_NATIVE("Events.RegisterOnClientKeyStateChangedCallback", Bridge_Events_RegisterOnClientKeyStateChangedCallback);
DEFINE_NATIVE("Events.RegisterOnClientPutInServerCallback", Bridge_Events_RegisterOnClientPutInServerCallback);
DEFINE_NATIVE("Events.RegisterOnClientSteamAuthorizeCallback", Bridge_Events_RegisterOnClientSteamAuthorizeCallback);
DEFINE_NATIVE("Events.RegisterOnClientSteamAuthorizeFailCallback", Bridge_Events_RegisterOnClientSteamAuthorizeFailCallback);
DEFINE_NATIVE("Events.RegisterOnEntityCreatedCallback", Bridge_Events_RegisterOnEntityCreatedCallback);
DEFINE_NATIVE("Events.RegisterOnEntityDeletedCallback", Bridge_Events_RegisterOnEntityDeletedCallback);
DEFINE_NATIVE("Events.RegisterOnEntityParentChangedCallback", Bridge_Events_RegisterOnEntityParentChangedCallback);
DEFINE_NATIVE("Events.RegisterOnEntitySpawnedCallback", Bridge_Events_RegisterOnEntitySpawnedCallback);
DEFINE_NATIVE("Events.RegisterOnMapLoadCallback", Bridge_Events_RegisterOnMapLoadCallback);
DEFINE_NATIVE("Events.RegisterOnMapUnloadCallback", Bridge_Events_RegisterOnMapUnloadCallback);
DEFINE_NATIVE("Events.RegisterOnPrecacheResourceCallback", Bridge_Events_RegisterOnPrecacheResourceCallback);
DEFINE_NATIVE("Events.RegisterOnPreworldUpdateCallback", Bridge_Events_RegisterOnPreworldUpdateCallback);
DEFINE_NATIVE("Events.RegisterOnStartupServerCallback", Bridge_Events_RegisterOnStartupServerCallback);
