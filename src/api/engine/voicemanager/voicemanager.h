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

 /******************************************************************
  *
  * This feature is inspired from CounterStrikeSharp.
  * You can find the source code used by us in the following files:
  *
  * https://github.com/roflmuffin/CounterStrikeSharp/blob/a87bd25b48ff1407a71cfdce3222f5f55c8a2e0b/src/core/managers/voice_manager.h#L26
  * https://github.com/roflmuffin/CounterStrikeSharp/blob/a87bd25b48ff1407a71cfdce3222f5f55c8a2e0b/src/core/managers/voice_manager.cpp#L30
  *
  ******************************************************************/

#ifndef src_api_engine_voicemanager_voicemanager_h
#define src_api_engine_voicemanager_voicemanager_h

enum ListenOverride
{
    Listen_Default = 0,
    Listen_Mute,
    Listen_Hear
};

enum VoiceFlagValue
{
    Speak_Normal = 0,
    Speak_Muted = 1 << 0,
    Speak_All = 1 << 1,
    Speak_ListenAll = 1 << 2,
    Speak_Team = 1 << 3,
    Speak_ListenTeam = 1 << 4,
};

class IVoiceManager
{
public:
    virtual void Initialize() = 0;
    virtual void Shutdown() = 0;

    virtual void SetClientListenOverride(int playerid, int targetid, ListenOverride override) = 0;
    virtual ListenOverride GetClientListenOverride(int playerid, int targetid) = 0;

    virtual void SetClientVoiceFlags(int playerid, VoiceFlagValue flags) = 0;
    virtual VoiceFlagValue GetClientVoiceFlags(int playerid) = 0;
};

#endif