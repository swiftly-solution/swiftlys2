/************************************************************************************************
 * SwiftlyS2 is a scripting framework for Source2-based games.
 * Copyright (C) 2023-2026 Swiftly Solution SRL via Sava Andrei-Sebastian and it's contributors
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 ************************************************************************************************/

#include <api/interfaces/interfaces.h>
#include <scripting/scripting.h>

#include <public/entity2/entitykeyvalues.h>
#include <public/tier1/utlstringtoken.h>

static char* Bridge_CEntityKeyValues_CopyString(const std::string& value, int* size)
{
    int outSize = static_cast<int>(value.size());
    *size = outSize;

    char* out = (char*)g_pMemoryAllocator->Alloc(outSize + 1);
    g_pMemoryAllocator->Copy(out, (void*)value.c_str(), outSize);
    out[outSize] = '\0';
    return out;
}

void* Bridge_CEntityKeyValues_Allocate()
{
    CEntityKeyValues* kv = new CEntityKeyValues();
    kv->AddRef();
    return kv;
}

void Bridge_CEntityKeyValues_Deallocate(void* ptr)
{
    ((CEntityKeyValues*)ptr)->Release();
}

enum CEntityKeyValuesValueKind
{
    CEntityKeyValuesValueKind_Bool,
    CEntityKeyValuesValueKind_Int32,
    CEntityKeyValuesValueKind_UInt32,
    CEntityKeyValuesValueKind_Int64,
    CEntityKeyValuesValueKind_UInt64,
    CEntityKeyValuesValueKind_Float,
    CEntityKeyValuesValueKind_Double,
    CEntityKeyValuesValueKind_Ptr,
    CEntityKeyValuesValueKind_StringToken,
    CEntityKeyValuesValueKind_Color,
    CEntityKeyValuesValueKind_Vector,
    CEntityKeyValuesValueKind_Vector2D,
    CEntityKeyValuesValueKind_Vector4D,
    CEntityKeyValuesValueKind_QAngle
};

void Bridge_CEntityKeyValues_GetValue(void* keyvalues, const char* keyName, int kind, void* out)
{
    CEntityKeyValues* kv = (CEntityKeyValues*)keyvalues;
    switch (kind)
    {
        case CEntityKeyValuesValueKind_Bool: *(bool*)out = kv->GetBool(keyName); break;
        case CEntityKeyValuesValueKind_Int32: *(int32_t*)out = kv->GetInt(keyName); break;
        case CEntityKeyValuesValueKind_UInt32: *(uint32_t*)out = kv->GetUint(keyName); break;
        case CEntityKeyValuesValueKind_Int64: *(int64_t*)out = kv->GetInt64(keyName); break;
        case CEntityKeyValuesValueKind_UInt64: *(uint64_t*)out = kv->GetUint64(keyName); break;
        case CEntityKeyValuesValueKind_Float: *(float*)out = kv->GetFloat(keyName); break;
        case CEntityKeyValuesValueKind_Double: *(double*)out = kv->GetDouble(keyName); break;
        case CEntityKeyValuesValueKind_Ptr: *(void**)out = kv->GetPtr(keyName); break;
        case CEntityKeyValuesValueKind_StringToken: *(CUtlStringToken*)out = kv->GetStringToken(keyName); break;
        case CEntityKeyValuesValueKind_Color: *(Color*)out = kv->GetColor(keyName); break;
        case CEntityKeyValuesValueKind_Vector: *(Vector*)out = kv->GetVector(keyName); break;
        case CEntityKeyValuesValueKind_Vector2D: *(Vector2D*)out = kv->GetVector2D(keyName); break;
        case CEntityKeyValuesValueKind_Vector4D: *(Vector4D*)out = kv->GetVector4D(keyName); break;
        case CEntityKeyValuesValueKind_QAngle: *(QAngle*)out = kv->GetQAngle(keyName); break;
    }
}

void Bridge_CEntityKeyValues_SetValue(void* keyvalues, const char* keyName, int kind, const void* value)
{
    CEntityKeyValues* kv = (CEntityKeyValues*)keyvalues;
    switch (kind)
    {
        case CEntityKeyValuesValueKind_Bool: kv->SetBool(keyName, *(const bool*)value); break;
        case CEntityKeyValuesValueKind_Int32: kv->SetInt(keyName, *(const int32_t*)value); break;
        case CEntityKeyValuesValueKind_UInt32: kv->SetUint(keyName, *(const uint32_t*)value); break;
        case CEntityKeyValuesValueKind_Int64: kv->SetInt64(keyName, *(const int64_t*)value); break;
        case CEntityKeyValuesValueKind_UInt64: kv->SetUint64(keyName, *(const uint64_t*)value); break;
        case CEntityKeyValuesValueKind_Float: kv->SetFloat(keyName, *(const float*)value); break;
        case CEntityKeyValuesValueKind_Double: kv->SetDouble(keyName, *(const double*)value); break;
        case CEntityKeyValuesValueKind_Ptr: kv->SetPtr(keyName, *(void* const*)value); break;
        case CEntityKeyValuesValueKind_StringToken: kv->SetStringToken(keyName, *(const CUtlStringToken*)value); break;
        case CEntityKeyValuesValueKind_Color: kv->SetColor(keyName, *(const Color*)value); break;
        case CEntityKeyValuesValueKind_Vector: kv->SetVector(keyName, *(const Vector*)value); break;
        case CEntityKeyValuesValueKind_Vector2D: kv->SetVector2D(keyName, *(const Vector2D*)value); break;
        case CEntityKeyValuesValueKind_Vector4D: kv->SetVector4D(keyName, *(const Vector4D*)value); break;
        case CEntityKeyValuesValueKind_QAngle: kv->SetQAngle(keyName, *(const QAngle*)value); break;
    }
}

char* Bridge_CEntityKeyValues_GetString(int* size, void* keyvalues, const char* keyName)
{
    std::string s = ((CEntityKeyValues*)keyvalues)->GetString(keyName);
    return Bridge_CEntityKeyValues_CopyString(s, size);
}

void Bridge_CEntityKeyValues_SetString(void* keyvalues, const char* keyName, const char* value)
{
    ((CEntityKeyValues*)keyvalues)->SetString(keyName, value);
}

bool Bridge_CEntityKeyValues_HasKey(void* keyvalues, const char* keyName)
{
    bool attr = false;
    return ((CEntityKeyValues*)keyvalues)->GetKeyValue(keyName, &attr) != nullptr;
}

DEFINE_NATIVE("CEntityKeyValues.Allocate", Bridge_CEntityKeyValues_Allocate);
DEFINE_NATIVE("CEntityKeyValues.Deallocate", Bridge_CEntityKeyValues_Deallocate);
DEFINE_NATIVE("CEntityKeyValues.GetValue", Bridge_CEntityKeyValues_GetValue);
DEFINE_NATIVE("CEntityKeyValues.SetValue", Bridge_CEntityKeyValues_SetValue);
DEFINE_NATIVE("CEntityKeyValues.GetString", Bridge_CEntityKeyValues_GetString);
DEFINE_NATIVE("CEntityKeyValues.SetString", Bridge_CEntityKeyValues_SetString);
DEFINE_NATIVE("CEntityKeyValues.HasKey", Bridge_CEntityKeyValues_HasKey);
