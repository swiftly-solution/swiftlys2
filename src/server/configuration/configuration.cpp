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
#include <api/shared/profiler.h>
#include <api/interfaces/interfaces.h>

#include <core/entrypoint.h>

#include <format>

#include <nlohmann/json.hpp>

using json = nlohmann::json;

static ValueType FromJson(const json& j)
{
    PROFILE_FUNCTION();

    if (j.is_number_integer()) return j.get<int>();
    if (j.is_number_float()) return j.get<double>();
    if (j.is_boolean()) return j.get<bool>();
    if (j.is_string()) return j.get<std::string>();

    if (j.is_array())
    {
        std::vector<ValueStruct> items;
        for (const auto& item : j)
            items.push_back({ FromJson(item) });
        return items;
    }

    if (j.is_object())
    {
        std::map<ValueStruct, ValueStruct> items;
        for (const auto& [key, item] : j.items())
            items.emplace(ValueStruct{ key }, ValueStruct{ FromJson(item) });
        return items;
    }

    return ValueType{};
}

static json ToJson(const ValueType& value)
{
    PROFILE_FUNCTION();

    return std::visit([](const auto& v) -> json {
        using T = std::decay_t<decltype(v)>;
        if constexpr (std::is_same_v<T, std::vector<ValueStruct>>) return json::array();
        else if constexpr (std::is_same_v<T, std::map<ValueStruct, ValueStruct>>) return json::object();
        else return v;
        }, value);
}

static bool SameKind(const json& value, const json& defaultValue)
{
    PROFILE_FUNCTION();

    if (defaultValue.is_number_integer()) return value.is_number_integer();
    if (defaultValue.is_number_float()) return value.is_number_float();
    return value.type() == defaultValue.type();
}

struct CoreConfig
{
    json doc;
    bool edited = false;
    std::map<std::string, ValueType> values;

    json& Resolve(const std::string& key)
    {
        PROFILE_FUNCTION();

        json* node = &doc;
        for (const auto& part : explode(key, "."))
        {
            if (!node->is_object())
                *node = json::object();
            node = &(*node)[part];
        }
        return *node;
    }

    void Add(const std::string& key, ValueType defaultValue)
    {
        PROFILE_FUNCTION();

        const json defaultJson = ToJson(defaultValue);
        json& node = Resolve(key);

        if (!SameKind(node, defaultJson))
        {
            node = defaultJson;
            edited = true;
        }

        values["core." + key] = std::holds_alternative<float>(defaultValue) ? ValueType(node.get<float>()) : FromJson(node);
    }

    void AddList(const std::string& key, const std::vector<std::string>& defaultValue, const std::string& delimiter)
    {
        PROFILE_FUNCTION();

        json& node = Resolve(key);

        if (!node.is_array())
        {
            node = defaultValue;
            edited = true;
        }

        std::vector<std::string> items;
        for (size_t i = 0; i < node.size(); i++)
        {
            if (node[i].is_string())
                items.push_back(node[i].get<std::string>());
            else
                g_pLogger->Error("Configuration", std::format("The field \"{}[{}]\" is not a string in core.jsonc.", key, i));
        }

        values["core." + key] = implode(items, delimiter);
    }
};

void Configuration::InitializeExamples()
{
    PROFILE_FUNCTION();

    for (const auto& file : Files::FetchFileNames(g_SwiftlyCore.GetCorePath() + "configs"))
    {
        if (!file.ends_with(".example.jsonc"))
            continue;

        const std::string config_name = replace(file, ".example", "");
        if (!Files::ExistsPath(config_name))
            Files::Write(config_name, Files::Read(file), false);
    }
}

