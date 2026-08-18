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

void* Bridge_Memory_Alloc(uint64_t size)
{
    return g_pMemoryAllocator->Alloc(size);
}

void Bridge_Memory_Free(void* ptr)
{
    g_pMemoryAllocator->Free(ptr);
}

void* Bridge_Memory_Resize(void* ptr, uint64_t newSize)
{
    return g_pMemoryAllocator->Resize(ptr, newSize);
}

uint64_t Bridge_Memory_GetSize(void* ptr)
{
    return g_pMemoryAllocator->GetSize(ptr);
}

uint64_t Bridge_Memory_GetTotalAllocated()
{
    return g_pMemoryAllocator->GetTotalAllocated();
}

bool Bridge_Memory_IsPointerValid(void* ptr)
{
    return g_pMemoryAllocator->IsPointerValid(ptr);
}

void Bridge_Memory_Copy(void* dest, void* src, uint64_t size)
{
    g_pMemoryAllocator->Copy(dest, src, size);
}

void Bridge_Memory_Move(void* dest, void* src, uint64_t size)
{
    g_pMemoryAllocator->Move(dest, src, size);
}

DEFINE_NATIVE("Allocator.Alloc", Bridge_Memory_Alloc);
DEFINE_NATIVE("Allocator.Free", Bridge_Memory_Free);
DEFINE_NATIVE("Allocator.Resize", Bridge_Memory_Resize);
DEFINE_NATIVE("Allocator.GetSize", Bridge_Memory_GetSize);
DEFINE_NATIVE("Allocator.GetTotalAllocated", Bridge_Memory_GetTotalAllocated);
DEFINE_NATIVE("Allocator.IsPointerValid", Bridge_Memory_IsPointerValid);
DEFINE_NATIVE("Allocator.Copy", Bridge_Memory_Copy);
DEFINE_NATIVE("Allocator.Move", Bridge_Memory_Move);