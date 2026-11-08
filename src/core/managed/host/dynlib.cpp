#include "dynlib.h"

#include <api/shared/profiler.h>

#ifdef _WIN32
void* load_library(const char_t* path)
{
    PROFILE_FUNCTION();

    return ::LoadLibraryW(path);
}

void* get_export(void* lib, const char* name)
{
    PROFILE_FUNCTION();

    return reinterpret_cast<void*>(::GetProcAddress(static_cast<HMODULE>(lib), name));
}

void unload_library(void* lib)
{
    PROFILE_FUNCTION();

    if (lib)
        ::FreeLibrary(static_cast<HMODULE>(lib));
}
#else
void* load_library(const char_t* path)
{
    PROFILE_FUNCTION();

    return dlopen(path, RTLD_LAZY | RTLD_LOCAL);
}

void* get_export(void* lib, const char* name)
{
    PROFILE_FUNCTION();

    return dlsym(lib, name);
}

void unload_library(void* lib)
{
    PROFILE_FUNCTION();

    if (lib)
        dlclose(lib);
}
#endif
