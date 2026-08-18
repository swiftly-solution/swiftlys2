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
#include <public/tier1/convar.h>
#include <public/tier1/utlstring.h>
#include <scripting/scripting.h>

#include <optional>
#include <variant>

std::map<std::string, ConVarRefAbstract> convarRefCache;
extern bool bypassConvarCallbacks;

static char* Bridge_Convars_CopyString(const char* value, int* size)
{
    int outSize = value ? static_cast<int>(strlen(value)) : 0;
    *size = outSize;

    char* out = (char*)g_pMemoryAllocator->Alloc(outSize + 1);
    if (outSize > 0)
    {
        g_pMemoryAllocator->Copy(out, (void*)value, outSize);
    }

    out[outSize] = '\0';
    return out;
}

ConVarRefAbstract& GetConVarRef(const char* cvarName)
{
    std::string name(cvarName);
    auto it = convarRefCache.find(name);
    if (it != convarRefCache.end())
    {
        return it->second;
    }
    else
    {
        ConVarRefAbstract ref(cvarName);
        convarRefCache[name] = ref;
        return convarRefCache[name];
    }
}

void Bridge_Convars_QueryClientConvar(int playerid, const char* cvarName)
{
    g_pConvarManager->QueryClientConvar(playerid, cvarName);
}

int Bridge_Convars_AddQueryClientCvarCallback(void* callback)
{
    return g_pConvarManager->AddQueryClientCvarCallback([callback](int playerid, std::string cvarName, std::string cvarValue) -> void {
        ((void(*)(int, const char*, const char*))callback)(playerid, cvarName.c_str(), cvarValue.c_str());
        });
}

void Bridge_Convars_RemoveQueryClientCvarCallback(int callbackId)
{
    g_pConvarManager->RemoveQueryClientCvarCallback(callbackId);
}

void Bridge_Convars_CreateConvarInt16(const char* convarName, int cvarType, uint64_t cvarFlags, const char* helpMessage, int16_t value, int16_t* minValue, int16_t* maxValue)
{
    std::optional<ConvarValue> minValueOptional;
    std::optional<ConvarValue> maxValueOptional;
    if (minValue != nullptr) minValueOptional = *minValue;
    if (maxValue != nullptr) maxValueOptional = *maxValue;
    g_pConvarManager->CreateConvar(convarName, (EConVarType)cvarType, cvarFlags, helpMessage, value, minValueOptional, maxValueOptional);
}

void Bridge_Convars_CreateConvarUInt16(const char* convarName, int cvarType, uint64_t cvarFlags, const char* helpMessage, uint16_t value, uint16_t* minValue, uint16_t* maxValue)
{
    std::optional<ConvarValue> minValueOptional;
    std::optional<ConvarValue> maxValueOptional;
    if (minValue != nullptr) minValueOptional = *minValue;
    if (maxValue != nullptr) maxValueOptional = *maxValue;
    g_pConvarManager->CreateConvar(convarName, (EConVarType)cvarType, cvarFlags, helpMessage, value, minValueOptional, maxValueOptional);
}

void Bridge_Convars_CreateConvarInt32(const char* convarName, int cvarType, uint64_t cvarFlags, const char* helpMessage, int32_t value, int32_t* minValue, int32_t* maxValue)
{
    std::optional<ConvarValue> minValueOptional;
    std::optional<ConvarValue> maxValueOptional;
    if (minValue != nullptr) minValueOptional = *minValue;
    if (maxValue != nullptr) maxValueOptional = *maxValue;
    g_pConvarManager->CreateConvar(convarName, (EConVarType)cvarType, cvarFlags, helpMessage, value, minValueOptional, maxValueOptional);
}

void Bridge_Convars_CreateConvarUInt32(const char* convarName, int cvarType, uint64_t cvarFlags, const char* helpMessage, uint32_t value, uint32_t* minValue, uint32_t* maxValue)
{
    std::optional<ConvarValue> minValueOptional;
    std::optional<ConvarValue> maxValueOptional;
    if (minValue != nullptr) minValueOptional = *minValue;
    if (maxValue != nullptr) maxValueOptional = *maxValue;
    g_pConvarManager->CreateConvar(convarName, (EConVarType)cvarType, cvarFlags, helpMessage, value, minValueOptional, maxValueOptional);
}

void Bridge_Convars_CreateConvarInt64(const char* convarName, int cvarType, uint64_t cvarFlags, const char* helpMessage, int64_t value, int64_t* minValue, int64_t* maxValue)
{
    std::optional<ConvarValue> minValueOptional;
    std::optional<ConvarValue> maxValueOptional;
    if (minValue != nullptr) minValueOptional = *minValue;
    if (maxValue != nullptr) maxValueOptional = *maxValue;
    g_pConvarManager->CreateConvar(convarName, (EConVarType)cvarType, cvarFlags, helpMessage, value, minValueOptional, maxValueOptional);
}

