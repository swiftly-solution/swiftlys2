#include "host.h"
#include "dynlib.h"
#include "strconv.h"

#include <iostream>
#include <string.h>

#define MIN(a, b) (((a) < (b)) ? (a) : (b))

hostfxr_initialize_for_runtime_config_fn _initialize_for_runtime_config = nullptr;
hostfxr_get_runtime_delegate_fn _get_runtime_delegate = nullptr;
hostfxr_close_fn _close = nullptr;
load_assembly_and_get_function_pointer_fn _load_assembly_and_get_function_pointer = nullptr;
hostfxr_set_runtime_property_value_fn _set_runtime_prop_value = nullptr;
hostfxr_handle fxrcxt;

void* GetDotnetPointer(int kind);

typedef int(CORECLR_DELEGATE_CALLTYPE* load_file_fn)(void* context, const char* filePath, int len);
typedef void(CORECLR_DELEGATE_CALLTYPE* remove_file_fn)(void* context);
typedef void(CORECLR_DELEGATE_CALLTYPE* interpret_as_string_fn)(void* object, int type, const char* out, int len);
typedef void*(CORECLR_DELEGATE_CALLTYPE* allocate_pointer_fn)(int size, int count);
typedef uint64_t(CORECLR_DELEGATE_CALLTYPE* get_plugin_memory_fn)(void* context);
typedef void(CORECLR_DELEGATE_CALLTYPE* execute_function_fn)(void* ctx, void* pctx);
typedef void(CORECLR_DELEGATE_CALLTYPE* state_fn)(int state);

load_file_fn loadFile = nullptr;
interpret_as_string_fn interpretAsString = nullptr;
remove_file_fn removeFile = nullptr;

void* hostfxr_lib = nullptr;

#ifdef _WIN32
char_t dotnet_path[1024];
#else
char dotnet_path[1024];
#endif

#ifdef _WIN32
std::wstring widenedOriginPath;
#else
std::string widenedOriginPath;
#endif

std::string original_path;

allocate_pointer_fn allocatePointer = nullptr;
get_plugin_memory_fn getMemory = nullptr;
execute_function_fn execFunction = nullptr;
state_fn set_state = nullptr;

