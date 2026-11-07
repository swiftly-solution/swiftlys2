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

#include "extension.h"
#include <api/extension/sw_extension.h>
#include <api/shared/profiler.h>

#include <extension/impl/core/core_impl_00001.h>
#include <core/managed/host/dynlib.h>

Extension::Extension(const std::string& id, const std::string& path, void* library):
    initialized_(false),
    id_(id),
    path_(path),
    name_("<unknown>"),
    version_("<unknown>"),
    author_("Anonymous"),
    description_("None"),
    library_(library),
    core_(nullptr),
    hot_reloaded_(false),
    initializing_(true),
    unload_callback_(nullptr),
    unload_user_data_(nullptr),
    on_all_extensions_loaded_callback_(nullptr),
    on_all_extensions_loaded_user_data_(nullptr)
{
    PROFILE_FUNCTION();
}

Extension::~Extension()
{
    PROFILE_FUNCTION();

    FinishInitialization();
    if (unload_callback_)
        unload_callback_(unload_user_data_);
    delete static_cast<SwCoreImpl00001*>(core_);
    unload_library(library_);
}

void Extension::Init(
    const char* name,
    const char* version,
    const char* author,
    const char* description
)
{
    PROFILE_FUNCTION();

    if (name && version)
    {
        initialized_ = true;
    }
    if (name)
        name_ = name;
    if (version)
        version_ = version;
    if (author)
        author_ = author;
    if (description)
        description_ = description;
}

bool Extension::IsInitialized()
{
    PROFILE_FUNCTION();

    return initialized_;
}

void* Extension::GetCore(const char* core_name)
{
    PROFILE_FUNCTION();

    std::string core(core_name);
    if (core != SW_IFACE_CORE_00001)
        return nullptr;

    if (!core_)
        core_ = new SwCoreImpl00001(this);

    return core_;
}

void Extension::SetHotReloaded(bool hotreloaded)
{
    PROFILE_FUNCTION();

    hot_reloaded_ = hotreloaded;
}

bool Extension::IsHotReloaded()
{
    PROFILE_FUNCTION();

    return hot_reloaded_;
}

void Extension::FinishInitialization()
{
    PROFILE_FUNCTION();

    initializing_ = false;
}

void Extension::SetUnloadCallback(sw_extension_callback_fn callback, void* user_data)
{
    PROFILE_FUNCTION();

    unload_callback_ = callback;
    unload_user_data_ = user_data;
}

void Extension::SetOnAllExtensionsLoadedCallback(sw_extension_callback_fn callback, void* user_data)
{
    PROFILE_FUNCTION();

    on_all_extensions_loaded_callback_ = callback;
    on_all_extensions_loaded_user_data_ = user_data;
}

void Extension::OnAllExtensionsLoaded()
{
    PROFILE_FUNCTION();

    if (on_all_extensions_loaded_callback_)
        on_all_extensions_loaded_callback_(on_all_extensions_loaded_user_data_);
}