bool Configuration::Load()
{
    PROFILE_FUNCTION();

    if (m_bLoaded)
        return true;

    const std::string path = g_SwiftlyCore.GetCorePath() + "configs/core.jsonc";
    const std::string game = g_SwiftlyCore.GetCurrentGame();

    try {
        CoreConfig cfg{ parseJsonc(Files::Read(path)) };

        cfg.AddList("CommandPrefixes", { "!" }, " ");
        cfg.AddList("CommandSilentPrefixes", { "/" }, " ");
        cfg.Add("AutoHotReload", true);
        cfg.Add("ProfilerLevel", 0);
        cfg.Add("ConsoleFilter", true);
        cfg.AddList("PatchesToPerform", {}, " ");

        if (game == "cs2") cfg.Add("CS2ServerGuidelines", "https://blog.counter-strike.net/index.php/server_guidelines/");
        cfg.Add(std::format("Follow{}ServerGuidelines", str_toupper(game)), true);

        cfg.Add("Language", "en");
        cfg.Add("UsePlayerLanguage", true);

        cfg.Add("Unlocker.Convars", false);
        cfg.Add("Unlocker.ConCommands", false);

        cfg.Add("Menu.NavigationPrefix", "➤");

        cfg.Add("Menu.Sound.Use.Name", "Vote.Cast.Yes");
        cfg.Add("Menu.Sound.Use.Volume", 0.75);

        cfg.Add("Menu.Sound.Scroll.Name", "UI.ContractType");
        cfg.Add("Menu.Sound.Scroll.Volume", 0.75);

        cfg.Add("Menu.Sound.Exit.Name", "Vote.Failed");
        cfg.Add("Menu.Sound.Exit.Volume", 0.75);

        cfg.Add("Menu.Buttons.Use", "e");
        cfg.Add("Menu.Buttons.Scroll", "shift");
        cfg.Add("Menu.Buttons.ScrollBack", "f");
        cfg.Add("Menu.Buttons.Exit", "tab");

        cfg.Add("ConsoleLogger.Enable", true);
        cfg.Add("ConsoleLogger.ManagedEnable", true);
        cfg.Add("ConsoleLogger.WriteIntervalMs", 2000);
        cfg.Add("ConsoleLogger.Rotation.Enable", true);
        cfg.Add("ConsoleLogger.Rotation.Mode", "file_count");
        cfg.AddList("ConsoleLogger.Rotation.AvailableModes", { "file_count", "time_interval" }, " ");
        cfg.Add("ConsoleLogger.Rotation.MaximumFiles", 60);
        cfg.Add("ConsoleLogger.Rotation.DeleteOlderThanHours", 168);

        cfg.Add("ManualLoadPlugins", false);
        cfg.AddList("PluginLoadOrder", {}, "\x01");

        cfg.AddList("Menu.AvailableInputModes", { "button", "wasd" }, " ");
        cfg.Add("Menu.InputMode", "button");

        cfg.Add("Menu.ItemsPerPage", 5);

        cfg.Add("SteamAuth.Mode", "flexible");
        cfg.AddList("SteamAuth.AvailableModes", { "flexible", "strict" }, " ");

        cfg.Add("DotnetCrashTracerLevel", 0);
        cfg.Add("WindowsFullDump", false);

        if (cfg.edited)
            Files::Write(path, cfg.doc.dump(4), false);

        std::unique_lock lock(m_mutex);
        m_mConfiguration = std::move(cfg.values);
    }
    catch (json::parse_error& e) {
        g_pLogger->Error("Configuration", std::format("Failed to parse the core configuration ('{}').\nError: {}.\n", path, e.what()));
    }

    m_bLoaded = true;
    return true;
}

ValueType& Configuration::GetValue(const std::string& key)
{
    PROFILE_FUNCTION();

    static ValueType s_missing;

    std::shared_lock lock(m_mutex);
    auto it = m_mConfiguration.find(key);
    return it != m_mConfiguration.end() ? it->second : s_missing;
}

void Configuration::SetValue(const std::string& key, ValueType value)
{
    PROFILE_FUNCTION();

    std::unique_lock lock(m_mutex);
    m_mConfiguration.insert_or_assign(key, std::move(value));
}

bool Configuration::HasKey(const std::string& key)
{
    PROFILE_FUNCTION();

    std::shared_lock lock(m_mutex);
    return m_mConfiguration.contains(key);
}