void Bridge_Convars_CreateConvarUInt64(const char* convarName, int cvarType, uint64_t cvarFlags, const char* helpMessage, uint64_t value, uint64_t* minValue, uint64_t* maxValue)
{
    std::optional<ConvarValue> minValueOptional;
    std::optional<ConvarValue> maxValueOptional;
    if (minValue != nullptr) minValueOptional = *minValue;
    if (maxValue != nullptr) maxValueOptional = *maxValue;
    g_pConvarManager->CreateConvar(convarName, (EConVarType)cvarType, cvarFlags, helpMessage, value, minValueOptional, maxValueOptional);
}

void Bridge_Convars_CreateConvarBool(const char* convarName, int cvarType, uint64_t cvarFlags, const char* helpMessage, bool value, bool* minValue, bool* maxValue)
{
    g_pConvarManager->CreateConvar(convarName, (EConVarType)cvarType, cvarFlags, helpMessage, value);
}

void Bridge_Convars_CreateConvarFloat(const char* convarName, int cvarType, uint64_t cvarFlags, const char* helpMessage, float value, float* minValue, float* maxValue)
{
    std::optional<ConvarValue> minValueOptional;
    std::optional<ConvarValue> maxValueOptional;
    if (minValue != nullptr) minValueOptional = *minValue;
    if (maxValue != nullptr) maxValueOptional = *maxValue;
    g_pConvarManager->CreateConvar(convarName, (EConVarType)cvarType, cvarFlags, helpMessage, value, minValueOptional, maxValueOptional);
}

void Bridge_Convars_CreateConvarDouble(const char* convarName, int cvarType, uint64_t cvarFlags, const char* helpMessage, double value, double* minValue, double* maxValue)
{
    std::optional<ConvarValue> minValueOptional;
    std::optional<ConvarValue> maxValueOptional;
    if (minValue != nullptr) minValueOptional = *minValue;
    if (maxValue != nullptr) maxValueOptional = *maxValue;
    g_pConvarManager->CreateConvar(convarName, (EConVarType)cvarType, cvarFlags, helpMessage, value, minValueOptional, maxValueOptional);
}

void Bridge_Convars_CreateConvarColor(const char* convarName, int cvarType, uint64_t cvarFlags, const char* helpMessage, Color value, Color* minValue, Color* maxValue)
{
    g_pConvarManager->CreateConvar(convarName, (EConVarType)cvarType, cvarFlags, helpMessage, value);
}

void Bridge_Convars_CreateConvarVector2D(const char* convarName, int cvarType, uint64_t cvarFlags, const char* helpMessage, Vector2D value, Vector2D* minValue, Vector2D* maxValue)
{
    g_pConvarManager->CreateConvar(convarName, (EConVarType)cvarType, cvarFlags, helpMessage, value);
}

void Bridge_Convars_CreateConvarVector(const char* convarName, int cvarType, uint64_t cvarFlags, const char* helpMessage, Vector value, Vector* minValue, Vector* maxValue)
{
    g_pConvarManager->CreateConvar(convarName, (EConVarType)cvarType, cvarFlags, helpMessage, value);
}

void Bridge_Convars_CreateConvarVector4D(const char* convarName, int cvarType, uint64_t cvarFlags, const char* helpMessage, Vector4D value, Vector4D* minValue, Vector4D* maxValue)
{
    g_pConvarManager->CreateConvar(convarName, (EConVarType)cvarType, cvarFlags, helpMessage, value);
}

void Bridge_Convars_CreateConvarQAngle(const char* convarName, int cvarType, uint64_t cvarFlags, const char* helpMessage, QAngle value, QAngle* minValue, QAngle* maxValue)
{
    g_pConvarManager->CreateConvar(convarName, (EConVarType)cvarType, cvarFlags, helpMessage, value);
}

void Bridge_Convars_CreateConvarString(const char* convarName, int cvarType, uint64_t cvarFlags, const char* helpMessage, const char* value, const char* minValue, const char* maxValue)
{
    g_pConvarManager->CreateConvar(convarName, (EConVarType)cvarType, cvarFlags, helpMessage, std::string(value));
}

void Bridge_Convars_DeleteConvar(const char* convarName)
{
    g_pConvarManager->DeleteConvar(convarName);
}

