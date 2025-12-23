/************************************************************************************************
 * SwiftlyS2 is a scripting framework for Source2-based games.
 * Copyright (C) 2023-2026 Swiftly Solution SRL via Sava Andrei-Sebastian and it's contributors
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 ************************************************************************************************/

#include "translations.h"

#include <api/interfaces/manager.h>

#include <api/shared/files.h>
#include <api/shared/jsonc.h>
#include <api/shared/plat.h>
#include <api/shared/string.h>

#include <nlohmann/json.hpp>

using json = nlohmann::json;

static std::map<std::string, std::string> l_mLanguages = {
    { "arabic", "ar" },
    { "bulgarian", "bg" },
    { "schinese", "zh-CN" },
    { "tchinese", "zh-TW" },
    { "czech", "cs" },
    { "danish", "da" },
    { "dutch", "nl" },
    { "english", "en" },
    { "finnish", "fi" },
    { "french", "fr" },
    { "german", "de" },
    { "greek", "el" },
    { "hungarian", "hu" },
    { "indonesian", "id" },
    { "italian", "it" },
    { "japanese", "ja" },
    { "koreana", "ko" },
    { "norwegian", "no" },
    { "polish", "pl" },
    { "portuguese", "pt" },
    { "brazilian", "pt-BR" },
    { "romanian", "ro" },
    { "russian", "ru" },
    { "spanish", "es" },
    { "latam", "es-419" },
    { "swedish", "sv" },
    { "thai", "th" },
    { "turkish", "tr" },
    { "ukrainian", "uk" },
    { "vietnamese", "vn" },
};

void CTranslations::Initialize()
{
    auto cvarmanager = g_ifaceService.FetchInterface<IConvarManager>(CONVARMANAGER_INTERFACE_VERSION);

    cvarmanager->AddQueryClientCvarCallback([](int playerid, std::string cvar_name, std::string cvar_value) {
        if (cvar_name != "cl_language") return;

        auto configuration = g_ifaceService.FetchInterface<IConfiguration>(CONFIGURATION_INTERFACE_VERSION);
        auto playermanager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
        auto player = playermanager->GetPlayer(playerid);
        if (!player) return;

        auto it = l_mLanguages.find(cvar_value);
        if (it != l_mLanguages.end())
            player->GetLanguage() = it->second;
        else
            player->GetLanguage() = std::get<std::string>(configuration->GetValue("core.Language"));
        });
}