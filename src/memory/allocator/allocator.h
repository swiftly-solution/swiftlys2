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

#ifndef src_memory_allocator_allocator_h
#define src_memory_allocator_allocator_h

#include <api/memory/allocator/allocator.h>

class MemoryAllocator : public IMemoryAllocator
{
public:
    virtual void* Alloc(uint64_t size) override;

    virtual void Free(void* ptr) override;
    virtual void* Resize(void* ptr, uint64_t newSize) override;

    virtual uint64_t GetSize(void* ptr) override;

    virtual uint64_t GetTotalAllocated() override;

    virtual bool IsPointerValid(void* ptr) override;

    virtual void Copy(void* dest, void* src, uint64_t size) override;
    virtual void Move(void* dest, void* src, uint64_t size) override;

    ~MemoryAllocator();
private:
    uint64_t totalAllocated = 0;
};

#endif