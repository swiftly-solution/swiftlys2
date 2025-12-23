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

#ifndef src_api_server_configuration_h
#define src_api_server_configuration_h

#include <string>
#include <variant>
#include <vector>
#include <map>

struct ValueStruct;

using ValueType = std::variant<int, float, double, bool, std::string, std::vector<ValueStruct>, std::map<ValueStruct, ValueStruct>>;

struct ValueStruct
{
    ValueType value;

    bool operator<(const ValueStruct& other) const
    {
        return value.index() < other.value.index() ||
            (value.index() == other.value.index() && value < other.value);
    }

    bool operator==(const ValueStruct& other) const
    {
        return value == other.value;
    }
};

class IConfiguration
{
public:
    virtual void InitializeExamples() = 0;

    virtual bool Load() = 0;
    virtual bool IsLoaded() = 0;

    virtual std::map<std::string, ValueType>& GetConfiguration() = 0;

    virtual ValueType& GetValue(const std::string& key) = 0;
    virtual void SetValue(const std::string& key, ValueType value) = 0;
    virtual bool HasKey(const std::string& key) = 0;
};

#endif