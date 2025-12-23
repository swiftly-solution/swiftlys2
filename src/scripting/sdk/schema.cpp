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

#include <api/interfaces/manager.h>
#include <scripting/scripting.h>

void Bridge_SDK_Schema_SetStateChanged(void* pEntity, uint64_t uHash)
{
    static auto schema = g_ifaceService.FetchInterface<ISDKSchema>(SDKSCHEMA_INTERFACE_VERSION);
    schema->SetStateChanged(pEntity, uHash);
}

uint32_t Bridge_SDK_Schema_FindChainOffset(const char* sClassName)
{
    static auto schema = g_ifaceService.FetchInterface<ISDKSchema>(SDKSCHEMA_INTERFACE_VERSION);
    return schema->FindChainOffset(sClassName);
}

int32_t Bridge_SDK_Schema_GetOffset(uint64_t uHash)
{
    static auto schema = g_ifaceService.FetchInterface<ISDKSchema>(SDKSCHEMA_INTERFACE_VERSION);
    return schema->GetOffset(uHash);
}

bool Bridge_SDK_Schema_IsStruct(const char* sClassName)
{
    static auto schema = g_ifaceService.FetchInterface<ISDKSchema>(SDKSCHEMA_INTERFACE_VERSION);
    return schema->IsStruct(sClassName);
}

bool Bridge_SDK_Schema_IsClassLoaded(const char* sClassName)
{
    static auto schema = g_ifaceService.FetchInterface<ISDKSchema>(SDKSCHEMA_INTERFACE_VERSION);
    return schema->IsClassLoaded(sClassName);
}

void* Bridge_SDK_Schema_GetPropPtr(void* pEntity, uint64_t uHash)
{
    static auto schema = g_ifaceService.FetchInterface<ISDKSchema>(SDKSCHEMA_INTERFACE_VERSION);
    if (!schema)
        return nullptr;

    return schema->GetPropPtr(pEntity, uHash);
}

void Bridge_SDK_Schema_WritePropPtr(void* pEntity, uint64_t uHash, void* pValue, uint32_t size)
{
    static auto schema = g_ifaceService.FetchInterface<ISDKSchema>(SDKSCHEMA_INTERFACE_VERSION);
    schema->WritePropPtr(pEntity, uHash, pValue, size);
}

void* Bridge_SDK_Schema_GetVData(void* pEntity)
{
    static auto schema = g_ifaceService.FetchInterface<ISDKSchema>(SDKSCHEMA_INTERFACE_VERSION);
    return schema->GetVData(pEntity);
}

DEFINE_NATIVE("Schema.SetStateChanged", Bridge_SDK_Schema_SetStateChanged);
DEFINE_NATIVE("Schema.FindChainOffset", Bridge_SDK_Schema_FindChainOffset);
DEFINE_NATIVE("Schema.GetOffset", Bridge_SDK_Schema_GetOffset);
DEFINE_NATIVE("Schema.IsStruct", Bridge_SDK_Schema_IsStruct);
DEFINE_NATIVE("Schema.IsClassLoaded", Bridge_SDK_Schema_IsClassLoaded);
DEFINE_NATIVE("Schema.GetPropPtr", Bridge_SDK_Schema_GetPropPtr);
DEFINE_NATIVE("Schema.WritePropPtr", Bridge_SDK_Schema_WritePropPtr);
DEFINE_NATIVE("Schema.GetVData", Bridge_SDK_Schema_GetVData);