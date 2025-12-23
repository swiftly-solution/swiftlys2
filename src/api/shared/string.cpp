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

#include "string.h"
#include "plat.h"

#include <api/interfaces/manager.h>

#include <map>
#include <random>
#include <chrono>

#include <regex>
#include <ranges>

#include <fmt/format.h>

const char* wws = " \t\n\r\f\v";

std::map<std::string, std::string> terminalColors = {
    {"[default]", WIN_LINUX("\033[38;2;255;255;255m", "\e[39m")},
    {"[/]", WIN_LINUX("\033[38;2;255;255;255m", "\e[39m")},
    {"[white]", WIN_LINUX("\033[38;2;255;255;255m", "\e[39m")},
    {"[darkred]", WIN_LINUX("\x1B[31m", "\e[31m")},
    {"[lightpurple]", WIN_LINUX("\x1B[95m", "\e[95m")},
    {"[green]", WIN_LINUX("\x1B[32m", "\e[32m")},
    {"[olive]", WIN_LINUX("\x1B[33m", "\e[33m")},
    {"[lime]", WIN_LINUX("\x1B[92m", "\e[92m")},
    {"[red]", WIN_LINUX("\x1B[31m", "\e[31m")},
    {"[gray]", WIN_LINUX("\x1B[37m", "\e[37m")},
    {"[grey]", WIN_LINUX("\x1B[37m", "\e[37m")},
    {"[lightyellow]", WIN_LINUX("\x1B[93m", "\e[93m")},
    {"[yellow]", WIN_LINUX("\x1B[93m", "\e[93m")},
    {"[silver]", WIN_LINUX("\x1B[37m", "\e[37m")},
    {"[bluegrey]", WIN_LINUX("\x1B[94m", "\e[94m")},
    {"[lightblue]", WIN_LINUX("\x1B[94m", "\e[94m")},
    {"[blue]", WIN_LINUX("\x1B[34m", "\e[34m")},
    {"[darkblue]", WIN_LINUX("\x1B[34m", "\e[34m")},
    {"[purple]", WIN_LINUX("\x1B[35m", "\e[35m")},
    {"[magenta]", WIN_LINUX("\x1B[35m", "\e[35m")},
    {"[lightred]", WIN_LINUX("\x1B[91m", "\e[91m")},
    {"[gold]", WIN_LINUX("\x1B[93m", "\e[93m")},
    {"[orange]", WIN_LINUX("\x1B[33m", "\e[33m")},

    {"[bgdefault]", WIN_LINUX("\x1B[40m", "\e[40m")},
    {"[bgdarkred]", WIN_LINUX("\x1B[41m", "\e[41m")},
    {"[bglightpurple]", WIN_LINUX("\x1B[105m", "\e[105m")},
    {"[bggreen]", WIN_LINUX("\x1B[42m", "\e[42m")},
    {"[bgolive]", WIN_LINUX("\x1B[43m", "\e[43m")},
    {"[bglime]", WIN_LINUX("\x1B[102m", "\e[102m")},
    {"[bgred]", WIN_LINUX("\x1B[41m", "\e[41m")},
    {"[bggray]", WIN_LINUX("\x1B[47m", "\e[47m")},
    {"[bggrey]", WIN_LINUX("\x1B[47m", "\e[47m")},
    {"[bglightyellow]", WIN_LINUX("\x1B[103m", "\e[103m")},
    {"[bgyellow]", WIN_LINUX("\x1B[103m", "\e[103m")},
    {"[bgsilver]", WIN_LINUX("\x1B[47m", "\e[47m")},
    {"[bgbluegrey]", WIN_LINUX("\x1B[104m", "\e[104m")},
    {"[bglightblue]", WIN_LINUX("\x1B[104m", "\e[104m")},
    {"[bgblue]", WIN_LINUX("\x1B[44m", "\e[44m")},
    {"[bgdarkblue]", WIN_LINUX("\x1B[44m", "\e[44m")},
    {"[bgpurple]", WIN_LINUX("\x1B[45m", "\e[45m")},
    {"[bgmagenta]", WIN_LINUX("\x1B[45m", "\e[45m")},
    {"[bglightred]", WIN_LINUX("\x1B[101m", "\e[101m")},
    {"[bggold]", WIN_LINUX("\x1B[103m", "\e[103m")},
    {"[bgorange]", WIN_LINUX("\x1B[43m", "\e[43m")},
};

