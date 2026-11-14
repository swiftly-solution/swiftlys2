#ifndef _src_host_h
#define _src_host_h

#include <coreclr_delegates.h>
#include <hostfxr.h>
#include <cstdint>
#include <string_view>

[[nodiscard]] bool InitializeHostFXR(std::string_view origin_path);

enum class SteamAuthMode : int32_t
{
    Flexible = 0,
    Strict = 1
};

struct GlobalState
{
    int32_t size;
    const char* rootPath;
    const char* logPath;
    const char* logLevel;
    int32_t hideLogInConsole;
    int32_t managedLogEnabled;
    int32_t managedLogIntervalMs;
    int32_t profilerLevel;
    int32_t pluginManualLoadState;
    const char* pluginLoadOrder;
    const char* serverLanguage;
    int32_t followingServerGuidelines;
    int32_t usePlayerLanguage;
    int32_t useAutoHotReload;
    SteamAuthMode steamAuthMode;
};

extern GlobalState g_GlobalState;

[[nodiscard]] bool InitializeDotNetAPI(void* scripting_table, int scripting_table_size);

#endif
