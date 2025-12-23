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

#include <api/interfaces/manager.h>
#include <scripting/scripting.h>

int Bridge_Database_GetDefaultDriver(char* out)
{
    static auto db = g_ifaceService.FetchInterface<IDatabaseManager>(DATABASEMANAGER_INTERFACE_VERSION);
    static std::string o;
    o = db->GetDefaultDriver();

    if (out != nullptr)
    {
        strcpy(out, o.c_str());
    }

    return o.size();
}

int Bridge_Database_GetDefaultConnectionName(char* out)
{
    static auto db = g_ifaceService.FetchInterface<IDatabaseManager>(DATABASEMANAGER_INTERFACE_VERSION);
    static std::string o;
    o = db->GetDefaultConnectionName();

    if (out != nullptr)
    {
        strcpy(out, o.c_str());
    }

    return o.size();
}

int Bridge_Database_GetConnectionDriver(char* out, const char* connectionName)
{
    static auto db = g_ifaceService.FetchInterface<IDatabaseManager>(DATABASEMANAGER_INTERFACE_VERSION);
    static std::string o;
    auto conn = db->GetConnection(connectionName);
    o = conn.driver;

    if (out != nullptr)
    {
        strcpy(out, o.c_str());
    }

    return o.size();
}

int Bridge_Database_GetConnectionHost(char* out, const char* connectionName)
{
    static auto db = g_ifaceService.FetchInterface<IDatabaseManager>(DATABASEMANAGER_INTERFACE_VERSION);
    static std::string o;
    auto conn = db->GetConnection(connectionName);
    o = conn.host;

    if (out != nullptr)
    {
        strcpy(out, o.c_str());
    }

    return o.size();
}

int Bridge_Database_GetConnectionDatabase(char* out, const char* connectionName)
{
    static auto db = g_ifaceService.FetchInterface<IDatabaseManager>(DATABASEMANAGER_INTERFACE_VERSION);
    static std::string o;
    auto conn = db->GetConnection(connectionName);
    o = conn.database;

    if (out != nullptr)
    {
        strcpy(out, o.c_str());
    }

    return o.size();
}

int Bridge_Database_GetConnectionUser(char* out, const char* connectionName)
{
    static auto db = g_ifaceService.FetchInterface<IDatabaseManager>(DATABASEMANAGER_INTERFACE_VERSION);
    static std::string o;
    auto conn = db->GetConnection(connectionName);
    o = conn.user;

    if (out != nullptr)
    {
        strcpy(out, o.c_str());
    }

    return o.size();
}

int Bridge_Database_GetConnectionPass(char* out, const char* connectionName)
{
    static auto db = g_ifaceService.FetchInterface<IDatabaseManager>(DATABASEMANAGER_INTERFACE_VERSION);
    static std::string o;
    auto conn = db->GetConnection(connectionName);
    o = conn.pass;

    if (out != nullptr)
    {
        strcpy(out, o.c_str());
    }

    return o.size();
}

uint32_t Bridge_Database_GetConnectionTimeout(const char* connectionName)
{
    static auto db = g_ifaceService.FetchInterface<IDatabaseManager>(DATABASEMANAGER_INTERFACE_VERSION);
    auto conn = db->GetConnection(connectionName);
    return conn.timeout;
}

uint16_t Bridge_Database_GetConnectionPort(const char* connectionName)
{
    static auto db = g_ifaceService.FetchInterface<IDatabaseManager>(DATABASEMANAGER_INTERFACE_VERSION);
    auto conn = db->GetConnection(connectionName);
    return conn.port;
}

int Bridge_Database_GetConnectionRawUri(char* out, const char* connectionName)
{
    static auto db = g_ifaceService.FetchInterface<IDatabaseManager>(DATABASEMANAGER_INTERFACE_VERSION);
    static std::string o;
    auto conn = db->GetConnection(connectionName);
    o = conn.rawUri;

    if (out != nullptr)
    {
        strcpy(out, o.c_str());
    }

    return o.size();
}

bool Bridge_Database_ConnectionExists(const char* connectionName)
{
    static auto db = g_ifaceService.FetchInterface<IDatabaseManager>(DATABASEMANAGER_INTERFACE_VERSION);
    return db->ConnectionExists(connectionName);
}

DEFINE_NATIVE("Database.GetDefaultDriver", Bridge_Database_GetDefaultDriver);
DEFINE_NATIVE("Database.GetDefaultConnectionName", Bridge_Database_GetDefaultConnectionName);
DEFINE_NATIVE("Database.GetConnectionDriver", Bridge_Database_GetConnectionDriver);
DEFINE_NATIVE("Database.GetConnectionHost", Bridge_Database_GetConnectionHost);
DEFINE_NATIVE("Database.GetConnectionDatabase", Bridge_Database_GetConnectionDatabase);
DEFINE_NATIVE("Database.GetConnectionUser", Bridge_Database_GetConnectionUser);
DEFINE_NATIVE("Database.GetConnectionPass", Bridge_Database_GetConnectionPass);
DEFINE_NATIVE("Database.GetConnectionTimeout", Bridge_Database_GetConnectionTimeout);
DEFINE_NATIVE("Database.GetConnectionPort", Bridge_Database_GetConnectionPort);
DEFINE_NATIVE("Database.GetConnectionRawUri", Bridge_Database_GetConnectionRawUri);
DEFINE_NATIVE("Database.ConnectionExists", Bridge_Database_ConnectionExists);