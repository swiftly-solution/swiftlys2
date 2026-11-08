#ifndef _embedder_src_dotnet_dynlib_h
#define _embedder_src_dotnet_dynlib_h

#include <coreclr_delegates.h>

#ifdef _WIN32
#include <Windows.h>

#define STR(s) L##s
#else
#include <dlfcn.h>

#define STR(s) s
#endif

template <class T>
constexpr T WIN_LIN([[maybe_unused]] T win, [[maybe_unused]] T lin)
{
#ifdef _WIN32
    return win;
#else
    return lin;
#endif
}

[[nodiscard]] void* load_library(const char_t* path);
[[nodiscard]] void* get_export(void* lib, const char* name);
void unload_library(void* lib);

#endif
