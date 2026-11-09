/************************************************************************************************
 *  SwiftlyS2 is a scripting framework for Source2-based games.
 *  Copyright (C) 2023-2026 Swiftly Solution SRL via Sava Andrei-Sebastian and it's contributors (samyycX)
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
#include <api/shared/profiler.h>

void* Bridge_Memory_Alloc(uint64_t size)
{
    PROFILE_FUNCTION();

    return g_pMemoryAllocator->Alloc(size);
}

void Bridge_Memory_Free(void* ptr)
{
    PROFILE_FUNCTION();

    g_pMemoryAllocator->Free(ptr);
}

void* Bridge_Memory_Resize(void* ptr, uint64_t newSize)
{
    PROFILE_FUNCTION();

    return g_pMemoryAllocator->Resize(ptr, newSize);
}

void Bridge_Memory_Copy(void* dest, void* src, uint64_t size)
{
    PROFILE_FUNCTION();

    g_pMemoryAllocator->Copy(dest, src, size);
}

void Bridge_Memory_Move(void* dest, void* src, uint64_t size)
{
    PROFILE_FUNCTION();

    g_pMemoryAllocator->Move(dest, src, size);
}

DEFINE_NATIVE("Allocator.Alloc", Bridge_Memory_Alloc);
DEFINE_NATIVE("Allocator.Free", Bridge_Memory_Free);
DEFINE_NATIVE("Allocator.Resize", Bridge_Memory_Resize);
DEFINE_NATIVE("Allocator.Copy", Bridge_Memory_Copy);
DEFINE_NATIVE("Allocator.Move", Bridge_Memory_Move);