/************************************************************************************************
 * SwiftlyS2 is a scripting framework for Source2-based games.
 * Copyright (C) 2023-2026 Swiftly Solution SRL via Sava Andrei-Sebastian and it's contributors
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 ************************************************************************************************/

#include <api/interfaces/interfaces.h>
#include <api/shared/profiler.h>
#include <scripting/scripting.h>
#include <core/entrypoint.h>

#include <memory/gamedata/manager.h>

void* Bridge_MemoryHelpers_FetchInterfaceByName(const char* iface_name)
{
    PROFILE_FUNCTION();

    return g_SwiftlyCore.GetInterface(iface_name);
}

void* Bridge_MemoryHelpers_GetVirtualTableAddress(const char* binary, const char* vtable_name)
{
    PROFILE_FUNCTION();

    void* result = nullptr;

    g_pS2BinLib->FindVtable(binary, vtable_name, &result);
    return result;
}

void* Bridge_MemoryHelpers_GetVirtualTableAddressNested2(const char* binary, const char* class1, const char* class2)
{
    PROFILE_FUNCTION();

    void* result = nullptr;

    g_pS2BinLib->FindVtableNested2(binary, class1, class2, &result);
    return result;
}

std::string BytesToIdaSignature(const unsigned char* data, int size) {
    PROFILE_FUNCTION();

    static const char* hex = "0123456789ABCDEF";
    std::string result;
    result.reserve(size * 3);

    for (int i = 0; i < size; ++i) {
        if (data[i] == 0x2A) {
            result.push_back('?');
        }
        else {
            result.push_back(hex[(data[i] >> 4) & 0xF]);
            result.push_back(hex[data[i] & 0xF]);
        }

        if (i + 1 < size) {
            result.push_back(' ');
        }
    }

    return result;
}

void* Bridge_MemoryHelpers_GetAddressBySignature(const char* binary, const char* signature, int len, bool rawBytes)
{
    PROFILE_FUNCTION();

    return FindSignature(binary, rawBytes ? BytesToIdaSignature(reinterpret_cast<const unsigned char*>(signature), len) : signature);
}

char* Bridge_MemoryHelpers_GetObjectPtrVtableName(int* size, void* objptr)
{
    PROFILE_FUNCTION();

    char buffer[1024] = { 0 };
    int ret = g_pS2BinLib->GetObjectPtrVtableName(objptr, buffer, sizeof(buffer));
    if (ret != 0)
    {
        return Bridge_CopyString("", size);
    }

    return Bridge_CopyString(buffer, size);
}

bool Bridge_MemoryHelpers_ObjectPtrHasVtable(void* objptr)
{
    PROFILE_FUNCTION();

    return g_pS2BinLib->ObjectPtrHasVtable(objptr) == 1;
}

bool Bridge_MemoryHelpers_ObjectPtrHasBaseClass(void* objptr, const char* base_class_name)
{
    PROFILE_FUNCTION();

    return g_pS2BinLib->ObjectPtrHasBaseClass(objptr, base_class_name) == 1;
}

DEFINE_NATIVE("MemoryHelpers.FetchInterfaceByName", Bridge_MemoryHelpers_FetchInterfaceByName);
DEFINE_NATIVE("MemoryHelpers.GetVirtualTableAddress", Bridge_MemoryHelpers_GetVirtualTableAddress);
DEFINE_NATIVE("MemoryHelpers.GetVirtualTableAddressNested2", Bridge_MemoryHelpers_GetVirtualTableAddressNested2);
DEFINE_NATIVE("MemoryHelpers.GetAddressBySignature", Bridge_MemoryHelpers_GetAddressBySignature);
DEFINE_NATIVE("MemoryHelpers.GetObjectPtrVtableName", Bridge_MemoryHelpers_GetObjectPtrVtableName);
DEFINE_NATIVE("MemoryHelpers.ObjectPtrHasVtable", Bridge_MemoryHelpers_ObjectPtrHasVtable);
DEFINE_NATIVE("MemoryHelpers.ObjectPtrHasBaseClass", Bridge_MemoryHelpers_ObjectPtrHasBaseClass);