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
#include <mutex>

#include <public/tier0/icvar.h>
#include <public/tier0/convar.h>

#include <functional>
#include <map>
#include <optional>
#include <string>
#include <string_view>
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
    void Initialize() override;
    void Shutdown() override;

    void QueryClientConvar(int playerid, const std::string& cvar_name) override;
    int AddQueryClientCvarCallback(std::function<void(int, std::string, std::string)> callback) override;
    void OnClientQueryCvar(int playerid, const std::string& cvar_name, const std::string& cvar_value) override;
    void OnConvarChanged(ConVarRefAbstract* ref, CSplitScreenSlot slot, const char* new_value, const char* old_value) override;

    void CreateConvar(const std::string& cvar_name, EConVarType type, uint64_t flags, const char* help_message, const ConvarValue& defaultValue, const std::optional<ConvarValue>& minValue = std::nullopt, const std::optional<ConvarValue>& maxValue = std::nullopt) override;

    [[nodiscard]] ConVarRefAbstract& GetConvarRef(const char* cvar_name) override;

    uint64_t AddGlobalChangeListener(std::function<void(const char*, int, const char*, const char*)> callback) override;
    void RemoveGlobalChangeListener(uint64_t callback_id) override;

    uint64_t AddConvarCreatedListener(std::function<void(const char*)> callback) override;
    void RemoveConvarCreatedListener(uint64_t callback_id) override;

    uint64_t AddConCommandCreatedListener(std::function<void(const char*)> callback) override;
    void RemoveConCommandCreatedListener(uint64_t callback_id) override;

    // IConVarListener
    void OnConVarCreated(ConVarRefAbstract* pNewCvar) override;
    void OnConCommandCreated(ConCommand* pNewCommand) override;

private:
    template <typename Callback>
    using Listeners = std::map<uint64_t, Callback>;

    // installs or removes the engine callbacks depending on which listeners exist
    void UpdateChangeCallback();
    void UpdateCreationListener();

    std::mutex m_mtxListeners;
    uint64_t m_lastListenerId = 0;

    Listeners<std::function<void(const char*, int, const char*, const char*)>> m_changeListeners;
    Listeners<std::function<void(const char*)>> m_convarCreatedListeners;
    Listeners<std::function<void(const char*)>> m_conCommandCreatedListeners;
    bool m_bChangeCallbackInstalled = false;
    bool m_bCreationListenerInstalled = false;
    bool m_bUpdatingChangeCallback = false;
    bool m_bUpdatingCreationListener = false;
    bool m_bShuttingDown = false;

    std::mutex m_mtxQueryCallbacks;
    int m_lastQueryCallbackId = 0;
    std::map<int, std::function<void(int, std::string, std::string)>> m_queryCallbacks;

    std::mutex m_mtxConvars;
    std::unordered_map<std::string, ConVarRefAbstract, ConvarNameHash, std::equal_to<>> m_convarRefs;
};

#endif
