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

#ifndef src_api_memory_allocator_h
#define src_api_memory_allocator_h

#include <string>
#include <map>
#include <cstdint>
#include <vector>

class IMemoryAllocator
{
public:
    virtual void* Alloc(uint64_t size) = 0;
    virtual void* TrackedAlloc(uint64_t size, std::string identifier, std::string details) = 0;

    virtual void Free(void* ptr) = 0;

    virtual void* Resize(void* ptr, uint64_t newSize) = 0;

    virtual uint64_t GetSize(void* ptr) = 0;

    virtual uint64_t GetTotalAllocated() = 0;
    virtual uint64_t GetAllocatedByTrackedIdentifier(std::string identifier) = 0;

    virtual std::vector<std::pair<std::string, void*>> GetTrackedAllocations(std::string identifier) = 0;

    virtual bool IsPointerValid(void* ptr) = 0;

    virtual void Copy(void* dest, void* src, uint64_t size) = 0;
    virtual void Move(void* dest, void* src, uint64_t size) = 0;

    virtual std::map<void*, uint64_t> GetAllocations() = 0;
};

#endif