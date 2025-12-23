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

#include <api/interfaces/manager.h>
#include <scripting/scripting.h>

#include <public/entity2/entitykeyvalues.h>
#include <public/tier1/utlstringtoken.h>

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

bool Bridge_CEntityKeyValues_GetBool(void* keyvalues, const char* keyName)
{
    return ((CEntityKeyValues*)keyvalues)->GetBool(keyName);
}

int Bridge_CEntityKeyValues_GetInt(void* keyvalues, const char* keyName)
{
    return ((CEntityKeyValues*)keyvalues)->GetInt(keyName);
}

uint32_t Bridge_CEntityKeyValues_GetUint(void* keyvalues, const char* keyName)
{
    return ((CEntityKeyValues*)keyvalues)->GetUint(keyName);
}

int64_t Bridge_CEntityKeyValues_GetInt64(void* keyvalues, const char* keyName)
{
    return ((CEntityKeyValues*)keyvalues)->GetInt64(keyName);
}

uint64_t Bridge_CEntityKeyValues_GetUint64(void* keyvalues, const char* keyName)
{
    return ((CEntityKeyValues*)keyvalues)->GetUint64(keyName);
}

float Bridge_CEntityKeyValues_GetFloat(void* keyvalues, const char* keyName)
{
    return ((CEntityKeyValues*)keyvalues)->GetFloat(keyName);
}

double Bridge_CEntityKeyValues_GetDouble(void* keyvalues, const char* keyName)
{
    return ((CEntityKeyValues*)keyvalues)->GetDouble(keyName);
}

int Bridge_CEntityKeyValues_GetString(char* out, void* keyvalues, const char* keyName)
{
    static std::string s;
    s = ((CEntityKeyValues*)keyvalues)->GetString(keyName);

    if (out) strcpy(out, s.c_str());

    return s.size();
}

void* Bridge_CEntityKeyValues_GetPtr(void* keyvalues, const char* keyName)
{
    return ((CEntityKeyValues*)keyvalues)->GetPtr(keyName);
}

CUtlStringToken Bridge_CEntityKeyValues_GetStringToken(void* keyvalues, const char* keyName)
{
    return ((CEntityKeyValues*)keyvalues)->GetStringToken(keyName);
}

Color Bridge_CEntityKeyValues_GetColor(void* keyvalues, const char* keyName)
{
    return ((CEntityKeyValues*)keyvalues)->GetColor(keyName);
}

Vector Bridge_CEntityKeyValues_GetVector(void* keyvalues, const char* keyName)
{
    return ((CEntityKeyValues*)keyvalues)->GetVector(keyName);
}

Vector2D Bridge_CEntityKeyValues_GetVector2D(void* keyvalues, const char* keyName)
{
    return ((CEntityKeyValues*)keyvalues)->GetVector2D(keyName);
}

Vector4D Bridge_CEntityKeyValues_GetVector4D(void* keyvalues, const char* keyName)
{
    return ((CEntityKeyValues*)keyvalues)->GetVector4D(keyName);
}

QAngle Bridge_CEntityKeyValues_GetQAngle(void* keyvalues, const char* keyName)
{
    return ((CEntityKeyValues*)keyvalues)->GetQAngle(keyName);
}

void Bridge_CEntityKeyValues_SetBool(void* keyvalues, const char* keyName, bool value)
{
    ((CEntityKeyValues*)keyvalues)->SetBool(keyName, value);
}

void Bridge_CEntityKeyValues_SetInt(void* keyvalues, const char* keyName, int value)
{
    ((CEntityKeyValues*)keyvalues)->SetInt(keyName, value);
}

void Bridge_CEntityKeyValues_SetUint(void* keyvalues, const char* keyName, uint32_t value)
{
    ((CEntityKeyValues*)keyvalues)->SetUint(keyName, value);
}

void Bridge_CEntityKeyValues_SetInt64(void* keyvalues, const char* keyName, int64_t value)
{
    ((CEntityKeyValues*)keyvalues)->SetInt64(keyName, value);
}

void Bridge_CEntityKeyValues_SetUint64(void* keyvalues, const char* keyName, uint64_t value)
{
    ((CEntityKeyValues*)keyvalues)->SetUint64(keyName, value);
}

void Bridge_CEntityKeyValues_SetFloat(void* keyvalues, const char* keyName, float value)
{
    ((CEntityKeyValues*)keyvalues)->SetFloat(keyName, value);
}

void Bridge_CEntityKeyValues_SetDouble(void* keyvalues, const char* keyName, double value)
{
    ((CEntityKeyValues*)keyvalues)->SetDouble(keyName, value);
}

void Bridge_CEntityKeyValues_SetString(void* keyvalues, const char* keyName, const char* value)
{
    ((CEntityKeyValues*)keyvalues)->SetString(keyName, value);
}

void Bridge_CEntityKeyValues_SetPtr(void* keyvalues, const char* keyName, void* value)
{
    ((CEntityKeyValues*)keyvalues)->SetPtr(keyName, value);
}

void Bridge_CEntityKeyValues_SetStringToken(void* keyvalues, const char* keyName, CUtlStringToken value)
{
    ((CEntityKeyValues*)keyvalues)->SetStringToken(keyName, value);
}