bool InitializeHostFXR(std::string origin_path)
{
#ifdef _WIN32
    for (size_t i = 0; i < origin_path.size(); ++i)
    {
        if (origin_path[i] == '/')
        {
            origin_path[i] = '\\';
        }
    }
#endif

    original_path = origin_path;

#ifdef _WIN32
    widenedOriginPath = StringWide(origin_path);
#else
    widenedOriginPath = origin_path;
#endif

    // Construct hostfxr library path
#ifdef _WIN32
    std::wstring hostfxr_path = widenedOriginPath + L"bin\\win64\\hostfxr.dll";
#else
    std::string hostfxr_path = widenedOriginPath + "bin/linuxsteamrt64/libhostfxr.so";
#endif

    hostfxr_lib = load_library(hostfxr_path.c_str());
    if (!hostfxr_lib)
    {
        std::cerr << "[Swiftly] Error: Failed to load hostfxr library from: " << original_path << std::endl;
        return false;
    }

    _initialize_for_runtime_config = (hostfxr_initialize_for_runtime_config_fn)get_export(hostfxr_lib, "hostfxr_initialize_for_runtime_config");
    if (!_initialize_for_runtime_config)
    {
        return false;
    }

    _get_runtime_delegate = (hostfxr_get_runtime_delegate_fn)get_export(hostfxr_lib, "hostfxr_get_runtime_delegate");
    if (!_get_runtime_delegate)
    {
        return false;
    }

    _close = (hostfxr_close_fn)get_export(hostfxr_lib, "hostfxr_close");
    if (!_close)
    {
        return false;
    }

    _set_runtime_prop_value = (hostfxr_set_runtime_property_value_fn)get_export(hostfxr_lib, "hostfxr_set_runtime_property_value");
    if (!_set_runtime_prop_value)
    {
        return false;
    }

    // Initialize params structure completely
    hostfxr_initialize_parameters params;
    memset(&params, 0, sizeof(params));
    params.size = sizeof(hostfxr_initialize_parameters);

#ifdef _WIN32
    std::wstring dotnet_root_path = widenedOriginPath + L"bin\\managed\\dotnet";
    std::wstring runtime_config_path = widenedOriginPath + L"bin\\managed\\SwiftlyS2.CS2.runtimeconfig.json";
#else
    std::string dotnet_root_path = widenedOriginPath + "bin/managed/dotnet";
    std::string runtime_config_path = widenedOriginPath + "bin/managed/SwiftlyS2.CS2.runtimeconfig.json";
#endif

    // Validate origin path
    if (widenedOriginPath.empty())
    {
        std::cerr << "[Swiftly] Error: Origin path is empty!" << std::endl;
        return false;
    }

    // Validate constructed paths
    if (dotnet_root_path.empty() || runtime_config_path.empty())
    {
        std::cerr << "[Swiftly] Error: Runtime paths are empty. Origin path: " << original_path << std::endl;
        return false;
    }

    // Clear and copy dotnet root path to buffer with bounds checking
    memset(dotnet_path, 0, sizeof(dotnet_path));
#ifdef _WIN32
    size_t copy_size = MIN(dotnet_root_path.size() * sizeof(wchar_t), sizeof(dotnet_path) - sizeof(wchar_t));
#else
    size_t copy_size = MIN(dotnet_root_path.size(), sizeof(dotnet_path) - 1);
#endif
    memcpy(dotnet_path, dotnet_root_path.c_str(), copy_size);

    params.dotnet_root = dotnet_path;

    // Initialize .NET runtime (using local variable to avoid dangling pointer)
    int returnCode = _initialize_for_runtime_config(runtime_config_path.c_str(), &params, &fxrcxt);
    if (returnCode != 0)
    {
        std::cerr << "[Swiftly] Error: Failed to initialize .NET runtime (code: " << returnCode << ")" << std::endl;
        std::cerr << "[Swiftly] Config path: " << original_path << std::endl;
        _close(fxrcxt);
        return false;
    }

    _set_runtime_prop_value(fxrcxt, WIN_LIN(L"APP_CONTEXT_BASE_DIRECTORY", "APP_CONTEXT_BASE_DIRECTORY"), dotnet_path);

    returnCode = _get_runtime_delegate(fxrcxt, hdt_load_assembly_and_get_function_pointer, (void**)&_load_assembly_and_get_function_pointer);
    if (returnCode != 0 || (void*)_load_assembly_and_get_function_pointer == nullptr)
    {
        _close(fxrcxt);
        return false;
    }

    return true;
}

bool InitializeDotNetAPI(void* scripting_table, int scripting_table_size, std::string log_path)
{
    typedef void(CORECLR_DELEGATE_CALLTYPE * custom_loader_fn)(void*, int, const char*, const char*);
    static custom_loader_fn custom_loader = nullptr;

    if (custom_loader == nullptr)
    {
        // Construct DLL path as local variable to avoid dangling pointer
#ifdef _WIN32
        std::wstring dll_path = widenedOriginPath + L"bin\\managed\\SwiftlyS2.CS2.dll";
#else
        std::string dll_path = widenedOriginPath + "bin/managed/SwiftlyS2.CS2.dll";
#endif

        int returnCode = _load_assembly_and_get_function_pointer(dll_path.c_str(), STR("SwiftlyS2.Entrypoint, SwiftlyS2.CS2"), STR("Start"), UNMANAGEDCALLERSONLY_METHOD, nullptr, (void**)&custom_loader);

        if (returnCode != 0 || (void*)custom_loader == nullptr)
        {
            std::cerr << "[Swiftly] Error: Failed to load .NET assembly (code: " << returnCode << ")" << std::endl;
            return false;
        }

        static std::string s = log_path;

        // Pass the callback setter function pointer to C#
        custom_loader(scripting_table, scripting_table_size, original_path.c_str(), s.c_str());
    }

    return true;
}

void CloseHostFXR()
{
    if (fxrcxt && _close)
    {
        _close(fxrcxt);
    }
    unload_library(hostfxr_lib);
}