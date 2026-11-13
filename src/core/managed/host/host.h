#ifndef _src_host_h
#define _src_host_h

#include <coreclr_delegates.h>
#include <hostfxr.h>
#include <cstdint>
#include <string>
#include <string_view>

[[nodiscard]] bool InitializeHostFXR(std::string_view origin_path);

struct ManagedBootstrapInfo
{
    int32_t size;
    const char* rootPath;
    const char* logPath;
    const char* logLevel;
    int32_t hideLogInConsole;
    int32_t managedLogEnabled;
    int32_t managedLogIntervalMs;
};

[[nodiscard]] bool InitializeDotNetAPI(void* scripting_table, int scripting_table_size, ManagedBootstrapInfo info);

#endif
