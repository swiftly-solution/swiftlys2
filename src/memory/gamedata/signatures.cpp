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

#include "signatures.h"
#include "manager.h"

#include <api/interfaces/manager.h>

#include <api/shared/files.h>
#include <api/shared/plat.h>
#include <api/shared/string.h>
#include <api/shared/jsonc.h>

#include <core/entrypoint.h>

#include <nlohmann/json.hpp>

#include <fmt/format.h>
#include <s2binlib/s2binlib.h>

using json = nlohmann::json;

void GameDataSignatures::Load(const std::string& game)
{
    auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);

    auto files = Files::FetchFileNames(g_SwiftlyCore.GetCorePath() + "gamedata/" + game);
    for (auto file : files) {
        if (!ends_with(file, "signatures.jsonc")) continue;

        try {
            json signaturesJson = json::object();
            signaturesJson = parseJsonc(Files::Read(file));

            for (auto& [key, value] : signaturesJson.items()) {
                if (!value.contains("lib")) {
                    logger->Error("GameData", fmt::format("Failed to parse signature '{}'.\nError: Couldn't find the field for Library. ('{}.lib')\n", key, key));
                    continue;
                }

                if (!value["lib"].is_string()) {
                    logger->Error("GameData", fmt::format("Failed to parse signature '{}'.\nError: Library is not a string. ('{}.lib')\n", key, key));
                    continue;
                }

                if (!value.contains("windows")) {
                    logger->Error("GameData", fmt::format("Failed to parse signature '{}'.\nError: Couldn't find the signature field for Windows. ('{}.windows')\n", key, key));
                    continue;
                }

                if (!value["windows"].is_string()) {
                    logger->Error("GameData", fmt::format("Failed to parse signature '{}'.\nError: Windows signature is not a string. ('{}.windows')\n", key, key));
                    continue;
                }

                if (!value.contains("linux")) {
                    logger->Error("GameData", fmt::format("Failed to parse signature '{}'.\nError: Couldn't find the signature field for Linux. ('{}.linux')\n", key, key));
                    continue;
                }

                if (!value["linux"].is_string()) {
                    logger->Error("GameData", fmt::format("Failed to parse signature '{}'.\nError: Linux signature is not a string. ('{}.linux')\n", key, key));
                    continue;
                }

                auto lib = value["lib"].get<std::string>();
                auto signature = value[WIN_LINUX("windows", "linux")].get<std::string>();

                logger->Info("GameData", fmt::format("Searching for signature '{}'...\n", key));

                void* sig = nullptr;
                if (signature.at(0) == '@' && signature.find(" ") == std::string::npos) s2binlib_find_symbol(lib.c_str(), signature.substr(1).c_str(), &sig);
                else sig = FindSignature(lib, signature);

                if (!sig)
                {
                    logger->Error("GameData", fmt::format("Couldn't find signature '{}'. (lib='{}')\n", key, lib));
                }
                else
                {
                    m_mSignatures.insert({ key, sig });
                    logger->Info("GameData", fmt::format("Loaded signature '{}' => '{}' (lib='{}').\n", key, sig, lib));
                }
            }
        }
        catch (json::parse_error& e) {
            logger->Error("GameData", fmt::format("Failed to parse file '{}'.\nError: {}.\n", file, e.what()));
            continue;
        }
    }
}

bool GameDataSignatures::Exists(const std::string& name)
{
    return m_mSignatures.contains(name);
}

void* GameDataSignatures::Fetch(const std::string& name)
{
    auto it = m_mSignatures.find(name);
    if (it != m_mSignatures.end()) return it->second;
    return nullptr;
}