void Bridge_CEntityKeyValues_SetColor(void* keyvalues, const char* keyName, Color value)
{
    ((CEntityKeyValues*)keyvalues)->SetColor(keyName, value);
}

void Bridge_CEntityKeyValues_SetVector(void* keyvalues, const char* keyName, Vector value)
{
    ((CEntityKeyValues*)keyvalues)->SetVector(keyName, value);
}

void Bridge_CEntityKeyValues_SetVector2D(void* keyvalues, const char* keyName, Vector2D value)
{
    ((CEntityKeyValues*)keyvalues)->SetVector2D(keyName, value);
}

void Bridge_CEntityKeyValues_SetVector4D(void* keyvalues, const char* keyName, Vector4D value)
{
    ((CEntityKeyValues*)keyvalues)->SetVector4D(keyName, value);
}

void Bridge_CEntityKeyValues_SetQAngle(void* keyvalues, const char* keyName, QAngle value)
{
    ((CEntityKeyValues*)keyvalues)->SetQAngle(keyName, value);
}

DEFINE_NATIVE("CEntityKeyValues.Allocate", Bridge_CEntityKeyValues_Allocate);
DEFINE_NATIVE("CEntityKeyValues.Deallocate", Bridge_CEntityKeyValues_Deallocate);
DEFINE_NATIVE("CEntityKeyValues.GetBool", Bridge_CEntityKeyValues_GetBool);
DEFINE_NATIVE("CEntityKeyValues.GetInt", Bridge_CEntityKeyValues_GetInt);
DEFINE_NATIVE("CEntityKeyValues.GetUint", Bridge_CEntityKeyValues_GetUint);
DEFINE_NATIVE("CEntityKeyValues.GetInt64", Bridge_CEntityKeyValues_GetInt64);
DEFINE_NATIVE("CEntityKeyValues.GetUint64", Bridge_CEntityKeyValues_GetUint64);
DEFINE_NATIVE("CEntityKeyValues.GetFloat", Bridge_CEntityKeyValues_GetFloat);
DEFINE_NATIVE("CEntityKeyValues.GetDouble", Bridge_CEntityKeyValues_GetDouble);
DEFINE_NATIVE("CEntityKeyValues.GetString", Bridge_CEntityKeyValues_GetString);
DEFINE_NATIVE("CEntityKeyValues.GetPtr", Bridge_CEntityKeyValues_GetPtr);
DEFINE_NATIVE("CEntityKeyValues.GetStringToken", Bridge_CEntityKeyValues_GetStringToken);
DEFINE_NATIVE("CEntityKeyValues.GetColor", Bridge_CEntityKeyValues_GetColor);
DEFINE_NATIVE("CEntityKeyValues.GetVector", Bridge_CEntityKeyValues_GetVector);
DEFINE_NATIVE("CEntityKeyValues.GetVector2D", Bridge_CEntityKeyValues_GetVector2D);
DEFINE_NATIVE("CEntityKeyValues.GetVector4D", Bridge_CEntityKeyValues_GetVector4D);
DEFINE_NATIVE("CEntityKeyValues.GetQAngle", Bridge_CEntityKeyValues_GetQAngle);
DEFINE_NATIVE("CEntityKeyValues.SetBool", Bridge_CEntityKeyValues_SetBool);
DEFINE_NATIVE("CEntityKeyValues.SetInt", Bridge_CEntityKeyValues_SetInt);
DEFINE_NATIVE("CEntityKeyValues.SetUint", Bridge_CEntityKeyValues_SetUint);
DEFINE_NATIVE("CEntityKeyValues.SetInt64", Bridge_CEntityKeyValues_SetInt64);
DEFINE_NATIVE("CEntityKeyValues.SetUint64", Bridge_CEntityKeyValues_SetUint64);
DEFINE_NATIVE("CEntityKeyValues.SetFloat", Bridge_CEntityKeyValues_SetFloat);
DEFINE_NATIVE("CEntityKeyValues.SetDouble", Bridge_CEntityKeyValues_SetDouble);
DEFINE_NATIVE("CEntityKeyValues.SetString", Bridge_CEntityKeyValues_SetString);
DEFINE_NATIVE("CEntityKeyValues.SetPtr", Bridge_CEntityKeyValues_SetPtr);
DEFINE_NATIVE("CEntityKeyValues.SetStringToken", Bridge_CEntityKeyValues_SetStringToken);
DEFINE_NATIVE("CEntityKeyValues.SetColor", Bridge_CEntityKeyValues_SetColor);
DEFINE_NATIVE("CEntityKeyValues.SetVector", Bridge_CEntityKeyValues_SetVector);
DEFINE_NATIVE("CEntityKeyValues.SetVector2D", Bridge_CEntityKeyValues_SetVector2D);
DEFINE_NATIVE("CEntityKeyValues.SetVector4D", Bridge_CEntityKeyValues_SetVector4D);
DEFINE_NATIVE("CEntityKeyValues.SetQAngle", Bridge_CEntityKeyValues_SetQAngle);