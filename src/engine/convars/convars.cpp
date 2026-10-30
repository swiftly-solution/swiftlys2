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

#include "convars.h"
#include <memory/hooks/vfunction.h>

#include <api/interfaces/interfaces.h>
#include <public/tier0/recipientfilter.h>
#include <public/engine2/serversideclient.h>
#include <api/shared/profiler.h>

#include <optional>
#include <vector>

#include <public/networksystem/inetworkmessages.h>
#include <public/engine2/igameeventsystem.h>

#include <format>

#include "networkbasetypes.pb.h"

#define CONVAR_FLAGS_TO_REMOVE (CVarFlags::HIDDEN | CVarFlags::DEVELOPMENTONLY | CVarFlags::CLIENTDLL)

bool OnConvarQuery(CServerSideClientBase* client, const CNetMessagePB<CCLCMsg_RespondCvarValue>& msg);
VFunctionHook g_ProcessRespondCvarValueHook(reinterpret_cast<void*>(OnConvarQuery), "engine2", "CServerSideClient", "CServerSideClient::ProcessRespondCvarValue");

extern bool bypassPostEventAbstractHook;

void ChangedConvarCallback(ConVarRefAbstract* ref, CSplitScreenSlot nSlot, const char* pNewValue, const char* pOldValue, ConVarUserInfoSet_t* userData)
{
    PROFILE_FUNCTION();

    g_pConvarManager->OnConvarChanged(ref, nSlot, pNewValue, pOldValue);
}

bool OnConvarQuery(CServerSideClientBase* client, const CNetMessagePB<CCLCMsg_RespondCvarValue>& msg)
{
    PROFILE_FUNCTION();

    g_pConvarManager->OnClientQueryCvar(client->GetPlayerSlot().Get(), msg.name(), msg.value());

    PROFILE_PAUSE();

    return reinterpret_cast<bool(*)(CServerSideClientBase*, const CNetMessagePB<CCLCMsg_RespondCvarValue>&)>(g_ProcessRespondCvarValueHook.GetOriginal())(client, msg);
}

template <typename Listeners>
static std::vector<typename Listeners::mapped_type> CopyListeners(std::mutex& mutex, const Listeners& listeners)
{
    PROFILE_FUNCTION();

    std::lock_guard<std::mutex> lock(mutex);

    std::vector<typename Listeners::mapped_type> copy;
    copy.reserve(listeners.size());
    for (const auto& [id, listener] : listeners)
        copy.push_back(listener);

    return copy;
}

template <typename T, typename V = T>
static void* NewConVar(const std::string& cvar_name, uint64_t flags, const char* help_message, const ConvarValue& defaultValue, const std::optional<ConvarValue>& minValue, const std::optional<ConvarValue>& maxValue)
{
    PROFILE_FUNCTION();

    auto defaultOrZero = (T)std::get<V>(defaultValue);

    bool hasMin = minValue.has_value();
    bool hasMax = maxValue.has_value();
    auto min = hasMin ? (T)std::get<V>(*minValue) : defaultOrZero;
    auto max = hasMax ? (T)std::get<V>(*maxValue) : defaultOrZero;

    return new CConVar<T>(cvar_name.c_str(), flags, help_message, defaultOrZero, hasMin, min, hasMax, max);
}

template <typename T>
static void FreeConVar(void* convar)
{
    PROFILE_FUNCTION();

    delete (CConVar<T>*)convar;
}

