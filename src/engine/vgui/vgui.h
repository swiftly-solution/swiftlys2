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

#ifndef src_engine_vgui_vgui_h
#define src_engine_vgui_vgui_h

#include <api/engine/vgui/vgui.h>

class CVGUI : public IVGUI
{
public:
    virtual uint64_t RegisterScreenText() override;
    virtual IScreenText* GetScreenText(uint64_t id) override;
    virtual void UnregisterScreenText(uint64_t id) override;

    virtual void RegenerateScreenTexts() override;
    virtual void ResetStateOfScreenTexts() override;

    virtual void CheckRenderForPlayer(IPlayer* player, CHandle<CEntityInstance> specView) override;
    virtual void UnregisterForPlayer(IPlayer* player) override;

    virtual void Update() override;
private:
    uint64_t internalScreenTextID = 0;
    std::map<uint64_t, IScreenText*> screenTexts;
    uint64_t rendersToSpectator = 0;
};

#endif