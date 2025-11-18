namespace SwiftlyS2.Shared.FileSystem;

public interface IGameFileSystem
{
    /// <summary>
    /// Prints the current search paths to the console.
    /// </summary>
    public void PrintSearchPaths();

    /// <summary>
    /// Checks if a directory exists at the given path and path ID.
    /// </summary>
    /// <param name="path">The path to check.</param>
    /// <param name="pathId">The ID of the path to search in.</param>
    /// <returns>True if the directory exists, false otherwise.</returns>
    public bool IsDirectory( string path, string pathId );

    /// <summary>
    /// Removes a search path from the file system.
    /// </summary>
    /// <param name="path">The path to remove.</param>
    /// <param name="pathId">The ID of the path to remove in.</param>
    /// <returns>True if the path was removed successfully, false otherwise.</returns>
    public bool RemoveSearchPath( string path, string pathId );

    /// <summary>
    /// Adds a search path to the file system.
    /// </summary>
    /// <param name="path">The path to add.</param>
    /// <param name="pathId">The ID of the path to add in.</param>
    /// <param name="addType">The type of addition to perform.</param>
    /// <param name="priority">The priority of the search path.</param>
    public void AddSearchPath( string path, string pathId, SearchPathAdd_t addType, SearchPathPriority_t priority );

    /// <summary>
    /// Checks if a file exists at the given file path and path ID.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="pathId">The ID of the path to check in.</param>
    /// <returns>True if the file exists, false otherwise.</returns>
    public bool FileExists( string filePath, string pathId );

    /// <summary>
    /// Gets the search path(s) for the given path ID and search path type.
    /// </summary>
    /// <param name="pathId">The ID of the path to get the search paths for.</param>
    /// <param name="searchPathType">The type of search path to get.</param>
    /// <param name="searchPathsToGet">The number of search paths to get.</param>
    /// <returns>The search path(s) for the given path ID and search path type.</returns>
    public string GetSearchPath( string pathId, GetSearchPathTypes_t searchPathType, int searchPathsToGet );

    /// <summary>
    /// Reads the contents of a file at the given file path and path ID.
    /// </summary>
    /// <param name="filePath">The path to the file to read.</param>
    /// <param name="pathId">The ID of the path to read the file from.</param>
    /// <returns>The contents of the file as a string.</returns>
    public string ReadFile( string filePath, string pathId );

    /// <summary>
    /// Writes content to a file at the given file path and path ID.
    /// </summary>
    /// <param name="filePath">The path to the file to write.</param>
    /// <param name="pathId">The ID of the path to write the file to.</param>
    /// <param name="content">The content to write to the file.</param>
    /// <returns>True if the file was written successfully, false otherwise.</returns>
    public bool WriteFile( string filePath, string pathId, string content );

    /// <summary>
    /// Gets the size of a file at the given file path and path ID.
    /// </summary>
    /// <param name="filePath">The path to the file to get the size of.</param>
    /// <param name="pathId">The ID of the path to get the file size from.</param>
    /// <returns>The size of the file in bytes.</returns>
    public uint GetFileSize( string filePath, string pathId );

    /// <summary>
    /// Precaches a file at the given file path and path ID.
    /// </summary>
    /// <param name="filePath">The path to the file to precache.</param>
    /// <param name="pathId">The ID of the path to precache the file in.</param>
    /// <returns>True if the file was precached successfully, false otherwise.</returns>
    public bool PrecacheFile( string filePath, string pathId );

    /// <summary>
    /// Checks if a file is writable at the given file path and path ID.
    /// </summary>
    /// <param name="filePath">The path to the file to check.</param>
    /// <param name="pathId">The ID of the path to check in.</param>
    /// <returns>True if the file is writable, false otherwise.</returns>
    public bool IsFileWritable( string filePath, string pathId );

    /// <summary>
    /// Sets the writable status of a file at the given file path and path ID.
    /// </summary>
    /// <param name="filePath">The path to the file to set the writable status for.</param>
    /// <param name="pathId">The ID of the path to set the writable status in.</param>
    /// <param name="writable">True to make the file writable, false to make it read-only.</param>
    /// <returns>True if the writable status was set successfully, false otherwise.</returns>
    public bool SetFileWritable( string filePath, string pathId, bool writable );
}