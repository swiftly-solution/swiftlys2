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

#include <unordered_map>

#include <api/extension/sw_extension.h>
#include "impl/memory/memory_impl.h"

static const std::unordered_map<std::string, void*> ifaces = {
    {"SwMemory00001", &g_SwMemoryImpl}
};

HostInitContext* from_self(const sw_init_context* self)
{
    return reinterpret_cast<HostInitContext*>(const_cast<sw_init_context*>(self));
}

sw_status init(
    const sw_init_context* self,
    const char* name,
    const char* version,
    const char* author,
    const char* description)
{
    if (!name || !version)
    {
        return SW_EINVALID_ARG;
    }
    from_self(self)->data->Init(name, version, author, description);
    return SW_OK;
}

sw_status query_interface(
    const sw_init_context* self,
    const char* iface, 
    void** iface_out)
{
    auto it = ifaces.find(iface);
    if (it == ifaces.end())
    {
        *iface_out = 0;
        return SW_EINVALID_ARG;
    }
    
    *iface_out = it->second;
    
    return SW_OK;
}

HostInitContext CreateContext(Extension* data)
{
    HostInitContext ctx{
        .api = {
            init,
            query_interface
        },
        .data = data
    };
    return ctx;
}