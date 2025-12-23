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

#include "configuration.h"

#include <api/shared/files.h>
#include <api/shared/string.h>
#include <api/shared/jsonc.h>
#include <api/interfaces/manager.h>
#include <api/interfaces/interfaces.h>

#include <core/entrypoint.h>

#include <fmt/format.h>

#include <nlohmann/json.hpp>
#include <variant>
#include <type_traits>

using json = nlohmann::json;

void Configuration::InitializeExamples()
{
    auto files = Files::FetchFileNames(g_SwiftlyCore.GetCorePath() + "configs");
    for (auto file : files) {
        const std::string config_name = replace(file, ".example", "");
        if (ends_with(file, ".example.jsonc") && !Files::ExistsPath(config_name)) {
            Files::Write(config_name, Files::Read(file), false);
        }
    }
}

json& GetJSONDoc(json& document, std::string key, json defaultValue, bool& wasCreated)
{
    auto keys = explode(key, ".");
    json* currentDoc = &document;

    while (keys.size() > 1)
    {
        std::string k = keys[0];
        keys.erase(keys.begin());

        if (!currentDoc->contains(k) || !(*currentDoc)[k].is_object())
            (*currentDoc)[k] = nlohmann::json::object();

        currentDoc = &(*currentDoc)[k];
    }

    std::string finalKey = keys[0];
    if (!currentDoc->contains(finalKey))
    {
        (*currentDoc)[finalKey] = defaultValue;
        wasCreated = true;
    }

    return (*currentDoc)[finalKey];
}

