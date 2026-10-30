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

#include "allocator.h"
#include <api/shared/profiler.h>
#include <cstring>

#include <public/tier0/memdbgon.h>

void* MemoryAllocator::Alloc(uint64_t size)
{
    PROFILE_FUNCTION();

    return malloc(size);
}

void MemoryAllocator::Free(void* ptr)
{
    PROFILE_FUNCTION();

    free(ptr);
}

void* MemoryAllocator::Resize(void* ptr, uint64_t newSize)
{
    PROFILE_FUNCTION();

    return realloc(ptr, newSize);
}

uint64_t MemoryAllocator::GetSize(void* ptr)
{
    PROFILE_FUNCTION();

    return _msize(ptr);
}

uint64_t MemoryAllocator::GetTotalAllocated()
{
    PROFILE_FUNCTION();

    return totalAllocated;
}

bool MemoryAllocator::IsPointerValid(void* ptr)
{
    PROFILE_FUNCTION();

    return true;
}

void MemoryAllocator::Copy(void* dest, void* src, uint64_t size)
{
    PROFILE_FUNCTION();

    memcpy(dest, src, size);
}

void MemoryAllocator::Move(void* dest, void* src, uint64_t size)
{
    PROFILE_FUNCTION();

    memmove(dest, src, size);
}