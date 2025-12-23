/************************************************************************************************
 *  SwiftlyS2 is a scripting framework for Source2-based games.
 *  Copyright (C) 2023-2026 Swiftly Solution SRL via Sava Andrei-Sebastian and it's contributors (samyycX)
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

#ifndef _src_scripting_scripting_h
#define _src_scripting_scripting_h

#include <api/scripting/scripting.h>

class CScriptingAPI : public IScriptingAPI
{
public:
    virtual NativeFunction* GetNativeFunctions() override;
    virtual int GetNativeFunctionsCount() override;
};

const int MAX_NATIVE_FUNCTIONS = 1024;
extern NativeFunction g_NativeFunctions[MAX_NATIVE_FUNCTIONS];
extern int g_NativeFunctionCount;

#endif 

#define DEFINE_NATIVE(name, func) \
    static int dummy_##func = (g_NativeFunctions[g_NativeFunctionCount++] = {name, (void*)func}, 0)