namespace SwiftlyS2.Shared.Natives;

public class ManagedCUtlMemory<T> : IDisposable where T : unmanaged
{
    private CUtlMemory<T> _memory;
    private bool _disposed;

    public ManagedCUtlMemory()
    {
        _memory = new CUtlMemory<T>(0, 1);
    }
    public ManagedCUtlMemory(int growSize, int initSize)
    {
        _memory = new CUtlMemory<T>(growSize, initSize);
    }

    ~ManagedCUtlMemory()
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

        _memory.Purge();

        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(ManagedCUtlMemory<T>));
    }

    public ref CUtlMemory<T> Value { get { ThrowIfDisposed(); return ref _memory; } }
}