void RegisterConfiguration(bool& wasCreated, json& document, std::string configFilePath, std::string config_prefix, std::string key, ValueType default_value)
{
    json defaultJson;

    std::visit([&defaultJson](auto&& arg) {
        using T = std::decay_t<decltype(arg)>;
        if constexpr (std::is_same_v<T, int>)
            defaultJson = arg;
        else if constexpr (std::is_same_v<T, bool>)
            defaultJson = arg;
        else if constexpr (std::is_same_v<T, float>)
            defaultJson = arg;
        else if constexpr (std::is_same_v<T, double>)
            defaultJson = arg;
        else if constexpr (std::is_same_v<T, std::string>)
            defaultJson = arg;
        else if constexpr (std::is_same_v<T, std::vector<ValueStruct>>)
            defaultJson = json::array();
        else if constexpr (std::is_same_v<T, std::map<ValueStruct, ValueStruct>>)
            defaultJson = json::object();
        }, default_value);

    json& jsonDoc = GetJSONDoc(document, key, defaultJson, wasCreated);

    std::visit([&jsonDoc, &wasCreated, &defaultJson, &config_prefix, &key](auto&& arg) {
        using T = std::decay_t<decltype(arg)>;
        if constexpr (std::is_same_v<T, int>)
        {
            if (!jsonDoc.is_number_integer())
            {
                jsonDoc = defaultJson;
                wasCreated = true;
            }
            auto config = g_ifaceService.FetchInterface<IConfiguration>(CONFIGURATION_INTERFACE_VERSION);
            config->SetValue(config_prefix + "." + key, jsonDoc.get<int>());
        }
        else if constexpr (std::is_same_v<T, float>)
        {
            if (!jsonDoc.is_number_float())
            {
                jsonDoc = defaultJson;
                wasCreated = true;
            }
            auto config = g_ifaceService.FetchInterface<IConfiguration>(CONFIGURATION_INTERFACE_VERSION);
            config->SetValue(config_prefix + "." + key, jsonDoc.get<float>());
        }
        else if constexpr (std::is_same_v<T, bool>)
        {
            if (!jsonDoc.is_boolean())
            {
                jsonDoc = defaultJson;
                wasCreated = true;
            }
            auto config = g_ifaceService.FetchInterface<IConfiguration>(CONFIGURATION_INTERFACE_VERSION);
            config->SetValue(config_prefix + "." + key, jsonDoc.get<bool>());
        }
        else if constexpr (std::is_same_v<T, double>)
        {
            if (!jsonDoc.is_number_float())
            {
                jsonDoc = defaultJson;
                wasCreated = true;
            }
            auto config = g_ifaceService.FetchInterface<IConfiguration>(CONFIGURATION_INTERFACE_VERSION);
            config->SetValue(config_prefix + "." + key, jsonDoc.get<double>());
        }
        else if constexpr (std::is_same_v<T, std::string>)
        {
            if (!jsonDoc.is_string())
            {
                jsonDoc = defaultJson;
                wasCreated = true;
            }
            auto config = g_ifaceService.FetchInterface<IConfiguration>(CONFIGURATION_INTERFACE_VERSION);
            config->SetValue(config_prefix + "." + key, jsonDoc.get<std::string>());
        }
        else if constexpr (std::is_same_v<T, std::vector<ValueStruct>>)
        {
            if (!jsonDoc.is_array())
            {
                jsonDoc = defaultJson;
                wasCreated = true;
            }

            std::vector<ValueStruct> vectorValue;
            if (jsonDoc.is_array())
            {
                for (const auto& item : jsonDoc)
                {
                    ValueStruct vs;
                    if (item.is_number_integer())
                        vs.value = item.get<int>();
                    else if (item.is_number_float())
                        vs.value = item.get<double>();
                    else if (item.is_boolean())
                        vs.value = item.get<bool>();
                    else if (item.is_string())
                        vs.value = item.get<std::string>();
                    else if (item.is_array())
                    {
                        std::vector<ValueStruct> nestedVector;
                        vs.value = nestedVector;
                    }
                    else if (item.is_object())
                    {
                        std::map<ValueStruct, ValueStruct> nestedMap;
                        vs.value = nestedMap;
                    }
                    vectorValue.push_back(vs);
                }
            }

            auto config = g_ifaceService.FetchInterface<IConfiguration>(CONFIGURATION_INTERFACE_VERSION);
            config->SetValue(config_prefix + "." + key, vectorValue);
        }
        else if constexpr (std::is_same_v<T, std::map<ValueStruct, ValueStruct>>)
        {
            if (!jsonDoc.is_object())
            {
                jsonDoc = defaultJson;
                wasCreated = true;
            }

            std::map<ValueStruct, ValueStruct> mapValue;
            if (jsonDoc.is_object())
            {
                for (auto& [jsonKey, jsonValue] : jsonDoc.items())
                {
                    ValueStruct keyStruct;
                    keyStruct.value = jsonKey;

                    ValueStruct valueStruct;
                    if (jsonValue.is_number_integer())
                        valueStruct.value = jsonValue.get<int>();
                    else if (jsonValue.is_number_float())
                        valueStruct.value = jsonValue.get<double>();
                    else if (jsonValue.is_boolean())
                        valueStruct.value = jsonValue.get<bool>();
                    else if (jsonValue.is_string())
                        valueStruct.value = jsonValue.get<std::string>();
                    else if (jsonValue.is_array())
                    {
                        std::vector<ValueStruct> nestedVector;
                        valueStruct.value = nestedVector;
                    }
                    else if (jsonValue.is_object())
                    {
                        std::map<ValueStruct, ValueStruct> nestedMap;
                        valueStruct.value = nestedMap;
                    }

                    mapValue[keyStruct] = valueStruct;
                }
            }

            auto config = g_ifaceService.FetchInterface<IConfiguration>(CONFIGURATION_INTERFACE_VERSION);
            config->SetValue(config_prefix + "." + key, mapValue);
        }
        }, default_value);
}

