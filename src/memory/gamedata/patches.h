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

#ifndef src_memory_gamedata_patches_h
#define src_memory_gamedata_patches_h

#include <api/memory/gamedata/patches.h>

#include <string>
#include <map>
#include <vector>
#include <api/utils/mutex.h>

class GameDataPatches : public IGameDataPatches
{
public:
    virtual void Load(const std::string& game) override;
    virtual bool Exists(const std::string& name) override;

    virtual void Apply(const std::string& name) override;
    virtual void Revert(const std::string& name) override;
private:
    std::map<std::string, std::pair<std::string, std::string>> m_mPatches;
    std::map<std::string, std::vector<uint8_t>> m_mOriginalBytes;

    QueueMutex m_mtxLock;
};

#endif