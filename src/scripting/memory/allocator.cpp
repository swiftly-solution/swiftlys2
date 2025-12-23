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
#include <api/interfaces/manager.h>

void* Bridge_Memory_Alloc(uint64_t size)
{
    auto memalloc = g_ifaceService.FetchInterface<IMemoryAllocator>(MEMORYALLOCATOR_INTERFACE_VERSION);
    return memalloc->Alloc(size);
}

void* Bridge_Memory_TrackedAlloc(uint64_t size, const char* identifier, const char* details)
{
    auto memalloc = g_ifaceService.FetchInterface<IMemoryAllocator>(MEMORYALLOCATOR_INTERFACE_VERSION);
    return memalloc->TrackedAlloc(size, identifier, details);
}

void Bridge_Memory_Free(void* ptr)
{
    auto memalloc = g_ifaceService.FetchInterface<IMemoryAllocator>(MEMORYALLOCATOR_INTERFACE_VERSION);
    memalloc->Free(ptr);
}

void* Bridge_Memory_Resize(void* ptr, uint64_t newSize)
{
    auto memalloc = g_ifaceService.FetchInterface<IMemoryAllocator>(MEMORYALLOCATOR_INTERFACE_VERSION);
    return memalloc->Resize(ptr, newSize);
}

uint64_t Bridge_Memory_GetSize(void* ptr)
{
    auto memalloc = g_ifaceService.FetchInterface<IMemoryAllocator>(MEMORYALLOCATOR_INTERFACE_VERSION);
    return memalloc->GetSize(ptr);
}

uint64_t Bridge_Memory_GetTotalAllocated()
{
    auto memalloc = g_ifaceService.FetchInterface<IMemoryAllocator>(MEMORYALLOCATOR_INTERFACE_VERSION);
    return memalloc->GetTotalAllocated();
}

uint64_t Bridge_Memory_GetAllocatedByTrackedIdentifier(const char* identifier)
{
    auto memalloc = g_ifaceService.FetchInterface<IMemoryAllocator>(MEMORYALLOCATOR_INTERFACE_VERSION);
    return memalloc->GetAllocatedByTrackedIdentifier(identifier);
}

bool Bridge_Memory_IsPointerValid(void* ptr)
{
    auto memalloc = g_ifaceService.FetchInterface<IMemoryAllocator>(MEMORYALLOCATOR_INTERFACE_VERSION);
    return memalloc->IsPointerValid(ptr);
}

void Bridge_Memory_Copy(void* dest, void* src, uint64_t size)
{
    auto memalloc = g_ifaceService.FetchInterface<IMemoryAllocator>(MEMORYALLOCATOR_INTERFACE_VERSION);
    memalloc->Copy(dest, src, size);
}

void Bridge_Memory_Move(void* dest, void* src, uint64_t size)
{
    auto memalloc = g_ifaceService.FetchInterface<IMemoryAllocator>(MEMORYALLOCATOR_INTERFACE_VERSION);
    memalloc->Move(dest, src, size);
}

DEFINE_NATIVE("Allocator.Alloc", Bridge_Memory_Alloc);
DEFINE_NATIVE("Allocator.TrackedAlloc", Bridge_Memory_TrackedAlloc);
DEFINE_NATIVE("Allocator.Free", Bridge_Memory_Free);
DEFINE_NATIVE("Allocator.Resize", Bridge_Memory_Resize);
DEFINE_NATIVE("Allocator.GetSize", Bridge_Memory_GetSize);
DEFINE_NATIVE("Allocator.GetTotalAllocated", Bridge_Memory_GetTotalAllocated);
DEFINE_NATIVE("Allocator.GetAllocatedByTrackedIdentifier", Bridge_Memory_GetAllocatedByTrackedIdentifier);
DEFINE_NATIVE("Allocator.IsPointerValid", Bridge_Memory_IsPointerValid);
DEFINE_NATIVE("Allocator.Copy", Bridge_Memory_Copy);
DEFINE_NATIVE("Allocator.Move", Bridge_Memory_Move);