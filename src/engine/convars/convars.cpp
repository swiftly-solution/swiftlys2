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

#include <api/interfaces/interfaces.h>
#include <api/sdk/recipientfilter.h>
#include <api/sdk/serversideclient.h>

#include <optional>
#include <type_traits>
#include <vector>

#include <memory/gamedata/manager.h>

#include <public/networksystem/inetworkmessages.h>
#include <public/engine/igameeventsystem.h>

#include <fmt/format.h>

#include "networkbasetypes.pb.h"

#define CONVAR_FLAGS_TO_REMOVE (FCVAR_HIDDEN | FCVAR_DEVELOPMENTONLY | FCVAR_CLIENTDLL)

IVFunctionHook* g_pProcessRespondCvarValueHook = nullptr;

extern bool bypassPostEventAbstractHook;

void ChangedConvarCallback(ConVarRefAbstract* ref, CSplitScreenSlot nSlot, const char* pNewValue, const char* pOldValue, void* __unk01)
{
    g_pConvarManager->OnConvarChanged(ref, nSlot, pNewValue, pOldValue);
}

bool OnConvarQuery(CServerSideClientBase* client, const CNetMessagePB<CCLCMsg_RespondCvarValue>& msg)
{
    g_pConvarManager->OnClientQueryCvar(client->GetPlayerSlot().Get(), msg.name(), msg.value());
    return reinterpret_cast<bool(*)(CServerSideClientBase*, const CNetMessagePB<CCLCMsg_RespondCvarValue>&)>(g_pProcessRespondCvarValueHook->GetOriginal())(client, msg);
}

template <typename Listeners>
static std::vector<typename Listeners::mapped_type> CopyListeners(QueueMutex& mutex, const Listeners& listeners)
{
    QueueLockGuard lock(mutex);

    std::vector<typename Listeners::mapped_type> copy;
    copy.reserve(listeners.size());
    for (const auto& [id, listener] : listeners)
        copy.push_back(listener);

    return copy;
}

template <typename T, typename V = T>
static void* NewConVar(const std::string& cvar_name, uint64_t flags, const char* help_message, const ConvarValue& defaultValue, const std::optional<ConvarValue>& minValue, const std::optional<ConvarValue>& maxValue)
{
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
    delete (CConVar<T>*)convar;
}

static std::string ConvertConvarValueToString(const ConvarValue& value)
{
    return std::visit([](const auto& v) -> std::string {
        using T = std::decay_t<decltype(v)>;

        if constexpr (std::is_same_v<T, std::string>)
            return v;
        else if constexpr (std::is_same_v<T, bool>)
            return v ? "1" : "0";
        else if constexpr (std::is_same_v<T, Color>)
            return fmt::format("{},{},{},{}", v.r(), v.g(), v.b(), v.a());
        else if constexpr (std::is_same_v<T, Vector2D>)
            return fmt::format("{},{}", v.x, v.y);
        else if constexpr (std::is_same_v<T, Vector> || std::is_same_v<T, QAngle>)
            return fmt::format("{},{},{}", v.x, v.y, v.z);
        else if constexpr (std::is_same_v<T, Vector4D>)
            return fmt::format("{},{},{},{}", v.x, v.y, v.z, v.w);
        else
            return fmt::format("{}", v);
        }, value);
}

void CConvarManager::Initialize()
{
    void* serverSideClientVTable;
    g_pS2BinLib->FindVtable("engine2", "CServerSideClient", &serverSideClientVTable);

    g_pProcessRespondCvarValueHook = g_pHooksManager->CreateVFunctionHook();
    g_pProcessRespondCvarValueHook->SetHookFunction(serverSideClientVTable, g_pGameDataManager->GetOffsets()->Fetch("CServerSideClient::ProcessRespondCvarValue"), reinterpret_cast<void*>(OnConvarQuery), true);
    g_pProcessRespondCvarValueHook->Enable();

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

            g_pLogger->Info("Unlocker", fmt::format("Unlocked {} convars.\n", unlockedConvars));
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

            g_pLogger->Info("Unlocker", fmt::format("Unlocked {} concommands.\n", unlockedCommands));
        }
    }
}

