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

#ifndef src_engine_vgui_screentext_h
#define src_engine_vgui_screentext_h

#include <api/engine/vgui/screentext.h>

class CScreenText : public IScreenText
{
public:
    virtual void Create(Color color, const std::string& font, int size, bool drawBackground, bool isMenu) override;
    virtual void SetText(const std::string& text) override;
    virtual void SetColor(Color color) override;
    virtual void SetPosition(float posX = 0.0, float posY = 0.0) override;
    virtual void SetRenderingTo(CEntityInstance* ent) override;
    virtual void SetPlayer(IPlayer* player) override;
    virtual void RegenerateText(bool recreate = true) override;
    virtual void ResetSpawnState() override;
    virtual void UpdatePosition() override;

    virtual bool IsValidEntity() override;
    virtual IPlayer* GetPlayer() override;
    virtual int GetEntityIndex() override;
    virtual bool IsRenderingTo(CHandle<CEntityInstance> ent) override;
private:
    CHandle<CEntityInstance> pScreenEntity;
    CHandle<CEntityInstance> pRenderingTo;

    Color m_col;
    std::string m_font;
    int m_size;
    IPlayer* m_player;
    float m_posX;
    float m_posY;
    std::string m_text;
    bool m_drawBackground;
    bool m_isMenu;
};

#endif