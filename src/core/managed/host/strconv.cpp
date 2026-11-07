#include "strconv.h"
#include <api/shared/profiler.h>
#include <sstream>

std::wstring StringWide(std::string str) {
    PROFILE_FUNCTION();

    std::wostringstream s;
    auto& man = std::use_facet<std::ctype<wchar_t>>(s.getloc());
    for (int i = 0; i < str.size(); i++) {
        s << man.widen(str[i]);
    }
    return s.str();
}

std::string StringTight(std::wstring str) {
    PROFILE_FUNCTION();

    std::ostringstream s;
    auto& man = std::use_facet<std::ctype<wchar_t>>(s.getloc());
    for (int i = 0; i < str.size(); i++) {
        s << man.narrow(str[i], 0);
    }
    return s.str();
}