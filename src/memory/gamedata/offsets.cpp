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
#include "manager.h"

#include <api/interfaces/interfaces.h>
#include <api/shared/plat.h>
#include <api/shared/profiler.h>

#include <format>

void GameDataOffsets::Load(const std::string& game)
{
    PROFILE_FUNCTION();

    ForEachGameDataEntry(game, "offsets.jsonc", [this](const std::string& key, const nlohmann::json& value) {
        if (m_mOffsets.contains(key)) {
            g_pLogger->Warning("GameData", std::format("Offset '{}' is already defined. Skipping...\n", key));
            return;
        }

        auto win = ReadGameDataField<int>(value, "offset", key, "windows");
        auto lin = ReadGameDataField<int>(value, "offset", key, "linux");
        if (!win || !lin) return;

        m_mOffsets.emplace(key, WIN_LINUX(*win, *lin));
    });

    g_pLogger->Info("GameData", std::format("Loaded {} offsets.\n", m_mOffsets.size()));
}

int GameDataOffsets::Fetch(const std::string& name)
{
    PROFILE_FUNCTION();

    auto it = m_mOffsets.find(name);
    return it != m_mOffsets.end() ? it->second : 0;
}
