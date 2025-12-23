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

#include "soundevent.h"

#include <api/interfaces/manager.h>

#include <public/networksystem/inetworkmessages.h>
#include <public/engine/igameeventsystem.h>

#include <api/memory/virtual/call.h>

#include "gameevents.pb.h"

#define GETCHECK_FIELD(return_value)								\
	SosField* field = GetField(name);					            \
	if (field == nullptr)											\
		return return_value;

#define CHECK_FIELD_TYPE(check_type, return_value)					\
    if (field->GetType() != check_type) return return_value;

#define SET_FIELD(check_type, value)								\
	SosField field(check_type, &value);								\
	AddOrReplaceField(name, field);

#define RETURN_FIELD(return_type)										\
	return *reinterpret_cast<const return_type*>(field->GetData());

void insert(std::vector<uint8_t>& vec, const void* value, uint8_t size) {
    const char* bytes = reinterpret_cast<const char*>(value);

    for (int i = 0; i < size; i++) {
        vec.push_back(bytes[i]);
    }
}

extern bool bypassPostEventAbstractHook;

uint32_t CSoundEvent::Emit()
{
    // packed_param serialization
    std::vector<uint8_t> buffer;
    for (const auto& pair : this->m_mParameters) {
        auto fieldName = pair.first;
        auto fieldData = &pair.second;
        uint32_t paramNameHash = MurmurHash2LowerCase(fieldName.c_str(), SOUNDEVENT_FIELD_NAME_HASH_SEED);
        insert(buffer, &paramNameHash, 4);													// name hash
        buffer.push_back(fieldData->GetType());												// data type
        buffer.push_back(SosFieldTypeSize(fieldData->GetType()));							// data size
        buffer.push_back(0);																// padding
        insert(buffer, fieldData->GetData(), SosFieldTypeSize(fieldData->GetType()));		// data
    }

    auto networkmessage = g_ifaceService.FetchInterface<INetworkMessages>(NETWORKMESSAGES_INTERFACE_VERSION);
    auto gameEventSystem = g_ifaceService.FetchInterface<IGameEventSystem>(GAMEEVENTSYSTEM_INTERFACE_VERSION);
    auto soundsystem = g_ifaceService.FetchInterface<ISoundSystem>(SOUNDSYSTEM_INTERFACE_VERSION);
    static auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);

    auto soundeventHash = MurmurHash2LowerCase(m_sName.c_str(), SOUNDEVENT_NAME_HASH_SEED);
    INetworkMessageInternal* pNetMsg = networkmessage->FindNetworkMessageById(208);
    auto data = pNetMsg->AllocateMessage()->ToPB<CMsgSosStartSoundEvent>();

    uint32_t guid;
#ifdef _WIN32
    CALL_VIRTUAL(void, gamedata->GetOffsets()->Fetch("CSoundSystem::TakeGuid"), soundsystem, &guid);
#else
    guid = CALL_VIRTUAL(uint32_t, gamedata->GetOffsets()->Fetch("CSoundSystem::TakeGuid"), soundsystem);
#endif

    data->set_soundevent_hash(soundeventHash);
    data->set_source_entity_index(m_iSourceEntityIndex);
    data->set_soundevent_guid(guid);
    data->set_seed(guid);
    data->set_packed_params(std::string(buffer.begin(), buffer.end()));

    bypassPostEventAbstractHook = true;

    gameEventSystem->PostEventAbstract(-1, false, &m_fClients, pNetMsg, data, 0);

    bypassPostEventAbstractHook = false;

    delete data;

    return guid;
}
void CSoundEvent::SetClientMask(uint64_t mask)
{
    QueueLockGuard lock(m_mtxLock);
    *(reinterpret_cast<uint64_t*>(const_cast<uint32*>(m_fClients.GetRecipients().Base()))) = mask;
}

void CSoundEvent::SetName(const std::string& name)
{
    QueueLockGuard lock(m_mtxLock);
    m_sName = name;
}

std::string& CSoundEvent::GetName()
{
    return m_sName;
}

void CSoundEvent::SetSourceEntityIndex(int index)
{
    QueueLockGuard lock(m_mtxLock);
    m_iSourceEntityIndex = index;
}

