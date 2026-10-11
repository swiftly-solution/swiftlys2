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

#ifndef src_engine_convars_convars_h
#define src_engine_convars_convars_h

#include <api/engine/convars/convars.h>
#include <api/utils/mutex.h>

#include <public/icvar.h>
#include <public/tier1/convar.h>

#include <map>
#include <optional>
#include <unordered_map>

// lookups by const char* or string_view without creating a std::string
struct ConvarNameHash
{
    using is_transparent = void;

    size_t operator()(std::string_view name) const { return std::hash<std::string_view>{}(name); }
};

class CConvarManager : public IConvarManager, public IConVarListener
{
public:
    virtual void Initialize() override;
    virtual void Shutdown() override;

    virtual void QueryClientConvar(int playerid, const std::string& cvar_name) override;
    virtual int AddQueryClientCvarCallback(std::function<void(int, std::string, std::string)> callback) override;
    virtual void RemoveQueryClientCvarCallback(int callback_id) override;
    virtual void OnClientQueryCvar(int playerid, const std::string& cvar_name, const std::string& cvar_value) override;
    virtual void OnConvarChanged(ConVarRefAbstract* ref, CSplitScreenSlot slot, const char* new_value, const char* old_value) override;

    virtual void CreateConvar(const std::string& cvar_name, EConVarType type, uint64_t flags, const char* help_message, const ConvarValue& defaultValue, const std::optional<ConvarValue>& minValue = std::nullopt, const std::optional<ConvarValue>& maxValue = std::nullopt) override;
    virtual void DeleteConvar(const std::string& cvar_name) override;
    virtual bool ExistsConvar(const std::string& cvar_name) override;
    virtual EConVarType GetConvarType(const std::string& cvar_name) override;

    virtual ConVarRefAbstract& GetConvarRef(const char* cvar_name) override;

    virtual void* GetConvarDataAddress(const std::string& cvar_name) override;
    virtual ConvarValue GetConvarValue(const std::string& cvar_name) override;

    virtual void SetConvarValue(const std::string& cvar_name, const ConvarValue& value) override;
    virtual void SetClientConvar(int playerid, const std::string& cvar_name, const std::string& value) override;

    virtual void AddFlags(const std::string& cvar_name, uint64_t flags) override;
    virtual void RemoveFlags(const std::string& cvar_name, uint64_t flags) override;
    virtual void ClearFlags(const std::string& cvar_name) override;
    virtual uint64_t GetFlags(const std::string& cvar_name) override;

    virtual uint64_t AddGlobalChangeListener(std::function<void(const char*, int, const char*, const char*)> callback) override;
    virtual void RemoveGlobalChangeListener(uint64_t callback_id) override;

    virtual uint64_t AddConvarCreatedListener(std::function<void(const char*)> callback) override;
    virtual void RemoveConvarCreatedListener(uint64_t callback_id) override;

    virtual uint64_t AddConCommandCreatedListener(std::function<void(const char*)> callback) override;
    virtual void RemoveConCommandCreatedListener(uint64_t callback_id) override;

    // IConVarListener
    virtual void OnConVarCreated(ConVarRefAbstract* pNewCvar) override;
    virtual void OnConCommandCreated(ConCommand* pNewCommand) override;
private:
    struct CreatedConvar
    {
        void* convar = nullptr;
        void (*free)(void*) = nullptr;
    };

    template <typename Callback>
    using Listeners = std::map<uint64_t, Callback>;

    // installs or removes the engine callbacks depending on which listeners exist
    void UpdateChangeCallback();
    void UpdateCreationListener();

    QueueMutex m_mtxListeners;
    uint64_t m_lastListenerId = 0;

    Listeners<std::function<void(const char*, int, const char*, const char*)>> m_changeListeners;
    Listeners<std::function<void(const char*)>> m_convarCreatedListeners;
    Listeners<std::function<void(const char*)>> m_conCommandCreatedListeners;
    bool m_bChangeCallbackInstalled = false;
    bool m_bCreationListenerInstalled = false;

    QueueMutex m_mtxQueryCallbacks;
    int m_lastQueryCallbackId = 0;
    std::map<int, std::function<void(int, std::string, std::string)>> m_queryCallbacks;

    QueueMutex m_mtxConvars;
    std::map<std::string, CreatedConvar> m_createdConvars;
    std::unordered_map<std::string, ConVarRefAbstract, ConvarNameHash, std::equal_to<>> m_convarRefs;
};

#endif
