#ifndef _src_host_h
#define _src_host_h

#include <coreclr_delegates.h>
#include <hostfxr.h>
#include <string>
#include <string_view>

[[nodiscard]] bool InitializeHostFXR(std::string_view origin_path);
[[nodiscard]] bool InitializeDotNetAPI(void* scripting_table, int scripting_table_size, std::string_view log_path);

#endif
