/************************************************************************************************
 * SwiftlyS2 is a scripting framework for Source2-based games.
 * Copyright (C) 2025 Swiftly Solution SRL via Sava Andrei-Sebastian and it's contributors
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

using json = nlohmann::json;

void CDatabaseManager::Initialize()
{
    std::string filePath = g_SwiftlyCore.GetCorePath() + "configs/databases.jsonc";
    json j = parseJsonc(Files::Read(filePath));

    auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);

    if (j.empty())
    {
        logger->Error("Database Manager", fmt::format("Failed to load database config. The '{}' file is missing or invalid.\n", filePath));
        return;
    }

    m_sDefaultDriver = j.value("driver_default", "mysql");

    for (auto& [key, value] : j.items())
    {
        if (key == "driver_default")
        {
            continue;
        }

        if (!value.is_object())
        {
            continue;
        }
        DatabaseConnection conn;

        std::string driver = value.value("driver", "default");
        conn.driver = (driver == "default") ? m_sDefaultDriver : driver;
        conn.host = value.value("host", "");
        conn.database = value.value("database", "");
        conn.user = value.value("user", "");
        conn.pass = value.value("pass", "");
        conn.timeout = value.value("timeout", 0);
        conn.port = value.value("port", static_cast<uint16_t>(0));

        m_mConnections[key] = conn;

        if (m_sDefaultConnectionName.empty())
        {
            m_sDefaultConnectionName = key;
        }
    }

    logger->Info("Database Manager", fmt::format("Loaded {} database connections. (Default Driver: {}, Default Connection: {})\n", m_mConnections.size(), m_sDefaultDriver, m_sDefaultConnectionName));
}

std::string CDatabaseManager::GetDefaultDriver()
{
    return m_sDefaultDriver;
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