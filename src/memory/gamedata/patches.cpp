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

#include <api/interfaces/manager.h>

#include <api/shared/files.h>
#include <api/shared/string.h>
#include <api/shared/plat.h>
#include <api/shared/jsonc.h>

#include <core/entrypoint.h>

#include <nlohmann/json.hpp>

#include <fmt/format.h>

using json = nlohmann::json;

void GameDataPatches::Load(const std::string& game)
{
    auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);
    auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);

    auto files = Files::FetchFileNames(g_SwiftlyCore.GetCorePath() + "gamedata/" + game);
    for (auto file : files) {
        if (!ends_with(file, "patches.jsonc")) continue;

        try {
            json patchesJson = json::object();
            patchesJson = parseJsonc(Files::Read(file));

            for (auto& [key, value] : patchesJson.items()) {
                if (!value.contains("signature")) {
                    logger->Error("GameData", fmt::format("Failed to parse patch '{}'.\nError: Couldn't find the field for Signature. ('{}.signature')\n", key, key));
                    continue;
                }

                if (!value["signature"].is_string()) {
                    logger->Error("GameData", fmt::format("Failed to parse patch '{}'.\nError: Signature is not a string. ('{}.signature')\n", key, key));
                    continue;
                }

                if (!value.contains("windows")) {
                    logger->Error("GameData", fmt::format("Failed to parse patch '{}'.\nError: Couldn't find the patch field for Windows. ('{}.windows')\n", key, key));
                    continue;
                }

                if (!value["windows"].is_string()) {
                    logger->Error("GameData", fmt::format("Failed to parse patch '{}'.\nError: Windows patch is not a string. ('{}.windows')\n", key, key));
                    continue;
                }

                if (!value.contains("linux")) {
                    logger->Error("GameData", fmt::format("Failed to parse patch '{}'.\nError: Couldn't find the patch field for Linux. ('{}.linux')\n", key, key));
                    continue;
                }

                if (!value["linux"].is_string()) {
                    logger->Error("GameData", fmt::format("Failed to parse patch '{}'.\nError: Linux patch is not a string. ('{}.linux')\n", key, key));
                    continue;
                }

                std::string signature = value["signature"].get<std::string>();
                if (!gamedata->GetSignatures()->Exists(signature))
                {
                    logger->Error("GameData", fmt::format("Failed to parse patch '{}'.\nError: Couldn't find the signature '{}'.\n", key, signature));
                    continue;
                }

                auto patch = value[WIN_LINUX("windows", "linux")].get<std::string>();
                m_mPatches.insert({ key, {patch, signature} });
                logger->Info("GameData", fmt::format("Loaded patch '{}' => '{}' (signature='{}').\n", key, patch, signature));
            }
        }
        catch (json::parse_error& e) {
            logger->Error("GameData", fmt::format("Failed to parse file '{}'.\nError: {}.\n", file, e.what()));
            continue;
        }
    }
}

bool GameDataPatches::Exists(const std::string& name)
{
    return m_mPatches.contains(name);
}

uint64_t HexStringToUint8Array(const char* hexString, std::vector<uint8_t>& byteArray, uint64_t maxBytes)
{
    if (!hexString)
        return -1;

    uint64_t hexStringLength = strlen(hexString);
    uint64_t byteCount = hexStringLength / 4;

    if (hexStringLength % 4 != 0 || byteCount == 0 || byteCount > maxBytes)
        return -1;

    for (uint64_t i = 0; i < hexStringLength; i += 4)
    {
        uint8_t byte;
        if (sscanf(hexString + i, "\\x%2hhX", &byte) != 1)
            return -1;

        byteArray.push_back(byte);
    }

    byteArray.push_back('\0');

    return byteCount;
}

std::vector<uint8_t> HexToByte(const char* src, uint64_t& length)
{
    if (!src || strlen(src) <= 0)
        return {};

    length = strlen(src) / 4;
    std::vector<uint8_t> dest;
    uint64_t byteCount = HexStringToUint8Array(src, dest, length);
    if (byteCount <= 0)
        return {};

    return dest;
}

void GameDataPatches::Apply(const std::string& name)
{
    QueueLockGuard lock(m_mtxLock);
    auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);
    auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);
    auto signature = m_mPatches[name].second;

    if (!gamedata->GetSignatures()->Exists(signature))
    {
        logger->Error("GameData", fmt::format("Failed to apply patch '{}'.\nError: Couldn't find the signature '{}'.\n", name, signature));
        return;
    }

    void* signaturePtr = gamedata->GetSignatures()->Fetch(signature);
    auto patch = m_mPatches[name].first;

    uint64_t length = 0;
    std::vector<uint8_t> patchBytes = HexToByte(("\\x" + replace(replace(patch, "?", "2A"), " ", "\\x")).c_str(), length);

    if (!m_mOriginalBytes.contains(name)) {
        m_mOriginalBytes.insert({ name, {} });
        uint8_t* originalBytes = (uint8_t*)signaturePtr;
        for (int i = 0; i < patchBytes.size(); i++)
            m_mOriginalBytes[name].push_back(originalBytes[i]);
    }

    Plat_WriteMemory(signaturePtr, patchBytes.data(), patchBytes.size());
    logger->Info("GameData", fmt::format("Applied patch '{}' (signature='{}', bytes={:02}).\n", name, signature, patchBytes.size()));
}

void GameDataPatches::Revert(const std::string& name)
{
    QueueLockGuard lock(m_mtxLock);
    auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);
    auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);

    if (m_mOriginalBytes[name].empty())
    {
        logger->Error("GameData", fmt::format("Failed to revert patch '{}'.\nError: Patch wasn't applied or doesn't exist.\n", name));
        return;
    }

    auto signature = m_mPatches[name].second;

    if (!gamedata->GetSignatures()->Exists(signature))
    {
        logger->Error("GameData", fmt::format("Failed to apply patch '{}'.\nError: Couldn't find the signature '{}'.\n", name, signature));
        return;
    }

    void* signaturePtr = gamedata->GetSignatures()->Fetch(signature);

    Plat_WriteMemory(signaturePtr, m_mOriginalBytes[name].data(), m_mOriginalBytes[name].size());

    m_mOriginalBytes[name].clear();
    logger->Info("GameData", fmt::format("Reverted patch '{}' (signature='{}', bytes={:02}).\n", name, m_mPatches[name].second, m_mOriginalBytes[name].size()));
}
