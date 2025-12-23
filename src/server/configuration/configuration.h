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

#ifndef src_server_configuration_h
#define src_server_configuration_h

#include <api/server/configuration/configuration.h>

class Configuration : public IConfiguration
{
public:
    virtual void InitializeExamples() override;

    virtual bool Load() override;
    virtual bool IsLoaded() override;

    virtual std::map<std::string, ValueType>& GetConfiguration() override;

    virtual ValueType& GetValue(const std::string& key) override;
    virtual void SetValue(const std::string& key, ValueType value) override;
    virtual bool HasKey(const std::string& key) override;

private:
    std::map<std::string, ValueType> m_mConfiguration;
    std::map<std::string, int> m_mConfigurationArraySizes;

    bool m_bLoaded = false;
};

#endif