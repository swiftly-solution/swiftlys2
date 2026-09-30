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

#include <extension/impl/core/core_impl_00001.h>

Extension::Extension(const std::string& path):
    initialized_(false),
    path_(path),
    name_("<unknown>"),
    version_("<unknown>"),
    author_("Anonymous"),
    description_("None"),
    core_(nullptr),
    hot_reloaded_(false)
{
}

void Extension::Init(
    const char* name,
    const char* version,
    const char* author,
    const char* description
)
{
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
    return initialized_;
}

void* Extension::GetCore(const char* core_name)
{
    std::string core(core_name);
    if (core != SW_IFACE_CORE_00001)
        return nullptr;

    if (!core_)
        core_ = new SwCoreImpl00001(this);

    return core_;
}

void Extension::SetHotReloaded(bool hotreloaded)
{
    hot_reloaded_ = hotreloaded;
}

bool Extension::IsHotReloaded()
{
    return hot_reloaded_;
}
