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

#include "schema_impl_00001.h"

#include <api/interfaces/interfaces.h>
#include <api/shared/hash.h>

namespace
{

sw_status get_schema_offset(
    const sw_schema_00001* self,
    const char* class_name,
    const char* field_name,
    uint32_t* offset_out
)
{
    if (!self || !class_name || !field_name || !offset_out)
        return SW_E_INVALID_ARG;

    int32_t offset = g_pSDKSchema->GetOffset(class_name, field_name);
    if (offset == -1)
        return SW_E_INVALID_ARG;

    *offset_out = static_cast<uint32_t>(offset);
    return SW_OK;
}

sw_status set_state_changed(
    const sw_schema_00001* self,
    void* object,
    const char* class_name,
    const char* field_name
)
{
    if (!self || !object || !class_name || !field_name)
        return SW_E_INVALID_ARG;

    uint64_t field_hash = (static_cast<uint64_t>(hash_32_fnv1a_const(class_name)) << 32) | hash_32_fnv1a_const(field_name);
    if (g_pSDKSchema->GetOffset(field_hash) == -1)
        return SW_E_INVALID_ARG;

    g_pSDKSchema->SetStateChanged(object, field_hash);
    return SW_OK;
}

}

SwSchemaImpl00001 g_SwSchemaImpl00001{
    .api = {
        .get_schema_offset = get_schema_offset,
        .set_state_changed = set_state_changed
    }
};
