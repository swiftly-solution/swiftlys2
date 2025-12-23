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

#include "manager.h"

#include <api/shared/files.h>
#include <api/shared/jsonc.h>

#include <api/interfaces/manager.h>

#include <core/entrypoint.h>

#include <nlohmann/json.hpp>

#include <fmt/format.h>

#include <public/tier1/strtools.h>

using json = nlohmann::json;

DatabaseConnection CDatabaseManager::ParseUri(const std::string& uri)
{
    DatabaseConnection conn;
    conn.rawUri = uri;

    // Format: driver://user:password@host:port/database
    // SQLite format: sqlite://path/to/database.db
    auto protoEnd = uri.find("://");
    if (protoEnd == std::string::npos)
    {
        return conn;
    }

    conn.driver = uri.substr(0, protoEnd);
    std::string rest = uri.substr(protoEnd + 3);

    // Helper lambda to get default port for a driver
    auto getDefaultPort = [](const std::string& driver) -> uint16_t
        {
            if (driver == "mysql" || driver == "mariadb")
            {
                return 3306;
            }
            if (driver == "postgresql" || driver == "postgres")
            {
                return 5432;
            }
            return 0;
        };

    // Handle SQLite specially (no host/user/pass)
    if (conn.driver == "sqlite")
    {
        conn.database = rest;
        return conn;
    }

    // Find @ to separate credentials from host
    auto atPos = rest.rfind('@');
    if (atPos == std::string::npos)
    {
        // No credentials, just host:port/database
        auto slashPos = rest.find('/');
        if (slashPos != std::string::npos)
        {
            std::string hostPort = rest.substr(0, slashPos);
            conn.database = rest.substr(slashPos + 1);

            auto colonPos = hostPort.rfind(':');
            if (colonPos != std::string::npos)
            {
                conn.host = hostPort.substr(0, colonPos);
                conn.port = static_cast<uint16_t>(V_StringToInt16(hostPort.substr(colonPos + 1).c_str(), getDefaultPort(conn.driver)));
            }
            else
            {
                conn.host = hostPort;
                conn.port = getDefaultPort(conn.driver);
            }
        }
        return conn;
    }

    // Parse user:password
    std::string credentials = rest.substr(0, atPos);
    auto colonPos = credentials.find(':');
    if (colonPos != std::string::npos)
    {
        conn.user = credentials.substr(0, colonPos);
        conn.pass = credentials.substr(colonPos + 1);
    }
    else
    {
        conn.user = credentials;
    }

    // Parse host:port/database
    std::string hostPart = rest.substr(atPos + 1);
    auto slashPos = hostPart.find('/');
    if (slashPos != std::string::npos)
    {
        std::string hostPort = hostPart.substr(0, slashPos);
        conn.database = hostPart.substr(slashPos + 1);

        auto portColonPos = hostPort.rfind(':');
        if (portColonPos != std::string::npos)
        {
            conn.host = hostPort.substr(0, portColonPos);
            conn.port = static_cast<uint16_t>(V_StringToInt16(hostPort.substr(portColonPos + 1).c_str(), getDefaultPort(conn.driver)));
        }
        else
        {
            conn.host = hostPort;
            conn.port = getDefaultPort(conn.driver);
        }
    }

    return conn;
}

void CDatabaseManager::Initialize()
{
    std::string filePath = g_SwiftlyCore.GetCorePath() + "configs/database.jsonc";
    json j = parseJsonc(Files::Read(filePath));

    auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);

    if (j.empty())
    {
        logger->Error("Database Manager", fmt::format("Failed to load database config. The '{}' file is missing or invalid.\n", filePath));
        return;
    }

    m_sDefaultConnectionName = j.value("default_connection", "");

    if (!j.contains("connections") || !j["connections"].is_object())
    {
        logger->Error("Database Manager", "Database config missing 'connections' object.\n");
        return;
    }

    for (auto& [key, value] : j["connections"].items())
    {
        DatabaseConnection conn;

        if (value.is_string())
        {
            conn = ParseUri(value.get<std::string>());
        }
        else if (value.is_object())
        {
            conn.driver = value.value("driver", "mysql");
            conn.host = value.value("host", "localhost");
            conn.database = value.value("database", "");
            conn.user = value.value("user", "");
            conn.pass = value.value("pass", "");
            conn.timeout = value.value("timeout", 0u);
            conn.port = value.value("port", static_cast<uint16_t>(0));
        }
        else
        {
            continue;
        }

        m_mConnections[key] = conn;

        if (m_sDefaultConnectionName.empty())
        {
            m_sDefaultConnectionName = key;
        }
    }

    logger->Info("Database Manager", fmt::format("Loaded {} database connections. (Default Connection: {})\n", m_mConnections.size(), m_sDefaultConnectionName));
}

std::string CDatabaseManager::GetDefaultDriver()
{
    auto conn = GetDefaultConnection();
    return conn.driver;
}

std::string CDatabaseManager::GetDefaultConnectionName()
{
    return m_sDefaultConnectionName;
}

DatabaseConnection CDatabaseManager::GetDefaultConnection()
{
    return GetConnection(m_sDefaultConnectionName);
}

DatabaseConnection CDatabaseManager::GetConnection(const std::string& connectionName)
{
    auto it = m_mConnections.find(connectionName);
    return it != m_mConnections.end() ? it->second : DatabaseConnection{};
}

bool CDatabaseManager::ConnectionExists(const std::string& connectionName)
{
    return m_mConnections.contains(connectionName);
}