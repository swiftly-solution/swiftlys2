namespace SwiftlyS2.Shared.Natives;

public class ManagedCUtlVector<T> : IDisposable where T : unmanaged
{
    private CUtlVector<T> _vector;
    private bool _disposed;

    public ManagedCUtlVector()
    {
        _vector = new CUtlVector<T>(0, 1);
    }

    public ManagedCUtlVector(int growSize, int initSize)
    {
        _vector = new CUtlVector<T>(growSize, initSize);
    }

    ~ManagedCUtlVector()
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
            throw new ObjectDisposedException(nameof(ManagedCUtlVector<T>));
    }


    public ref CUtlVector<T> Value { get { ThrowIfDisposed(); return ref _vector; } }
}