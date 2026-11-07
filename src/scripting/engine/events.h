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

#ifndef _src_scripting_engine_events_h
#define _src_scripting_engine_events_h

#include <cstdint>

#define DEFINE_EVENT(variable, return_type, ...) return_type(*variable)(__VA_ARGS__)

extern DEFINE_EVENT(g_pOnGameTickCallback, void, bool, bool, bool);
extern DEFINE_EVENT(g_pOnClientConnectCallback, bool, int);
extern DEFINE_EVENT(g_pOnClientDisconnectCallback, void, int, int);
extern DEFINE_EVENT(g_pOnClientKeyStateChangedCallback, void, int, uint32_t, bool);
extern DEFINE_EVENT(g_pOnClientPutInServerCallback, void, int, int);
extern DEFINE_EVENT(g_pOnClientSteamAuthorizeCallback, void, int);
extern DEFINE_EVENT(g_pOnClientSteamAuthorizeFailCallback, void, int);
extern DEFINE_EVENT(g_pOnEntityCreatedCallback, void, void*);
extern DEFINE_EVENT(g_pOnEntityDeletedCallback, void, void*);
extern DEFINE_EVENT(g_pOnEntityParentChangedCallback, void, void*, void*);
extern DEFINE_EVENT(g_pOnEntitySpawnedCallback, void, void*);
extern DEFINE_EVENT(g_pOnMapLoadCallback, void, const char*);
extern DEFINE_EVENT(g_pOnMapUnloadCallback, void, const char*);
extern DEFINE_EVENT(g_pOnPrecacheResourceCallback, void, void*);
extern DEFINE_EVENT(g_pOnPreworldUpdateCallback, void, bool);
extern DEFINE_EVENT(g_pOnStartupServerCallback, void);

#endif
