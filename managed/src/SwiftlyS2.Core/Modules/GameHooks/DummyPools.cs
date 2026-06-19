using System.Collections.Concurrent;
using SwiftlyS2.Core.SchemaDefinitions;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class DummyEntityPool<T> where T : class
{
    private readonly ConcurrentQueue<T> _queue = new();
    private readonly Func<T> _factory;

    internal DummyEntityPool( int capacity, Func<T> factory )
    {
        _factory = factory;
        for (var i = 0; i < capacity; i++)
            _queue.Enqueue(factory());
    }

    internal T Rent() => _queue.TryDequeue(out var item) ? item : _factory();
    internal void Return( T item ) => _queue.Enqueue(item);
}

internal static partial class GameHooksPublisher
{
    private const int DummyPoolSize = 1024;

    private static readonly DummyEntityPool<CPlayerPawnComponentImpl> _pawnComponentPool =
        new(DummyPoolSize, static () => new CPlayerPawnComponentImpl(0));

    private static readonly DummyEntityPool<CCSPlayerControllerImpl> _controllerPool =
        new(DummyPoolSize, static () => new CCSPlayerControllerImpl(0));

    private static readonly DummyEntityPool<CCSPlayerPawnImpl> _pawnPool =
        new(DummyPoolSize, static () => new CCSPlayerPawnImpl(0));

    private static readonly DummyEntityPool<CEntityIdentityImpl> _entityIdentityPool =
        new(DummyPoolSize, static () => new CEntityIdentityImpl(0));

    private static readonly DummyEntityPool<CMoveDataImpl> _moveDataPool =
        new(DummyPoolSize, static () => new CMoveDataImpl());

    private static readonly DummyEntityPool<CEconItemViewImpl> _econItemViewPool =
        new(DummyPoolSize, static () => new CEconItemViewImpl(0));
}
