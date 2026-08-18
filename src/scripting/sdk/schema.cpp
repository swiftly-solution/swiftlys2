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

#include <api/interfaces/interfaces.h>
#include <scripting/scripting.h>

void Bridge_SDK_Schema_SetStateChanged(void* pEntity, uint64_t uHash)
{
    g_pSDKSchema->SetStateChanged(pEntity, uHash);
}

int32_t Bridge_SDK_Schema_GetOffset(uint64_t uHash)
{
    return g_pSDKSchema->GetOffset(uHash);
}

void* Bridge_SDK_Schema_GetVData(void* pEntity)
{
    return g_pSDKSchema->GetVData(pEntity);
}

void* Bridge_SDK_Schema_GetDatamapFunction(uint32_t uHash)
{
    return g_pSDKSchema->GetDatamapFunction(uHash);
}

DEFINE_NATIVE("Schema.SetStateChanged", Bridge_SDK_Schema_SetStateChanged);
DEFINE_NATIVE("Schema.GetOffset", Bridge_SDK_Schema_GetOffset);
DEFINE_NATIVE("Schema.GetVData", Bridge_SDK_Schema_GetVData);
DEFINE_NATIVE("Schema.GetDatamapFunction", Bridge_SDK_Schema_GetDatamapFunction);
