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

#include <api/interfaces/interfaces.h>
#include <scripting/scripting.h>

void Bridge_VoiceManager_SetClientListenOverride(int playerid, int targetid, int override)
{
    g_pVoiceManager->SetClientListenOverride(playerid, targetid, (ListenOverride)override);
}

int Bridge_VoiceManager_GetClientListenOverride(int playerid, int targetid)
{
    return static_cast<int>(g_pVoiceManager->GetClientListenOverride(playerid, targetid));
}

void Bridge_VoiceManager_SetClientVoiceFlags(int playerid, int flags)
{
    g_pVoiceManager->SetClientVoiceFlags(playerid, (VoiceFlagValue)flags);
}

int Bridge_VoiceManager_GetClientVoiceFlags(int playerid)
{
    return static_cast<int>(g_pVoiceManager->GetClientVoiceFlags(playerid));
}

DEFINE_NATIVE("VoiceManager.SetClientListenOverride", Bridge_VoiceManager_SetClientListenOverride);
DEFINE_NATIVE("VoiceManager.GetClientListenOverride", Bridge_VoiceManager_GetClientListenOverride);
DEFINE_NATIVE("VoiceManager.SetClientVoiceFlags", Bridge_VoiceManager_SetClientVoiceFlags);
DEFINE_NATIVE("VoiceManager.GetClientVoiceFlags", Bridge_VoiceManager_GetClientVoiceFlags);