void CConvarManager::Initialize()
{
    PROFILE_FUNCTION();

    {
        std::lock_guard<std::mutex> lock(m_mtxListeners);
        m_bShuttingDown = false;
    }

    if (bool* unlockedCvars = std::get_if<bool>(&g_pConfiguration->GetValue("core.Unlocker.Convars")))
    {
        if (*unlockedCvars == true)
        {
            int unlockedConvars = 0;

            for (ConVarRefAbstract ref(ConVarRef((uint16)0)); ref.IsValidRef(); ref = ConVarRefAbstract(ConVarRef(ref.GetAccessIndex() + 1)))
            {
                if (!ref.IsFlagSet(CONVAR_FLAGS_TO_REMOVE)) continue;

                ref.RemoveFlags(CONVAR_FLAGS_TO_REMOVE);
                unlockedConvars++;
            }

            g_pLogger->Info("Unlocker", std::format("Unlocked {} convars.\n", unlockedConvars));
        }
    }

    if (bool* unlockedConCommands = std::get_if<bool>(&g_pConfiguration->GetValue("core.Unlocker.ConCommands")))
    {
        if (*unlockedConCommands == true)
        {
            int unlockedCommands = 0;

            ConCommandData* data = g_pGameCvar->GetConCommandData(ConCommandRef());
            for (ConCommandRef ref = ConCommandRef((uint16)0); ref.GetRawData() != data; ref = ConCommandRef(ref.GetAccessIndex() + 1))
            {
                if (!ref.IsFlagSet(CONVAR_FLAGS_TO_REMOVE)) continue;

                ref.RemoveFlags(CONVAR_FLAGS_TO_REMOVE);
                unlockedCommands++;
            }

            g_pLogger->Info("Unlocker", std::format("Unlocked {} concommands.\n", unlockedCommands));
        }
    }
}

void CConvarManager::Shutdown()
{
    PROFILE_FUNCTION();

    {
        std::lock_guard<std::mutex> lock(m_mtxListeners);
        m_bShuttingDown = true;
    }

    UpdateChangeCallback();
    UpdateCreationListener();
}

void CConvarManager::QueryClientConvar(int playerid, const std::string& cvar_name)
{
    PROFILE_FUNCTION();

    auto netmsg = g_pGameNetworkMessages->FindNetworkMessagePartial("GetCvarValue");
    auto msg = netmsg->AllocateMessage()->ToPB<CSVCMsg_GetCvarValue>();

    msg->set_cvar_name(cvar_name);

    bypassPostEventAbstractHook = true;

    CSingleUserRecipientFilter filter(playerid);
    g_pGameEventSystem->PostEventAbstract(-1, false, &filter, netmsg, msg, 0);

    bypassPostEventAbstractHook = false;

    delete msg;
}

int CConvarManager::AddQueryClientCvarCallback(std::function<void(int, std::string, std::string)> callback)
{
    PROFILE_FUNCTION();

    std::lock_guard<std::mutex> lock(m_mtxQueryCallbacks);

    m_queryCallbacks[++m_lastQueryCallbackId] = std::move(callback);
    return m_lastQueryCallbackId;
}

void CConvarManager::RemoveQueryClientCvarCallback(int callback_id)
{
    PROFILE_FUNCTION();

    std::lock_guard<std::mutex> lock(m_mtxQueryCallbacks);
    m_queryCallbacks.erase(callback_id);
}

void CConvarManager::OnClientQueryCvar(int playerid, const std::string& cvar_name, const std::string& cvar_value)
{
    PROFILE_FUNCTION();

    for (const auto& callback : CopyListeners(m_mtxQueryCallbacks, m_queryCallbacks))
        callback(playerid, cvar_name, cvar_value);
}

void CConvarManager::OnConvarChanged(ConVarRefAbstract* ref, CSplitScreenSlot slot, const char* new_value, const char* old_value)
{
    PROFILE_FUNCTION();

    for (const auto& listener : CopyListeners(m_mtxListeners, m_changeListeners))
        listener(ref->GetName(), slot.Get(), new_value, old_value);
}

void CConvarManager::OnConVarCreated(ConVarRefAbstract* pNewCvar)
{
    PROFILE_FUNCTION();

    for (const auto& listener : CopyListeners(m_mtxListeners, m_convarCreatedListeners))
        listener(pNewCvar->GetName());
}

void CConvarManager::OnConCommandCreated(ConCommand* pNewCommand)
{
    PROFILE_FUNCTION();

    for (const auto& listener : CopyListeners(m_mtxListeners, m_conCommandCreatedListeners))
        listener(pNewCommand->GetName());
}

