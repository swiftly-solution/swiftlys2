/************************************************************************************************
 *  SwiftlyS2 is a scripting framework for Source2-based games.
 *  Copyright (C) 2023-2026 Swiftly Solution SRL via Sava Andrei-Sebastian and it's contributors
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
 ************************************************************************************************/

#ifndef src_api_memory_virtual_call_h
#define src_api_memory_virtual_call_h

template<class T, class... Args>
constexpr T CallVFunc(int iIndex, void* pThis, Args... args) noexcept
{
    return reinterpret_cast<T(*)(void*, Args...)>(reinterpret_cast<void***>(pThis)[0][iIndex])(pThis, args...);
}

#define CALL_VIRTUAL(retType, idx, ...) CallVFunc<retType>(idx, __VA_ARGS__)

#endif