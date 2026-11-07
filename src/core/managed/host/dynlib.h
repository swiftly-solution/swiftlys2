#ifndef _embedder_src_dotnet_dynlib_h
#define _embedder_src_dotnet_dynlib_h

#include <coreclr_delegates.h>

#ifdef _WIN32
#include <Windows.h>

#define STR(s)        L##s
#define WIN_LIN(win,lin) win
#else
#include <dlfcn.h>

#define STR(s)        s
#define WIN_LIN(win,lin) lin
#endif

void* load_library(const char_t*);
void* get_export(void*, const char*);
void unload_library(void* lib);

#endif