void CConvarManager::Shutdown()
{
    if (g_pProcessRespondCvarValueHook)
    {
        g_pProcessRespondCvarValueHook->Disable();
        g_pHooksManager->DestroyVFunctionHook(g_pProcessRespondCvarValueHook);
        g_pProcessRespondCvarValueHook = nullptr;
    }

    if (m_bChangeCallbackInstalled)
    {
        g_pGameCvar->RemoveGlobalChangeCallback(ChangedConvarCallback);
        m_bChangeCallbackInstalled = false;
    }

    if (m_bCreationListenerInstalled)
    {
        g_pGameCvar->RemoveCreationListeners(this);
        m_bCreationListenerInstalled = false;
    }
}

void CConvarManager::QueryClientConvar(int playerid, const std::string& cvar_name)
{
    auto netmsg = g_pGameNetworkMessages->FindNetworkMessagePartial("GetCvarValue");
    auto msg = netmsg->AllocateMessage()->ToPB<CSVCMsg_GetCvarValue>();

    msg->set_cvar_name(cvar_name);

    bypassPostEventAbstractHook = true;

    CSingleRecipientFilter filter(playerid);
    g_pGameEventSystem->PostEventAbstract(-1, false, &filter, netmsg, msg, 0);

    bypassPostEventAbstractHook = false;

    // see at the end of the file the comment for this one too
    delete msg;
}

int CConvarManager::AddQueryClientCvarCallback(std::function<void(int, std::string, std::string)> callback)
{
    QueueLockGuard lock(m_mtxQueryCallbacks);

    m_queryCallbacks[++m_lastQueryCallbackId] = std::move(callback);
    return m_lastQueryCallbackId;
}

void CConvarManager::RemoveQueryClientCvarCallback(int callback_id)
{
    QueueLockGuard lock(m_mtxQueryCallbacks);
    m_queryCallbacks.erase(callback_id);
}

void CConvarManager::OnClientQueryCvar(int playerid, const std::string& cvar_name, const std::string& cvar_value)
{
    for (const auto& callback : CopyListeners(m_mtxQueryCallbacks, m_queryCallbacks))
        callback(playerid, cvar_name, cvar_value);
}

void CConvarManager::OnConvarChanged(ConVarRefAbstract* ref, CSplitScreenSlot slot, const char* new_value, const char* old_value)
{
    for (const auto& listener : CopyListeners(m_mtxListeners, m_changeListeners))
        listener(ref->GetName(), slot.Get(), new_value, old_value);
}

void CConvarManager::OnConVarCreated(ConVarRefAbstract* pNewCvar)
{
    for (const auto& listener : CopyListeners(m_mtxListeners, m_convarCreatedListeners))
        listener(pNewCvar->GetName());
}

void CConvarManager::OnConCommandCreated(ConCommand* pNewCommand)
{
    for (const auto& listener : CopyListeners(m_mtxListeners, m_conCommandCreatedListeners))
        listener(pNewCommand->GetName());
}

void CConvarManager::CreateConvar(const std::string& cvar_name, EConVarType type, uint64_t flags, const char* help_message, const ConvarValue& defaultValue, const std::optional<ConvarValue>& minValue, const std::optional<ConvarValue>& maxValue)
{
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

    QueueLockGuard lock(m_mtxConvars);
    m_createdConvars[cvar_name] = created;
}

void CConvarManager::DeleteConvar(const std::string& cvar_name)
{
    CreatedConvar created;
    {
        QueueLockGuard lock(m_mtxConvars);

        auto it = m_createdConvars.find(cvar_name);
        if (it == m_createdConvars.end()) return;

        created = it->second;
        m_createdConvars.erase(it);

        m_convarRefs.erase(cvar_name);
    }

    created.free(created.convar);
}

bool CConvarManager::ExistsConvar(const std::string& cvar_name)
{
    ConVarRefAbstract cvar(cvar_name.c_str());
    return cvar.IsValidRef() && cvar.IsConVarDataValid();
}

