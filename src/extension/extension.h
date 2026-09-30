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

#ifndef src_extension_extension_h
#define src_extension_extension_h

#include <string>

enum class ExtensionLoadState
{
    Loading,
    Loaded,
    Failed
};

class Extension
{
public:
    Extension(const std::string& path);
    virtual void Init(const char* name, const char* version, const char* author, const char* description);
    
    virtual bool IsInitialized();
private:
    bool initialized_;
    std::string path_;
    std::string name_;
    std::string version_;
    std::string author_;
    std::string description_;
};

#endif