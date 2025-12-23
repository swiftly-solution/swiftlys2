/************************************************************************************************
 * SwiftlyS2 is a scripting framework for Source2-based games.
 * Copyright (C) 2023-2026 Swiftly Solution SRL via Sava Andrei-Sebastian and it's contributors
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 ************************************************************************************************/

#include <api/interfaces/manager.h>
#include <public/tier1/IKeyValuesSystem.h>
#include <scripting/scripting.h>

uint32_t Bridge_KeyValuesSystem_GetSymbolForString(const char* str)
{
    return KeyValuesSystem()->GetSymbolForString(str, true).Get();
}

int Bridge_KeyValuesSystem_GetStringForSymbol(char* out, int32_t symbol)
{
    std::string str = KeyValuesSystem()->GetStringForSymbol(HKeySymbol(symbol));

    if (out != nullptr) strcpy(out, str.c_str());

    return str.size();
}

DEFINE_NATIVE("KeyValuesSystem.GetSymbolForString", Bridge_KeyValuesSystem_GetSymbolForString);
DEFINE_NATIVE("KeyValuesSystem.GetStringForSymbol", Bridge_KeyValuesSystem_GetStringForSymbol);