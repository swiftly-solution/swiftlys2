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

#include <api/interfaces/manager.h>
#include <scripting/scripting.h>

uint64_t Bridge_VGUI_RegisterScreenText()
{
    static auto vgui = g_ifaceService.FetchInterface<IVGUI>(VGUI_INTERFACE_VERSION);
    return vgui->RegisterScreenText();
}

void Bridge_VGUI_UnregisterScreenText(uint64_t id)
{
    static auto vgui = g_ifaceService.FetchInterface<IVGUI>(VGUI_INTERFACE_VERSION);
    vgui->UnregisterScreenText(id);
}

void Bridge_VGUI_ScreenTextCreate(uint64_t id, Color col, int fontsize, bool drawBackground, bool isMenu)
{
    static auto vgui = g_ifaceService.FetchInterface<IVGUI>(VGUI_INTERFACE_VERSION);
    IScreenText* text = vgui->GetScreenText(id);
    if (text) text->Create(col, "Sans Serif", fontsize, drawBackground, isMenu);
}

void Bridge_VGUI_ScreenTextSetText(uint64_t id, const char* text)
{
    static auto vgui = g_ifaceService.FetchInterface<IVGUI>(VGUI_INTERFACE_VERSION);
    IScreenText* screenText = vgui->GetScreenText(id);
    if (screenText) screenText->SetText(text);
}

void Bridge_VGUI_ScreenTextSetColor(uint64_t id, Color col)
{
    static auto vgui = g_ifaceService.FetchInterface<IVGUI>(VGUI_INTERFACE_VERSION);
    IScreenText* text = vgui->GetScreenText(id);
    if (text) text->SetColor(col);
}

void Bridge_VGUI_ScreenTextSetPosition(uint64_t id, float x, float y)
{
    static auto vgui = g_ifaceService.FetchInterface<IVGUI>(VGUI_INTERFACE_VERSION);
    IScreenText* text = vgui->GetScreenText(id);
    if (text) text->SetPosition(x, y);
}

DEFINE_NATIVE("VGUI.RegisterScreenText", Bridge_VGUI_RegisterScreenText);
DEFINE_NATIVE("VGUI.UnregisterScreenText", Bridge_VGUI_UnregisterScreenText);
DEFINE_NATIVE("VGUI.ScreenTextCreate", Bridge_VGUI_ScreenTextCreate);
DEFINE_NATIVE("VGUI.ScreenTextSetText", Bridge_VGUI_ScreenTextSetText);
DEFINE_NATIVE("VGUI.ScreenTextSetColor", Bridge_VGUI_ScreenTextSetColor);
DEFINE_NATIVE("VGUI.ScreenTextSetPosition", Bridge_VGUI_ScreenTextSetPosition);