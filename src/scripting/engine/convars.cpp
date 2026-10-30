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
#include <api/shared/profiler.h>
#include <public/tier0/convar.h>
#include <public/tier0/utlstring.h>
#include <scripting/scripting.h>

#include <optional>

typedef void (*QueryClientCvarCallback)(int, const char*, const char*);
typedef void (*GlobalChangeListenerCallback)(const char*, int, const char*, const char*);
typedef void (*ConvarNameCallback)(const char*);

extern bool bypassConvarCallbacks;

ConVarRefAbstract& GetConVarRef(const char* cvarName)
{
    PROFILE_FUNCTION();

    return g_pConvarManager->GetConvarRef(cvarName);
}

void Bridge_Convars_QueryClientConvar(int playerid, const char* cvarName)
{
    PROFILE_FUNCTION();

    g_pConvarManager->QueryClientConvar(playerid, cvarName);
}

int Bridge_Convars_AddQueryClientCvarCallback(QueryClientCvarCallback callback)
{
    PROFILE_FUNCTION();

    return g_pConvarManager->AddQueryClientCvarCallback([callback](int playerid, std::string cvarName, std::string cvarValue) -> void {
        callback(playerid, cvarName.c_str(), cvarValue.c_str());
        });
}

void Bridge_Convars_RemoveQueryClientCvarCallback(int callbackId)
{
    PROFILE_FUNCTION();

    g_pConvarManager->RemoveQueryClientCvarCallback(callbackId);
}

static ConvarValue ReadConvarValue(EConVarType type, const void* value)
{
    PROFILE_FUNCTION();

    switch (type)
    {
        case EConVarType_Int16: return *(const int16_t*)value;
        case EConVarType_UInt16: return *(const uint16_t*)value;
        case EConVarType_Int32: return *(const int32_t*)value;
        case EConVarType_UInt32: return *(const uint32_t*)value;
        case EConVarType_Int64: return *(const int64_t*)value;
        case EConVarType_UInt64: return *(const uint64_t*)value;
        case EConVarType_Bool: return *(const bool*)value;
        case EConVarType_Float32: return *(const float*)value;
        case EConVarType_Float64: return *(const double*)value;
        case EConVarType_Color: return *(const Color*)value;
        case EConVarType_Vector2: return *(const Vector2D*)value;
        case EConVarType_Vector3: return *(const Vector*)value;
        case EConVarType_Vector4: return *(const Vector4D*)value;
        case EConVarType_Qangle: return *(const QAngle*)value;
        case EConVarType_String: return std::string((const char*)value);
        default: return 0;
    }
}

static bool HasMinMax(EConVarType type)
{
    PROFILE_FUNCTION();

    switch (type)
    {
        case EConVarType_Int16:
        case EConVarType_UInt16:
        case EConVarType_Int32:
        case EConVarType_UInt32:
        case EConVarType_Int64:
        case EConVarType_UInt64:
        case EConVarType_Float32:
        case EConVarType_Float64:
            return true;
        default:
            return false;
    }
}

void Bridge_Convars_CreateConvar(const char* convarName, int cvarType, uint64_t cvarFlags, const char* helpMessage, void* defaultValue, void* minValue, void* maxValue)
{
    PROFILE_FUNCTION();

    auto type = (EConVarType)cvarType;
    if (!defaultValue) return;

    std::optional<ConvarValue> minValueOptional;
    std::optional<ConvarValue> maxValueOptional;
    if (HasMinMax(type))
    {
        if (minValue != nullptr) minValueOptional = ReadConvarValue(type, minValue);
        if (maxValue != nullptr) maxValueOptional = ReadConvarValue(type, maxValue);
    }

    g_pConvarManager->CreateConvar(convarName, type, cvarFlags, helpMessage, ReadConvarValue(type, defaultValue), minValueOptional, maxValueOptional);
}

void Bridge_Convars_DeleteConvar(const char* convarName)
{
    PROFILE_FUNCTION();

    g_pConvarManager->DeleteConvar(convarName);
}

