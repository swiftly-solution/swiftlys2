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

#ifndef src_api_network_sounds_soundevent_h
#define src_api_network_sounds_soundevent_h

#include <string>
#include <vector>

#include <mathlib/vector.h>

class CRecipientFilter;

#define SOUNDEVENT_NAME_HASH_SEED 0x53524332
#define SOUNDEVENT_FIELD_NAME_HASH_SEED 0x31415926

/*
reversed from game, there's more type than the following,
but we only implement these for now, because we don't know what other types' serialization looks like,
and these should be sufficient in most case
*/

enum SosFieldType {
    SE_Bool = 1,
    SE_Int = 2,
    SE_UInt32 = 3,
    SE_UInt64 = 4,
    SE_Float = 8,
    SE_Float3 = 0xA
};

constexpr uint8_t SosFieldTypeSize(SosFieldType t) {
    switch (t) {
    case SosFieldType::SE_Bool:    return 1;
    case SosFieldType::SE_Int:     return 4;
    case SosFieldType::SE_UInt32:  return 4;
    case SosFieldType::SE_UInt64:  return 8;
    case SosFieldType::SE_Float:   return 4;
    case SosFieldType::SE_Float3:  return 12;
    default:                       return 0;
    }
}

class SosField {
private:
    SosFieldType type;
    char data[32];

public:
    // @samyycX: for container, never use this, i fell guilty
    SosField() {}
    SosField(SosFieldType type, const void* src) {
        int length = SosFieldTypeSize(type);
        this->type = type;
        memcpy(data, src, length);
    }

    SosFieldType GetType() const { return type; }
    const void* GetData() const { return data; }
};

class ISoundEvent
{
public:
    virtual uint32_t Emit() = 0;

    virtual void SetName(const std::string& name) = 0;
    virtual std::string& GetName() = 0;

    virtual void SetSourceEntityIndex(int index) = 0;
    virtual int GetSourceEntityIndex() = 0;

    virtual void SetRecipientFilter(CRecipientFilter filter) = 0;

    virtual void AddClient(int playerid) = 0;
    virtual void RemoveClient(int playerid) = 0;
    virtual void ClearClients() = 0;
    virtual void AddAllClients() = 0;
    virtual void SetClientMask(uint64_t mask) = 0;
    virtual std::vector<int> GetClients() = 0;

    virtual bool HasField(const std::string& name) = 0;

    virtual void SetBool(const std::string& name, bool value) = 0;
    virtual bool GetBool(const std::string& name) = 0;

    virtual void SetInt32(const std::string& name, int32_t value) = 0;
    virtual int32_t GetInt32(const std::string& name) = 0;

    virtual void SetUInt32(const std::string& name, uint32_t value) = 0;
    virtual uint32_t GetUInt32(const std::string& name) = 0;

    virtual void SetUInt64(const std::string& name, uint64_t value) = 0;
    virtual uint64_t GetUInt64(const std::string& name) = 0;

    virtual void SetFloat(const std::string& name, float value) = 0;
    virtual float GetFloat(const std::string& name) = 0;

    virtual void SetFloat3(const std::string& name, Vector value) = 0;
    virtual Vector GetFloat3(const std::string& name) = 0;
};

#endif