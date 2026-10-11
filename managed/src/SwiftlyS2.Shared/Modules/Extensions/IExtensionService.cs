namespace SwiftlyS2.Shared.Extensions;

/// <summary>
/// Manages native extensions and their shared pointer registry.
/// </summary>
public interface IExtensionService
{
    /// <summary>
    /// Loads an extension from extensions/{id}/{id}.dll or .so.
    /// </summary>
    /// <returns>Whether the extension was loaded successfully.</returns>
    public bool Load( string id );

    /// <summary>
    /// Unloads an extension and invokes its registered unload callback.
    /// </summary>
    /// <returns>Whether the extension was unloaded successfully.</returns>
    public bool Unload( string id );

    /// <summary>
    /// Loads an extension from an absolute path to its library.
    /// The extension ID is the library filename without its extension.
    /// </summary>
    /// <returns>Whether the extension was loaded successfully.</returns>
    /// <exception cref="ArgumentException">The path is empty or not fully qualified.</exception>
    public bool LoadFromPath( string absolutePath );

    /// <summary>
    /// Unloads an extension by the absolute path to its library.
    /// </summary>
    /// <returns>Whether the extension was unloaded successfully.</returns>
    /// <exception cref="ArgumentException">The path is empty or not fully qualified.</exception>
    public bool UnloadFromPath( string absolutePath );

    /// <summary>
    /// Gets a shared pointer, or zero if the key does not exist.
    /// Use HasSharedPointer to distinguish a stored zero from a missing key.
    /// </summary>
    public nint GetSharedPointer( string key );

    /// <summary>
    /// Inserts or replaces a shared pointer. The value can be zero.
    /// The registry does not own the pointed-to object or free replaced values.
    /// </summary>
    public void SetSharedPointer( string key, nint pointer );

    /// <summary>
    /// Checks whether a shared pointer key exists, including keys with zero values.
    /// </summary>
    public bool HasSharedPointer( string key );

    /// <summary>
    /// Removes a shared pointer key without freeing the pointed-to object.
    /// Missing keys are ignored.
    /// </summary>
    public void RemoveSharedPointer( string key );
}
