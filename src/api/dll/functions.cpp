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

#if defined(_WIN32) || defined(_WIN64)
#include <windows.h>
#else
#include <dlfcn.h>
#endif
#include <fmt/format.h>
#include <map>

#include "functions.h"
#include <api/shared/files.h>

static std::map<std::string, std::map<std::string, void*>> loaded_functions;
static std::map<std::string, void*> loaded_handles;

#ifdef _WIN32
const char* dlerror()
{
    static char buf[1024];
    DWORD num;

    num = GetLastError();

    if (FormatMessageA(FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS, NULL, num, MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), buf, sizeof(buf), NULL) == 0)
    {
        _snprintf(buf, sizeof(buf), "unknown error %x", num);
    }

    return buf;
}
#endif

void* GetBinaryFunction(std::string binary_relative_path, const std::string& function_name, std::string& error)
{
    binary_relative_path = Files::GeneratePath(binary_relative_path);

    if (loaded_functions.contains(binary_relative_path) && loaded_functions[binary_relative_path].contains(function_name))
    {
        return loaded_functions[binary_relative_path][function_name];
    }

    void* handle = nullptr;
#ifdef _WIN32
    if (loaded_handles.contains(binary_relative_path))
    {
        handle = loaded_handles[binary_relative_path];
    }
    else
    {
        handle = static_cast<void*>(LoadLibraryA(binary_relative_path.c_str()));
        if (!handle)
        {
            error = dlerror();
            return nullptr;
        }
        loaded_handles[binary_relative_path] = handle;
    }

    void* func = static_cast<void*>(GetProcAddress(static_cast<HMODULE>(handle), function_name.c_str()));
    if (!func)
    {
        error = fmt::format("Function '{}' not found in binary", function_name);
        return nullptr;
    }
    loaded_functions[binary_relative_path][function_name] = func;
    return func;
#else
    if (loaded_handles.contains(binary_relative_path))
    {
        handle = loaded_handles[binary_relative_path];
    }
    else
    {
        handle = dlopen(binary_relative_path.c_str(), RTLD_LAZY);
        if (!handle)
        {
            error = dlerror();
            return nullptr;
        }
        loaded_handles[binary_relative_path] = handle;
    }

    void* func = dlsym(handle, function_name.c_str());
    if (!func)
    {
        error = fmt::format("Function '{}' not found in binary", function_name);
        return nullptr;
    }
    loaded_functions[binary_relative_path][function_name] = func;
    return func;
#endif
}

void ClearBinaryCache(std::string binary_relative_path)
{
    std::string path = Files::GeneratePath(binary_relative_path);

    if (loaded_functions.contains(path))
    {
        loaded_functions.erase(path);
    }

    if (loaded_handles.contains(path))
    {
#ifdef _WIN32
        FreeLibrary(static_cast<HMODULE>(loaded_handles[path]));
#else
        dlclose(loaded_handles[path]);
#endif
        loaded_handles.erase(path);
    }
}

void* GetBinaryFunction(std::string binary_relative_path, const std::string& function_name)
{
    std::string error;
    return GetBinaryFunction(binary_relative_path, function_name, error);
}