std::vector<std::string> terminalPrefixColors = {
    "[default]",
    "[/]",
    "[white]",
    "[darkred]",
    "[lightpurple]",
    "[green]",
    "[olive]",
    "[lime]",
    "[red]",
    "[lightyellow]",
    "[yellow]",
    "[bluegrey]",
    "[lightblue]",
    "[blue]",
    "[darkblue]",
    "[purple]",
    "[magenta]",
    "[lightred]",
    "[gold]",
    "[orange]",
};

std::map<std::string, std::string> colors = {
    {"[default]", "\x01"},
    {"[/]", "\x01"},
    {"[white]", "\x01"},
    {"[darkred]", "\x02"},
    {"[lightpurple]", "\x03"},
    {"[green]", "\x04"},
    {"[olive]", "\x05"},
    {"[lime]", "\x06"},
    {"[red]", "\x07"},
    {"[gray]", "\x08"},
    {"[grey]", "\x08"},
    {"[lightyellow]", "\x09"},
    {"[yellow]", "\x09"},
    {"[silver]", "\x0A"},
    {"[bluegrey]", "\x0A"},
    {"[lightblue]", "\x0B"},
    {"[blue]", "\x0B"},
    {"[darkblue]", "\x0C"},
    {"[purple]", "\x0E"},
    {"[magenta]", "\x0E"},
    {"[lightred]", "\x0F"},
    {"[gold]", "\x10"},
    {"[orange]", "\x10"},
};

std::string ProcessColor(std::string str, int team)
{
    str = replace(str, "[teamcolor]", team == 3 ? "[lightblue]" : (team == 2 ? "[yellow]" : "[lightpurple]"));
    for (auto it = colors.begin(); it != colors.end(); ++it)
    {
        str = replace(str, it->first, it->second);
    }
    return str;
}

std::string ClearColors(std::string str)
{
    str = replace(str, "[teamcolor]", "");
    for (auto it = terminalColors.begin(); it != terminalColors.end(); ++it)
    {
        str = replace(str, it->first, "");
    }
    return str;
}

std::string TerminalProcessColor(std::string str)
{
    for (auto it = terminalColors.begin(); it != terminalColors.end(); ++it)
    {
        str = replace(str, it->first, it->second);
    }
    return str;
}

std::string ClearTerminalColors(std::string str)
{
    for (auto it = terminalColors.begin(); it != terminalColors.end(); ++it)
    {
        str = replace(str, it->first, "");
    }
    return str;
}

std::string GetTerminalStringColor(std::string plugin_name)
{
    auto hash = hash_64_fnv1a_const(plugin_name.c_str());
    uint64_t steps = (hash % terminalPrefixColors.size());
    return terminalPrefixColors[steps];
}

std::string replace(std::string str, const std::string from, const std::string to)
{
    if (from.empty())
        return str;
    int start_pos = 0;
    while ((start_pos = str.find(from, start_pos)) != std::string::npos)
    {
        str.replace(start_pos, from.length(), to);
        start_pos += to.length();
    }
    return str;
}

std::vector<std::string> explode(std::string s, std::string delimiter)
{
    if (s.size() == 0) return {};
    int pos_start = 0, pos_end, delim_len = delimiter.length();
    std::string token;
    std::vector<std::string> res;

    while ((pos_end = s.find(delimiter, pos_start)) != std::string::npos)
    {
        token = s.substr(pos_start, pos_end - pos_start);
        pos_start = pos_end + delim_len;
        res.push_back(token);
    }

    res.push_back(s.substr(pos_start));
    return res;
}

