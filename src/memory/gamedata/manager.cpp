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

#include "manager.h"

#include <api/interfaces/interfaces.h>
#include <api/shared/files.h>
#include <api/shared/jsonc.h>
#include <api/shared/profiler.h>

#include <core/entrypoint.h>

#include <format>

void* FindSignature(const std::string& library, const std::string& pattern)
{
    PROFILE_FUNCTION();

    void* result = nullptr;
    g_pS2BinLib->PatternScan(library.c_str(), pattern.c_str(), &result);
    return result;
}

void ForEachGameDataEntry(const std::string& game, const std::string& suffix, const std::function<void(const std::string&, const nlohmann::json&)>& callback)
{
    PROFILE_FUNCTION();

    for (const auto& file : Files::FetchFileNames(g_SwiftlyCore.GetCorePath() + "gamedata/" + game)) {
        if (!file.ends_with(suffix)) continue;

        try {
            auto entries = parseJsonc(Files::Read(file));
            for (auto& [key, value] : entries.items())
                callback(key, value);
        }
        catch (const nlohmann::json::exception& e) {
            g_pLogger->Error("GameData", std::format("Failed to parse file '{}'.\nError: {}.\n", file, e.what()));
        }
    }
}
