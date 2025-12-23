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

#include "offsets.h"

#include <api/shared/files.h>
#include <api/shared/string.h>
#include <api/shared/plat.h>
#include <api/shared/jsonc.h>

#include <api/interfaces/manager.h>

#include <fmt/format.h>

#include <core/entrypoint.h>

#include <nlohmann/json.hpp>

using json = nlohmann::json;

void GameDataOffsets::Load(const std::string& game)
{
    auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);

    auto files = Files::FetchFileNames(g_SwiftlyCore.GetCorePath() + "gamedata/" + game);
    for (auto file : files) {
        if (!ends_with(file, "offsets.jsonc")) continue;

        try {
            json offsetsJson = json::object();
            offsetsJson = parseJsonc(Files::Read(file));

            for (auto& [key, value] : offsetsJson.items()) {
                if (m_mOffsets.contains(key)) {
                    logger->Warning("GameData", fmt::format("Offset '{}' is already defined. Skipping...\n", key));
                    continue;
                }

                if (!value.contains("windows")) {
                    logger->Error("GameData", fmt::format("Failed to parse offset '{}'.\nError: Couldn't find the offset field for Windows. ('{}.windows')\n", key, key));
                    continue;
                }

                if (!value["windows"].is_number_integer()) {
                    logger->Error("GameData", fmt::format("Failed to parse offset '{}'.\nError: Windows offset is not an integer. ('{}.windows')\n", key, key));
                    continue;
                }

                if (!value.contains("linux")) {
                    logger->Error("GameData", fmt::format("Failed to parse offset '{}'.\nError: Couldn't find the offset field for Linux. ('{}.linux')\n", key, key));
                    continue;
                }

                if (!value["linux"].is_number_integer()) {
                    logger->Error("GameData", fmt::format("Failed to parse offset '{}'.\nError: Linux offset is not an integer. ('{}.linux')\n", key, key));
                    continue;
                }

                m_mOffsets.insert({ key, value[WIN_LINUX("windows", "linux")].get<int>() });
                logger->Info("GameData", fmt::format("Loaded offset '{}' => '{}'.\n", key, value[WIN_LINUX("windows", "linux")].get<int>()));
            }
        }
        catch (json::parse_error& e) {
            logger->Error("GameData", fmt::format("Failed to parse file '{}'.\nError: {}.\n", file, e.what()));
            continue;
        }
    }
}

bool GameDataOffsets::Exists(const std::string& name)
{
    return m_mOffsets.contains(name);
}

int GameDataOffsets::Fetch(const std::string& name)
{
    return m_mOffsets.contains(name) ? m_mOffsets.at(name) : 0;
}