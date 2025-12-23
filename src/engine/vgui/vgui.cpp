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

#include "vgui.h"
#include "screentext.h"

uint64_t CVGUI::RegisterScreenText()
{
    internalScreenTextID++;
    IScreenText* txt = new CScreenText();
    screenTexts.insert({ internalScreenTextID, txt });

    return internalScreenTextID;
}

IScreenText* CVGUI::GetScreenText(uint64_t id)
{
    if (screenTexts.find(id) == screenTexts.end()) return nullptr;

    return screenTexts[id];
}

void CVGUI::UnregisterScreenText(uint64_t id)
{
    if (screenTexts.find(id) == screenTexts.end()) return;

    IScreenText* txt = screenTexts[id];
    delete (CScreenText*)txt;

    screenTexts.erase(id);
}

void CVGUI::ResetStateOfScreenTexts()
{
    for (auto it = screenTexts.begin(); it != screenTexts.end(); ++it) {
        it->second->ResetSpawnState();
    }
}

void CVGUI::RegenerateScreenTexts()
{
    for (auto it = screenTexts.begin(); it != screenTexts.end(); ++it) {
        it->second->RegenerateText();
    }
}

void CVGUI::UnregisterForPlayer(IPlayer* player)
{
    std::vector<uint64_t> eraseIDs;
    for (auto it = screenTexts.begin(); it != screenTexts.end(); ++it) {
        if (it->second->GetPlayer() == player) {
            delete it->second;
            eraseIDs.push_back(it->first);
        }
    }

    for (auto id : eraseIDs)
        screenTexts.erase(id);
}

void CVGUI::CheckRenderForPlayer(IPlayer* player, CHandle<CEntityInstance> specView)
{
    bool shouldRegenerate = false;
    if (((rendersToSpectator & (1ULL << player->GetSlot())) != 0) && !specView) {
        rendersToSpectator &= ~(1ULL << player->GetSlot());
        shouldRegenerate = true;
    }
    else if (((rendersToSpectator & (1ULL << player->GetSlot())) == 0) && specView.IsValid()) {
        rendersToSpectator |= (1ULL << player->GetSlot());
        shouldRegenerate = true;
    }

    auto end = screenTexts.end();
    if (shouldRegenerate) {
        for (auto it = screenTexts.begin(); it != end; ++it) {
            if (it->second->GetPlayer() == player) {
                it->second->RegenerateText(false);
                it->second->SetRenderingTo(specView.Get());
            }
        }
    }
    else {
        for (auto it = screenTexts.begin(); it != end; ++it) {
            if (it->second->GetPlayer() == player && !it->second->IsRenderingTo(specView)) {
                it->second->RegenerateText(false);
                it->second->SetRenderingTo(specView.Get());
            }
        }
    }
}

void CVGUI::Update()
{
    auto end = screenTexts.end();
    for (auto it = screenTexts.begin(); it != end; ++it) {
        it->second->UpdatePosition();
    }
}