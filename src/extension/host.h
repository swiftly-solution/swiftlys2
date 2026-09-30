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

#ifndef src_extension_host_h
#define src_extension_host_h

#include "extension.h"

#include <api/extension/host.h>

#include <string>
#include <vector>
#include <unordered_map>

class CExtensionHost : public IExtensionHost
{
public:
    virtual void LoadExtensions(std::string origin_path) override;
    virtual void LoadExtension(const std::string& path);
    virtual void* GetSharedPointer(const std::string& key) override;
    virtual void SetSharedPointer(const std::string& key, void* value) override;
    virtual bool HasSharedPointer(const std::string& key) override;
    virtual void RemoveSharedPointer(const std::string& key) override;
private:
    std::vector<Extension> extensions_;
    std::unordered_map<std::string, void*> shared_pointers_;

};

#endif