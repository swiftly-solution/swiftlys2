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

#ifndef src_api_shared_files_h
#define src_api_shared_files_h

#include <string>
#include <vector>

namespace Files
{
    std::string GeneratePath(std::string path);
    std::string Read(std::string path);
    void Append(std::string path, std::string content, bool hasdate = true);
    void Write(std::string path, std::string content, bool hasdate = true);
    void Delete(std::string path);
    std::string GetFileName(std::string filePath);
    bool ExistsPath(std::string path);
    bool IsDirectory(std::string path);
    std::vector<std::string> FetchFileNames(std::string path);
    std::vector<std::string> FetchDirectories(std::string path);
    bool CreateDir(std::string path);
}; // namespace Files

#endif