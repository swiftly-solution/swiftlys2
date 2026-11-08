#include "strconv.h"

#include <api/shared/profiler.h>

#include <algorithm>

#ifdef _WIN32
#include <Windows.h>
#endif

std::wstring StringWide(std::string_view str)
{
    PROFILE_FUNCTION();

#ifdef _WIN32
    if (str.empty())
        return {};

    std::wstring out(str.size(), L'\0');
    const int length = ::MultiByteToWideChar(CP_UTF8, 0, str.data(), static_cast<int>(str.size()), out.data(), static_cast<int>(out.size()));
    out.resize(length > 0 ? static_cast<size_t>(length) : 0);
    return out;
#else
    return std::wstring(str.begin(), str.end());
#endif
}

std::string StringTight(std::wstring_view str)
{
    PROFILE_FUNCTION();

#ifdef _WIN32
    if (str.empty())
        return {};

    const int size = ::WideCharToMultiByte(CP_UTF8, 0, str.data(), static_cast<int>(str.size()), nullptr, 0, nullptr, nullptr);
    if (size <= 0)
        return {};

    std::string out(static_cast<size_t>(size), '\0');
    ::WideCharToMultiByte(CP_UTF8, 0, str.data(), static_cast<int>(str.size()), out.data(), size, nullptr, nullptr);
    return out;
#else
    std::string out(str.size(), '\0');
    std::ranges::transform(str, out.begin(), [](wchar_t c) { return static_cast<char>(c); });
    return out;
#endif
}
