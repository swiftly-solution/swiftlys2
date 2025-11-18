using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.FileSystem;

namespace SwiftlyS2.Core.FileSystem;

internal class GameFileSystem : IGameFileSystem
{
    public void AddSearchPath( string path, string pathId, SearchPathAdd_t addType, SearchPathPriority_t priority )
    {
        NativeFileSystem.AddSearchPath(path, pathId, (int)addType, (int)priority);
    }

    public bool FileExists( string filePath, string pathId )
    {
        return NativeFileSystem.FileExists(filePath, pathId);
    }

    public uint GetFileSize( string filePath, string pathId )
    {
        return NativeFileSystem.GetFileSize(filePath, pathId);
    }

    public string GetSearchPath( string pathId, GetSearchPathTypes_t searchPathType, int searchPathsToGet )
    {
        return NativeFileSystem.GetSearchPath(pathId, (int)searchPathType, searchPathsToGet);
    }

    public bool IsDirectory( string path, string pathId )
    {
        return NativeFileSystem.IsDirectory(path, pathId);
    }

    public bool IsFileWritable( string filePath, string pathId )
    {
        return NativeFileSystem.IsFileWritable(filePath, pathId);
    }

    public bool PrecacheFile( string filePath, string pathId )
    {
        return NativeFileSystem.PrecacheFile(filePath, pathId);
    }

    public void PrintSearchPaths()
    {
        NativeFileSystem.PrintSearchPaths();
    }

    public string ReadFile( string filePath, string pathId )
    {
        return NativeFileSystem.ReadFile(filePath, pathId);
    }

    public bool RemoveSearchPath( string path, string pathId )
    {
        return NativeFileSystem.RemoveSearchPath(path, pathId);
    }

    public bool SetFileWritable( string filePath, string pathId, bool writable )
    {
        return NativeFileSystem.SetFileWritable(filePath, pathId, writable);
    }

    public bool WriteFile( string filePath, string pathId, string content )
    {
        return NativeFileSystem.WriteFile(filePath, pathId, content);
    }
}