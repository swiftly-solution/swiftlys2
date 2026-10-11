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

#include "shared_impl_00001.h"

#include <api/interfaces/interfaces.h>

namespace
{

sw_status get_shared_pointer(
    const sw_shared_00001* self,
    const char* key,
    sw_ptr_out shared_pointer_out
)
{
    if (!self || !key || !shared_pointer_out)
        return SW_E_INVALID_ARG;

    if (!g_pExtensionHost->HasSharedPointer(key))
        return SW_E_INVALID_ARG;

    *shared_pointer_out = g_pExtensionHost->GetSharedPointer(key);
    return SW_OK;
}

sw_status set_shared_pointer(
    const sw_shared_00001* self,
    const char* key,
    void* shared_pointer
)
{
    if (!self || !key)
        return SW_E_INVALID_ARG;

    g_pExtensionHost->SetSharedPointer(key, shared_pointer);
    return SW_OK;
}

sw_status has_shared_pointer(
    const sw_shared_00001* self,
    const char* key,
    int32_t* result_out
)
{
    if (!self || !key || !result_out)
        return SW_E_INVALID_ARG;

    *result_out = g_pExtensionHost->HasSharedPointer(key) ? 1 : 0;
    return SW_OK;
}

sw_status remove_shared_pointer(
    const sw_shared_00001* self,
    const char* key
)
{
    if (!self || !key)
        return SW_E_INVALID_ARG;

    if (!g_pExtensionHost->HasSharedPointer(key))
        return SW_E_INVALID_ARG;
    g_pExtensionHost->RemoveSharedPointer(key);
    return SW_OK;
}

}

SwSharedImpl00001 g_SwSharedImpl00001{
    .api = {
        .get_shared_pointer = get_shared_pointer,
        .set_shared_pointer = set_shared_pointer,
        .has_shared_pointer = has_shared_pointer,
        .remove_shared_pointer = remove_shared_pointer
    }
};
