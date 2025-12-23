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

#include <api/interfaces/manager.h>
#include <api/sdk/recipientfilter.h>
#include <api/sdk/serversideclient.h>

#include <optional>

#include <memory/gamedata/manager.h>

#include <public/networksystem/inetworkmessages.h>
#include <public/engine/igameeventsystem.h>

#include <fmt/format.h>
#include <s2binlib/s2binlib.h>

#include "networkbasetypes.pb.h"

#define NEW_CVAR(data_type, default_value) \
    auto min = minValue.has_value(); \
    auto minValueOrDefault = min ? std::get<data_type>(minValue.value()) : std::get<data_type>(defaultValue); \
    auto max = maxValue.has_value(); \
    auto maxValueOrDefault = max ? std::get<data_type>(maxValue.value()) : std::get<data_type>(defaultValue); \
    cvarptr = (void*)(new CConVar<data_type>(cvar_name.c_str(), flags, help_message, std::get<data_type>(defaultValue), min, minValueOrDefault, max, maxValueOrDefault))

#define NEW_CVAR_INT64(default_value) \
    auto min = minValue.has_value(); \
    auto minValueOrDefault = min ? std::get<int64_t>(minValue.value()) : std::get<int64_t>(defaultValue); \
    auto max = maxValue.has_value(); \
    auto maxValueOrDefault = max ? std::get<int64_t>(maxValue.value()) : std::get<int64_t>(defaultValue); \
    cvarptr = (void*)(new CConVar<int64>(cvar_name.c_str(), flags, help_message, (int64)std::get<int64_t>(defaultValue), min, minValueOrDefault, max, maxValueOrDefault))

#define NEW_CVAR_UINT64(default_value) \
    auto min = minValue.has_value(); \
    auto minValueOrDefault = min ? std::get<uint64_t>(minValue.value()) : std::get<uint64_t>(defaultValue); \
    auto max = maxValue.has_value(); \
    auto maxValueOrDefault = max ? std::get<uint64_t>(maxValue.value()) : std::get<uint64_t>(defaultValue); \
    cvarptr = (void*)(new CConVar<uint64>(cvar_name.c_str(), flags, help_message, (uint64)std::get<uint64_t>(defaultValue), min, minValueOrDefault, max, maxValueOrDefault))


#define FREE_CVAR(data_type) \
    delete (CConVar<data_type>*)cvarptr;

std::map<std::string, void*> g_mCvars;
uint64_t g_uQueryCallbacks = 0;
std::map<uint64_t, std::function<void(int, std::string, std::string)>> g_mQueryCallbacks;

std::map<uint64_t, std::function<void(const char*, int, const char*, const char*)>> g_mChangeCallbacks;
uint64_t g_uChangeCallbackId = 0;

std::map<uint64_t, std::function<void(const char*)>> g_mCreatedConvarsCallbacks;
uint64_t g_uCreatedConvarId = 0;

std::map<uint64_t, std::function<void(const char*)>> g_mCreateConCommandCallbacks;
uint64_t g_uCreatedConCommandId = 0;

IVFunctionHook* g_pProcessRespondCvarValueHook = nullptr;

extern INetworkMessages* networkMessages;
extern bool bypassPostEventAbstractHook;

class CConvarListener : public IConVarListener
{
    virtual void OnConVarCreated(ConVarRefAbstract* pNewCvar) override
    {
        for (const auto& [id, callback] : g_mCreatedConvarsCallbacks)
        {
            callback(pNewCvar->GetName());
        }
    }

    virtual void OnConCommandCreated(ConCommand* pNewCommand) override
    {
        for (const auto& [id, callback] : g_mCreateConCommandCallbacks)
        {
            callback(pNewCommand->GetName());
        }
    }
};

CConvarListener g_CvarListener;

void ChangedConvarCallback(ConVarRefAbstract* ref, CSplitScreenSlot nSlot, const char* pNewValue, const char* pOldValue, void* __unk01)
{
    for (auto it = g_mChangeCallbacks.begin(); it != g_mChangeCallbacks.end(); ++it)
    {
        it->second(ref->GetName(), nSlot.Get(), pNewValue, pOldValue);
    }
}