bool Bridge_Convars_ExistsConvar(const char* convarName)
{
    PROFILE_FUNCTION();

    auto& cvar = GetConVarRef(convarName);
    return cvar.IsValidRef() && cvar.IsConVarDataValid();
}

int Bridge_Convars_GetConvarType(const char* convarName)
{
    PROFILE_FUNCTION();

    auto& cvar = GetConVarRef(convarName);
    return (int)(cvar.GetType());
}

uint64_t Bridge_Convars_GetFlags(const char* cvarName)
{
    PROFILE_FUNCTION();

    auto& cvar = GetConVarRef(cvarName);
    return cvar.GetConVarData()->m_nFlags;
}

void Bridge_Convars_SetFlags(const char* cvarName, uint64_t flags)
{
    PROFILE_FUNCTION();

    auto& cvar = GetConVarRef(cvarName);
    cvar.GetConVarData()->m_nFlags = flags;
}

uint64_t Bridge_Convars_AddGlobalChangeListener(GlobalChangeListenerCallback callback)
{
    PROFILE_FUNCTION();

    return g_pConvarManager->AddGlobalChangeListener([callback](const char* convarName, int slot, const char* newValue, const char* oldValue) -> void {
        callback(convarName, slot, newValue, oldValue);
        });
}

void Bridge_Convars_RemoveGlobalChangeListener(uint64_t listenerID)
{
    PROFILE_FUNCTION();

    g_pConvarManager->RemoveGlobalChangeListener(listenerID);
}

uint64_t Bridge_Convars_AddConvarCreatedListener(ConvarNameCallback callback)
{
    PROFILE_FUNCTION();

    return g_pConvarManager->AddConvarCreatedListener([callback](const char* convarName) -> void {
        callback(convarName);
        });
}

void Bridge_Convars_RemoveConvarCreatedListener(uint64_t listenerID)
{
    PROFILE_FUNCTION();

    g_pConvarManager->RemoveConvarCreatedListener(listenerID);
}

uint64_t Bridge_Convars_AddConCommandCreatedListener(ConvarNameCallback callback)
{
    PROFILE_FUNCTION();

    return g_pConvarManager->AddConCommandCreatedListener([callback](const char* convarName) -> void {
        callback(convarName);
        });
}

void Bridge_Convars_RemoveConCommandCreatedListener(uint64_t listenerID)
{
    PROFILE_FUNCTION();

    g_pConvarManager->RemoveConCommandCreatedListener(listenerID);
}

void* Bridge_Convars_GetMinValuePtrPtr(const char* cvarName)
{
    PROFILE_FUNCTION();

    auto& cvar = GetConVarRef(cvarName);
    return &cvar.GetConVarData()->m_minValue;
}

void* Bridge_Convars_GetMaxValuePtrPtr(const char* cvarName)
{
    PROFILE_FUNCTION();

    auto& cvar = GetConVarRef(cvarName);
    return &cvar.GetConVarData()->m_maxValue;
}

bool Bridge_Convars_HasDefaultValue(const char* cvarName)
{
    PROFILE_FUNCTION();

    auto& cvar = GetConVarRef(cvarName);
    return cvar.HasDefault();
}

void* Bridge_Convars_GetDefaultValuePtr(const char* cvarName)
{
    PROFILE_FUNCTION();

    auto& cvar = GetConVarRef(cvarName);
    return cvar.GetConVarData()->DefaultValue();
}

void* Bridge_Convars_GetValuePtr(const char* cvarName)
{
    PROFILE_FUNCTION();

    auto& cvar = GetConVarRef(cvarName);
    return cvar.GetConVarData()->Value(0);
}

void Bridge_Convars_SetValuePtr(const char* cvarName, void* value)
{
    PROFILE_FUNCTION();

    auto& cvar = GetConVarRef(cvarName);
    cvar.SetOrQueueValueInternal(0, (CVValue_t*)value);
}

