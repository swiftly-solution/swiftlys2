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

#include "files.h"
#include "plat.h"
#include <public/tier0/platform.h>

#include "string.h"
#include <ctime>
#include <time.h>

#include <filesystem>
#include <fmt/format.h>

std::string GetRelativePath(std::string path)
{
    std::string str = fmt::format("{}{}csgo{}", Plat_GetGameDirectory(), WIN_LINUX("\\", "/"), WIN_LINUX("\\", "/"));
    return replace(path, str, "");
}

std::string Files::GeneratePath(std::string path)
{
    return fmt::format("{}{}csgo{}{}", Plat_GetGameDirectory(), WIN_LINUX("\\", "/"), WIN_LINUX("\\", "/"), path);
}

std::string Files::Read(std::string path)
{
    path = Files::GeneratePath(path);
    if (!std::filesystem::exists(path))
    {
        return "";
    }

    auto fp = std::fopen(path.c_str(), "rb");
    std::string s;
    std::fseek(fp, 0u, SEEK_END);
    auto size = std::ftell(fp);
    std::fseek(fp, 0u, SEEK_SET);
    s.resize(size);
    std::fread(&s[0], 1u, size, fp);
    std::fclose(fp);
    return s;
}

std::string Files::GetFileName(std::string filePath)
{
    std::vector<std::string> v = explode(filePath, "/");
    v.pop_back();
    return implode(v, "/");
}

void Files::Delete(std::string path)
{
    path = Files::GeneratePath(path);
    if (!std::filesystem::exists(path))
    {
        return;
    }

    std::filesystem::remove(path);
}

void Files::Append(std::string path, std::string content, bool hasdate)
{
    if (!Files::ExistsPath(Files::GetFileName(path)) && Files::GetFileName(path) != "")
    {
        std::filesystem::create_directories(Files::GetFileName(Files::GeneratePath(path)));
    }

    time_t now = time(0);
    tm* ltm = localtime(&now);

    char date[32];

#if GCC_COMPILER
#pragma GCC diagnostic push
#pragma GCC diagnostic ignored "-Wformat-truncation"
#endif
    if (hasdate)
    {
        snprintf(date, sizeof(date), "[%02d/%02d/%04d - %02d:%02d:%02d] ", ltm->tm_mday, ltm->tm_mon + 1, ltm->tm_year + 1900, ltm->tm_hour, ltm->tm_min, ltm->tm_sec);
    }
#if GCC_COMPILER
#pragma GCC diagnostic pop
#endif
    path = Files::GeneratePath(path);
    auto f = std::fopen(path.c_str(), "a");
    if (f)
    {
        fprintf(f, "%s%s", hasdate ? date : "", content.c_str());
        fclose(f);
    }
}

void Files::Write(std::string path, std::string content, bool hasdate)
{
    if (!Files::ExistsPath(Files::GetFileName(path)) && Files::GetFileName(path) != "")
    {
        std::filesystem::create_directories(Files::GetFileName(Files::GeneratePath(path)));
    }
    time_t now = time(0);
    tm* ltm = localtime(&now);

    char date[32];

#if GCC_COMPILER
#pragma GCC diagnostic push
#pragma GCC diagnostic ignored "-Wformat-truncation"
#endif
    if (hasdate)
    {
        snprintf(date, sizeof(date), "[%02d/%02d/%04d - %02d:%02d:%02d] ", ltm->tm_mday, ltm->tm_mon + 1, ltm->tm_year + 1900, ltm->tm_hour, ltm->tm_min, ltm->tm_sec);
    }
#if GCC_COMPILER
#pragma GCC diagnostic pop
#endif

    path = Files::GeneratePath(path);
    auto f = std::fopen(path.c_str(), "w");
    if (f)
    {
        fprintf(f, "%s%s", hasdate ? date : "", content.c_str());
        fclose(f);
    }
}

bool Files::ExistsPath(std::string path)
{
    path = Files::GeneratePath(path);
    return std::filesystem::exists(path);
}

bool Files::IsDirectory(std::string path)
{
    path = Files::GeneratePath(path);
    return std::filesystem::is_directory(path);
}

bool Files::CreateDir(std::string path)
{
    path = Files::GeneratePath(path);
    return std::filesystem::create_directory(path);
}

std::vector<std::string> Files::FetchFileNames(std::string path)
{
    std::vector<std::string> files;
    if (!ExistsPath(path))
    {
        return files;
    }

    if (!IsDirectory(path))
    {
        return files;
    }

    path = Files::GeneratePath(path);
    for (const auto& entry : std::filesystem::directory_iterator(path))
    {
        if (entry.is_directory())
        {
            std::vector<std::string> fls = Files::FetchFileNames(GetRelativePath(entry.path().string()));
            for (auto fl : fls)
            {
                files.push_back(fl);
            }
        }
        else
        {
            files.push_back(GetRelativePath(entry.path().string()));
        }
    }
    return files;
}

std::vector<std::string> Files::FetchDirectories(std::string path)
{
    std::vector<std::string> directories;
    if (!ExistsPath(path))
    {
        return directories;
    }

    if (!IsDirectory(path))
    {
        return directories;
    }

    path = Files::GeneratePath(path);
    for (const auto& entry : std::filesystem::directory_iterator(path))
    {
        if (entry.is_directory())
        {
            directories.push_back(GetRelativePath(entry.path().string()));
        }
    }
    return directories;
}