bool OnConvarQuery(CServerSideClientBase* client, const CNetMessagePB<CCLCMsg_RespondCvarValue>& msg)
{
    static auto cvarmanager = g_ifaceService.FetchInterface<IConvarManager>(CONVARMANAGER_INTERFACE_VERSION);

    cvarmanager->OnClientQueryCvar(client->GetPlayerSlot().Get(), msg.name(), msg.value());

    return reinterpret_cast<bool(*)(CServerSideClientBase*, const CNetMessagePB<CCLCMsg_RespondCvarValue>&)>(g_pProcessRespondCvarValueHook->GetOriginal())(client, msg);
}

void CConvarManager::Initialize()
{
    void* serverSideClientVTable;
    s2binlib_find_vtable("engine2", "CServerSideClient", &serverSideClientVTable);

    static auto hooksmanager = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);
    static auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);

    g_pProcessRespondCvarValueHook = hooksmanager->CreateVFunctionHook();
    g_pProcessRespondCvarValueHook->SetHookFunction(serverSideClientVTable, gamedata->GetOffsets()->Fetch("CServerSideClient::ProcessRespondCvarValue"), reinterpret_cast<void*>(OnConvarQuery), true);
    g_pProcessRespondCvarValueHook->Enable();

    auto cvars = g_ifaceService.FetchInterface<ICvar>(CVAR_INTERFACE_VERSION);
    cvars->InstallGlobalChangeCallback(ChangedConvarCallback);
    cvars->RegisterCreationListeners(&g_CvarListener);
}

void CConvarManager::Shutdown()
{
    if (g_pProcessRespondCvarValueHook)
    {
        g_pProcessRespondCvarValueHook->Disable();
        static auto hooksmanager = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);
        hooksmanager->DestroyVFunctionHook(g_pProcessRespondCvarValueHook);
        g_pProcessRespondCvarValueHook = nullptr;
    }

    auto cvars = g_ifaceService.FetchInterface<ICvar>(CVAR_INTERFACE_VERSION);
    cvars->RemoveGlobalChangeCallback(ChangedConvarCallback);
    cvars->RemoveCreationListeners(&g_CvarListener);
}

void CConvarManager::QueryClientConvar(int playerid, std::string cvar_name)
{
    static auto gameEventSystem = g_ifaceService.FetchInterface<IGameEventSystem>(GAMEEVENTSYSTEM_INTERFACE_VERSION);

    auto netmsg = networkMessages->FindNetworkMessagePartial("GetCvarValue");
    auto msg = netmsg->AllocateMessage()->ToPB<CSVCMsg_GetCvarValue>();

    msg->set_cvar_name(cvar_name);

    bypassPostEventAbstractHook = true;

    CSingleRecipientFilter filter(playerid);
    gameEventSystem->PostEventAbstract(-1, false, &filter, netmsg, msg, 0);

    bypassPostEventAbstractHook = false;

    // see at the end of the file the comment for this one too
    delete msg;
}

int CConvarManager::AddQueryClientCvarCallback(std::function<void(int, std::string, std::string)> callback)
{
    g_mQueryCallbacks[g_uQueryCallbacks++] = callback;
    return g_uQueryCallbacks - 1;
}

void CConvarManager::RemoveQueryClientCvarCallback(int callback_id)
{
    g_mQueryCallbacks.erase(callback_id);
}

void CConvarManager::OnClientQueryCvar(int playerid, std::string cvar_name, std::string cvar_value)
{
    for (const auto& [id, callback] : g_mQueryCallbacks)
    {
        callback(playerid, cvar_name, cvar_value);
    }
}

