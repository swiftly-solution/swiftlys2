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

#include <scripting/scripting.h>
#include <public/tier0/icommandline.h>
#include <public/tier1/utlstringtoken.h>

#include <string>

bool Scripting_CommandLine_HasParameter(const char* param)
{
    ICommandLine* cmdLine = CommandLine();
    if (!cmdLine) return false;

    CUtlStringToken token(param);
    return cmdLine->HasParm(token);
}

int Scripting_CommandLine_GetParameterCount()
{
    ICommandLine* cmdLine = CommandLine();
    if (!cmdLine) return 0;

    return cmdLine->ParmCount();
}

int Scripting_CommandLine_GetParameterValueString(char* out, const char* param, const char* defaultValue)
{
    ICommandLine* cmdLine = CommandLine();
    if (!cmdLine) return 1;

    CUtlStringToken token(param);
    std::string s = cmdLine->ParmValue(token, defaultValue);

    if (out != nullptr) strcpy(out, s.c_str());

    return s.size();
}

int Scripting_CommandLine_GetParameterValueInt(const char* param, int defaultValue)
{
    ICommandLine* cmdLine = CommandLine();
    if (!cmdLine) return defaultValue;

    CUtlStringToken token(param);
    return cmdLine->ParmValue(token, defaultValue);
}

float Scripting_CommandLine_GetParameterValueFloat(const char* param, float defaultValue)
{
    ICommandLine* cmdLine = CommandLine();
    if (!cmdLine) return defaultValue;

    CUtlStringToken token(param);
    return cmdLine->ParmValue(token, defaultValue);
}

int Scripting_CommandLine_GetCommandLine(char* out)
{
    ICommandLine* cmdLine = CommandLine();
    if (!cmdLine) return 0;

    std::string s = cmdLine->GetCmdLine();

    if (out != nullptr) strcpy(out, s.c_str());

    return s.size();
}

bool Scripting_CommandLine_HasParameters()
{
    ICommandLine* cmdLine = CommandLine();
    if (!cmdLine) return false;

    return cmdLine->HasParms();
}

DEFINE_NATIVE("CommandLine.HasParameter", Scripting_CommandLine_HasParameter);
DEFINE_NATIVE("CommandLine.GetParameterCount", Scripting_CommandLine_GetParameterCount);
DEFINE_NATIVE("CommandLine.GetParameterValueString", Scripting_CommandLine_GetParameterValueString);
DEFINE_NATIVE("CommandLine.GetParameterValueInt", Scripting_CommandLine_GetParameterValueInt);
DEFINE_NATIVE("CommandLine.GetParameterValueFloat", Scripting_CommandLine_GetParameterValueFloat);
DEFINE_NATIVE("CommandLine.GetCommandLine", Scripting_CommandLine_GetCommandLine);
DEFINE_NATIVE("CommandLine.HasParameters", Scripting_CommandLine_HasParameters);