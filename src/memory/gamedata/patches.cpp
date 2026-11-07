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

#include "patches.h"
#include "manager.h"

#include <api/interfaces/interfaces.h>
#include <api/shared/plat.h>
#include <api/shared/profiler.h>

#include <format>

static std::vector<uint8_t> ParsePatchBytes(const std::string& patch)
{
    PROFILE_FUNCTION();

    auto nibble = [](char c) -> int {
        if (c >= '0' && c <= '9') return c - '0';
        if (c >= 'a' && c <= 'f') return c - 'a' + 10;
        if (c >= 'A' && c <= 'F') return c - 'A' + 10;
        return -1;
    };

    std::vector<uint8_t> bytes;
    for (size_t i = 0; i < patch.size();) {
        if (patch[i] == ' ') { ++i; continue; }

        size_t end = patch.find(' ', i);
        if (end == std::string::npos) end = patch.size();
        std::string token = patch.substr(i, end - i);
        i = end;

        if (token == "?" || token == "??") { bytes.push_back(0x2A); continue; }
        if (token.size() != 2) return {};
        int hi = nibble(token[0]), lo = nibble(token[1]);
        if (hi < 0 || lo < 0) return {};

        bytes.push_back(static_cast<uint8_t>(hi << 4 | lo));
    }
    return bytes;
}

void GameDataPatches::Load(const std::string& game)
{
    PROFILE_FUNCTION();

    ForEachGameDataEntry(game, "patches.jsonc", [this](const std::string& key, const nlohmann::json& value) {
        auto signature = ReadGameDataField<std::string>(value, "patch", key, "signature");
        auto win = ReadGameDataField<std::string>(value, "patch", key, "windows");
        auto lin = ReadGameDataField<std::string>(value, "patch", key, "linux");
        if (!signature || !win || !lin) return;

        if (!g_pGameDataManager->GetSignatures()->Exists(*signature)) {
            g_pLogger->Error("GameData", std::format("Failed to parse patch '{}'.\nError: Couldn't find the signature '{}'.\n", key, *signature));
            return;
        }

        auto bytes = ParsePatchBytes(WIN_LINUX(*win, *lin));
        if (bytes.empty()) {
            g_pLogger->Error("GameData", std::format("Failed to parse patch '{}'.\nError: Couldn't parse the patch bytes.\n", key));
            return;
        }

        m_mPatches.emplace(key, Patch{ *signature, std::move(bytes), {} });
    });

    g_pLogger->Info("GameData", std::format("Loaded {} patches.\n", m_mPatches.size()));
}

void GameDataPatches::Apply(const std::string& name)
{
    PROFILE_FUNCTION();

    std::lock_guard lock(m_mtxLock);

    auto it = m_mPatches.find(name);
    if (it == m_mPatches.end()) {
        g_pLogger->Error("GameData", std::format("Failed to apply patch '{}'.\nError: Patch doesn't exist.\n", name));
        return;
    }

    auto& patch = it->second;
    auto* address = static_cast<uint8_t*>(g_pGameDataManager->GetSignatures()->Fetch(patch.signature));
    if (!address) {
        g_pLogger->Error("GameData", std::format("Failed to apply patch '{}'.\nError: Couldn't find the signature '{}'.\n", name, patch.signature));
        return;
    }

    if (patch.original.empty()) patch.original.assign(address, address + patch.bytes.size());

    Plat_WriteMemory(address, patch.bytes.data(), static_cast<int>(patch.bytes.size()));
    g_pLogger->Info("GameData", std::format("Applied patch '{}' (signature='{}', bytes_count={:02}).\n", name, patch.signature, patch.bytes.size()));
}

void GameDataPatches::Revert(const std::string& name)
{
    PROFILE_FUNCTION();

    std::lock_guard lock(m_mtxLock);

    auto it = m_mPatches.find(name);
    if (it == m_mPatches.end() || it->second.original.empty()) {
        g_pLogger->Error("GameData", std::format("Failed to revert patch '{}'.\nError: Patch wasn't applied or doesn't exist.\n", name));
        return;
    }

    auto& patch = it->second;
    void* address = g_pGameDataManager->GetSignatures()->Fetch(patch.signature);
    if (!address) {
        g_pLogger->Error("GameData", std::format("Failed to revert patch '{}'.\nError: Couldn't find the signature '{}'.\n", name, patch.signature));
        return;
    }

    Plat_WriteMemory(address, patch.original.data(), static_cast<int>(patch.original.size()));
    g_pLogger->Info("GameData", std::format("Reverted patch '{}' (signature='{}', bytes_count={:02}).\n", name, patch.signature, patch.original.size()));
    patch.original.clear();
}