bool Bridge_Convars_ExistsConvar(const char* convarName)
{
    return g_pConvarManager->ExistsConvar(convarName);
}

int Bridge_Convars_GetConvarType(const char* convarName)
{
    return (int)(g_pConvarManager->GetConvarType(convarName));
}

uint64_t Bridge_Convars_GetFlags(const char* cvarName)
{
    auto& cvar = GetConVarRef(cvarName);
    return cvar.GetConVarData()->m_nFlags;
}

void Bridge_Convars_SetFlags(const char* cvarName, uint64_t flags)
{
    auto& cvar = GetConVarRef(cvarName);
    cvar.GetConVarData()->m_nFlags = flags;
}

uint64_t Bridge_Convars_AddGlobalChangeListener(void* callback)
{
    return g_pConvarManager->AddGlobalChangeListener([callback](const char* convarName, int slot, const char* newValue, const char* oldValue) -> void {
        ((void(*)(const char*, int, const char*, const char*))callback)(convarName, slot, newValue, oldValue);
        });
}

void Bridge_Convars_RemoveGlobalChangeListener(uint64_t listenerID)
{
    g_pConvarManager->RemoveGlobalChangeListener(listenerID);
}

uint64_t Bridge_Convars_AddConvarCreatedListener(void* callback)
{
    return g_pConvarManager->AddConvarCreatedListener([callback](const char* convarName) -> void {
        ((void(*)(const char*))callback)(convarName);
        });
}

void Bridge_Convars_RemoveConvarCreatedListener(uint64_t listenerID)
{
    g_pConvarManager->RemoveConvarCreatedListener(listenerID);
}

uint64_t Bridge_Convars_AddConCommandCreatedListener(void* callback)
{
    return g_pConvarManager->AddConCommandCreatedListener([callback](const char* convarName) -> void {
        ((void(*)(const char*))callback)(convarName);
        });
}

void Bridge_Convars_RemoveConCommandCreatedListener(uint64_t listenerID)
{
    g_pConvarManager->RemoveConCommandCreatedListener(listenerID);
}

void* Bridge_Convars_GetMinValuePtrPtr(const char* cvarName)
{
    auto& cvar = GetConVarRef(cvarName);
    return &cvar.GetConVarData()->m_minValue;
}

void* Bridge_Convars_GetMaxValuePtrPtr(const char* cvarName)
{
    auto& cvar = GetConVarRef(cvarName);
    return &cvar.GetConVarData()->m_maxValue;
}

bool Bridge_Convars_HasDefaultValue(const char* cvarName)
{
    auto& cvar = GetConVarRef(cvarName);
    return cvar.HasDefault();
}

void* Bridge_Convars_GetDefaultValuePtr(const char* cvarName)
{
    auto& cvar = GetConVarRef(cvarName);
    return cvar.GetConVarData()->DefaultValue();
}

void Bridge_Convars_SetDefaultValue(const char* cvarName, void* defaultValue)
{
    auto& cvar = GetConVarRef(cvarName);
    cvar.GetConVarData()->SetDefaultValue((CVValue_t*)defaultValue);
}

void Bridge_Convars_SetDefaultValueString(const char* cvarName, const char* defaultValue)
{
    auto& cvar = GetConVarRef(cvarName);
    CUtlString string(defaultValue);
    CVValue_t value(string);
    cvar.GetConVarData()->SetDefaultValue(&value);
}

void* Bridge_Convars_GetValuePtr(const char* cvarName)
{
    auto& cvar = GetConVarRef(cvarName);
    return cvar.GetConVarData()->Value(0);
}

void Bridge_Convars_SetValuePtr(const char* cvarName, void* value)
{
    auto& cvar = GetConVarRef(cvarName);
    cvar.SetOrQueueValueInternal(0, (CVValue_t*)value);
}

void Bridge_Convars_SetValueInternalPtr(const char* cvarName, void* value)
{
    bypassConvarCallbacks = true;
    auto& cvar = GetConVarRef(cvarName);
    cvar.SetValueInternal(0, (CVValue_t*)value);
    bypassConvarCallbacks = false;
}

bool Bridge_Convars_SetValueAsString(const char* cvarName, const char* value)
{
    bypassConvarCallbacks = true;
    auto& cvar = GetConVarRef(cvarName);
    bool result = cvar.SetString(CUtlString(value), CSplitScreenSlot(0));
    bypassConvarCallbacks = false;

    return result;
}

char* Bridge_Convars_GetValueAsString(int* size, const char* cvarName)
{
    auto& cvar = GetConVarRef(cvarName);
    CBufferString buf;
    cvar.GetValueAsString(buf, CSplitScreenSlot(0));

    return Bridge_Convars_CopyString(buf.Get(), size);
}