void Bridge_Convars_SetValueInternalPtr(const char* cvarName, void* value)
{
    PROFILE_FUNCTION();

    bypassConvarCallbacks = true;
    auto& cvar = GetConVarRef(cvarName);
    cvar.SetValueInternal(0, (CVValue_t*)value);
    bypassConvarCallbacks = false;
}

bool Bridge_Convars_SetValueAsString(const char* cvarName, const char* value)
{
    PROFILE_FUNCTION();

    bypassConvarCallbacks = true;
    auto& cvar = GetConVarRef(cvarName);
    bool result = cvar.SetString(CUtlString(value), CSplitScreenSlot(0));
    bypassConvarCallbacks = false;

    return result;
}

char* Bridge_Convars_GetValueAsString(int* size, const char* cvarName)
{
    PROFILE_FUNCTION();

    auto& cvar = GetConVarRef(cvarName);
    CBufferString buf;
    cvar.GetValueAsString(buf, CSplitScreenSlot(0));

    return Bridge_CopyString(buf.Get(), size);
}

bool Bridge_Convars_SetDefaultValueAsString(const char* cvarName, const char* defaultValue)
{
    PROFILE_FUNCTION();

    auto& cvar = GetConVarRef(cvarName);
    auto data = cvar.GetConVarData();
    if (!data->m_defaultValue)
    {
        data->m_defaultValue = new CVValue_t();
    }
    return cvar.GetConVarData()->TypeTraits()->StringToValue(defaultValue, data->m_defaultValue);
}

char* Bridge_Convars_GetDefaultValueAsString(int* size, const char* cvarName)
{
    PROFILE_FUNCTION();

    auto& cvar = GetConVarRef(cvarName);
    CBufferString buf;
    cvar.GetConVarData()->DefaultValueToString(buf);

    return Bridge_CopyString(buf.Get(), size);
}

char* Bridge_Convars_GetMinValueAsString(int* size, const char* cvarName)
{
    PROFILE_FUNCTION();

    auto& cvar = GetConVarRef(cvarName);
    CBufferString buf;
    cvar.GetConVarData()->MinValueToString(buf);

    return Bridge_CopyString(buf.Get(), size);
}

bool Bridge_Convars_SetMinValueAsString(const char* cvarName, const char* minValue)
{
    PROFILE_FUNCTION();

    auto& cvar = GetConVarRef(cvarName);
    auto data = cvar.GetConVarData();
    if (!data->m_minValue)
    {
        data->m_minValue = new CVValue_t();
    }
    return cvar.GetConVarData()->TypeTraits()->StringToValue(minValue, data->m_minValue);
}

char* Bridge_Convars_GetMaxValueAsString(int* size, const char* cvarName)
{
    PROFILE_FUNCTION();

    auto& cvar = GetConVarRef(cvarName);
    CBufferString buf;
    cvar.GetConVarData()->MaxValueToString(buf);

    return Bridge_CopyString(buf.Get(), size);
}

bool Bridge_Convars_SetMaxValueAsString(const char* cvarName, const char* maxValue)
{
    PROFILE_FUNCTION();

    auto& cvar = GetConVarRef(cvarName);
    auto data = cvar.GetConVarData();
    if (!data->m_maxValue)
    {
        data->m_maxValue = new CVValue_t();
    }
    return cvar.GetConVarData()->TypeTraits()->StringToValue(maxValue, data->m_maxValue);
}

void Bridge_Convars_SetValueInternalAsString(const char* cvarName, const char* value)
{
    PROFILE_FUNCTION();

    auto& cvar = GetConVarRef(cvarName);
    CVValue_t v;
    cvar.GetConVarData()->TypeTraits()->StringToValue(value, &v);
    cvar.SetValueInternal(0, &v);
}

