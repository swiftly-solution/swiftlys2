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

#ifndef src_engine_voicemanager_voicemanager_h
#define src_engine_voicemanager_voicemanager_h

#include <api/engine/voicemanager/voicemanager.h>

#include <public/entity2/entitysystem.h>

class CVoiceManager : public IVoiceManager
{
public:
    virtual void Initialize() override;
    virtual void Shutdown() override;

    virtual void SetClientListenOverride(int playerid, int targetid, ListenOverride override) override;
    virtual ListenOverride GetClientListenOverride(int playerid, int targetid) override;

    virtual void SetClientVoiceFlags(int playerid, VoiceFlagValue flags) override;
    virtual VoiceFlagValue GetClientVoiceFlags(int playerid) override;
};

#endif