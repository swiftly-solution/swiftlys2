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

#include <scripting/scripting.h>
#include <api/interfaces/interfaces.h>

static char* Bridge_FileSystem_CopyString(const std::string& value, int* size)
{
    int outSize = static_cast<int>(value.size());
    *size = outSize;

    char* out = (char*)g_pMemoryAllocator->Alloc(outSize + 1);
    if (outSize > 0)
    {
        g_pMemoryAllocator->Copy(out, (void*)value.data(), outSize);
    }

    out[outSize] = '\0';
    return out;
}

char* Bridge_FileSystem_GetSearchPath(int* size, char* pathId, int32_t searchPathType, int32_t searchPathsToGet)
{
    CBufferStringGrowable<MAX_PATH> searchPath;
    g_pGameFileSystem->GetSearchPath(pathId, (GetSearchPathTypes_t)searchPathType, searchPath, searchPathsToGet);

    std::string result = searchPath.Get();
    return Bridge_FileSystem_CopyString(result, size);
}

bool Bridge_FileSystem_FileExists(char* fileName, char* pathId)
{
    return g_pGameFileSystem->FileExists(fileName, pathId);
}

void Bridge_FileSystem_AddSearchPath(char* path, char* pathId, int32_t searchPathAdd, int32_t searchPathPriority)
{
    g_pGameFileSystem->AddSearchPath(path, pathId, (SearchPathAdd_t)searchPathAdd, (SearchPathPriority_t)searchPathPriority, 0);
}

bool Bridge_FileSystem_RemoveSearchPath(char* path, char* pathId)
{
    return g_pGameFileSystem->RemoveSearchPath(path, pathId);
}

bool Bridge_FileSystem_IsDirectory(char* path, char* pathId)
{
    return g_pGameFileSystem->IsDirectory(path, pathId);
}

void Bridge_FileSystem_PrintSearchPaths()
{
    g_pGameFileSystem->PrintSearchPaths();
}

char* Bridge_FileSystem_ReadFile(int* size, char* fileName, char* pathId)
{
    CUtlBuffer buf;

    int fileSize = g_pGameFileSystem->Size(fileName, pathId);
    if (fileSize <= 0)
    {
        return Bridge_FileSystem_CopyString("", size);
    }

    buf.EnsureCapacity(fileSize);

    bool success = g_pGameFileSystem->ReadFile(fileName, pathId, buf, fileSize);

    if (!success)
    {
        return Bridge_FileSystem_CopyString("", size);
    }

    std::string result((const char*)buf.Base(), fileSize);
    return Bridge_FileSystem_CopyString(result, size);
}

bool Bridge_FileSystem_WriteFile(char* fileName, char* pathId, char* inputBuffer)
{
    CUtlBuffer buf;
    buf.Put(inputBuffer, strlen(inputBuffer) + 1);

    return g_pGameFileSystem->WriteFile(fileName, pathId, buf);
}

uint32_t Bridge_FileSystem_GetFileSize(char* fileName, char* pathId)
{
    return g_pGameFileSystem->Size(fileName, pathId);
}

bool Bridge_FileSystem_PrecacheFile(char* fileName, char* pathId)
{
    return g_pGameFileSystem->Precache(fileName, pathId);
}

bool Bridge_FileSystem_IsFileWritable(char* fileName, char* pathId)
{
    return g_pGameFileSystem->IsFileWritable(fileName, pathId);
}

bool Bridge_FileSystem_SetFileWritable(char* fileName, char* pathId, bool writable)
{
    return g_pGameFileSystem->SetFileWritable(fileName, writable, pathId);
}

void Bridge_FileSystem_FindFileAbsoluteList(void* outVector, const char* wildcard, const char* pathId)
{
    g_pGameFileSystem->FindFileAbsoluteList(*reinterpret_cast<CUtlVector<CUtlString>*>(outVector), wildcard, pathId);
}

DEFINE_NATIVE("FileSystem.GetSearchPath", Bridge_FileSystem_GetSearchPath);
DEFINE_NATIVE("FileSystem.FileExists", Bridge_FileSystem_FileExists);
DEFINE_NATIVE("FileSystem.AddSearchPath", Bridge_FileSystem_AddSearchPath);
DEFINE_NATIVE("FileSystem.RemoveSearchPath", Bridge_FileSystem_RemoveSearchPath);
DEFINE_NATIVE("FileSystem.IsDirectory", Bridge_FileSystem_IsDirectory);
DEFINE_NATIVE("FileSystem.PrintSearchPaths", Bridge_FileSystem_PrintSearchPaths);
DEFINE_NATIVE("FileSystem.ReadFile", Bridge_FileSystem_ReadFile);
DEFINE_NATIVE("FileSystem.WriteFile", Bridge_FileSystem_WriteFile);
DEFINE_NATIVE("FileSystem.GetFileSize", Bridge_FileSystem_GetFileSize);
DEFINE_NATIVE("FileSystem.PrecacheFile", Bridge_FileSystem_PrecacheFile);
DEFINE_NATIVE("FileSystem.IsFileWritable", Bridge_FileSystem_IsFileWritable);
DEFINE_NATIVE("FileSystem.SetFileWritable", Bridge_FileSystem_SetFileWritable);
DEFINE_NATIVE("FileSystem.FindFileAbsoluteList", Bridge_FileSystem_FindFileAbsoluteList);