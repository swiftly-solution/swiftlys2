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

#include "init_impl_00001.h"
#include <extension/extension.h>

namespace
{

SwInitImpl00001* from_self(const sw_init_00001* self)
{
    return reinterpret_cast<SwInitImpl00001*>(const_cast<sw_init_00001*>(self));
}

sw_status set_info(
    const sw_init_00001* self,
    const char* name,
    const char* version,
    const char* author,
    const char* description)
{
    if (!name || !version)
    {
        return SW_E_INVALID_ARG;
    }
    from_self(self)->extension_->Init(name, version, author, description);
    return SW_OK;
}

sw_status is_hotreloaded(
    const sw_init_00001* self,
    int32_t* hotreloaded_out)
{
    *hotreloaded_out = from_self(self)->extension_->IsHotReloaded() ? 1 : 0;
    return SW_OK;
}

}

SwInitImpl00001::SwInitImpl00001(Extension* extension):
    api_{set_info, is_hotreloaded},
    extension_(extension)
{
}
