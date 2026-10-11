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

#include <algorithm>
#include <filesystem>

#include <fmt/format.h>

#include <api/shared/plat.h>
#include <api/interfaces/interfaces.h>
#include <core/managed/host/dynlib.h>
#include "extension.h"
#include "init.h"

namespace
{

struct ExtensionUpdateScope
{
    bool& updating;

    explicit ExtensionUpdateScope(bool& value): updating(value)
    {
        updating = true;
    }

    ~ExtensionUpdateScope()
    {
        updating = false;
    }
};

bool IsSameExtensionPath(const std::string& loaded_path, const std::filesystem::path& path)
{
    std::error_code error;
    return loaded_path == path.string() || std::filesystem::equivalent(loaded_path, path, error);
}

}

CExtensionHost::~CExtensionHost()
{
    UnloadExtensions();
}

void CExtensionHost::UnloadExtensions()
{
    if (updating_extensions_)
        return;
    ExtensionUpdateScope update(updating_extensions_);

    while (!extensions_.empty())
    {
        auto ext = std::move(extensions_.back());
        extensions_.pop_back();
        ext.reset();
        NotifyAllExtensionsLoaded();
    }
}

void CExtensionHost::LoadExtensions(std::string origin_path)
{
    if (updating_extensions_)
        return;
    ExtensionUpdateScope update(updating_extensions_);

    std::filesystem::path path{origin_path};
    std::filesystem::path ext_folder = path / "extensions";
    extensions_folder_ = ext_folder.string();
    if (!std::filesystem::is_directory(ext_folder))
    {
        NotifyAllExtensionsLoaded();
        return;
    }

    for (const auto& entry : std::filesystem::directory_iterator(ext_folder))
    {
        if (!entry.is_directory())
        {
            continue;
        }

        const auto id = entry.path().filename().string();
        const auto module_path = entry.path() / (id + WIN_LINUX(".dll", ".so"));
        if (!std::filesystem::is_regular_file(module_path))
        {
            continue;
        }

        LoadExtensionFromPath(id, module_path.string(), false);
    }
    NotifyAllExtensionsLoaded();
}

bool CExtensionHost::LoadExtension(const std::string& id)
{
    if (updating_extensions_ || id.empty() || extensions_folder_.empty())
    {
        return false;
    }

    ExtensionUpdateScope update(updating_extensions_);
    const auto module_path = std::filesystem::path(extensions_folder_) / id / (id + WIN_LINUX(".dll", ".so"));
    if (!LoadExtensionFromPath(id, module_path.string(), true))
        return false;

    NotifyAllExtensionsLoaded();
    return true;
}

bool CExtensionHost::LoadExtensionFromPath(const std::string& path)
{
    const std::filesystem::path module_path{path};
    if (updating_extensions_ || !module_path.is_absolute())
        return false;

    const auto id = module_path.stem().string();
    if (id.empty())
        return false;

    ExtensionUpdateScope update(updating_extensions_);
    if (!LoadExtensionFromPath(id, path, true))
        return false;

    NotifyAllExtensionsLoaded();
    return true;
}

bool CExtensionHost::LoadExtensionFromPath(const std::string& id, const std::string& path, bool hotreloaded)
{
    std::error_code error;
    const auto module_path = std::filesystem::canonical(path, error);
    if (error || !std::filesystem::is_regular_file(module_path, error))
    {
        g_pLogger->Error("Extension", fmt::format("Extension path does not exist: {}\n", path));
        return false;
    }

    const auto normalized_path = module_path.string();
    const auto it = std::find_if(extensions_.begin(), extensions_.end(), [&](const auto& ext) {
        return ext->GetId() == id || IsSameExtensionPath(ext->GetPath(), module_path);
    });
    if (it != extensions_.end())
    {
        g_pLogger->Warning("Extension", fmt::format("Extension is already loaded: {}\n", id));
        return false;
    }

    void* lib = load_library(module_path.c_str());
    if (!lib)
    {
        g_pLogger->Error("Extension", fmt::format("Failed to load extension: {}\n", path));
        return false;
    }

    sw_extension_init_fn init_fn = (sw_extension_init_fn)get_export(lib, "sw_extension_init");
    if (!init_fn)
    {
        g_pLogger->Error("Extension", fmt::format("Extension missing entry function sw_extension_init: {}\n", path));
        unload_library(lib);
        return false;
    }

    auto ext = std::make_unique<Extension>(id, normalized_path, lib);
    ext->SetHotReloaded(hotreloaded);

    auto ctx = CreateContext(ext.get());

    int32_t status = init_fn(&ctx.api);
    ext->FinishInitialization();
    if (status != SW_OK)
    {
        g_pLogger->Error("Extension", fmt::format("Extension initialization failed with status {}: {}\n", status, path));
        return false;
    }

    if (!ext->IsInitialized())
    {
        g_pLogger->Warning("Extension", fmt::format("Extension is not properly initialized: {}\n", path));
    }

    extensions_.push_back(std::move(ext));
    return true;
}

bool CExtensionHost::UnloadExtension(const std::string& id)
{
    if (updating_extensions_ || id.empty())
        return false;

    const auto it = std::find_if(extensions_.begin(), extensions_.end(), [&](const auto& ext) { return ext->GetId() == id; });
    if (it == extensions_.end())
    {
        g_pLogger->Warning("Extension", fmt::format("Extension is not loaded: {}\n", id));
        return false;
    }

    ExtensionUpdateScope update(updating_extensions_);
    auto ext = std::move(*it);
    extensions_.erase(it);
    ext.reset();
    NotifyAllExtensionsLoaded();
    return true;
}

bool CExtensionHost::UnloadExtensionFromPath(const std::string& path)
{
    const std::filesystem::path module_path{path};
    if (updating_extensions_ || !module_path.is_absolute())
        return false;

    std::error_code error;
    const auto normalized_path = std::filesystem::weakly_canonical(module_path, error);
    if (error)
        return false;

    const auto it = std::find_if(extensions_.begin(), extensions_.end(), [&](const auto& ext) { return IsSameExtensionPath(ext->GetPath(), normalized_path); });
    if (it == extensions_.end())
        return false;

    const auto id = (*it)->GetId();
    return UnloadExtension(id);
}

void CExtensionHost::NotifyAllExtensionsLoaded()
{
    for (const auto& ext : extensions_)
        ext->OnAllExtensionsLoaded();
}

std::vector<ExtensionInfo> CExtensionHost::GetExtensions()
{
    std::vector<ExtensionInfo> result;
    result.reserve(extensions_.size());
    for (const auto& ext : extensions_)
    {
        result.push_back({
            ext->GetId(),
            ext->GetName(),
            ext->GetVersion(),
            ext->GetAuthor(),
            ext->GetDescription(),
            ext->GetPath(),
            ext->IsInitialized()
        });
    }
    return result;
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
