set_project("swiftlys2")
set_version("1.0.0")

add_requires("fmt", {configs = {header_only = true}})
add_requires("pcre2")

set_languages("cxx23")

add_rules("mode.debug", "mode.release")

add_includedirs("include")
add_includedirs("external/include")

local GITHUB_SHA = os.getenv("GITHUB_SHA") or "Local"
local SWIFTLY_VERSION = os.getenv("SWIFTLY_VERSION") or "Local"

local sdk_path = "vendor/s2sdk"
local metamod_path = "vendor/metamod"

function GetDistDirName()
    if is_plat("windows") then
        return "win64"
    else
        return "linuxsteamrt64"
    end
end

target("swiftlys2")
    set_kind("shared")
    add_packages("fmt")
    add_packages("pcre2")

    add_files({
        "src/**/*.cpp",

        sdk_path.."/public/tier1/keyvalues3.cpp",
        sdk_path.."/public/entity2/entitysystem.cpp",
        sdk_path.."/public/entity2/entityidentity.cpp",
        sdk_path.."/public/tier1/convar.cpp",
        sdk_path.."/public/entity2/entitykeyvalues.cpp",
        sdk_path.."/public/tier0/memoverride.cpp",
    }, { cxxflags = "-rdynamic -g1" })

    --[[ -------------------------------- Include Section -------------------------------- ]]

    add_includedirs({
        "src",
        "vendor",

        "build/proto",

        "src/core/managed/host",
        "src/core/managed/libs/dotnet",

        sdk_path,
        sdk_path.."/thirdparty/protobuf-3.21.8/src",
        sdk_path.."/public",
        sdk_path.."/public/engine",
        sdk_path.."/public/mathlib",
        sdk_path.."/public/vstdlib",
        sdk_path.."/public/tier0",
        sdk_path.."/public/tier1",
        sdk_path.."/public/entity2",
        sdk_path.."/public/game/server",
        sdk_path.."/game/shared",
        sdk_path.."/game/server",
        sdk_path.."/common",

        metamod_path,
        metamod_path.."/core",
        metamod_path.."/core/sourcehook",
    })

    --[[ -------------------------------- Flags Section -------------------------------- ]]

    if is_plat("windows") then
        add_cxflags("/utf-8")
    end

    add_cxxflags("gcc::-Wno-invalid-offsetof")
    add_cxxflags("gcc::-Wno-return-local-addr")
    add_cxxflags("gcc::-Wno-overloaded-virtual")
    add_cxxflags("gcc::-Wno-unknown-pragmas")
    add_cxxflags("gcc::-Wno-non-virtual-dtor")
    add_cxxflags("gcc::-Wno-attributes")
    add_cxxflags("gcc::-Wno-array-bounds")
    add_cxxflags("gcc::-Wno-int-to-pointer-cast")
    add_cxxflags("gcc::-Wno-sign-compare")
    add_cxxflags("gcc::-Wno-write-strings")
    add_cxxflags("gcc::-Wno-class-memaccess")
    add_cxxflags("gcc::-fexceptions")
    add_cxxflags("gcc::-fPIC")
    
    add_cflags("gcc::-Wno-return-local-addr")
    add_cflags("gcc::-Wno-unknown-pragmas")
    add_cflags("gcc::-Wno-attributes")
    add_cflags("gcc::-Wno-array-bounds")
    add_cflags("gcc::-Wno-int-to-pointer-cast")
    add_cflags("gcc::-Wno-sign-compare")
    add_cflags("gcc::-Wno-write-strings")
    add_cflags("gcc::-fexceptions")
    add_cflags("gcc::-fPIC")
    add_cflags("gcc::-pipe")
    add_cflags("gcc::-fno-strict-aliasing")
    add_cflags("gcc::-Wall")
    add_cflags("gcc::-Wno-uninitialized")
    add_cflags("gcc::-Wno-unused")
    add_cflags("gcc::-Wno-switch")
    add_cflags("gcc::-msse")
    add_cflags("gcc::-fvisibility=hidden")
    add_cflags("gcc::-mfpmath=sse")
    add_cflags("gcc::-fno-omit-frame-pointer")
    add_cflags("gcc::-fvisibility-inlines-hidden")
    add_cflags("gcc::-fno-exceptions")
    add_cflags("gcc::-fno-threadsafe-statics")
    add_cflags("gcc::-Wno-register")
    add_cflags("gcc::-Wno-delete-non-virtual-dtor")

    add_cxxflags("cl::/Zc:__cplusplus")
    add_cxxflags("cl::/Ox")
    add_cxxflags("cl::/Zo")
    add_cxxflags("cl::/Oy-")
    add_cxxflags("cl::/Z7")
    add_cxxflags("cl::/TP")
    add_cxxflags("cl::/W3")
    add_cxxflags("cl::/Z7")
    add_cxxflags("cl::/EHsc")
    add_cxxflags("cl::/IGNORE:4101,4267,4244,4005,4003,4530,D9025")

    set_runtimes("MT")

    --[[ -------------------------------- HL2SDK Mandatory Libs Section -------------------------------- ]]

    add_files({
        sdk_path.."/mathlib/mathlib.cpp"
    })

    if is_plat("windows") then
        add_links({
            sdk_path.."/lib/public/win64/tier0.lib",
            sdk_path.."/lib/public/win64/tier1.lib",
            sdk_path.."/lib/public/win64/interfaces.lib",
            sdk_path.."/lib/public/win64/2015/libprotobuf.lib",
            sdk_path.."/lib/public/win64/steam_api64.lib",
            "vendor/s2binlib/s2binlib.lib"
        })
    else
        add_links({
            sdk_path.."/lib/linux64/libtier0.so",
            sdk_path.."/lib/linux64/tier1.a",
            sdk_path.."/lib/linux64/interfaces.a",
            sdk_path.."/lib/linux64/release/libprotobuf.a",
            sdk_path.."/lib/linux64/libsteam_api.so",
            "vendor/s2binlib/libs2binlib.a"
        })
    end

    --[[ -------------------------------- Defines Section -------------------------------- ]]

    if(is_plat("windows")) then
        add_defines({
            "COMPILER_MSVC",
            "COMPILER_MSVC64",
            "WIN32",
            "WINDOWS",
            "CRT_SECURE_NO_WARNINGS",
            "CRT_SECURE_NO_DEPRECATE",
            "CRT_NONSTDC_NO_DEPRECATE",
            "_MBCS",
            "META_IS_SOURCE2",
            "COMPILER_MSVC",
            "COMPILER_MSVC64",
            "WIN32",
            "_WIN32",
            "WINDOWS",
            "_WINDOWS",
            "CRT_SECURE_NO_WARNINGS",
            "_CRT_SECURE_NO_WARNINGS",
            "CRT_SECURE_NO_DEPRECATE",
            "_CRT_SECURE_NO_DEPRECATE",
            "CRT_NONSTDC_NO_DEPRECATE",
            "_CRT_NONSTDC_NO_DEPRECATE",
            "_MBCS",
            "META_IS_SOURCE2",
            "X64BITS",
            "PLATFORM_64BITS",
            "NDEBUG",
            "JSON_HAS_CPP_14",
            "JSON_HAS_CPP_11",
            "LUA_USE_WINDOWS",
        })
    else
        add_defines({
            "_LINUX",
            "LINUX",
            "POSIX",
            "GNUC",
            "COMPILER_GCC",
            "PLATFORM_64BITS",
            "META_IS_SOURCE2",
            "_GLIBCXX_USE_CXX11_ABI=0",
            "LUA_USE_LINUX",

            "_vsnprintf=vsnprintf",
            "_alloca=alloca",
            "strcmpi=strcasecmp",
            "strnicmp=strncasecmp",
            "_snprintf=snprintf",
            "_stricmp=strcasecmp",
            "_strnicmp=strncasecmp",
            "stricmp=strcasecmp",
        })
    end

    add_defines({
        "GITHUB_SHA=\""..GITHUB_SHA.."\"",
        "SWIFTLY_VERSION=\""..SWIFTLY_VERSION.."\"",
        "ASMJIT_STATIC",
        "HAVE_STRUCT_TIMESPEC",
        "BUILDING_CORE",
    })

    --[[ -------------------------------- Libraries Section -------------------------------- ]]

    if is_plat("windows") then
        add_links({
            "psapi",
            "winmm",
            "ws2_32",
            "wldap32",
            "advapi32",
            "kernel32",
            "comdlg32",
            "crypt32",
            "normaliz",
            "wsock32",
            "legacy_stdio_definitions",
            "legacy_stdio_wide_specifiers",
            "user32",
            "gdi32",
            "winspool",
            "shell32",
            "ole32",
            "oleaut32",
            "uuid",
            "odbc32",
            "odbccp32",
            "dbghelp",
            "ntdll",
            "kernel32",
        })
    else
        add_links({
            "gnutls",
            "z",
            "pthread",
            "ssl",
            "crypto",
            "m",
            "dl",
            "readline",
            "rt",
            "idn2",
            "psl",
            "brotlidec",
            "backtrace",
            "stdc++",
        })
    end

    --[[ -------------------------------- Vendor Section -------------------------------- ]]

    add_files({
        "vendor/safetyhook/safetyhook.cpp",
        "vendor/safetyhook/Zydis.c"
    })

    --[[ -------------------------------- Protobuf Section -------------------------------- ]]

    on_load(function(target)
        if os.exists("build/proto") then
            return
        end

        local protoc = is_plat("windows") and sdk_path.."/devtools/bin/protoc.exe" or sdk_path.."/devtools/bin/linux/protoc" 
        local args = "--proto_path="..sdk_path.."/thirdparty/protobuf-3.21.8/src --proto_path=./protobufs/cs2 --cpp_out=build/proto"

        function mysplit (inputstr, sep)
            if sep == nil then sep = "%s" end
            local t={}
            for str in string.gmatch(inputstr, "([^"..sep.."]+)") do table.insert(t, str) end
            return t
        end
    
        os.mkdir("build/proto")

        for _, sourcefile in ipairs(os.files("./protobufs/cs2/*.proto")) do
            local splitted = mysplit(sourcefile, "/")
            local filename = splitted[#splitted]

            try {
                function()
                    os.iorun(protoc .. " "..args.." --dependency_out=build/proto/"..filename..".d "..sourcefile)
                end,
                catch {
                    function(err)
                        print(err)
                    end
                }
            }
        end
    end)

    add_files("build/proto/**.cc")

    --[[ -------------------------------- Misc Section -------------------------------- ]]

    set_languages("cxx23")
    set_optimize("fastest")
    set_symbols("debug")
    set_strip("none")

    after_build(function(target)
        function GetDistDirName()
            if is_plat("windows") then
                return "win64"
            else
                return "linuxsteamrt64"
            end
        end

        if os.exists("build/package") then
            os.rmdir("build/package")
        end
        
        os.mkdir('build/package/addons/metamod')
        os.cp("plugin_files/", 'build/package/addons/swiftlys2')
        os.mkdir('build/package/addons/swiftlys2/bin/'..GetDistDirName())
        os.cp(target:targetfile(), 'build/package/addons/swiftlys2/bin/'..GetDistDirName().."/swiftlys2."..(is_plat("windows") and "dll" or "so"))
        io.writefile("build/package/addons/metamod/swiftlys2.vdf", [["Metamod Plugin"
{
    "alias"	"swiftlys2"
    "file"	"addons/swiftlys2/bin/]]..GetDistDirName()..[[/swiftlys2"
}
]])
    end)
