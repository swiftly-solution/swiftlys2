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

#include "schema.h"

#include <algorithm>
#include <cstddef>
#include <cstring>
#include <format>
#include <vector>

#include <api/interfaces/interfaces.h>
#include <api/shared/profiler.h>
#include <public/networksystem/networkvar.h>

#include <public/entity2/entityclass.h>

constexpr uint64_t CBaseEntity_m_nSubclassID = SchemaFieldHash("CBaseEntity", "m_nSubclassID");

std::optional<SchemaField> CSDKSchema::FindField(uint64_t uHash) const
{
    PROFILE_FUNCTION();

    auto it = m_fields.find(uHash);
    if (it == m_fields.end()) return std::nullopt;
    return it->second;
}

std::optional<int32_t> CSDKSchema::FindStateChangedIndex(void* pVtable) const
{
    PROFILE_FUNCTION();

    auto it = m_networkVarStateChangedOffsets.find(pVtable);
    if (it == m_networkVarStateChangedOffsets.end()) return std::nullopt;
    return it->second;
}

void CSDKSchema::Load()
{
    PROFILE_FUNCTION();

    g_pLogger->Info("SDK", "Loading SDK classes and network var vtables...\n");

    m_fields.clear();
    m_networkVarStateChangedOffsets.clear();

    std::vector<ISchemaSystemTypeScope*> scopes{ g_pGameSchemaSystem->GlobalTypeScope() };
    for (int i = 0; i < g_pGameSchemaSystem->m_TypeScopes.GetNumStrings(); i++)
        scopes.push_back(g_pGameSchemaSystem->m_TypeScopes[i]);

    size_t classCount = 0, fieldCount = 0;
    for (auto scope : scopes)
    {
        auto rawScope = (CSchemaSystemTypeScope*)scope;
        classCount += rawScope->m_DeclaredClasses.m_Map.Count();

        FOR_EACH_MAP(rawScope->m_DeclaredClasses.m_Map, iter)
        {
            if (auto classInfo = rawScope->m_DeclaredClasses.m_Map.Element(iter)->m_pClassInfo)
                fieldCount += classInfo->m_nNumFields;
        }
    }

    m_fields.reserve(fieldCount);

    for (auto scope : scopes)
    {
        auto rawScope = (CSchemaSystemTypeScope*)scope;
        FOR_EACH_MAP(rawScope->m_DeclaredClasses.m_Map, iter)
        {
            ReadClasses(rawScope->m_DeclaredClasses.m_Map.Element(iter));
        }
    }

    g_pLogger->Info("SDK", std::format("Finished loading {} SDK classes ({} fields).\n", classCount, m_fields.size()));

    const auto networkVarFields = std::ranges::count_if(m_fields, [](const auto& entry) { return entry.second.m_nStateChangedOffset != -1; });
    g_pLogger->Info("SDK", std::format("Loaded {} network var fields.\n", networkVarFields));

    g_pGameSchemaSystem->PrintSchemaStats("");
}

void CSDKSchema::SetStateChanged(void* pEntity, uint64_t uHash)
{
    PROFILE_FUNCTION();

    if (pEntity == nullptr) return;

    auto fieldInfo = FindField(uHash);
    if (!fieldInfo) return;

    const SchemaField& field = *fieldInfo;

    if (field.m_nStateChangedOffset != -1) {
        // This slot belongs to the embedded field's vtable. Its thunk adds the
        // field offset when forwarding the notification to its enclosing object.
        auto networkVar = reinterpret_cast<NetworkVar*>(reinterpret_cast<uintptr_t>(pEntity) + field.m_uOffset);
        networkVar->StateChanged(field.m_nStateChangedOffset, NetworkStateChangedData(uint32_t(0)));
        return;
    }

    if (field.m_bHasVtable) {
        // A nested schema handle can itself point at a NetworkVar subclass,
        // e.g. fogparams_t inside CFogController. Use its actual runtime vtable.
        auto stateChanged = FindStateChangedIndex(*reinterpret_cast<void**>(pEntity));
        if (stateChanged.has_value()) {
            reinterpret_cast<NetworkVar*>(pEntity)->StateChanged(*stateChanged, NetworkStateChangedData(field.m_uOffset));
            return;
        }
    }

    if (field.m_nChainerOffset != -1) {
        auto pChainer = reinterpret_cast<CNetworkVarChainer*>(reinterpret_cast<uintptr_t>(pEntity) + field.m_nChainerOffset);
        if(pChainer == nullptr) return;
        
        NetworkStateChangedData data(field.m_uOffset, -1, pChainer->m_PathIndex);
        pChainer->NetworkStateChanged(data);
    } else if (!field.m_bIsStruct) {
        reinterpret_cast<CEntityInstance*>(pEntity)->NetworkStateChanged(NetworkStateChangedData(field.m_uOffset));
    }
}

int32_t CSDKSchema::GetOffset(const char* sClassName, const char* sMemberName)
{
    PROFILE_FUNCTION();

    return GetOffset(SchemaFieldHash(sClassName, sMemberName));
}

int32_t CSDKSchema::GetOffset(uint64_t uHash)
{
    PROFILE_FUNCTION();

    auto field = FindField(uHash);
    return field.has_value() ? static_cast<int32_t>(field->m_uOffset) : -1;
}

void* CSDKSchema::GetPropPtr(void* pEntity, const char* sClassName, const char* sMemberName)
{
    PROFILE_FUNCTION();

    return GetPropPtr(pEntity, SchemaFieldHash(sClassName, sMemberName));
}

void* CSDKSchema::GetPropPtr(void* pEntity, uint64_t uHash)
{
    PROFILE_FUNCTION();

    if (pEntity == nullptr) return nullptr;

    auto field = FindField(uHash);
    if (!field) return nullptr;

    return reinterpret_cast<void*>(reinterpret_cast<uintptr_t>(pEntity) + field->m_uOffset);
}

void* CSDKSchema::GetVData(void* pEntity)
{
    PROFILE_FUNCTION();

    void* subclassPtr = GetPropPtr(pEntity, CBaseEntity_m_nSubclassID);
    return *reinterpret_cast<void**>(reinterpret_cast<uintptr_t>(subclassPtr) + 4);
}

void* CSDKSchema::GetDatamapFunction(const char* className, const char* functionName)
{
    PROFILE_FUNCTION();

    auto entitySystem = g_pEntSystem->GetEntitySystem();
    if (entitySystem == nullptr) return nullptr;

    auto entityClassIndex = entitySystem->m_entClassesByCPPClassname.Find(className);
    if (!entitySystem->m_entClassesByCPPClassname.IsValidIndex(entityClassIndex)) return nullptr;

    auto entityClass = entitySystem->m_entClassesByCPPClassname[entityClassIndex];
    if (entityClass == nullptr || entityClass->m_NameToThinkFunc == nullptr) return nullptr;

    auto func = entityClass->m_NameToThinkFunc(functionName);
    void* address = nullptr;
    std::memcpy(&address, &func, sizeof(address));
    return address;
}