std::set<std::string> explodeToSet(std::string str, std::string delimiter)
{
    if (str.size() == 0) return {};
    int pos_start = 0, pos_end, delim_len = delimiter.length();
    std::string token;
    std::set<std::string> res;

    while ((pos_end = str.find(delimiter, pos_start)) != std::string::npos)
    {
        token = str.substr(pos_start, pos_end - pos_start);
        pos_start = pos_end + delim_len;
        res.insert(token);
    }

    res.insert(str.substr(pos_start));
    return res;
}

std::string implode(std::vector<std::string>& elements, std::string delimiter)
{
    if (elements.size() == 0) return "";

    auto joined = elements | std::views::join_with(delimiter);
    return std::string(joined.begin(), joined.end());
}

bool ends_with(std::string value, std::string ending)
{
    if (value.size() < ending.size())
        return false;
    return std::equal(ending.rbegin(), ending.rend(), value.rbegin());
}

bool starts_with(std::string value, std::string starting)
{
    if (value.size() < starting.size())
        return false;
    return std::equal(starting.begin(), starting.end(), value.begin());
}

uint64_t GetTime()
{
    return std::chrono::duration_cast<std::chrono::milliseconds>(std::chrono::system_clock::now().time_since_epoch()).count();
}

std::string str_tolower(std::string s)
{
    std::transform(s.begin(), s.end(), s.begin(), [](unsigned char c)
        { return std::tolower(c); });
    return s;
}

std::string str_toupper(std::string s)
{
    std::transform(s.begin(), s.end(), s.begin(), [](unsigned char c)
        { return std::toupper(c); });
    return s;
}

int32_t genrand()
{
    static std::random_device rd;
    static std::mt19937 rng(rd());
    return std::uniform_int_distribution<int>(0, INT_MAX)(rng);
}

std::string get_uuid()
{
    return fmt::format(
        "{:04x}{:04x}-{:04x}-{:04x}-{:04x}-{:04x}{:04x}{:04x}",
        (genrand() & 0xFFFF), (genrand() & 0xFFFF),
        (genrand() & 0xFFFF),
        ((genrand() & 0x0fff) | 0x4000),
        (genrand() % 0x3fff + 0x8000),
        (genrand() & 0xFFFF), (genrand() & 0xFFFF), (genrand() & 0xFFFF));
}

std::vector<std::string> TokenizeCommand(std::string cmd)
{
    std::vector<std::string> tokens;
    std::string tmp_token;

    bool single_quote = false;
    bool double_quote = false;

    for (const char& c : cmd)
    {
        if (c == '"' && !single_quote) {
            double_quote = !double_quote;
            continue;
        }
        else if (c == '\'' && !double_quote) {
            single_quote = !single_quote;
            continue;
        }

        if (std::isspace(c) && !single_quote && !double_quote) {
            if (!tmp_token.empty()) {
                tokens.push_back(tmp_token);
                tmp_token.clear();
            }
        }
        else {
            tmp_token += c;
        }
    }

    if (!tmp_token.empty())
        tokens.push_back(tmp_token);

    return tokens;
}

void PrintTextTable(LogType type, std::string category, TextTable table)
{
    static auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);

    std::vector<std::string> rows = explode(TableToString(table), "\n");
    for (int i = 0; i < rows.size() - 1; i++)
        logger->Log(type, category, rows[i] + "\n");
}

std::string& rtrim(std::string& s, const char* t)
{
    s.erase(s.find_last_not_of(t) + 1);
    return s;
}

std::string& ltrim(std::string& s, const char* t)
{
    s.erase(0, s.find_first_not_of(t));
    return s;
}

std::string& trim(std::string& s, const char* t)
{
    return ltrim(rtrim(s, t), t);
}

std::string RemoveHtmlTags(std::string input) {
    std::regex pattern("<(/?)(div|font)[^>]*>");

    return std::regex_replace(input, pattern, "");
}