template <class T>
void RegisterConfigurationVector(bool& wasCreated, json& document, std::string configFilePath, std::string config_prefix, std::string key, std::vector<T> default_value, bool shouldImplode, std::string delimiter)
{
    json defaultJson = json::array();

    for (const T& val : default_value)
    {
        if constexpr (std::is_same_v<T, std::string>)
            defaultJson.push_back(val);
        else if constexpr (std::is_same_v<T, const char*>)
            defaultJson.push_back(std::string(val));
        else if constexpr (std::is_same_v<T, bool>)
            defaultJson.push_back(val);
        else if constexpr (std::is_same_v<T, uint64_t>)
            defaultJson.push_back(val);
        else if constexpr (std::is_same_v<T, uint32_t>)
            defaultJson.push_back(val);
        else if constexpr (std::is_same_v<T, uint16_t>)
            defaultJson.push_back(val);
        else if constexpr (std::is_same_v<T, uint8_t>)
            defaultJson.push_back(val);
        else if constexpr (std::is_same_v<T, int64_t>)
            defaultJson.push_back(val);
        else if constexpr (std::is_same_v<T, int32_t>)
            defaultJson.push_back(val);
        else if constexpr (std::is_same_v<T, int16_t>)
            defaultJson.push_back(val);
        else if constexpr (std::is_same_v<T, int8_t>)
            defaultJson.push_back(val);
        else if constexpr (std::is_same_v<T, float>)
            defaultJson.push_back(val);
        else if constexpr (std::is_same_v<T, double>)
            defaultJson.push_back(val);
    }

    json& jsonDoc = GetJSONDoc(document, key, defaultJson, wasCreated);

    if (!jsonDoc.is_array())
    {
        jsonDoc = defaultJson;
        wasCreated = true;
    }

    std::vector<T> result;

    for (size_t i = 0; i < jsonDoc.size(); i++)
    {
        if constexpr (std::is_same_v<T, std::string>)
        {
            if (!jsonDoc[i].is_string())
            {
                auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);
                logger->Error("Configuration", fmt::format("The field \"{}[{}]\" is not a string in {}.json.", key, i, configFilePath));
                continue;
            }
            result.push_back(jsonDoc[i].get<std::string>());
        }
        else if constexpr (std::is_same_v<T, const char*>)
        {
            if (!jsonDoc[i].is_string())
            {
                auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);
                logger->Error("Configuration", fmt::format("The field \"{}[{}]\" is not a string in {}.json.", key, i, configFilePath));
                continue;
            }
            result.push_back(jsonDoc[i].get<std::string>().c_str());
        }
        else if constexpr (std::is_same_v<T, bool>)
        {
            if (!jsonDoc[i].is_boolean())
            {
                auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);
                logger->Error("Configuration", fmt::format("The field \"{}[{}]\" is not a boolean in {}.json.", key, i, configFilePath));
                continue;
            }
            result.push_back(jsonDoc[i].get<bool>());
        }
        else if constexpr (std::is_same_v<T, uint64_t>)
        {
            if (!jsonDoc[i].is_number_unsigned())
            {
                auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);
                logger->Error("Configuration", fmt::format("The field \"{}[{}]\" is not an unsigned integer (64-bit) in {}.json.", key, i, configFilePath));
                continue;
            }
            result.push_back(jsonDoc[i].get<uint64_t>());
        }
        else if constexpr (std::is_same_v<T, uint32_t>)
        {
            if (!jsonDoc[i].is_number_unsigned())
            {
                auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);
                logger->Error("Configuration", fmt::format("The field \"{}[{}]\" is not an unsigned integer (32-bit) in {}.json.", key, i, configFilePath));
                continue;
            }
            result.push_back(jsonDoc[i].get<uint32_t>());
        }
        else if constexpr (std::is_same_v<T, uint16_t>)
        {
            if (!jsonDoc[i].is_number_unsigned())
            {
                auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);
                logger->Error("Configuration", fmt::format("The field \"{}[{}]\" is not an unsigned integer (16-bit) in {}.json.", key, i, configFilePath));
                continue;
            }
            result.push_back(static_cast<uint16_t>(jsonDoc[i].get<uint32_t>()));
        }
        else if constexpr (std::is_same_v<T, uint8_t>)
        {
            if (!jsonDoc[i].is_number_unsigned())
            {
                auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);
                logger->Error("Configuration", fmt::format("The field \"{}[{}]\" is not an unsigned integer (8-bit) in {}.json.", key, i, configFilePath));
                continue;
            }
            result.push_back(static_cast<uint8_t>(jsonDoc[i].get<uint32_t>()));
        }
        else if constexpr (std::is_same_v<T, int64_t>)
        {
            if (!jsonDoc[i].is_number_integer())
            {
                auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);
                logger->Error("Configuration", fmt::format("The field \"{}[{}]\" is not an integer (64-bit) in {}.json.", key, i, configFilePath));
                continue;
            }
            result.push_back(jsonDoc[i].get<int64_t>());
        }
        else if constexpr (std::is_same_v<T, int32_t>)
        {
            if (!jsonDoc[i].is_number_integer())
            {
                auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);
                logger->Error("Configuration", fmt::format("The field \"{}[{}]\" is not an integer (32-bit) in {}.json.", key, i, configFilePath));
                continue;
            }
            result.push_back(jsonDoc[i].get<int32_t>());
        }
        else if constexpr (std::is_same_v<T, int16_t>)
        {
            if (!jsonDoc[i].is_number_integer())
            {
                auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);
                logger->Error("Configuration", fmt::format("The field \"{}[{}]\" is not an integer (16-bit) in {}.json.", key, i, configFilePath));
                continue;
            }
            result.push_back(static_cast<int16_t>(jsonDoc[i].get<int32_t>()));
        }
        else if constexpr (std::is_same_v<T, int8_t>)
        {
            if (!jsonDoc[i].is_number_integer())
            {
                auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);
                logger->Error("Configuration", fmt::format("The field \"{}[{}]\" is not an integer (8-bit) in {}.json.", key, i, configFilePath));
                continue;
            }
            result.push_back(static_cast<int8_t>(jsonDoc[i].get<int32_t>()));
        }
        else if constexpr (std::is_same_v<T, float>)
        {
            if (!jsonDoc[i].is_number_float())
            {
                auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);
                logger->Error("Configuration", fmt::format("The field \"{}[{}]\" is not a float in {}.json.", key, i, configFilePath));
                continue;
            }
            result.push_back(jsonDoc[i].get<float>());
        }
        else if constexpr (std::is_same_v<T, double>)
        {
            if (!jsonDoc[i].is_number_float())
            {
                auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);
                logger->Error("Configuration", fmt::format("The field \"{}[{}]\" is not a double in {}.json.", key, i, configFilePath));
                continue;
            }
            result.push_back(jsonDoc[i].get<double>());
        }
    }

    if (shouldImplode)
    {
        std::vector<std::string> implodeArr;
        for (const T& val : result)
        {
            if constexpr (std::is_same_v<T, std::string>)
                implodeArr.push_back(val);
            else if constexpr (std::is_same_v<T, const char*>)
                implodeArr.push_back(std::string(val));
            else
                implodeArr.push_back(std::to_string(val));
        }

        auto config = g_ifaceService.FetchInterface<IConfiguration>(CONFIGURATION_INTERFACE_VERSION);
        config->SetValue(config_prefix + "." + key, implode(implodeArr, delimiter));
    }
    else
    {
        std::vector<ValueStruct> valueStructVector;
        for (const T& val : result)
        {
            ValueStruct vs;
            vs.value = val;
            valueStructVector.push_back(vs);
        }

        auto config = g_ifaceService.FetchInterface<IConfiguration>(CONFIGURATION_INTERFACE_VERSION);
        config->SetValue(config_prefix + "." + key, valueStructVector);
    }
}

