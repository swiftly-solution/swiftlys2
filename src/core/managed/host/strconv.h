#ifndef _embedder_src_dotnet_strconv_h
#define _embedder_src_dotnet_strconv_h

#include <string>
#include <string_view>

[[nodiscard]] std::wstring StringWide(std::string_view str);
[[nodiscard]] std::string StringTight(std::wstring_view str);

#endif
