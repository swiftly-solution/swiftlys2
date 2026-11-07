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

#include <cstdint>
#include <set>
#include <string>
#include <string_view>
#include <vector>

#include <api/interfaces/interfaces.h>
#include <api/shared/hash.h>

std::string replace(std::string str, std::string_view from, std::string_view to);
std::vector<std::string> explode(std::string_view str, std::string_view delimiter);
std::set<std::string> explodeToSet(std::string_view str, std::string_view delimiter);
std::string implode(const std::vector<std::string>& elements, const std::string& delimiter);
std::string ProcessColor(std::string str, int team);
std::string ClearColors(std::string str);
std::string str_tolower(std::string_view s);
std::string str_toupper(std::string_view s);
std::string get_uuid();
std::string TerminalProcessColor(std::string str);
std::string ClearTerminalColors(std::string str);
std::string StripAnsiEscapes(std::string_view str);
std::string GetTerminalStringColor(const std::string& plugin_name);
std::vector<std::string> TokenizeCommand(std::string_view cmd);
std::string RemoveHtmlTags(const std::string& input);
std::string replace_case_insensitive(std::string str, std::string_view from, std::string_view to);

#endif
