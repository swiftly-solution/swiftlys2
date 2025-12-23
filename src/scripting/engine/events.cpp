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
#include <api/interfaces/manager.h>

void* g_pOnGameTickCallback = nullptr;
void* g_pOnClientConnectCallback = nullptr;
void* g_pOnClientDisconnectCallback = nullptr;
void* g_pOnClientKeyStateChangedCallback = nullptr;
void* g_pOnClientProcessUsercmdsCallback = nullptr;
void* g_pOnClientPutInServerCallback = nullptr;
void* g_pOnClientSteamAuthorizeCallback = nullptr;
void* g_pOnClientSteamAuthorizeFailCallback = nullptr;
void* g_pOnEntityCreatedCallback = nullptr;
void* g_pOnEntityDeletedCallback = nullptr;
void* g_pOnEntityParentChangedCallback = nullptr;
void* g_pOnEntitySpawnedCallback = nullptr;
void* g_pOnMapLoadCallback = nullptr;
void* g_pOnMapUnloadCallback = nullptr;
void* g_pOnEntityTakeDamageCallback = nullptr;
void* g_pOnPrecacheResourceCallback = nullptr;
void* g_pOnPreworldUpdateCallback = nullptr;
void* g_pOnStartupServerCallback = nullptr;

void Bridge_Events_RegisterOnGameTickCallback(void* callback)
{
    g_pOnGameTickCallback = callback;
}

void Bridge_Events_RegisterOnClientConnectCallback(void* callback)
{
    g_pOnClientConnectCallback = callback;
}

void Bridge_Events_RegisterOnClientDisconnectCallback(void* callback)
{
    g_pOnClientDisconnectCallback = callback;
}

void Bridge_Events_RegisterOnClientKeyStateChangedCallback(void* callback)
{
    g_pOnClientKeyStateChangedCallback = callback;
}

void Bridge_Events_RegisterOnClientProcessUsercmdsCallback(void* callback)
{
    g_pOnClientProcessUsercmdsCallback = callback;
}

void Bridge_Events_RegisterOnClientPutInServerCallback(void* callback)
{
    g_pOnClientPutInServerCallback = callback;
}

void Bridge_Events_RegisterOnClientSteamAuthorizeCallback(void* callback)
{
    g_pOnClientSteamAuthorizeCallback = callback;
}

void Bridge_Events_RegisterOnClientSteamAuthorizeFailCallback(void* callback)
{
    g_pOnClientSteamAuthorizeFailCallback = callback;
}

void Bridge_Events_RegisterOnEntityCreatedCallback(void* callback)
{
    g_pOnEntityCreatedCallback = callback;
}

void Bridge_Events_RegisterOnEntityDeletedCallback(void* callback)
{
    g_pOnEntityDeletedCallback = callback;
}

void Bridge_Events_RegisterOnEntityParentChangedCallback(void* callback)
{
    g_pOnEntityParentChangedCallback = callback;
}

void Bridge_Events_RegisterOnEntitySpawnedCallback(void* callback)
{
    g_pOnEntitySpawnedCallback = callback;
}

void Bridge_Events_RegisterOnMapLoadCallback(void* callback)
{
    g_pOnMapLoadCallback = callback;
}

void Bridge_Events_RegisterOnMapUnloadCallback(void* callback)
{
    g_pOnMapUnloadCallback = callback;
}

void Bridge_Events_RegisterOnEntityTakeDamageCallback(void* callback)
{
    g_pOnEntityTakeDamageCallback = callback;
}

void Bridge_Events_RegisterOnPrecacheResourceCallback(void* callback)
{
    g_pOnPrecacheResourceCallback = callback;
}

void Bridge_Events_RegisterOnPreworldUpdateCallback(void* callback)
{
    g_pOnPreworldUpdateCallback = callback;
}

void Bridge_Events_RegisterOnStartupServerCallback(void* callback)
{
    g_pOnStartupServerCallback = callback;
}

DEFINE_NATIVE("Events.RegisterOnGameTickCallback", Bridge_Events_RegisterOnGameTickCallback);
DEFINE_NATIVE("Events.RegisterOnClientConnectCallback", Bridge_Events_RegisterOnClientConnectCallback);
DEFINE_NATIVE("Events.RegisterOnClientDisconnectCallback", Bridge_Events_RegisterOnClientDisconnectCallback);
DEFINE_NATIVE("Events.RegisterOnClientKeyStateChangedCallback", Bridge_Events_RegisterOnClientKeyStateChangedCallback);
DEFINE_NATIVE("Events.RegisterOnClientProcessUsercmdsCallback", Bridge_Events_RegisterOnClientProcessUsercmdsCallback);
DEFINE_NATIVE("Events.RegisterOnClientPutInServerCallback", Bridge_Events_RegisterOnClientPutInServerCallback);
DEFINE_NATIVE("Events.RegisterOnClientSteamAuthorizeCallback", Bridge_Events_RegisterOnClientSteamAuthorizeCallback);
DEFINE_NATIVE("Events.RegisterOnClientSteamAuthorizeFailCallback", Bridge_Events_RegisterOnClientSteamAuthorizeFailCallback);
DEFINE_NATIVE("Events.RegisterOnEntityCreatedCallback", Bridge_Events_RegisterOnEntityCreatedCallback);
DEFINE_NATIVE("Events.RegisterOnEntityDeletedCallback", Bridge_Events_RegisterOnEntityDeletedCallback);
DEFINE_NATIVE("Events.RegisterOnEntityParentChangedCallback", Bridge_Events_RegisterOnEntityParentChangedCallback);
DEFINE_NATIVE("Events.RegisterOnEntitySpawnedCallback", Bridge_Events_RegisterOnEntitySpawnedCallback);
DEFINE_NATIVE("Events.RegisterOnMapLoadCallback", Bridge_Events_RegisterOnMapLoadCallback);
DEFINE_NATIVE("Events.RegisterOnMapUnloadCallback", Bridge_Events_RegisterOnMapUnloadCallback);
DEFINE_NATIVE("Events.RegisterOnEntityTakeDamageCallback", Bridge_Events_RegisterOnEntityTakeDamageCallback);
DEFINE_NATIVE("Events.RegisterOnPrecacheResourceCallback", Bridge_Events_RegisterOnPrecacheResourceCallback);
DEFINE_NATIVE("Events.RegisterOnPreworldUpdateCallback", Bridge_Events_RegisterOnPreworldUpdateCallback);
DEFINE_NATIVE("Events.RegisterOnStartupServerCallback", Bridge_Events_RegisterOnStartupServerCallback);