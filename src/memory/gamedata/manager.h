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

#ifndef src_memory_gamedata_manager_h
#define src_memory_gamedata_manager_h

#include <api/memory/gamedata/manager.h>

#include "offsets.h"
#include "patches.h"
#include "signatures.h"

#include <api/interfaces/interfaces.h>

#include <nlohmann/json.hpp>

#include <format>
#include <functional>
#include <optional>
#include <string>
#include <type_traits>

class GameDataManager : public IGameDataManager
{
public:
    IGameDataSignatures* GetSignatures() override { return &m_signatures; }
    IGameDataOffsets* GetOffsets() override { return &m_offsets; }
    IGameDataPatches* GetPatches() override { return &m_patches; }

private:
    GameDataSignatures m_signatures;
    GameDataOffsets m_offsets;
    GameDataPatches m_patches;
};

void* FindSignature(const std::string& library, const std::string& pattern);

void ForEachGameDataEntry(const std::string& game, const std::string& suffix, const std::function<void(const std::string&, const nlohmann::json&)>& callback);

template <typename T>
std::optional<T> ReadGameDataField(const nlohmann::json& entry, const char* kind, const std::string& key, const char* field)
{
    auto it = entry.find(field);
    bool valid = it != entry.end() && (std::is_same_v<T, std::string> ? it->is_string() : it->is_number_integer());
    if (!valid) {
        g_pLogger->Error("GameData", std::format("Failed to parse {} '{}'.\nError: Field '{}.{}' is missing or has the wrong type.\n", kind, key, key, field));
        return std::nullopt;
    }
    return it->template get<T>();
}

#endif
