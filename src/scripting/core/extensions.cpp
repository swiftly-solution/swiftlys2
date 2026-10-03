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

#include <scripting/scripting.h>
#include <api/interfaces/interfaces.h>

#include <nlohmann/json.hpp>

static char* Bridge_Extensions_CopyString(const std::string& value, int* size)
{
    int outSize = static_cast<int>(value.size());
    *size = outSize;

    char* out = (char*)g_pMemoryAllocator->Alloc(outSize + 1);
    g_pMemoryAllocator->Copy(out, (void*)value.c_str(), outSize);
    out[outSize] = '\0';
    return out;
}

char* Bridge_Extensions_GetExtensions(int* size)
{
    nlohmann::json result = nlohmann::json::array();
    for (const auto& ext : g_pExtensionHost->GetExtensions())
    {
        result.push_back({
            {"id", ext.id},
            {"name", ext.name},
            {"version", ext.version},
            {"author", ext.author},
            {"description", ext.description},
            {"path", ext.path},
            {"initialized", ext.initialized}
        });
    }

    return Bridge_Extensions_CopyString(result.dump(-1, ' ', false, nlohmann::json::error_handler_t::replace), size);
}

bool Bridge_Extensions_Load(const char* id)
{
    return id && g_pExtensionHost->LoadExtension(id);
}

bool Bridge_Extensions_Unload(const char* id)
{
    return id && g_pExtensionHost->UnloadExtension(id);
}

bool Bridge_Extensions_LoadFromPath(const char* path)
{
    return path && g_pExtensionHost->LoadExtensionFromPath(path);
}

bool Bridge_Extensions_UnloadFromPath(const char* path)
{
    return path && g_pExtensionHost->UnloadExtensionFromPath(path);
}

void* Bridge_Extensions_GetSharedPointer(const char* key)
{
    return key ? g_pExtensionHost->GetSharedPointer(key) : nullptr;
}

void Bridge_Extensions_SetSharedPointer(const char* key, void* pointer)
{
    if (key)
        g_pExtensionHost->SetSharedPointer(key, pointer);
}

bool Bridge_Extensions_HasSharedPointer(const char* key)
{
    return key && g_pExtensionHost->HasSharedPointer(key);
}

void Bridge_Extensions_RemoveSharedPointer(const char* key)
{
    if (key)
        g_pExtensionHost->RemoveSharedPointer(key);
}

DEFINE_NATIVE("Extensions.GetExtensions", Bridge_Extensions_GetExtensions);
DEFINE_NATIVE("Extensions.Load", Bridge_Extensions_Load);
DEFINE_NATIVE("Extensions.Unload", Bridge_Extensions_Unload);
DEFINE_NATIVE("Extensions.LoadFromPath", Bridge_Extensions_LoadFromPath);
DEFINE_NATIVE("Extensions.UnloadFromPath", Bridge_Extensions_UnloadFromPath);
DEFINE_NATIVE("Extensions.GetSharedPointer", Bridge_Extensions_GetSharedPointer);
DEFINE_NATIVE("Extensions.SetSharedPointer", Bridge_Extensions_SetSharedPointer);
DEFINE_NATIVE("Extensions.HasSharedPointer", Bridge_Extensions_HasSharedPointer);
DEFINE_NATIVE("Extensions.RemoveSharedPointer", Bridge_Extensions_RemoveSharedPointer);
