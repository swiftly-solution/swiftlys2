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

#include <api/interfaces/interfaces.h>
#include <api/shared/plat.h>
#include <api/shared/profiler.h>

#include <format>

void GameDataSignatures::Load(const std::string& game)
{
    PROFILE_FUNCTION();

    ForEachGameDataEntry(game, "signatures.jsonc", [this](const std::string& key, const nlohmann::json& value) {
        auto lib = ReadGameDataField<std::string>(value, "signature", key, "lib");
        auto win = ReadGameDataField<std::string>(value, "signature", key, "windows");
        auto lin = ReadGameDataField<std::string>(value, "signature", key, "linux");
        if (!lib || !win || !lin) return;

        const std::string& signature = WIN_LINUX(*win, *lin);

        void* address = nullptr;
        if (signature.starts_with('@') && !signature.contains(' ')) g_pS2BinLib->FindSymbol(lib->c_str(), signature.c_str() + 1, &address);
        else address = FindSignature(*lib, signature);

        if (!address) {
            g_pLogger->Error("GameData", std::format("Couldn't find signature '{}'. (lib='{}')\n", key, *lib));
            return;
        }

        m_mSignatures.emplace(key, address);
    });

    g_pLogger->Info("GameData", std::format("Loaded {} signatures.\n", m_mSignatures.size()));
}

void* GameDataSignatures::Fetch(const std::string& name)
{
    PROFILE_FUNCTION();

    auto it = m_mSignatures.find(name);
    return it != m_mSignatures.end() ? it->second : nullptr;
}
