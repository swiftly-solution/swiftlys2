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

#include <set>
#include <fmt/format.h>

bool IsStandardLayoutClass(SchemaClassInfoData_t* classData) {
    {
        auto pClass = classData;
        int classesWithFields = 0;
        do {
            classesWithFields += ((pClass->m_nSize > 1) || (pClass->m_nFieldCount != 0)) ? 1 : 0;

            if (classesWithFields > 1) return false;

            pClass = (pClass->m_pBaseClasses == nullptr) ? nullptr : pClass->m_pBaseClasses->m_pClass;
        } while (pClass != nullptr);
    }

    auto fields = classData->m_pFields;
    auto fieldsCount = classData->m_nFieldCount;
    for (uint16_t i = 0; i < fieldsCount; i++) {
        auto fieldType = fields[i].m_pType;
        if (fieldType->m_eTypeCategory == SchemaTypeCategory_t::SCHEMA_TYPE_DECLARED_CLASS) {
            CSchemaType_DeclaredClass* fClass = reinterpret_cast<CSchemaType_DeclaredClass*>(fieldType);
            if (fClass->m_pClassInfo && !IsStandardLayoutClass(fClass->m_pClassInfo)) return false;
        }
    }

    return true;
}

void FindChainer(bool& has_chainer, int& chainer_offset, CSchemaClassInfo* classInfo)
{
    for (int i = 0; i < classInfo->m_nBaseClassCount; i++)
    {
        auto baseClass = classInfo->m_pBaseClasses[i].m_pClass;
        if (baseClass)
        {
            for (int j = 0; j < baseClass->m_nFieldCount; j++)
            {
                if (baseClass->m_pFields[j].m_pszName == std::string("__m_pChainEntity"))
                {
                    has_chainer = true;
                    chainer_offset = baseClass->m_pFields[j].m_nSingleInheritanceOffset;
                    break;
                }
            }
        }
        if (has_chainer) break;
    }

    if (!has_chainer)
    {
        for (int i = 0; i < classInfo->m_nBaseClassCount; i++)
        {
            auto baseClass = classInfo->m_pBaseClasses[i].m_pClass;
            if (baseClass)
            {
                FindChainer(has_chainer, chainer_offset, baseClass);
                if (has_chainer) break;
            }
        }
    }
}

void ReadClasses(CSchemaType_DeclaredClass* declClass)
{
    auto classInfo = declClass->m_pClassInfo;

    if (!classInfo) return;

    uint32_t class_hash = hash_32_fnv1a_const(classInfo->m_pszName);
    bool isStruct = IsStandardLayoutClass(classInfo);

    classes.insert({ class_hash, {isStruct, (uint32_t)classInfo->m_nSize, (uint32_t)classInfo->m_nAlignment, class_hash} });

    auto field_size = classInfo->m_nFieldCount;
    auto fields = classInfo->m_pFields;

    bool has_chainer = false;
    int chainer_offset = 0;

    for (int i = 0; i < field_size; i++)
    {
        if (fields[i].m_pszName == std::string("__m_pChainEntity"))
        {
            has_chainer = true;
            chainer_offset = fields[i].m_nSingleInheritanceOffset;
            break;
        }
    }

    if (!has_chainer)
    {
        FindChainer(has_chainer, chainer_offset, classInfo);
    }

    for (int i = 0; i < field_size; i++)
    {
        auto field = fields[i];
        uint64_t fieldHash = ((uint64_t)(class_hash) << 32 | hash_32_fnv1a_const(field.m_pszName));

        offsets.insert({ fieldHash, { has_chainer, isStruct, (uint32_t)field.m_nSingleInheritanceOffset, chainer_offset } });
    }
}