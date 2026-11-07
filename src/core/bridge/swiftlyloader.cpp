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

#include <core/entrypoint.h>

#include <api/dll/extern.h>
#include <api/shared/profiler.h>
#include <s2binlib/s2binlib.h>

S2BinLib004* g_pS2BinLib;
std::string g_sGameFolder;

SW_API bool StartCore(CreateIFaceFn serverFactory, CreateIFaceFn engineFactory, S2BinLib004* s2BinLib, const char* gameFolder)
{
    PROFILE_FUNCTION();

    g_sGameFolder = gameFolder;
    g_pS2BinLib = s2BinLib;
    return g_SwiftlyCore.Load(BridgeKind_t::SwiftlyLoader, serverFactory, engineFactory);
}

SW_API bool StopCore()
{
    PROFILE_FUNCTION();

    return g_SwiftlyCore.Unload();
}