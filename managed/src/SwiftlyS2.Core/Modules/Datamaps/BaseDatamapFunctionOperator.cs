using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Datamaps;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Profiler;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Core.Datamaps;

internal class BaseDatamapFunctionOperator<T, K> : IDatamapFunctionOperator<T, K>
    where T : ISchemaClass<T>
    where K : IDatamapFunctionHookContext<T>, new()
{

    private BaseDatamapFunction<T, K> _Owner { get; set; }
    private IContextedProfilerService _Profiler { get; set; }
    private bool _Disposed { get; set; }
    public bool IsDisposed => _Disposed;

    public BaseDatamapFunctionOperator( BaseDatamapFunction<T, K> owner, IContextedProfilerService profiler )
    {
        _Owner = owner;
        _Profiler = profiler;
    }

    private List<Action<K>> _PreCallbacks = [];
    private List<Action<K>> _PostCallbacks = [];

    internal bool CallbackPre( nint ptr )
    {

        var ctx = new K {
            SchemaObject = Helper.AsSchema<T>(ptr),
            HookResult = HookResult.Continue
        };

        foreach (var callback in _PreCallbacks)
        {
            callback(ctx);
            if (ctx.HookResult == HookResult.Handled) return true;
            if (ctx.HookResult == HookResult.Stop) return false;
        }
        return ctx.HookResult != HookResult.CancelOriginal;
    }

    internal void CallbackPost( nint ptr )
    {
        var ctx = new K {
            SchemaObject = Helper.AsSchema<T>(ptr),
            HookResult = HookResult.Continue
        };
        foreach (var callback in _PostCallbacks)
        {
            callback(ctx);
        }
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_Disposed, this);
    }

    public void HookPre( Action<K> callback )
    {
        ThrowIfDisposed();
        _Owner.Hook();
        _PreCallbacks.Add(callback);
    }

    public void HookPost( Action<K> callback )
    {
        ThrowIfDisposed();
        _Owner.Hook();
        _PostCallbacks.Add(callback);
    }

    public void Invoke( T schemaObject )
    {
        ThrowIfDisposed();
        _Owner.Invoke(schemaObject.Address);
    }

    public void InvokeOriginal( T schemaObject )
    {
        ThrowIfDisposed();
        _Owner.InvokeOriginal(schemaObject.Address);
    }

    public void Dispose()
    {
        _Disposed = true;
    }

}