int CSoundEvent::GetSourceEntityIndex()
{
    return m_iSourceEntityIndex;
}

void CSoundEvent::SetRecipientFilter(CRecipientFilter filter)
{
    m_fClients = filter;
}

void CSoundEvent::AddClient(int playerid)
{
    QueueLockGuard lock(m_mtxLock);
    m_fClients.AddRecipient(CPlayerSlot(playerid));
}

void CSoundEvent::RemoveClient(int playerid)
{
    QueueLockGuard lock(m_mtxLock);
    m_fClients.RemoveRecipient(CPlayerSlot(playerid));
}

void CSoundEvent::ClearClients()
{
    QueueLockGuard lock(m_mtxLock);
    m_fClients.RemoveAllPlayers();
}

void CSoundEvent::AddAllClients()
{
    QueueLockGuard lock(m_mtxLock);
    m_fClients.AddAllPlayers();
}

std::vector<int> CSoundEvent::GetClients()
{
    std::vector<int> clns;
    auto recipients = m_fClients.GetRecipients();
    if (m_fClients.GetRecipientCount() == 0) return clns;

    for (int i = 0; i < 64; i++)
        if (recipients.IsBitSet(i))
            clns.push_back(i);

    return clns;
}

SosField* CSoundEvent::GetField(std::string pszFieldName)
{
    auto it = m_mParameters.find(pszFieldName);
    return (it != m_mParameters.end()) ? &it->second : nullptr;
}

void CSoundEvent::AddOrReplaceField(std::string pszFieldName, const SosField field)
{
    QueueLockGuard lock(m_mtxLock);
    m_mParameters[pszFieldName] = field;
}

bool CSoundEvent::HasField(const std::string& name)
{
    return this->GetField(name) != nullptr;
}

void CSoundEvent::SetBool(const std::string& name, bool value)
{
    QueueLockGuard lock(m_mtxLock);
    SET_FIELD(SE_Bool, value);
}

bool CSoundEvent::GetBool(const std::string& name)
{
    GETCHECK_FIELD(false);
    CHECK_FIELD_TYPE(SE_Bool, false);
    RETURN_FIELD(bool);
}

void CSoundEvent::SetInt32(const std::string& name, int32_t value)
{
    QueueLockGuard lock(m_mtxLock);
    SET_FIELD(SE_Int, value);
}

int32_t CSoundEvent::GetInt32(const std::string& name)
{
    GETCHECK_FIELD(0);
    CHECK_FIELD_TYPE(SE_Int, 0);
    RETURN_FIELD(int32_t);
}

void CSoundEvent::SetUInt32(const std::string& name, uint32_t value)
{
    QueueLockGuard lock(m_mtxLock);
    SET_FIELD(SE_UInt32, value);
}

uint32_t CSoundEvent::GetUInt32(const std::string& name)
{
    GETCHECK_FIELD(0);
    CHECK_FIELD_TYPE(SE_UInt32, 0);
    RETURN_FIELD(uint32_t);
}

void CSoundEvent::SetUInt64(const std::string& name, uint64_t value)
{
    QueueLockGuard lock(m_mtxLock);
    SET_FIELD(SE_UInt64, value);
}

uint64_t CSoundEvent::GetUInt64(const std::string& name)
{
    GETCHECK_FIELD(0);
    CHECK_FIELD_TYPE(SE_UInt64, 0);
    RETURN_FIELD(uint64_t);
}

void CSoundEvent::SetFloat(const std::string& name, float value)
{
    QueueLockGuard lock(m_mtxLock);
    SET_FIELD(SE_Float, value);
}

float CSoundEvent::GetFloat(const std::string& name)
{
    GETCHECK_FIELD(0.0f);
    CHECK_FIELD_TYPE(SE_Float, 0.0f);
    RETURN_FIELD(float);
}

void CSoundEvent::SetFloat3(const std::string& name, Vector value)
{
    QueueLockGuard lock(m_mtxLock);
    SET_FIELD(SE_Float3, value);
}

Vector CSoundEvent::GetFloat3(const std::string& name)
{
    Vector def(0, 0, 0);
    GETCHECK_FIELD(def);
    CHECK_FIELD_TYPE(SE_Float3, def);
    RETURN_FIELD(Vector);
}