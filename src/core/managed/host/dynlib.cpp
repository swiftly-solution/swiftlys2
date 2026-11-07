#include "dynlib.h"

#include <api/shared/profiler.h>

#ifdef _WIN32
void* load_library(const char_t* path)
{
    PROFILE_FUNCTION();

    HMODULE h =::LoadLibraryW(path);
    return (void*)h;
}

void* get_export(void* h, const char* name)
{
    PROFILE_FUNCTION();

    void* f = ::GetProcAddress((HMODULE)h, name);
    return f;
}

void unload_library(void* lib)
{
    PROFILE_FUNCTION();

    if (!lib) return;
    ::FreeLibrary((HMODULE)lib);
}
#else
void* load_library(const char_t* path)
{
    PROFILE_FUNCTION();

    void* h = dlopen(path, RTLD_LAZY | RTLD_LOCAL);
    return h;
}
void* get_export(void* h, const char* name)
{
    PROFILE_FUNCTION();

    void* f = dlsym(h, name);
    return f;
}
void unload_library(void* lib)
{
    PROFILE_FUNCTION();

    if (!lib) return;
    dlclose(lib);
}
#endif