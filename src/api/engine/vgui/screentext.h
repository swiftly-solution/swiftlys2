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

#ifndef src_api_engine_vgui_screentext_h
#define src_api_engine_vgui_screentext_h

#include <public/Color.h>
#include <string>

#include <api/server/players/player.h>
#include <game/shared/ehandle.h>

class IScreenText
{
public:
    virtual void Create(Color color, const std::string& font, int size, bool drawBackground, bool isMenu) = 0;
    virtual void SetText(const std::string& text) = 0;
    virtual void SetColor(Color color) = 0;
    virtual void SetPosition(float posX, float posY) = 0;
    virtual void SetRenderingTo(CEntityInstance* ent) = 0;
    virtual void SetPlayer(IPlayer* player) = 0;
    virtual void RegenerateText(bool recreate = true) = 0;
    virtual void ResetSpawnState() = 0;
    virtual void UpdatePosition() = 0;

    virtual bool IsValidEntity() = 0;
    virtual IPlayer* GetPlayer() = 0;
    virtual int GetEntityIndex() = 0;
    virtual bool IsRenderingTo(CHandle<CEntityInstance> ent) = 0;
};

#endif