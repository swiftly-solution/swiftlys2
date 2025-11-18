using System.Numerics;

namespace SwiftlyS2.Shared.Natives;

public class ManagedCUtlLeanVector<T, I> : IDisposable where T : unmanaged where I : unmanaged, IBinaryInteger<I>, IMinMaxValue<I>
{
    private CUtlLeanVector<T, I> _vector;
    private bool _disposed;

    public ManagedCUtlLeanVector()
    {
        _vector = new CUtlLeanVector<T, I>(I.Zero, I.One);
    }

    public ManagedCUtlLeanVector(I growSize, I initSize)
    {
        _vector = new CUtlLeanVector<T, I>(growSize, initSize);
    }

    ~ManagedCUtlLeanVector()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        _vector.Purge();

        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(ManagedCUtlLeanVector<T, I>));
    }

    public ref CUtlLeanVector<T, I> Value { get { ThrowIfDisposed(); return ref _vector; } }
}