char* Bridge_Convars_GetDescription(int* size, const char* cvarName)
{
    PROFILE_FUNCTION();

    auto& cvar = GetConVarRef(cvarName);
    std::string s = cvar.GetHelpText();

    return Bridge_CopyString(s.c_str(), size);
}

DEFINE_NATIVE("Convars.QueryClientConvar", Bridge_Convars_QueryClientConvar);
DEFINE_NATIVE("Convars.AddQueryClientCvarCallback", Bridge_Convars_AddQueryClientCvarCallback);
DEFINE_NATIVE("Convars.RemoveQueryClientCvarCallback", Bridge_Convars_RemoveQueryClientCvarCallback);
DEFINE_NATIVE("Convars.AddGlobalChangeListener", Bridge_Convars_AddGlobalChangeListener);
DEFINE_NATIVE("Convars.RemoveGlobalChangeListener", Bridge_Convars_RemoveGlobalChangeListener);
DEFINE_NATIVE("Convars.AddConvarCreatedListener", Bridge_Convars_AddConvarCreatedListener);
DEFINE_NATIVE("Convars.RemoveConvarCreatedListener", Bridge_Convars_RemoveConvarCreatedListener);
DEFINE_NATIVE("Convars.AddConCommandCreatedListener", Bridge_Convars_AddConCommandCreatedListener);
DEFINE_NATIVE("Convars.RemoveConCommandCreatedListener", Bridge_Convars_RemoveConCommandCreatedListener);
DEFINE_NATIVE("Convars.CreateConvar", Bridge_Convars_CreateConvar);
DEFINE_NATIVE("Convars.DeleteConvar", Bridge_Convars_DeleteConvar);
DEFINE_NATIVE("Convars.ExistsConvar", Bridge_Convars_ExistsConvar);
DEFINE_NATIVE("Convars.GetConvarType", Bridge_Convars_GetConvarType);
DEFINE_NATIVE("Convars.GetFlags", Bridge_Convars_GetFlags);
DEFINE_NATIVE("Convars.SetFlags", Bridge_Convars_SetFlags);
DEFINE_NATIVE("Convars.GetMinValuePtrPtr", Bridge_Convars_GetMinValuePtrPtr);
DEFINE_NATIVE("Convars.GetMaxValuePtrPtr", Bridge_Convars_GetMaxValuePtrPtr);
DEFINE_NATIVE("Convars.HasDefaultValue", Bridge_Convars_HasDefaultValue);
DEFINE_NATIVE("Convars.GetDefaultValuePtr", Bridge_Convars_GetDefaultValuePtr);
DEFINE_NATIVE("Convars.GetValuePtr", Bridge_Convars_GetValuePtr);
DEFINE_NATIVE("Convars.SetValuePtr", Bridge_Convars_SetValuePtr);
DEFINE_NATIVE("Convars.SetValueInternalPtr", Bridge_Convars_SetValueInternalPtr);
DEFINE_NATIVE("Convars.SetValueAsString", Bridge_Convars_SetValueAsString);
DEFINE_NATIVE("Convars.GetValueAsString", Bridge_Convars_GetValueAsString);
DEFINE_NATIVE("Convars.SetDefaultValueAsString", Bridge_Convars_SetDefaultValueAsString);
DEFINE_NATIVE("Convars.GetDefaultValueAsString", Bridge_Convars_GetDefaultValueAsString);
DEFINE_NATIVE("Convars.SetMinValueAsString", Bridge_Convars_SetMinValueAsString);
DEFINE_NATIVE("Convars.GetMinValueAsString", Bridge_Convars_GetMinValueAsString);
DEFINE_NATIVE("Convars.SetMaxValueAsString", Bridge_Convars_SetMaxValueAsString);
DEFINE_NATIVE("Convars.GetMaxValueAsString", Bridge_Convars_GetMaxValueAsString);
DEFINE_NATIVE("Convars.SetValueInternalAsString", Bridge_Convars_SetValueInternalAsString);
DEFINE_NATIVE("Convars.GetDescription", Bridge_Convars_GetDescription);