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
#include <api/memory/virtual/call.h>

#include <public/entity2/entityinstance.h>
#include <public/schemasystem/schemasystem.h>

#include <cstdint>
#include <optional>
#include <unordered_map>

struct SchemaField
{
    uint32_t m_uOffset = 0;
    int32_t m_nChainerOffset = -1;
    int32_t m_nStateChangedOffset = -1;
    bool m_bIsStruct = false;
    bool m_bHasVtable = false;
};

struct IdentityHasher64
{
    std::size_t operator()(uint64_t key) const noexcept { return static_cast<std::size_t>(key); }
};

class CSDKSchema : public ISDKSchema
{
public:
    virtual void SetStateChanged(void* pEntity, uint64_t uHash) override;

    virtual int32_t GetOffset(const char* sClassName, const char* sMemberName) override;
    virtual int32_t GetOffset(uint64_t uHash) override;

    virtual void* GetPropPtr(void* pEntity, const char* sClassName, const char* sMemberName) override;
    virtual void* GetPropPtr(void* pEntity, uint64_t uHash) override;

    virtual void* GetVData(void* pEntity) override;

    virtual void* GetDatamapFunction(const char* className, const char* functionName) override;

    virtual void Load() override;

private:
    void ReadClasses(CSchemaType_DeclaredClass* declClass);

    std::optional<SchemaField> FindField(uint64_t uHash) const;
    std::optional<int32_t> FindStateChangedIndex(void* pVtable) const;

    std::unordered_map<uint64_t, SchemaField, IdentityHasher64> m_fields;
    std::unordered_map<void*, int32_t> m_networkVarStateChangedOffsets;
};

class NetworkVar {
public:
    void StateChanged(uint64_t index, const NetworkStateChangedData& data) {
        CALL_VIRTUAL(void, static_cast<int>(index), this, &data);
    }
};

#endif