void WriteJSONFile(std::string path, json& j)
{
    std::string content = j.dump(4);
    Files::Write(path, content, false);
}

bool Configuration::Load()
{
    if (m_bLoaded)
        return true;

    auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);

    try {
        json config_json = parseJsonc(Files::Read(g_SwiftlyCore.GetCorePath() + "configs/core.jsonc"));

        bool wasEdited = false;

        RegisterConfigurationVector<std::string>(wasEdited, config_json, "core", "core", "CommandPrefixes", { "!" }, true, " ");
        RegisterConfigurationVector<std::string>(wasEdited, config_json, "core", "core", "CommandSilentPrefixes", { "/" }, true, " ");
        RegisterConfiguration(wasEdited, config_json, "core", "core", "AutoHotReload", true);
        RegisterConfiguration(wasEdited, config_json, "core", "core", "EnableProfiler", false);
        RegisterConfiguration(wasEdited, config_json, "core", "core", "ConsoleFilter", true);
        RegisterConfigurationVector<std::string>(wasEdited, config_json, "core", "core", "PatchesToPerform", {}, true, " ");

        if (g_SwiftlyCore.GetCurrentGame() == "cs2") RegisterConfiguration(wasEdited, config_json, "core", "core", "CS2ServerGuidelines", "https://blog.counter-strike.net/index.php/server_guidelines/");
        RegisterConfiguration(wasEdited, config_json, "core", "core", fmt::format("Follow{}ServerGuidelines", str_toupper(g_SwiftlyCore.GetCurrentGame())), true);

        RegisterConfiguration(wasEdited, config_json, "core", "core", "Language", "en");
        RegisterConfiguration(wasEdited, config_json, "core", "core", "UsePlayerLanguage", true);

        RegisterConfiguration(wasEdited, config_json, "core", "core", "Menu.NavigationPrefix", "➤");

        RegisterConfiguration(wasEdited, config_json, "core", "core", "Menu.Sound.Use.Name", "Vote.Cast.Yes");
        RegisterConfiguration(wasEdited, config_json, "core", "core", "Menu.Sound.Use.Volume", 0.75);

        RegisterConfiguration(wasEdited, config_json, "core", "core", "Menu.Sound.Scroll.Name", "UI.ContractType");
        RegisterConfiguration(wasEdited, config_json, "core", "core", "Menu.Sound.Scroll.Volume", 0.75);

        RegisterConfiguration(wasEdited, config_json, "core", "core", "Menu.Sound.Exit.Name", "Vote.Failed");
        RegisterConfiguration(wasEdited, config_json, "core", "core", "Menu.Sound.Exit.Volume", 0.75);

        RegisterConfiguration(wasEdited, config_json, "core", "core", "Menu.Buttons.Use", "e");
        RegisterConfiguration(wasEdited, config_json, "core", "core", "Menu.Buttons.Scroll", "shift");
        RegisterConfiguration(wasEdited, config_json, "core", "core", "Menu.Buttons.ScrollBack", "f");
        RegisterConfiguration(wasEdited, config_json, "core", "core", "Menu.Buttons.Exit", "tab");

        RegisterConfiguration(wasEdited, config_json, "core", "core", "ManualLoadPlugins", false);
        RegisterConfigurationVector<std::string>(wasEdited, config_json, "core", "core", "PluginLoadOrder", {}, true, "\x01");

        RegisterConfigurationVector<std::string>(wasEdited, config_json, "core", "core", "Menu.AvailableInputModes", { "button", "wasd" }, true, " ");
        RegisterConfiguration(wasEdited, config_json, "core", "core", "Menu.InputMode", "button");

        // RegisterConfiguration(wasEdited, config_json, "core", "core", "Menu.KindSettings.Center.ItemsPerPage", 4);
        RegisterConfiguration(wasEdited, config_json, "core", "core", "Menu.ItemsPerPage", 5);

        // RegisterConfiguration(wasEdited, config_json, "core", "core", "Menu.KindSettings.Screen.Mode", "compatibility");

        // RegisterConfiguration(wasEdited, config_json, "core", "core", "Menu.KindSettings.Screen.Modes.Compatibility.X", 0.14);
        // RegisterConfiguration(wasEdited, config_json, "core", "core", "Menu.KindSettings.Screen.Modes.Compatibility.Y", 0.68);
        // RegisterConfiguration(wasEdited, config_json, "core", "core", "Menu.KindSettings.Screen.Modes.Compatibility.FontSize", 35);
        // RegisterConfiguration(wasEdited, config_json, "core", "core", "Menu.KindSettings.Screen.Modes.Compatibility.Font", "Sans Serif");

        // RegisterConfiguration(wasEdited, config_json, "core", "core", "Menu.KindSettings.Screen.Modes.Normal.X", 0.0);
        // RegisterConfiguration(wasEdited, config_json, "core", "core", "Menu.KindSettings.Screen.Modes.Normal.Y", 0.68);
        // RegisterConfiguration(wasEdited, config_json, "core", "core", "Menu.KindSettings.Screen.Modes.Normal.FontSize", 35);
        // RegisterConfiguration(wasEdited, config_json, "core", "core", "Menu.KindSettings.Screen.Modes.Normal.Font", "Sans Serif");

        // RegisterConfiguration(wasEdited, config_json, "core", "core", "Menu.KindSettings.Screen.DrawBackground", true);
        // RegisterConfiguration(wasEdited, config_json, "core", "core", "Menu.KindSettings.Screen.ItemsPerPage", 9);

        RegisterConfiguration(wasEdited, config_json, "core", "core", "VGUI.TextBackground.PaddingX", 0.1);
        RegisterConfiguration(wasEdited, config_json, "core", "core", "VGUI.TextBackground.PaddingY", 0.1);

        RegisterConfiguration(wasEdited, config_json, "core", "core", "SteamAuth.Mode", "flexible");
        RegisterConfigurationVector<std::string>(wasEdited, config_json, "core", "core", "SteamAuth.AvailableModes", { "flexible", "strict" }, true, " ");

        RegisterConfiguration(wasEdited, config_json, "core", "core", "DotnetCrashTracerLevel", 0);

        if (wasEdited) {
            WriteJSONFile(g_SwiftlyCore.GetCorePath() + "configs/core.jsonc", config_json);
        }
    }
    catch (json::parse_error& e) {
        logger->Error("Configuration", fmt::format("Failed to parse the core configuration ('{}configs/core.jsonc').\nError: {}.\n", g_SwiftlyCore.GetCorePath(), e.what()));
    }

    m_bLoaded = true;
    return true;
}

bool Configuration::IsLoaded()
{
    return m_bLoaded;
}

std::map<std::string, ValueType>& Configuration::GetConfiguration()
{
    return m_mConfiguration;
}

ValueType& Configuration::GetValue(const std::string& key)
{
    return m_mConfiguration[key];
}

void Configuration::SetValue(const std::string& key, ValueType value)
{
    m_mConfiguration[key] = value;
}

bool Configuration::HasKey(const std::string& key)
{
    return m_mConfiguration.contains(key);
}