namespace SwiftlyS2.Core.Events;

internal sealed class NativeListener( Func<ulong> add, Action<ulong> remove )
{
    private readonly Lock listenerLock = new();
    private int users;
    private ulong? listenerId;

    internal void Acquire()
    {
        lock (listenerLock)
        {
            users++;
            if (users == 1)
            {
                listenerId = add();
            }
        }
    }

    internal void Release()
    {
        lock (listenerLock)
        {
            if (users == 0)
            {
                return;
            }

            users--;
            if (users == 0 && listenerId is { } id)
            {
                remove(id);
                listenerId = null;
            }
        }
    }
}
