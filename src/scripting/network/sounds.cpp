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

void* Bridge_Sounds_CreateSoundEvent()
{
    auto soundsmanager = g_ifaceService.FetchInterface<ISoundEventManager>(SOUNDEVENTMANAGER_INTERFACE_VERSION);
    return soundsmanager->CreateSoundEvent();
}

void Bridge_Sounds_DestroySoundEvent(void* event)
{
    auto soundsmanager = g_ifaceService.FetchInterface<ISoundEventManager>(SOUNDEVENTMANAGER_INTERFACE_VERSION);
    soundsmanager->DestroySoundEvent((ISoundEvent*)event);
}

uint32_t Bridge_Sounds_Emit(void* event)
{
    return ((ISoundEvent*)event)->Emit();
}

void Bridge_Sounds_SetName(void* event, const char* name)
{
    ((ISoundEvent*)event)->SetName(name);
}

int Bridge_Sounds_GetName(char* buffer, void* event)
{
    static std::string s;
    s = ((ISoundEvent*)event)->GetName();

    if (buffer != nullptr) {
        strcpy(buffer, s.c_str());
    }

    return s.size();
}

void Bridge_Sounds_SetSourceEntityIndex(void* event, int index)
{
    ((ISoundEvent*)event)->SetSourceEntityIndex(index);
}

int Bridge_Sounds_GetSourceEntityIndex(void* event)
{
    return ((ISoundEvent*)event)->GetSourceEntityIndex();
}

void Bridge_Sounds_AddClient(void* event, int clientID)
{
    ((ISoundEvent*)event)->AddClient(clientID);
}

void Bridge_Sounds_RemoveClient(void* event, int clientID)
{
    ((ISoundEvent*)event)->RemoveClient(clientID);
}

void Bridge_Sounds_ClearClients(void* event)
{
    ((ISoundEvent*)event)->ClearClients();
}

void Bridge_Sounds_AddAllClients(void* event)
{
    ((ISoundEvent*)event)->AddAllClients();
}

bool Bridge_Sounds_HasField(void* event, const char* fieldName)
{
    return ((ISoundEvent*)event)->HasField(fieldName);
}

void Bridge_Sounds_SetBool(void* event, const char* fieldName, bool value)
{
    ((ISoundEvent*)event)->SetBool(fieldName, value);
}

bool Bridge_Sounds_GetBool(void* event, const char* fieldName)
{
    return ((ISoundEvent*)event)->GetBool(fieldName);
}

void Bridge_Sounds_SetInt32(void* event, const char* fieldName, int32_t value)
{
    ((ISoundEvent*)event)->SetInt32(fieldName, value);
}

int32_t Bridge_Sounds_GetInt32(void* event, const char* fieldName)
{
    return ((ISoundEvent*)event)->GetInt32(fieldName);
}

void Bridge_Sounds_SetUInt32(void* event, const char* fieldName, uint32_t value)
{
    ((ISoundEvent*)event)->SetUInt32(fieldName, value);
}

uint32_t Bridge_Sounds_GetUInt32(void* event, const char* fieldName)
{
    return ((ISoundEvent*)event)->GetUInt32(fieldName);
}

void Bridge_Sounds_SetUInt64(void* event, const char* fieldName, uint64_t value)
{
    ((ISoundEvent*)event)->SetUInt64(fieldName, value);
}

uint64_t Bridge_Sounds_GetUInt64(void* event, const char* fieldName)
{
    return ((ISoundEvent*)event)->GetUInt64(fieldName);
}

void Bridge_Sounds_SetFloat(void* event, const char* fieldName, float value)
{
    ((ISoundEvent*)event)->SetFloat(fieldName, value);
}

float Bridge_Sounds_GetFloat(void* event, const char* fieldName)
{
    return ((ISoundEvent*)event)->GetFloat(fieldName);
}

void Bridge_Sounds_SetFloat3(void* event, const char* fieldName, Vector value)
{
    ((ISoundEvent*)event)->SetFloat3(fieldName, value);
}

Vector Bridge_Sounds_GetFloat3(void* event, const char* fieldName)
{
    return ((ISoundEvent*)event)->GetFloat3(fieldName);
}

uint64_t Bridge_Sounds_GetClients(void* event)
{
    auto vec = ((ISoundEvent*)event)->GetClients();

    uint64_t res = 0;
    for (int i = 0; i < vec.size(); i++)
        res |= (1ull << vec[i]);

    return res;
}

void Bridge_Sounds_SetClients(void* event, uint64_t clients)
{
    ((ISoundEvent*)event)->SetClientMask(clients);
}

DEFINE_NATIVE("Sounds.CreateSoundEvent", Bridge_Sounds_CreateSoundEvent);
DEFINE_NATIVE("Sounds.DestroySoundEvent", Bridge_Sounds_DestroySoundEvent);
DEFINE_NATIVE("Sounds.Emit", Bridge_Sounds_Emit);
DEFINE_NATIVE("Sounds.SetName", Bridge_Sounds_SetName);
DEFINE_NATIVE("Sounds.GetName", Bridge_Sounds_GetName);
DEFINE_NATIVE("Sounds.SetSourceEntityIndex", Bridge_Sounds_SetSourceEntityIndex);
DEFINE_NATIVE("Sounds.GetSourceEntityIndex", Bridge_Sounds_GetSourceEntityIndex);
DEFINE_NATIVE("Sounds.AddClient", Bridge_Sounds_AddClient);
DEFINE_NATIVE("Sounds.RemoveClient", Bridge_Sounds_RemoveClient);
DEFINE_NATIVE("Sounds.ClearClients", Bridge_Sounds_ClearClients);
DEFINE_NATIVE("Sounds.AddAllClients", Bridge_Sounds_AddAllClients);
DEFINE_NATIVE("Sounds.HasField", Bridge_Sounds_HasField);
DEFINE_NATIVE("Sounds.SetBool", Bridge_Sounds_SetBool);
DEFINE_NATIVE("Sounds.GetBool", Bridge_Sounds_GetBool);
DEFINE_NATIVE("Sounds.SetInt32", Bridge_Sounds_SetInt32);
DEFINE_NATIVE("Sounds.GetInt32", Bridge_Sounds_GetInt32);
DEFINE_NATIVE("Sounds.SetUInt32", Bridge_Sounds_SetUInt32);
DEFINE_NATIVE("Sounds.GetUInt32", Bridge_Sounds_GetUInt32);
DEFINE_NATIVE("Sounds.SetUInt64", Bridge_Sounds_SetUInt64);
DEFINE_NATIVE("Sounds.GetUInt64", Bridge_Sounds_GetUInt64);
DEFINE_NATIVE("Sounds.SetFloat", Bridge_Sounds_SetFloat);
DEFINE_NATIVE("Sounds.GetFloat", Bridge_Sounds_GetFloat);
DEFINE_NATIVE("Sounds.SetFloat3", Bridge_Sounds_SetFloat3);
DEFINE_NATIVE("Sounds.GetFloat3", Bridge_Sounds_GetFloat3);
DEFINE_NATIVE("Sounds.GetClients", Bridge_Sounds_GetClients);
DEFINE_NATIVE("Sounds.SetClients", Bridge_Sounds_SetClients);