EConVarType CConvarManager::GetConvarType(const std::string& cvar_name)
{
    ConVarRefAbstract cvar(cvar_name.c_str());
    if (!cvar.IsConVarDataValid()) return EConVarType::EConVarType_Invalid;
    return cvar.GetType();
}

ConVarRefAbstract& CConvarManager::GetConvarRef(const char* cvar_name)
{
    QueueLockGuard lock(m_mtxConvars);

    auto it = m_convarRefs.find(std::string_view(cvar_name));
    if (it == m_convarRefs.end())
        return m_convarRefs.emplace(cvar_name, ConVarRefAbstract(cvar_name)).first->second;

    if (!it->second.IsValidRef())
        it->second = ConVarRefAbstract(cvar_name);

    return it->second;
}

void* CConvarManager::GetConvarDataAddress(const std::string& cvar_name)
{
    ConVarRefAbstract cvar(cvar_name.c_str());
    CSplitScreenSlot server(0);
    if (!cvar.IsValidRef()) return nullptr;
    if (!cvar.IsConVarDataValid()) return nullptr;

    return cvar.GetConVarData()->ValueOrDefault(server);
}

ConvarValue CConvarManager::GetConvarValue(const std::string& cvar_name)
{
    ConVarRefAbstract cvar(cvar_name.c_str());
    CSplitScreenSlot server(0);
    if (!cvar.IsConVarDataValid()) return 0;

    switch (cvar.GetType())
    {
        case EConVarType_Int16: return cvar.GetAs<int16_t>(server);
        case EConVarType_UInt16: return cvar.GetAs<uint16_t>(server);
        case EConVarType_Int32: return cvar.GetAs<int32_t>(server);
        case EConVarType_UInt32: return cvar.GetAs<uint32_t>(server);
        /*

        unsigned long long long long long long long long long long long long long long long long long long
        fuck you linux fuck you gcc

        * fuck sourcehook too, almost forget about you


        */
        case EConVarType_UInt64: return (uint64_t)cvar.GetAs<uint64>(server);
        case EConVarType_Int64: return (int64_t)cvar.GetAs<int64>(server);
        case EConVarType_Bool: return cvar.GetAs<bool>(server);
        case EConVarType_Float32: return cvar.GetAs<float>(server);
        case EConVarType_Float64: return cvar.GetAs<double>(server);
        case EConVarType_String: return std::string(cvar.GetString(server).String());
        case EConVarType_Color: return cvar.GetAs<Color>(server);
        case EConVarType_Vector2: return cvar.GetAs<Vector2D>(server);
        case EConVarType_Vector3: return cvar.GetAs<Vector>(server);
        case EConVarType_Vector4: return cvar.GetAs<Vector4D>(server);
        case EConVarType_Qangle: return cvar.GetAs<QAngle>(server);
        default:
            g_pLogger->Error("Convars", fmt::format("Unsupported ConVar type: {}", (int)cvar.GetType()));
            return 0;
    }
}

void CConvarManager::SetConvarValue(const std::string& cvar_name, const ConvarValue& value)
{
    ConVarRefAbstract cvar(cvar_name.c_str());
    CSplitScreenSlot server(0);
    if (!cvar.IsConVarDataValid()) return;

    auto v_str = ConvertConvarValueToString(value);
    cvar.SetString(CUtlString(v_str.c_str()), server);
}

void CConvarManager::SetClientConvar(int playerid, const std::string& cvar_name, const std::string& value)
{
    const auto netmsg = g_pGameNetworkMessages->FindNetworkMessageById(6);
    auto msg = netmsg->AllocateMessage()->ToPB<CNETMsg_SetConVar>();

    CMsg_CVars_CVar* cvar = msg->mutable_convars()->add_cvars();
    cvar->set_name(cvar_name);
    cvar->set_value(value);

    bypassPostEventAbstractHook = true;

    CSingleRecipientFilter filter(playerid);
    g_pGameEventSystem->PostEventAbstract(-1, false, &filter, netmsg, msg, 0);

    bypassPostEventAbstractHook = false;

    /*
    Finally it's been fixed, i've had this problem since a year ago, glad that it's fixed and i didn't need to use deamberd with it's "const const const" feature

    thanks memoverride, glad to have you here m8

    now i can finally delete it, even on windows!!!

    and 100% (i'm not sure of this number) i'll not need to stay again 4 hours to debug it

    fuck you windows
    */
    delete msg;
}