void CConvarManager::CreateConvar(const std::string& cvar_name, EConVarType type, uint64_t flags, const char* help_message, const ConvarValue& defaultValue, const std::optional<ConvarValue>& minValue, const std::optional<ConvarValue>& maxValue)
{
    PROFILE_FUNCTION();

    ConVarRefAbstract cvar(cvar_name.c_str());
    if (cvar.IsValidRef()) return;

    CreatedConvar created;

#define CREATE_CONVAR(data_type, variant_type) \
    created = { NewConVar<data_type, variant_type>(cvar_name, flags, help_message, defaultValue, minValue, maxValue), FreeConVar<data_type> }

    switch (type)
    {
        case EConVarType_Int16: CREATE_CONVAR(int16, int16); break;
        case EConVarType_UInt16: CREATE_CONVAR(uint16, uint16); break;
        case EConVarType_Int32: CREATE_CONVAR(int32, int32); break;
        case EConVarType_UInt32: CREATE_CONVAR(uint32, uint32); break;
        case EConVarType_Int64: CREATE_CONVAR(int64, int64_t); break;
        case EConVarType_UInt64: CREATE_CONVAR(uint64, uint64_t); break;
        case EConVarType_Bool: CREATE_CONVAR(bool, bool); break;
        case EConVarType_Float32: CREATE_CONVAR(float, float); break;
        case EConVarType_Float64: CREATE_CONVAR(double, double); break;
        case EConVarType_Color: CREATE_CONVAR(Color, Color); break;
        case EConVarType_Vector2: CREATE_CONVAR(Vector2D, Vector2D); break;
        case EConVarType_Vector3: CREATE_CONVAR(Vector, Vector); break;
        case EConVarType_Vector4: CREATE_CONVAR(Vector4D, Vector4D); break;
        case EConVarType_Qangle: CREATE_CONVAR(QAngle, QAngle); break;
        case EConVarType_String:
            created = {
                new CConVar<CUtlString>(cvar_name.c_str(), flags, help_message, CUtlString(std::get<std::string>(defaultValue).c_str())),
                FreeConVar<CUtlString>
            };
            break;
        default:
            break;
    }

#undef CREATE_CONVAR

    if (!created.convar) return;

    std::lock_guard<std::mutex> lock(m_mtxConvars);
    m_createdConvars[cvar_name] = created;
}

void CConvarManager::DeleteConvar(const std::string& cvar_name)
{
    PROFILE_FUNCTION();

    CreatedConvar created;
    {
        std::lock_guard<std::mutex> lock(m_mtxConvars);

        auto it = m_createdConvars.find(cvar_name);
        if (it == m_createdConvars.end()) return;

        created = it->second;
        m_createdConvars.erase(it);

        m_convarRefs.erase(cvar_name);
    }

    created.free(created.convar);
}

ConVarRefAbstract& CConvarManager::GetConvarRef(const char* cvar_name)
{
    PROFILE_FUNCTION();

    std::lock_guard<std::mutex> lock(m_mtxConvars);

    auto it = m_convarRefs.find(std::string_view(cvar_name));
    if (it == m_convarRefs.end())
        return m_convarRefs.emplace(cvar_name, ConVarRefAbstract(cvar_name)).first->second;

    if (!it->second.IsValidRef())
        it->second = ConVarRefAbstract(cvar_name);

    return it->second;
}

/*

it feels so good to not make "#define private public" stuff

some people don't even want to look through all the fields and just go to the shortest way

*/

uint64_t CConvarManager::AddGlobalChangeListener(std::function<void(const char*, int, const char*, const char*)> callback)
{
    PROFILE_FUNCTION();

    uint64_t id;
    {
        std::lock_guard<std::mutex> lock(m_mtxListeners);
        id = ++m_lastListenerId;
        m_changeListeners[id] = std::move(callback);
    }

    UpdateChangeCallback();
    return id;
}