void CConvarManager::CreateConvar(std::string cvar_name, EConVarType type, uint64_t flags, const char* help_message, ConvarValue defaultValue, std::optional<ConvarValue> minValue, std::optional<ConvarValue> maxValue)
{
    ConVarRefAbstract cvar(cvar_name.c_str());
    if (cvar.IsValidRef()) return;

    void* cvarptr = nullptr;
    if (type == EConVarType_Int16)
    {
        NEW_CVAR(int16, 0);
    }
    else if (type == EConVarType_UInt16)
    {
        NEW_CVAR(uint16, 0);
    }
    else if (type == EConVarType_UInt32)
    {
        NEW_CVAR(uint32, 0);
    }
    else if (type == EConVarType_Int32)
    {
        NEW_CVAR(int32, 0);
    }
    else if (type == EConVarType_UInt64)
    {
        NEW_CVAR_UINT64(0);
    }
    else if (type == EConVarType_Int64)
    {
        NEW_CVAR_INT64(0);
    }
    else if (type == EConVarType_Bool)
    {
        NEW_CVAR(bool, false);
    }
    else if (type == EConVarType_Float32)
    {
        NEW_CVAR(float, 0.0f);
    }
    else if (type == EConVarType_Float64)
    {
        NEW_CVAR(double, 0.0);
    }
    else if (type == EConVarType_Color)
    {
        NEW_CVAR(Color, Color(0, 0, 0, 255));
    }
    else if (type == EConVarType_Vector2)
    {
        NEW_CVAR(Vector2D, Vector2D(0, 0));
    }
    else if (type == EConVarType_Vector3)
    {
        NEW_CVAR(Vector, Vector(0, 0, 0));
    }
    else if (type == EConVarType_Vector4)
    {
        NEW_CVAR(Vector4D, Vector4D(0, 0, 0, 0));
    }
    else if (type == EConVarType_Qangle)
    {
        NEW_CVAR(QAngle, QAngle(0, 0, 0));
    }
    else if (type == EConVarType_String)
    {
        auto v = std::get<std::string>(defaultValue);
        cvarptr = (void*)(new CConVar<CUtlString>(cvar_name.c_str(), flags, help_message, CUtlString(v.c_str())));
    }

    if (!cvarptr) return;
    g_mCvars[cvar_name] = cvarptr;
}

void CConvarManager::DeleteConvar(std::string cvar_name)
{
    if (g_mCvars.find(cvar_name) == g_mCvars.end()) return;

    void* cvarptr = g_mCvars.at(cvar_name);
    ConVarRefAbstract cvar(cvar_name.c_str());

    if (cvar.GetType() == EConVarType_Int16)
    {
        FREE_CVAR(int16);
    }
    else if (cvar.GetType() == EConVarType_UInt16)
    {
        FREE_CVAR(uint16);
    }
    else if (cvar.GetType() == EConVarType_UInt32)
    {
        FREE_CVAR(uint32);
    }
    else if (cvar.GetType() == EConVarType_Int32)
    {
        FREE_CVAR(int32);
    }
    else if (cvar.GetType() == EConVarType_UInt64)
    {
        FREE_CVAR(uint64);
    }
    else if (cvar.GetType() == EConVarType_Int64)
    {
        FREE_CVAR(int64);
    }
    else if (cvar.GetType() == EConVarType_Bool)
    {
        FREE_CVAR(bool);
    }
    else if (cvar.GetType() == EConVarType_Float32)
    {
        FREE_CVAR(float);
    }
    else if (cvar.GetType() == EConVarType_Float64)
    {
        FREE_CVAR(double);
    }
    else if (cvar.GetType() == EConVarType_String)
    {
        FREE_CVAR(CUtlString);
    }
    else if (cvar.GetType() == EConVarType_Color)
    {
        FREE_CVAR(Color);
    }
    else if (cvar.GetType() == EConVarType_Vector2)
    {
        FREE_CVAR(Vector2D);
    }
    else if (cvar.GetType() == EConVarType_Vector3)
    {
        FREE_CVAR(Vector);
    }
    else if (cvar.GetType() == EConVarType_Vector4)
    {
        FREE_CVAR(Vector4D);
    }
    else if (cvar.GetType() == EConVarType_Qangle)
    {
        FREE_CVAR(QAngle);
    }
    else
    {
        free(cvarptr);
    }

    g_mCvars.erase(cvar_name);
}

bool CConvarManager::ExistsConvar(std::string cvar_name)
{
    ConVarRefAbstract cvar(cvar_name.c_str());
    return cvar.IsValidRef() && cvar.IsConVarDataValid();
}

EConVarType CConvarManager::GetConvarType(std::string cvar_name)
{
    ConVarRefAbstract cvar(cvar_name.c_str());
    if (!cvar.IsConVarDataValid()) return EConVarType::EConVarType_Invalid;
    return cvar.GetType();
}

void* CConvarManager::GetConvarDataAddress(std::string cvar_name)
{
    ConVarRefAbstract cvar(cvar_name.c_str());
    CSplitScreenSlot server(0);
    if (!cvar.IsValidRef()) return nullptr;
    if (!cvar.IsConVarDataValid()) return nullptr;

    return cvar.GetConVarData()->ValueOrDefault(server);
}

