#include "host.h"
#include "dynlib.h"
#include "strconv.h"

#include <api/shared/profiler.h>

#include <algorithm>
#include <filesystem>
#include <iostream>

static void* s_hostfxrLib = nullptr;
static hostfxr_handle s_context = nullptr;

static hostfxr_initialize_for_runtime_config_fn s_initializeForRuntimeConfig = nullptr;
static hostfxr_get_runtime_delegate_fn s_getRuntimeDelegate = nullptr;
static hostfxr_close_fn s_close = nullptr;
static hostfxr_set_runtime_property_value_fn s_setRuntimeProperty = nullptr;
static load_assembly_and_get_function_pointer_fn s_loadAssemblyAndGetFunctionPointer = nullptr;

static std::filesystem::path s_originPath;
static std::filesystem::path s_dotnetRoot;
static std::string s_originPathUtf8;

template <class Fn>
[[nodiscard]] static Fn GetHostFxrExport(const char* name)
{
    return reinterpret_cast<Fn>(get_export(s_hostfxrLib, name));
}

[[nodiscard]] static std::filesystem::path ToPath(const std::string& utf8)
{
#ifdef _WIN32
    return std::filesystem::path(StringWide(utf8));
#else
    return std::filesystem::path(utf8);
#endif
}

bool InitializeHostFXR(std::string_view origin_path)
{
    PROFILE_FUNCTION();

    if (origin_path.empty())
    {
        std::cerr << "[Swiftly] Error: Origin path is empty!" << std::endl;
        return false;
    }

    s_originPathUtf8 = origin_path;
#ifdef _WIN32
    std::ranges::replace(s_originPathUtf8, '/', '\\');
#endif
    s_originPath = ToPath(s_originPathUtf8);

    const auto hostfxrPath = s_originPath / "bin" / WIN_LIN("win64", "linuxsteamrt64") / WIN_LIN("hostfxr.dll", "libhostfxr.so");
    s_hostfxrLib = load_library(hostfxrPath.c_str());
    if (!s_hostfxrLib)
    {
        std::cerr << "[Swiftly] Error: Failed to load hostfxr library from: " << s_originPathUtf8 << std::endl;
        return false;
    }

    s_initializeForRuntimeConfig = GetHostFxrExport<hostfxr_initialize_for_runtime_config_fn>("hostfxr_initialize_for_runtime_config");
    s_getRuntimeDelegate = GetHostFxrExport<hostfxr_get_runtime_delegate_fn>("hostfxr_get_runtime_delegate");
    s_close = GetHostFxrExport<hostfxr_close_fn>("hostfxr_close");
    s_setRuntimeProperty = GetHostFxrExport<hostfxr_set_runtime_property_value_fn>("hostfxr_set_runtime_property_value");
    if (!s_initializeForRuntimeConfig || !s_getRuntimeDelegate || !s_close || !s_setRuntimeProperty)
        return false;

    s_dotnetRoot = s_originPath / "bin" / "managed" / "dotnet";
    const auto runtimeConfigPath = s_originPath / "bin" / "managed" / "SwiftlyS2.CS2.runtimeconfig.json";

    hostfxr_initialize_parameters params{};
    params.size = sizeof(params);
    params.dotnet_root = s_dotnetRoot.c_str();

    int returnCode = s_initializeForRuntimeConfig(runtimeConfigPath.c_str(), &params, &s_context);
    if (returnCode != 0)
    {
        std::cerr << "[Swiftly] Error: Failed to initialize .NET runtime (code: " << returnCode << ")" << std::endl;
        std::cerr << "[Swiftly] Config path: " << s_originPathUtf8 << std::endl;
        s_close(s_context);
        return false;
    }

    s_setRuntimeProperty(s_context, STR("APP_CONTEXT_BASE_DIRECTORY"), s_dotnetRoot.c_str());

    returnCode = s_getRuntimeDelegate(s_context, hdt_load_assembly_and_get_function_pointer, reinterpret_cast<void**>(&s_loadAssemblyAndGetFunctionPointer));
    if (returnCode != 0 || !s_loadAssemblyAndGetFunctionPointer)
    {
        s_close(s_context);
        return false;
    }

    return true;
}

bool InitializeDotNetAPI(void* scripting_table, int scripting_table_size)
{
    PROFILE_FUNCTION();

    using custom_loader_fn = void(CORECLR_DELEGATE_CALLTYPE*)(void*, int, const GlobalState*);
    static custom_loader_fn custom_loader = nullptr;

    if (custom_loader)
        return true;

    const auto dllPath = s_originPath / "bin" / "managed" / "SwiftlyS2.CS2.dll";
    const int returnCode = s_loadAssemblyAndGetFunctionPointer(dllPath.c_str(), STR("SwiftlyS2.Entrypoint, SwiftlyS2.CS2"), STR("Start"), UNMANAGEDCALLERSONLY_METHOD, nullptr, reinterpret_cast<void**>(&custom_loader));
    if (returnCode != 0 || !custom_loader)
    {
        std::cerr << "[Swiftly] Error: Failed to load .NET assembly (code: " << returnCode << ")" << std::endl;
        return false;
    }

    g_GlobalState.size = sizeof(g_GlobalState);
    g_GlobalState.rootPath = s_originPathUtf8.c_str();
    custom_loader(scripting_table, scripting_table_size, &g_GlobalState);

    return true;
}
