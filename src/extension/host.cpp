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

#include "host.h"

#include <filesystem>

#include <fmt/format.h>

#include <api/shared/plat.h>
#include <api/interfaces/interfaces.h>
#include <core/managed/host/dynlib.h>
#include "extension.h"
#include "init.h"

void CExtensionHost::LoadExtensions(std::string origin_path)
{
    std::filesystem::path path{origin_path};
    std::filesystem::path ext_folder = path / "extensions";
    if (!std::filesystem::is_directory(ext_folder))
    {
        return;
    }

    for (const auto& entry : std::filesystem::directory_iterator(ext_folder))
    {
        if (!entry.is_directory())
        {
            continue;
        }

        const auto module_path = entry.path() / (entry.path().filename().string() + WIN_LINUX(".dll", ".so"));
        if (!std::filesystem::is_regular_file(module_path))
        {
            continue;
        }

        LoadExtension(module_path.string());
    }
}

void CExtensionHost::LoadExtension(const std::string& path)
{
    if (!std::filesystem::is_regular_file(path))
    {
        g_pLogger->Error("Extension", fmt::format("Extension path does not exist: {}\n", path));
        return;
    }

    void* lib = load_library(path.c_str());
    if (!lib)
    {
        g_pLogger->Error("Extension", fmt::format("Failed to load extension: {}\n", path));
        return;
    }

    sw_extension_init_fn init_fn = (sw_extension_init_fn)get_export(lib, "sw_extension_init");
    if (!init_fn)
    {
        g_pLogger->Error("Extension", fmt::format("Extension missing entry function sw_extension_init: {}\n", path));
        return;
    }

    Extension ext{path};
    
    auto ctx = CreateContext(&ext);

    init_fn(&ctx.api);

    if (!ext.IsInitialized())
    {
        g_pLogger->Warning("Extension", fmt::format("Extension is not properly initialized: {}\n", path));
    }

    extensions_.push_back(ext);
}

void* CExtensionHost::GetSharedPointer(const std::string& key)
{
    const auto it = shared_pointers_.find(key);
    return it != shared_pointers_.end() ? it->second : nullptr;
}

void CExtensionHost::SetSharedPointer(const std::string& key, void* value)
{
    shared_pointers_.insert_or_assign(key, value);
}

bool CExtensionHost::HasSharedPointer(const std::string& key)
{
    return shared_pointers_.find(key) != shared_pointers_.end();
}

void CExtensionHost::RemoveSharedPointer(const std::string& key)
{
    shared_pointers_.erase(key);
}