ConvarValue CConvarManager::GetConvarValue(std::string cvar_name)
{
    ConVarRefAbstract cvar(cvar_name.c_str());
    CSplitScreenSlot server(0);
    if (!cvar.IsConVarDataValid()) return 0;

    if (cvar.GetType() == EConVarType_Int16)
    {
        return cvar.GetAs<int16_t>(server);
    }
    else if (cvar.GetType() == EConVarType_UInt16)
    {
        return cvar.GetAs<uint16_t>(server);
    }
    else if (cvar.GetType() == EConVarType_UInt32)
    {
        return cvar.GetAs<uint32_t>(server);
    }
    else if (cvar.GetType() == EConVarType_Int32)
    {
        return cvar.GetAs<int32_t>(server);
    }
    else if (cvar.GetType() == EConVarType_UInt64)
    {
        /*

        unsigned long long long long long long long long long long long long long long long long long long
        fuck you linux fuck you gcc

        * fuck sourcehook too, almost forget about you


        */
        return (uint64_t)cvar.GetAs<uint64>(server);
    }
    else if (cvar.GetType() == EConVarType_Int64)
    {
        return (int64_t)cvar.GetAs<int64>(server);
    }
    else if (cvar.GetType() == EConVarType_Bool)
    {
        return cvar.GetAs<bool>(server);
    }
    else if (cvar.GetType() == EConVarType_Float32)
    {
        return cvar.GetAs<float>(server);
    }
    else if (cvar.GetType() == EConVarType_Float64)
    {
        return cvar.GetAs<double>(server);
    }
    else if (cvar.GetType() == EConVarType_String)
    {
        return std::string(cvar.GetString(server).String());
    }
    else if (cvar.GetType() == EConVarType_Color)
    {
        return cvar.GetAs<Color>(server);
    }
    else if (cvar.GetType() == EConVarType_Vector2)
    {
        return cvar.GetAs<Vector2D>(server);
    }
    else if (cvar.GetType() == EConVarType_Vector3)
    {
        return cvar.GetAs<Vector>(server);
    }
    else if (cvar.GetType() == EConVarType_Vector4)
    {
        return cvar.GetAs<Vector4D>(server);
    }
    else if (cvar.GetType() == EConVarType_Qangle)
    {
        return cvar.GetAs<QAngle>(server);
    }
    else {
        static auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);
        logger->Error("Convars", fmt::format("Unsupported ConVar type: {}", (int)cvar.GetType()));
        return 0;
    }
}

std::string ConvertConvarValueToString(ConvarValue& value)
{
    std::string value_str;
    if (std::holds_alternative<int16_t>(value))
        value_str = fmt::format("{}", std::get<int16_t>(value));
    else if (std::holds_alternative<uint16_t>(value))
        value_str = fmt::format("{}", std::get<uint16_t>(value));
    else if (std::holds_alternative<int32_t>(value))
        value_str = fmt::format("{}", std::get<int32_t>(value));
    else if (std::holds_alternative<uint32_t>(value))
        value_str = fmt::format("{}", std::get<uint32_t>(value));
    else if (std::holds_alternative<float>(value))
        value_str = fmt::format("{}", std::get<float>(value));
    else if (std::holds_alternative<double>(value))
        value_str = fmt::format("{}", std::get<double>(value));
    else if (std::holds_alternative<int64_t>(value))
        value_str = fmt::format("{}", std::get<int64_t>(value));
    else if (std::holds_alternative<uint64_t>(value))
        value_str = fmt::format("{}", std::get<uint64_t>(value));
    else if (std::holds_alternative<std::string>(value))
        value_str = std::get<std::string>(value);
    else if (std::holds_alternative<bool>(value))
        value_str = std::get<bool>(value) ? "1" : "0";
    else if (std::holds_alternative<Color>(value))
    {
        Color clr = std::get<Color>(value);
        value_str = fmt::format("{},{},{},{}", clr.r(), clr.g(), clr.b(), clr.a());
    }
    else if (std::holds_alternative<Vector2D>(value))
    {
        Vector2D vec = std::get<Vector2D>(value);
        value_str = fmt::format("{},{}", vec.x, vec.y);
    }
    else if (std::holds_alternative<Vector>(value))
    {
        Vector vec = std::get<Vector>(value);
        value_str = fmt::format("{},{},{}", vec.x, vec.y, vec.z);
    }
    else if (std::holds_alternative<Vector4D>(value))
    {
        Vector4D vec = std::get<Vector4D>(value);
        value_str = fmt::format("{},{},{},{}", vec.x, vec.y, vec.z, vec.w);
    }
    else if (std::holds_alternative<QAngle>(value))
    {
        QAngle ang = std::get<QAngle>(value);
        value_str = fmt::format("{},{},{}", ang.x, ang.y, ang.z);
    }
    else value_str = "";

    return value_str;
}


