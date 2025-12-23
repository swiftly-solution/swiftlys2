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

#include "jsonc.h"
#include "files.h"
#include <sstream>

std::string stripJsonComments(const std::string& jsonc)
{
    std::string result;
    result.reserve(jsonc.size());

    bool inString = false;
    bool inSingleLineComment = false;
    bool inMultiLineComment = false;
    bool escaped = false;

    for (size_t i = 0; i < jsonc.size(); ++i)
    {
        char current = jsonc[i];
        char next = (i + 1 < jsonc.size()) ? jsonc[i + 1] : '\0';

        if (inString)
        {
            result += current;
            if (escaped)
            {
                escaped = false;
            }
            else if (current == '\\')
            {
                escaped = true;
            }
            else if (current == '"')
            {
                inString = false;
            }
        }
        else if (inSingleLineComment)
        {
            if (current == '\n' || current == '\r')
            {
                inSingleLineComment = false;
                result += current;
            }
        }
        else if (inMultiLineComment)
        {
            if (current == '*' && next == '/')
            {
                inMultiLineComment = false;
                ++i;
            }
        }
        else
        {
            if (current == '"')
            {
                inString = true;
                result += current;
            }
            else if (current == '/' && next == '/')
            {
                inSingleLineComment = true;
                ++i;
            }
            else if (current == '/' && next == '*')
            {
                inMultiLineComment = true;
                ++i;
            }
            else
            {
                result += current;
            }
        }
    }

    return result;
}

nlohmann::json parseJsonc(const std::string& jsonc)
{
    std::string cleanJson = stripJsonComments(jsonc);
    try {
        return nlohmann::json::parse(cleanJson);
    }
    catch (nlohmann::json::exception& e) {
        return nlohmann::json();
    }
}

void WriteJSON(std::string path, nlohmann::json& j)
{
    std::string content = j.dump(4);
    Files::Write(path, content, false);
}