bool Bridge_Convars_SetDefaultValueAsString(const char* cvarName, const char* defaultValue)
{
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
    auto& cvar = GetConVarRef(cvarName);
    CBufferString buf;
    cvar.GetConVarData()->DefaultValueToString(buf);

    return Bridge_Convars_CopyString(buf.Get(), size);
}

char* Bridge_Convars_GetMinValueAsString(int* size, const char* cvarName)
{
    auto& cvar = GetConVarRef(cvarName);
    CBufferString buf;
    cvar.GetConVarData()->MinValueToString(buf);

    return Bridge_Convars_CopyString(buf.Get(), size);
}

bool Bridge_Convars_SetMinValueAsString(const char* cvarName, const char* minValue)
{
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
    auto& cvar = GetConVarRef(cvarName);
    CBufferString buf;
    cvar.GetConVarData()->MaxValueToString(buf);

    return Bridge_Convars_CopyString(buf.Get(), size);
}

bool Bridge_Convars_SetMaxValueAsString(const char* cvarName, const char* maxValue)
{
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
    auto& cvar = GetConVarRef(cvarName);
    CVValue_t v;
    cvar.GetConVarData()->TypeTraits()->StringToValue(value, &v);
    cvar.SetValueInternal(0, &v);
}

char* Bridge_Convars_GetDescription(int* size, const char* cvarName)
{
    auto& cvar = GetConVarRef(cvarName);
    std::string s = cvar.GetHelpText();

    return Bridge_Convars_CopyString(s.c_str(), size);
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
DEFINE_NATIVE("Convars.CreateConvarInt16", Bridge_Convars_CreateConvarInt16);
DEFINE_NATIVE("Convars.CreateConvarUInt16", Bridge_Convars_CreateConvarUInt16);
DEFINE_NATIVE("Convars.CreateConvarInt32", Bridge_Convars_CreateConvarInt32);
DEFINE_NATIVE("Convars.CreateConvarUInt32", Bridge_Convars_CreateConvarUInt32);
DEFINE_NATIVE("Convars.CreateConvarInt64", Bridge_Convars_CreateConvarInt64);
DEFINE_NATIVE("Convars.CreateConvarUInt64", Bridge_Convars_CreateConvarUInt64);
DEFINE_NATIVE("Convars.CreateConvarBool", Bridge_Convars_CreateConvarBool);
DEFINE_NATIVE("Convars.CreateConvarFloat", Bridge_Convars_CreateConvarFloat);
DEFINE_NATIVE("Convars.CreateConvarDouble", Bridge_Convars_CreateConvarDouble);
DEFINE_NATIVE("Convars.CreateConvarColor", Bridge_Convars_CreateConvarColor);
DEFINE_NATIVE("Convars.CreateConvarVector2D", Bridge_Convars_CreateConvarVector2D);
DEFINE_NATIVE("Convars.CreateConvarVector", Bridge_Convars_CreateConvarVector);
DEFINE_NATIVE("Convars.CreateConvarVector4D", Bridge_Convars_CreateConvarVector4D);
DEFINE_NATIVE("Convars.CreateConvarQAngle", Bridge_Convars_CreateConvarQAngle);
DEFINE_NATIVE("Convars.CreateConvarString", Bridge_Convars_CreateConvarString);
DEFINE_NATIVE("Convars.DeleteConvar", Bridge_Convars_DeleteConvar);
DEFINE_NATIVE("Convars.ExistsConvar", Bridge_Convars_ExistsConvar);
DEFINE_NATIVE("Convars.GetConvarType", Bridge_Convars_GetConvarType);
DEFINE_NATIVE("Convars.GetFlags", Bridge_Convars_GetFlags);
DEFINE_NATIVE("Convars.SetFlags", Bridge_Convars_SetFlags);
DEFINE_NATIVE("Convars.GetMinValuePtrPtr", Bridge_Convars_GetMinValuePtrPtr);
DEFINE_NATIVE("Convars.GetMaxValuePtrPtr", Bridge_Convars_GetMaxValuePtrPtr);
DEFINE_NATIVE("Convars.HasDefaultValue", Bridge_Convars_HasDefaultValue);
DEFINE_NATIVE("Convars.GetDefaultValuePtr", Bridge_Convars_GetDefaultValuePtr);
DEFINE_NATIVE("Convars.SetDefaultValue", Bridge_Convars_SetDefaultValue);
DEFINE_NATIVE("Convars.SetDefaultValueString", Bridge_Convars_SetDefaultValueString);
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