void CConvarManager::SetConvarValue(std::string cvar_name, ConvarValue value)
{
    ConVarRefAbstract cvar(cvar_name.c_str());
    CSplitScreenSlot server(0);
    if (!cvar.IsConVarDataValid()) return;

    auto v_str = ConvertConvarValueToString(value);
    cvar.SetString(CUtlString(v_str.c_str()), server);
}

void CConvarManager::SetClientConvar(int playerid, const std::string& cvar_name, const std::string& value)
{
    static auto gameEventSystem = g_ifaceService.FetchInterface<IGameEventSystem>(GAMEEVENTSYSTEM_INTERFACE_VERSION);

    const auto netmsg = networkMessages->FindNetworkMessageById(6);
    auto msg = netmsg->AllocateMessage()->ToPB<CNETMsg_SetConVar>();

    CMsg_CVars_CVar* cvar = msg->mutable_convars()->add_cvars();
    cvar->set_name(cvar_name);
    cvar->set_value(value);

    bypassPostEventAbstractHook = true;

    CSingleRecipientFilter filter(playerid);
    gameEventSystem->PostEventAbstract(-1, false, &filter, netmsg, msg, 0);

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

void CConvarManager::AddFlags(std::string cvar_name, uint64_t flags)
{
    ConVarRefAbstract cvar(cvar_name.c_str());
    if (!cvar.IsConVarDataValid()) return;

    cvar.AddFlags(flags);
}

void CConvarManager::RemoveFlags(std::string cvar_name, uint64_t flags)
{
    ConVarRefAbstract cvar(cvar_name.c_str());
    if (!cvar.IsConVarDataValid()) return;

    cvar.RemoveFlags(flags);
}

void CConvarManager::ClearFlags(std::string cvar_name)
{
    ConVarRefAbstract cvar(cvar_name.c_str());
    if (!cvar.IsConVarDataValid()) return;

    cvar.RemoveFlags(cvar.GetFlags());
}

uint64_t CConvarManager::GetFlags(std::string cvar_name)
{
    ConVarRefAbstract cvar(cvar_name.c_str());
    if (!cvar.IsConVarDataValid()) return 0;

    return cvar.GetFlags();
}

uint64_t CConvarManager::AddGlobalChangeListener(std::function<void(const char*, int, const char*, const char*)> callback)
{
    g_mChangeCallbacks[g_uChangeCallbackId++] = callback;
    return g_uChangeCallbackId - 1;
}

void CConvarManager::RemoveGlobalChangeListener(uint64_t callback_id)
{
    if (g_mChangeCallbacks.find(callback_id) != g_mChangeCallbacks.end())
        g_mChangeCallbacks.erase(callback_id);
}

uint64_t CConvarManager::AddConvarCreatedListener(std::function<void(const char*)> callback)
{
    g_mCreatedConvarsCallbacks[g_uCreatedConvarId++] = callback;
    return g_uCreatedConvarId - 1;
}

void CConvarManager::RemoveConvarCreatedListener(uint64_t callback_id)
{
    if (g_mCreatedConvarsCallbacks.find(callback_id) != g_mCreatedConvarsCallbacks.end())
        g_mCreatedConvarsCallbacks.erase(callback_id);
}

uint64_t CConvarManager::AddConCommandCreatedListener(std::function<void(const char*)> callback)
{
    g_mCreateConCommandCallbacks[g_uCreatedConCommandId++] = callback;
    return g_uCreatedConCommandId - 1;
}

void CConvarManager::RemoveConCommandCreatedListener(uint64_t callback_id)
{
    if (g_mCreateConCommandCallbacks.find(callback_id) != g_mCreateConCommandCallbacks.end())
        g_mCreateConCommandCallbacks.erase(callback_id);
}