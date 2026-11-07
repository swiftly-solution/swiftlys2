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
#include <public/tier0/utlstringtoken.h>

#include <api/interfaces/interfaces.h>
#include <api/shared/profiler.h>

#include <string>

bool Scripting_CommandLine_HasParameter(const char* param)
{
    PROFILE_FUNCTION();

    ICommandLine* cmdLine = CommandLine();
    if (!cmdLine) return false;

    CUtlStringToken token(param);
    return cmdLine->HasParm(token);
}

int Scripting_CommandLine_GetParameterCount()
{
    PROFILE_FUNCTION();

    ICommandLine* cmdLine = CommandLine();
    if (!cmdLine) return 0;

    return cmdLine->ParmCount();
}

char* Scripting_CommandLine_GetParameterValueString(int* size, const char* param, const char* defaultValue)
{
    PROFILE_FUNCTION();

    ICommandLine* cmdLine = CommandLine();
    if (!cmdLine)
    {
        return Bridge_CopyString("", size);
    }

    CUtlStringToken token(param);
    std::string s = cmdLine->ParmValue(token, defaultValue);
    return Bridge_CopyString(s, size);
}

int Scripting_CommandLine_GetParameterValueInt(const char* param, int defaultValue)
{
    PROFILE_FUNCTION();

    ICommandLine* cmdLine = CommandLine();
    if (!cmdLine) return defaultValue;

    CUtlStringToken token(param);
    return cmdLine->ParmValue(token, defaultValue);
}

float Scripting_CommandLine_GetParameterValueFloat(const char* param, float defaultValue)
{
    PROFILE_FUNCTION();

    ICommandLine* cmdLine = CommandLine();
    if (!cmdLine) return defaultValue;

    CUtlStringToken token(param);
    return cmdLine->ParmValue(token, defaultValue);
}

char* Scripting_CommandLine_GetCommandLine(int* size)
{
    PROFILE_FUNCTION();

    ICommandLine* cmdLine = CommandLine();
    if (!cmdLine)
    {
        return Bridge_CopyString("", size);
    }

    std::string s = cmdLine->GetCmdLine();
    return Bridge_CopyString(s, size);
}

bool Scripting_CommandLine_HasParameters()
{
    PROFILE_FUNCTION();

    ICommandLine* cmdLine = CommandLine();
    if (!cmdLine) return false;

    return cmdLine->IsWellInitialized();
}

DEFINE_NATIVE("CommandLine.HasParameter", Scripting_CommandLine_HasParameter);
DEFINE_NATIVE("CommandLine.GetParameterCount", Scripting_CommandLine_GetParameterCount);
DEFINE_NATIVE("CommandLine.GetParameterValueString", Scripting_CommandLine_GetParameterValueString);
DEFINE_NATIVE("CommandLine.GetParameterValueInt", Scripting_CommandLine_GetParameterValueInt);
DEFINE_NATIVE("CommandLine.GetParameterValueFloat", Scripting_CommandLine_GetParameterValueFloat);
DEFINE_NATIVE("CommandLine.GetCommandLine", Scripting_CommandLine_GetCommandLine);
DEFINE_NATIVE("CommandLine.HasParameters", Scripting_CommandLine_HasParameters);