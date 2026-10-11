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

#include "init.h"

#include <api/extension/sw_extension.h>

InitContextImpl* from_self(const sw_init_context* self)
{
    return reinterpret_cast<InitContextImpl*>(const_cast<sw_init_context*>(self));
}

sw_status core(
    const sw_init_context* self,
    const char* core,
    sw_core** iface_out)
{
    if (!self || !core || !iface_out)
        return SW_E_INVALID_ARG;

    *iface_out = reinterpret_cast<sw_core*>(from_self(self)->data->GetCore(core));

    if (!*iface_out)
        return SW_E_FAILED;

    return SW_OK;
}

InitContextImpl CreateContext(Extension* data)
{
    InitContextImpl ctx{
        .api = {
            core
        },
        .data = data
    };
    return ctx;
}
