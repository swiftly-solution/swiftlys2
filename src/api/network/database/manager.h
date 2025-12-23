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

#ifndef src_api_network_database_manager_h
#define src_api_network_database_manager_h

#include <cstdint>
#include <string>

struct DatabaseConnection
{
    std::string driver;
    std::string host;
    std::string database;
    std::string user;
    std::string pass;
    uint32_t timeout = 0;
    uint16_t port = 0;
    std::string rawUri;  // Original URI if parsed from URI format
};

class IDatabaseManager
{
public:
    virtual void Initialize() = 0;

    virtual std::string GetDefaultDriver() = 0;
    virtual std::string GetDefaultConnectionName() = 0;
    virtual DatabaseConnection GetDefaultConnection() = 0;
    virtual DatabaseConnection GetConnection(const std::string& connectionName) = 0;
    virtual bool ConnectionExists(const std::string& connectionName) = 0;
};

#endif