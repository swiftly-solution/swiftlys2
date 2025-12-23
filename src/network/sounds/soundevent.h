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

#ifndef src_network_sounds_soundevent_h
#define src_network_sounds_soundevent_h

#include <api/network/sounds/soundevent.h>
#include <api/sdk/recipientfilter.h>

#include <map>
#include <api/utils/mutex.h>

class CSoundEvent : public ISoundEvent
{
public:
    virtual uint32_t Emit() override;

    virtual void SetName(const std::string& name) override;
    virtual std::string& GetName() override;

    virtual void SetSourceEntityIndex(int index) override;
    virtual int GetSourceEntityIndex() override;

    virtual void SetRecipientFilter(CRecipientFilter filter) override;

    virtual void AddClient(int playerid) override;
    virtual void RemoveClient(int playerid) override;
    virtual void ClearClients() override;
    virtual void AddAllClients() override;
    virtual void SetClientMask(uint64_t mask) override;
    virtual std::vector<int> GetClients() override;

    virtual bool HasField(const std::string& name) override;

    virtual void SetBool(const std::string& name, bool value) override;
    virtual bool GetBool(const std::string& name) override;

    virtual void SetInt32(const std::string& name, int32_t value) override;
    virtual int32_t GetInt32(const std::string& name) override;

    virtual void SetUInt32(const std::string& name, uint32_t value) override;
    virtual uint32_t GetUInt32(const std::string& name) override;

    virtual void SetUInt64(const std::string& name, uint64_t value) override;
    virtual uint64_t GetUInt64(const std::string& name) override;

    virtual void SetFloat(const std::string& name, float value) override;
    virtual float GetFloat(const std::string& name) override;

    virtual void SetFloat3(const std::string& name, Vector value) override;
    virtual Vector GetFloat3(const std::string& name) override;
private:
    std::string m_sName;
    int m_iSourceEntityIndex;
    std::map<std::string, SosField> m_mParameters;
    CRecipientFilter m_fClients;
    QueueMutex m_mtxLock;

    SosField* GetField(std::string pszFieldName);
    void AddOrReplaceField(std::string pszFieldName, const SosField field);
};

#endif