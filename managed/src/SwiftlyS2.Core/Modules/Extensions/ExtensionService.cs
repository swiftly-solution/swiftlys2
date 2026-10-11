using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Extensions;

namespace SwiftlyS2.Core.Extensions;

internal class ExtensionService : IExtensionService
{
    public bool Load( string id )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        return NativeExtensions.Load(id);
    }

    public bool Unload( string id )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        return NativeExtensions.Unload(id);
    }

    public bool LoadFromPath( string absolutePath )
    {
        ValidateAbsolutePath(absolutePath);
        return NativeExtensions.LoadFromPath(absolutePath);
    }

    public bool UnloadFromPath( string absolutePath )
    {
        ValidateAbsolutePath(absolutePath);
        return NativeExtensions.UnloadFromPath(absolutePath);
    }

    public nint GetSharedPointer( string key )
    {
        ArgumentNullException.ThrowIfNull(key);
        return NativeExtensions.GetSharedPointer(key);
    }

    public void SetSharedPointer( string key, nint pointer )
    {
        ArgumentNullException.ThrowIfNull(key);
        NativeExtensions.SetSharedPointer(key, pointer);
    }

    public bool HasSharedPointer( string key )
    {
        ArgumentNullException.ThrowIfNull(key);
        return NativeExtensions.HasSharedPointer(key);
    }

    public void RemoveSharedPointer( string key )
    {
        ArgumentNullException.ThrowIfNull(key);
        NativeExtensions.RemoveSharedPointer(key);
    }

    private static void ValidateAbsolutePath( string absolutePath )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(absolutePath);
        if (!Path.IsPathFullyQualified(absolutePath))
        {
            throw new ArgumentException("The extension path must be absolute.", nameof(absolutePath));
        }
    }
}
