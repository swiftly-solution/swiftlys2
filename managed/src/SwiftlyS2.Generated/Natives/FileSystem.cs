#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeFileSystem
{

    private unsafe static delegate* unmanaged<int*, byte*, int, int, byte*> _GetSearchPath;

    public unsafe static string GetSearchPath(string pathId, int searchPathType, int searchPathsToGet)
    {
        using var pathIdStr = new ScopedCString(pathId);
        fixed (byte* pathIdBufferPtr = pathIdStr)
        {
            var length = 0;
            var returnedPtr = _GetSearchPath(&length, pathIdBufferPtr, searchPathType, searchPathsToGet);
            var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
            NativeAllocator.Free((nint)returnedPtr);
            return outString;
        }
    }

    private unsafe static delegate* unmanaged<byte*, byte*, int, int, void> _AddSearchPath;

    public unsafe static void AddSearchPath(string path, string pathId, int searchPathAdd, int searchPathPriority)
    {
        if (!NativeBinding.IsMainThread)
        {
            throw new InvalidOperationException("This method can only be called from the main thread.");
        }
        using var pathStr = new ScopedCString(path);
        using var pathIdStr = new ScopedCString(pathId);
        fixed (byte* pathBufferPtr = pathStr)
        {
            fixed (byte* pathIdBufferPtr = pathIdStr)
            {
                _AddSearchPath(pathBufferPtr, pathIdBufferPtr, searchPathAdd, searchPathPriority);
            }
        }
    }

    private unsafe static delegate* unmanaged<byte*, byte*, byte> _RemoveSearchPath;

    public unsafe static bool RemoveSearchPath(string path, string pathId)
    {
        using var pathStr = new ScopedCString(path);
        using var pathIdStr = new ScopedCString(pathId);
        fixed (byte* pathBufferPtr = pathStr)
        {
            fixed (byte* pathIdBufferPtr = pathIdStr)
            {
                var ret = _RemoveSearchPath(pathBufferPtr, pathIdBufferPtr);
                return ret == 1;
            }
        }
    }

    private unsafe static delegate* unmanaged<byte*, byte*, byte> _FileExists;

    public unsafe static bool FileExists(string fileName, string pathId)
    {
        using var fileNameStr = new ScopedCString(fileName);
        using var pathIdStr = new ScopedCString(pathId);
        fixed (byte* fileNameBufferPtr = fileNameStr)
        {
            fixed (byte* pathIdBufferPtr = pathIdStr)
            {
                var ret = _FileExists(fileNameBufferPtr, pathIdBufferPtr);
                return ret == 1;
            }
        }
    }

    private unsafe static delegate* unmanaged<byte*, byte*, byte> _IsDirectory;

    public unsafe static bool IsDirectory(string path, string pathId)
    {
        using var pathStr = new ScopedCString(path);
        using var pathIdStr = new ScopedCString(pathId);
        fixed (byte* pathBufferPtr = pathStr)
        {
            fixed (byte* pathIdBufferPtr = pathIdStr)
            {
                var ret = _IsDirectory(pathBufferPtr, pathIdBufferPtr);
                return ret == 1;
            }
        }
    }

    private unsafe static delegate* unmanaged<void> _PrintSearchPaths;

    public unsafe static void PrintSearchPaths()
    {
        _PrintSearchPaths();
    }

    private unsafe static delegate* unmanaged<int*, byte*, byte*, byte*> _ReadFile;

    public unsafe static string ReadFile(string fileName, string pathId)
    {
        using var fileNameStr = new ScopedCString(fileName);
        using var pathIdStr = new ScopedCString(pathId);
        fixed (byte* fileNameBufferPtr = fileNameStr)
        {
            fixed (byte* pathIdBufferPtr = pathIdStr)
            {
                var length = 0;
                var returnedPtr = _ReadFile(&length, fileNameBufferPtr, pathIdBufferPtr);
                var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
                NativeAllocator.Free((nint)returnedPtr);
                return outString;
            }
        }
    }

    private unsafe static delegate* unmanaged<byte*, byte*, byte*, byte> _WriteFile;

    public unsafe static bool WriteFile(string fileName, string pathId, string content)
    {
        if (!NativeBinding.IsMainThread)
        {
            throw new InvalidOperationException("This method can only be called from the main thread.");
        }
        using var fileNameStr = new ScopedCString(fileName);
        using var pathIdStr = new ScopedCString(pathId);
        using var contentStr = new ScopedCString(content);
        fixed (byte* fileNameBufferPtr = fileNameStr)
        {
            fixed (byte* pathIdBufferPtr = pathIdStr)
            {
                fixed (byte* contentBufferPtr = contentStr)
                {
                    var ret = _WriteFile(fileNameBufferPtr, pathIdBufferPtr, contentBufferPtr);
                    return ret == 1;
                }
            }
        }
    }

    private unsafe static delegate* unmanaged<byte*, byte*, uint> _GetFileSize;

    public unsafe static uint GetFileSize(string fileName, string pathId)
    {
        using var fileNameStr = new ScopedCString(fileName);
        using var pathIdStr = new ScopedCString(pathId);
        fixed (byte* fileNameBufferPtr = fileNameStr)
        {
            fixed (byte* pathIdBufferPtr = pathIdStr)
            {
                var ret = _GetFileSize(fileNameBufferPtr, pathIdBufferPtr);
                return ret;
            }
        }
    }

    private unsafe static delegate* unmanaged<byte*, byte*, byte> _PrecacheFile;

    public unsafe static bool PrecacheFile(string fileName, string pathId)
    {
        using var fileNameStr = new ScopedCString(fileName);
        using var pathIdStr = new ScopedCString(pathId);
        fixed (byte* fileNameBufferPtr = fileNameStr)
        {
            fixed (byte* pathIdBufferPtr = pathIdStr)
            {
                var ret = _PrecacheFile(fileNameBufferPtr, pathIdBufferPtr);
                return ret == 1;
            }
        }
    }

    private unsafe static delegate* unmanaged<byte*, byte*, byte> _IsFileWritable;

    public unsafe static bool IsFileWritable(string fileName, string pathId)
    {
        using var fileNameStr = new ScopedCString(fileName);
        using var pathIdStr = new ScopedCString(pathId);
        fixed (byte* fileNameBufferPtr = fileNameStr)
        {
            fixed (byte* pathIdBufferPtr = pathIdStr)
            {
                var ret = _IsFileWritable(fileNameBufferPtr, pathIdBufferPtr);
                return ret == 1;
            }
        }
    }

    private unsafe static delegate* unmanaged<byte*, byte*, byte, byte> _SetFileWritable;

    public unsafe static bool SetFileWritable(string fileName, string pathId, bool writable)
    {
        if (!NativeBinding.IsMainThread)
        {
            throw new InvalidOperationException("This method can only be called from the main thread.");
        }
        using var fileNameStr = new ScopedCString(fileName);
        using var pathIdStr = new ScopedCString(pathId);
        fixed (byte* fileNameBufferPtr = fileNameStr)
        {
            fixed (byte* pathIdBufferPtr = pathIdStr)
            {
                var ret = _SetFileWritable(fileNameBufferPtr, pathIdBufferPtr, writable ? (byte)1 : (byte)0);
                return ret == 1;
            }
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte*, void> _FindFileAbsoluteList;

    public unsafe static void FindFileAbsoluteList(nint outVector, string wildcard, string pathId)
    {
        using var wildcardStr = new ScopedCString(wildcard);
        using var pathIdStr = new ScopedCString(pathId);
        fixed (byte* wildcardBufferPtr = wildcardStr)
        {
            fixed (byte* pathIdBufferPtr = pathIdStr)
            {
                _FindFileAbsoluteList(outVector, wildcardBufferPtr, pathIdBufferPtr);
            }
        }
    }
}