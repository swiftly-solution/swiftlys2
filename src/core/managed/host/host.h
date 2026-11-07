#ifndef _src_host_h
#define _src_host_h

#include <coreclr_delegates.h>
#include <hostfxr.h>
#include <string>

bool InitializeHostFXR(std::string origin_path);
bool InitializeDotNetAPI(void* scripting_table, int scripting_table_size, std::string log_path);

#endif