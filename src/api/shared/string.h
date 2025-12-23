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

#ifndef src_api_shared_string_h
#define src_api_shared_string_h

#include <string>
#include <vector>
#include <set>

#include "texttable.h"

#include <api/interfaces/manager.h>

constexpr uint32_t val_32_const = 0x811c9dc5;
constexpr uint32_t prime_32_const = 0x1000193;
constexpr uint64_t val_64_const = 0xcbf29ce484222325;
constexpr uint64_t prime_64_const = 0x100000001b3;

inline constexpr uint32_t hash_32_fnv1a_const(const char* const str, const uint32_t value = val_32_const) noexcept
{
    return (str[0] == '\0') ? value : hash_32_fnv1a_const(&str[1], (value ^ uint32_t(str[0])) * prime_32_const);
}

inline constexpr uint64_t hash_64_fnv1a_const(const char* const str, const uint64_t value = val_64_const) noexcept
{
    return (str[0] == '\0') ? value : hash_64_fnv1a_const(&str[1], (value ^ uint64_t(str[0])) * prime_64_const);
}

std::string replace(std::string str, const std::string from, const std::string to);
std::vector<std::string> explode(std::string str, std::string delimiter);
std::set<std::string> explodeToSet(std::string str, std::string delimiter);
std::string implode(std::vector<std::string>& elements, std::string delimiter);
std::string ProcessColor(std::string str, int team);
std::string ClearColors(std::string str);
bool ends_with(std::string value, std::string ending);
bool starts_with(std::string value, std::string starting);
uint64_t GetTime();
std::string str_tolower(std::string s);
std::string str_toupper(std::string s);
std::string get_uuid();
std::string TerminalProcessColor(std::string str);
std::string ClearTerminalColors(std::string str);
std::string GetTerminalStringColor(std::string plugin_name);
std::vector<std::string> TokenizeCommand(std::string cmd);
std::string RemoveHtmlTags(std::string input);

std::string& trim(std::string& s, const char* t = " \t\n\r\f\v");

void PrintTextTable(LogType type, std::string category, TextTable table);

#endif