void CConvarManager::RemoveGlobalChangeListener(uint64_t callback_id)
{
    PROFILE_FUNCTION();

    {
        std::lock_guard<std::mutex> lock(m_mtxListeners);
        m_changeListeners.erase(callback_id);
    }

    UpdateChangeCallback();
}

uint64_t CConvarManager::AddConvarCreatedListener(std::function<void(const char*)> callback)
{
    PROFILE_FUNCTION();

    uint64_t id;
    {
        std::lock_guard<std::mutex> lock(m_mtxListeners);
        id = ++m_lastListenerId;
        m_convarCreatedListeners[id] = std::move(callback);
    }

    UpdateCreationListener();
    return id;
}

void CConvarManager::RemoveConvarCreatedListener(uint64_t callback_id)
{
    PROFILE_FUNCTION();

    {
        std::lock_guard<std::mutex> lock(m_mtxListeners);
        m_convarCreatedListeners.erase(callback_id);
    }

    UpdateCreationListener();
}

uint64_t CConvarManager::AddConCommandCreatedListener(std::function<void(const char*)> callback)
{
    PROFILE_FUNCTION();

    uint64_t id;
    {
        std::lock_guard<std::mutex> lock(m_mtxListeners);
        id = ++m_lastListenerId;
        m_conCommandCreatedListeners[id] = std::move(callback);
    }

    UpdateCreationListener();
    return id;
}

void CConvarManager::RemoveConCommandCreatedListener(uint64_t callback_id)
{
    PROFILE_FUNCTION();

    {
        std::lock_guard<std::mutex> lock(m_mtxListeners);
        m_conCommandCreatedListeners.erase(callback_id);
    }

    UpdateCreationListener();
}

void CConvarManager::UpdateChangeCallback()
{
    PROFILE_FUNCTION();

    std::unique_lock<std::mutex> lock(m_mtxListeners);
    if (m_bUpdatingChangeCallback) return;
    if ((!m_bShuttingDown && !m_changeListeners.empty()) == m_bChangeCallbackInstalled) return;
    m_bUpdatingChangeCallback = true;
    try
    {
        while ((!m_bShuttingDown && !m_changeListeners.empty()) != m_bChangeCallbackInstalled)
        {
            const bool install = !m_bShuttingDown && !m_changeListeners.empty();
            lock.unlock();
            if (install)
                g_pGameCvar->InstallGlobalChangeCallback(ChangedConvarCallback);
            else
                g_pGameCvar->RemoveGlobalChangeCallback(ChangedConvarCallback);
            lock.lock();
            m_bChangeCallbackInstalled = install;
        }
        m_bUpdatingChangeCallback = false;
    }
    catch (...)
    {
        if (!lock.owns_lock()) lock.lock();
        m_bUpdatingChangeCallback = false;
        throw;
    }
}

void CConvarManager::UpdateCreationListener()
{
    PROFILE_FUNCTION();

    std::unique_lock<std::mutex> lock(m_mtxListeners);
    if (m_bUpdatingCreationListener) return;
    if ((!m_bShuttingDown && (!m_convarCreatedListeners.empty() || !m_conCommandCreatedListeners.empty())) == m_bCreationListenerInstalled) return;
    m_bUpdatingCreationListener = true;
    try
    {
        while ((!m_bShuttingDown && (!m_convarCreatedListeners.empty() || !m_conCommandCreatedListeners.empty())) != m_bCreationListenerInstalled)
        {
            const bool install = !m_bShuttingDown && (!m_convarCreatedListeners.empty() || !m_conCommandCreatedListeners.empty());
            lock.unlock();
            if (install)
                g_pGameCvar->RegisterCreationListeners(this);
            else
                g_pGameCvar->RemoveCreationListeners(this);
            lock.lock();
            m_bCreationListenerInstalled = install;
        }
        m_bUpdatingCreationListener = false;
    }
    catch (...)
    {
        if (!lock.owns_lock()) lock.lock();
        m_bUpdatingCreationListener = false;
        throw;
    }
}
