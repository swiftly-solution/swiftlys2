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

#include "filesystem.h"

#include <api/shared/plat.h>
#include <api/interfaces/manager.h>
#include <core/entrypoint.h>

#include <public/filesystem.h>
#include <fmt/format.h>

void FileSystemFix::Start()
{
    auto filesystem = g_ifaceService.FetchInterface<IFileSystem>(FILESYSTEM_INTERFACE_VERSION);

    std::string csgo_path = fmt::format("{}{}csgo", Plat_GetGameDirectory(), WIN_LINUX("\\", "/"));
    std::string swiftly_path = fmt::format("{}{}{}", csgo_path, WIN_LINUX("\\", "/"), g_SwiftlyCore.GetCorePath());

    filesystem->RemoveSearchPath(swiftly_path.c_str(), "GAME");
    filesystem->RemoveSearchPaths("DEFAULT_WRITE_PATH");
    filesystem->AddSearchPath(csgo_path.c_str(), "DEFAULT_WRITE_PATH", PATH_ADD_TO_TAIL, SEARCH_PATH_PRIORITY_DEFAULT, 0);
}