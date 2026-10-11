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

#include "core_impl_00001.h"
#include <extension/extension.h>
#include <extension/impl/memory/memory_impl_00001.h>
#include <extension/impl/schema/schema_impl_00001.h>
#include <extension/impl/shared/shared_impl_00001.h>

SwCoreImpl00001::SwCoreImpl00001(Extension* extension):
    api_{
        .init = &init_.api_,
        .memory = &g_SwMemoryImpl00001.api,
        .schema = &g_SwSchemaImpl00001.api,
        .shared = &g_SwSharedImpl00001.api
    },
    init_(extension)
{
}
