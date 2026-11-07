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

#include <api/interfaces/interfaces.h>
#include <api/shared/profiler.h>

#include <format>
#include <span>
#include <string>
#include <string_view>

constexpr std::string_view kChainEntityField = "__m_pChainEntity";

static bool IsStandardLayoutClass(const SchemaClassInfoData_t* classData)
{
    PROFILE_FUNCTION();

    int classesWithFields = 0;
    for (auto pClass = classData; pClass != nullptr; pClass = pClass->m_pBaseClasses ? pClass->m_pBaseClasses->m_pClass : nullptr)
    {
        if ((pClass->m_nSizeOf > 1) || (pClass->m_nNumFields != 0))
            classesWithFields++;

        if (classesWithFields > 1) return false;
    }

    for (const auto& field : std::span(classData->m_pFields, classData->m_nNumFields))
    {
        if (!field.m_pType || field.m_pType->m_eTypeCategory != SchemaTypeCategory_t::SCHEMA_TYPE_DECLARED_CLASS) continue;

        auto fClass = reinterpret_cast<const CSchemaType_DeclaredClass*>(field.m_pType);
        if (fClass->m_pClassInfo && !IsStandardLayoutClass(fClass->m_pClassInfo)) return false;
    }

    return true;
}

static int FindChainerOffset(const CSchemaClassInfo* classInfo)
{
    PROFILE_FUNCTION();

    for (const auto& field : std::span(classInfo->m_pFields, classInfo->m_nNumFields))
        if (kChainEntityField == field.m_pName)
            return field.m_nSingleInheritanceOffset;

    for (int i = 0; i < classInfo->m_nNumBaseClasses; i++)
    {
        auto baseClass = classInfo->m_pBaseClasses[i].m_pClass;
        if (!baseClass) continue;

        int offset = FindChainerOffset(baseClass);
        if (offset != -1)
            return offset;
    }

    return -1;
}

void CSDKSchema::ReadClasses(CSchemaType_DeclaredClass* declClass)
{
    PROFILE_FUNCTION();

    auto classInfo = declClass->m_pClassInfo;
    if (!classInfo) return;

    const uint64_t classHash = static_cast<uint64_t>(hash_32_fnv1a_const(classInfo->m_pName)) << 32;
    void* classVtable = nullptr;

    const SchemaField classDefaults{
        .m_nChainerOffset = FindChainerOffset(classInfo),
        .m_bIsStruct = IsStandardLayoutClass(classInfo),
        .m_bHasVtable = g_pS2BinLib->FindVtable("server", classInfo->m_pName, &classVtable) == 0,
    };

    std::string networkVarName;

    for (const auto& field : std::span(classInfo->m_pFields, classInfo->m_nNumFields))
    {
        SchemaField info = classDefaults;
        info.m_uOffset = static_cast<uint32_t>(field.m_nSingleInheritanceOffset);

        void* vtable = nullptr;
        uint64_t index = 0;
        networkVarName = std::format("NetworkVar_{}", field.m_pName);

        if (g_pS2BinLib->FindVtableNested2("server", classInfo->m_pName, networkVarName.c_str(), &vtable) == 0 &&
            g_pS2BinLib->FindNetworkvarVtableStatechanged(reinterpret_cast<uint64_t>(vtable), &index) == 0)
        {
            info.m_nStateChangedOffset = static_cast<int32_t>(index);
            m_networkVarStateChangedOffsets.try_emplace(vtable, info.m_nStateChangedOffset);
        }

        m_fields.try_emplace(classHash | hash_32_fnv1a_const(field.m_pName), info);
    }
}
