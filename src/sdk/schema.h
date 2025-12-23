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

#ifndef src_sdk_schema_h
#define src_sdk_schema_h

#include <api/sdk/schema.h>
#include <api/shared/string.h>
#include <api/memory/virtual/call.h>

#include <public/schemasystem/schemasystem.h>
#include <nlohmann/json.hpp>

#include <unordered_map>
#include <set>

using json = nlohmann::json;

void ReadClasses(CSchemaType_DeclaredClass* declClass, json& outJson);
void ReadEnums(CSchemaType_DeclaredEnum* declClass, json& outJson);

class CSDKSchema : public ISDKSchema
{
public:
    virtual void SetStateChanged(void* pEntity, const char* sClassName, const char* sMemberName) override;
    virtual void SetStateChanged(void* pEntity, uint64_t uHash) override;

    virtual int32_t FindChainOffset(const char* sClassName) override;

    virtual int32_t GetOffset(const char* sClassName, const char* sMemberName) override;
    virtual int32_t GetOffset(uint64_t uHash) override;

    virtual bool IsStruct(const char* sClassName) override;
    virtual bool IsClassLoaded(const char* sClassName) override;

    virtual void* GetPropPtr(void* pEntity, const char* sClassName, const char* sMemberName) override;
    virtual void* GetPropPtr(void* pEntity, uint64_t uHash) override;

    virtual void WritePropPtr(void* pEntity, const char* sClassName, const char* sMemberName, void* pValue, uint32_t size) override;
    virtual void WritePropPtr(void* pEntity, uint64_t uHash, void* pValue, uint32_t size) override;

    virtual void* GetVData(void* pEntity) override;

    virtual void Load() override;
};

struct SchemaField
{
    bool m_bNetworked;
    bool m_bChainer;
    bool m_bIsStruct;
    uint32_t m_uOffset;
    int32_t m_nChainerOffset;
};

struct SchemaClass
{
    bool m_bIsStruct;
    uint32_t m_uSize;
    uint32_t m_uAlignment;
    uint32_t m_uHash;
};

extern std::unordered_map<uint64_t, SchemaField> offsets;
extern std::unordered_map<uint32_t, SchemaClass> classes;

class NetworkVar {
public:
    uint64_t pVtable() const { return *(uint64_t*)this; };
    void StateChanged(uint64_t index, const NetworkStateChangedData& data) {
        CALL_VIRTUAL(void, (int)index, this, &data);
    }
};

#endif