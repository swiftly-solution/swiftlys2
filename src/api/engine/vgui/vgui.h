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

#ifndef src_api_engine_vgui_vgui_h
#define src_api_engine_vgui_vgui_h

#include <cstdint>
#include <api/server/players/player.h>
#include <game/shared/ehandle.h>

#include "screentext.h"

class IVGUI
{
public:
    virtual uint64_t RegisterScreenText() = 0;
    virtual IScreenText* GetScreenText(uint64_t id) = 0;
    virtual void UnregisterScreenText(uint64_t id) = 0;

    virtual void RegenerateScreenTexts() = 0;
    virtual void ResetStateOfScreenTexts() = 0;

    virtual void CheckRenderForPlayer(IPlayer* player, CHandle<CEntityInstance> specView) = 0;
    virtual void UnregisterForPlayer(IPlayer* player) = 0;

    virtual void Update() = 0;
};

#endif