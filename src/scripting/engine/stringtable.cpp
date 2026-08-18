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
#include <networkstringtabledefs.h>
#include <scripting/scripting.h>
#include <sstream>

static char* Bridge_StringTable_CopyString(const std::string& value, int* size)
{
    int outSize = static_cast<int>(value.size());
    *size = outSize;

    char* out = (char*)g_pMemoryAllocator->Alloc(outSize + 1);
    g_pMemoryAllocator->Copy(out, (void*)value.c_str(), outSize);
    out[outSize] = '\0';
    return out;
}

INetworkStringTable* Bridge_StringTable_ContainerFindTable(const char* tableName)
{
    return g_pGameNetworkStringTableContainer->FindTable(tableName);
}

INetworkStringTable* Bridge_StringTable_ContainerGetTableById(TABLEID tableId)
{
    return g_pGameNetworkStringTableContainer->GetTable(tableId);
}

int Bridge_StringTable_GetTableId(INetworkStringTable* table)
{
    return table->GetTableId();
}

char* Bridge_StringTable_GetTableName(int* size, INetworkStringTable* table)
{
    if (!table)
    {
        return Bridge_StringTable_CopyString("", size);
    }

    std::string s = table->GetTableName();
    return Bridge_StringTable_CopyString(s, size);
}

int Bridge_StringTable_GetNumStrings(INetworkStringTable* table)
{
    return table->GetNumStrings();
}

int Bridge_StringTable_FindStringIndex(INetworkStringTable* table, const char* string)
{
    return table->FindStringIndex(string);
}

bool Bridge_StringTable_IsStringIndexValid(INetworkStringTable* table, int index)
{
    return table->GetString(index) != nullptr;
}

char* Bridge_StringTable_GetString(int* size, INetworkStringTable* table, int index)
{
    if (!table)
    {
        return Bridge_StringTable_CopyString("", size);
    }

    auto ptr = table->GetString(index);
    if (!ptr)
    {
        return Bridge_StringTable_CopyString("", size);
    }

    std::string s = ptr;
    return Bridge_StringTable_CopyString(s, size);
}

const void* Bridge_StringTable_GetStringUserData(INetworkStringTable* table, int index)
{
    return table->GetStringUserData(index);
}

bool Bridge_StringTable_SetStringUserData(INetworkStringTable* table, int index, void* userData, int userDataSize, bool bForceOverride)
{
    SetStringUserDataRequest_t request;
    request.m_pRawData = userData;
    request.m_cbDataSize = userDataSize;
    return table->SetStringUserData(index, &request, bForceOverride);
}

int Bridge_StringTable_AddString(INetworkStringTable* table, const char* string)
{
    return table->AddString(true, string);
}

// Credits to https://github.com/ipsvn/ReplicateStringTableValue/blob/f72bb79048945593d25ffb7c523601dff0873d00/src/meta_interface.cpp#L46
int Bridge_StringTable_Serialize(uint8_t* out, INetworkStringTable* table, int stringIndex, const char* keyName, bool newKey, void* userData, int userDataSize)
{
    const int maxSize = 4096;
    if (!out) return maxSize;
    bf_write buf(out, maxSize);

    buf.WriteOneBit(0);
    buf.WriteVarInt32(stringIndex - 1);

    if (newKey) {
        buf.WriteOneBit(1);
        buf.WriteOneBit(0);
        buf.WriteString(keyName);
    }
    else {
        buf.WriteOneBit(0);
    }

    if (userData) {
        buf.WriteOneBit(1);
        buf.WriteUBitVar(userDataSize);
        buf.WriteBytes(userData, userDataSize);
    }
    else {
        buf.WriteOneBit(0);
    }

    if (buf.IsOverflowed()) return maxSize;

    return buf.GetNumBytesWritten();
}

DEFINE_NATIVE("StringTable.ContainerFindTable", Bridge_StringTable_ContainerFindTable);
DEFINE_NATIVE("StringTable.ContainerGetTableById", Bridge_StringTable_ContainerGetTableById);
DEFINE_NATIVE("StringTable.GetTableId", Bridge_StringTable_GetTableId);
DEFINE_NATIVE("StringTable.GetTableName", Bridge_StringTable_GetTableName);
DEFINE_NATIVE("StringTable.GetNumStrings", Bridge_StringTable_GetNumStrings);
DEFINE_NATIVE("StringTable.FindStringIndex", Bridge_StringTable_FindStringIndex);
DEFINE_NATIVE("StringTable.IsStringIndexValid", Bridge_StringTable_IsStringIndexValid);
DEFINE_NATIVE("StringTable.GetString", Bridge_StringTable_GetString);
DEFINE_NATIVE("StringTable.GetStringUserData", Bridge_StringTable_GetStringUserData);
DEFINE_NATIVE("StringTable.SetStringUserData", Bridge_StringTable_SetStringUserData);
DEFINE_NATIVE("StringTable.AddString", Bridge_StringTable_AddString);
DEFINE_NATIVE("StringTable.Serialize", Bridge_StringTable_Serialize);