/*

it feels so good to not make "#define private public" stuff

some people don't even want to look through all the fields and just go to the shortest way

*/

void CConvarManager::AddFlags(const std::string& cvar_name, uint64_t flags)
{
    ConVarRefAbstract cvar(cvar_name.c_str());
    if (!cvar.IsConVarDataValid()) return;

    cvar.AddFlags(flags);
}

void CConvarManager::RemoveFlags(const std::string& cvar_name, uint64_t flags)
{
    ConVarRefAbstract cvar(cvar_name.c_str());
    if (!cvar.IsConVarDataValid()) return;

    cvar.RemoveFlags(flags);
}

void CConvarManager::ClearFlags(const std::string& cvar_name)
{
    ConVarRefAbstract cvar(cvar_name.c_str());
    if (!cvar.IsConVarDataValid()) return;

    cvar.RemoveFlags(cvar.GetFlags());
}

uint64_t CConvarManager::GetFlags(const std::string& cvar_name)
{
    ConVarRefAbstract cvar(cvar_name.c_str());
    if (!cvar.IsConVarDataValid()) return 0;

    return cvar.GetFlags();
}

uint64_t CConvarManager::AddGlobalChangeListener(std::function<void(const char*, int, const char*, const char*)> callback)
{
    uint64_t id;
    {
        QueueLockGuard lock(m_mtxListeners);
        id = ++m_lastListenerId;
        m_changeListeners[id] = std::move(callback);
    }

    UpdateChangeCallback();
    return id;
}

void CConvarManager::RemoveGlobalChangeListener(uint64_t callback_id)
{
    {
        QueueLockGuard lock(m_mtxListeners);
        m_changeListeners.erase(callback_id);
    }

    UpdateChangeCallback();
}

uint64_t CConvarManager::AddConvarCreatedListener(std::function<void(const char*)> callback)
{
    uint64_t id;
    {
        QueueLockGuard lock(m_mtxListeners);
        id = ++m_lastListenerId;
        m_convarCreatedListeners[id] = std::move(callback);
    }

    UpdateCreationListener();
    return id;
}

void CConvarManager::RemoveConvarCreatedListener(uint64_t callback_id)
{
    {
        QueueLockGuard lock(m_mtxListeners);
        m_convarCreatedListeners.erase(callback_id);
    }

    UpdateCreationListener();
}

uint64_t CConvarManager::AddConCommandCreatedListener(std::function<void(const char*)> callback)
{
    uint64_t id;
    {
        QueueLockGuard lock(m_mtxListeners);
        id = ++m_lastListenerId;
        m_conCommandCreatedListeners[id] = std::move(callback);
    }

    UpdateCreationListener();
    return id;
}

void CConvarManager::RemoveConCommandCreatedListener(uint64_t callback_id)
{
    {
        QueueLockGuard lock(m_mtxListeners);
        m_conCommandCreatedListeners.erase(callback_id);
    }

    UpdateCreationListener();
}

void CConvarManager::UpdateChangeCallback()
{
    bool install;
    {
        QueueLockGuard lock(m_mtxListeners);

        install = !m_changeListeners.empty();
        if (install == m_bChangeCallbackInstalled) return;

        m_bChangeCallbackInstalled = install;
    }

    if (install)
        g_pGameCvar->InstallGlobalChangeCallback(ChangedConvarCallback);
    else
        g_pGameCvar->RemoveGlobalChangeCallback(ChangedConvarCallback);
}

void CConvarManager::UpdateCreationListener()
{
    bool install;
    {
        QueueLockGuard lock(m_mtxListeners);

        install = !m_convarCreatedListeners.empty() || !m_conCommandCreatedListeners.empty();
        if (install == m_bCreationListenerInstalled) return;

        m_bCreationListenerInstalled = install;
    }

    if (install)
        g_pGameCvar->RegisterCreationListeners(this);
    else
        g_pGameCvar->RemoveCreationListeners(this);
}
