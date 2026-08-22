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

#ifndef src_api_sdk_schema_h
#define src_api_sdk_schema_h

#include <string>
#include <cstdint>

#include <public/datamap.h>

class ISDKSchema
{
public:
    virtual void SetStateChanged(void* pEntity, uint64_t uHash) = 0;

    virtual int32_t GetOffset(const char* sClassName, const char* sMemberName) = 0;
    virtual int32_t GetOffset(uint64_t uHash) = 0;

    virtual void* GetPropPtr(void* pEntity, const char* sClassName, const char* sMemberName) = 0;
    virtual void* GetPropPtr(void* pEntity, uint64_t uHash) = 0;

    virtual void* GetVData(void* pEntity) = 0;

    virtual void* GetDatamapFunction(const char* className, const char* functionName) = 0;

    virtual void Load() = 0;
};

#endif