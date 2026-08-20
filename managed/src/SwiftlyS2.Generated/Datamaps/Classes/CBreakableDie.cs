using Spectre.Console;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Memory;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class DatamapHooksPublisher
{
    private delegate void CBreakableDieDelegate(nint a1);

    private static IUnmanagedFunction<CBreakableDieDelegate>? CBreakableDieUnmanagedFunction;
    private static Guid CBreakableDieHookGuid;

    private static IUnmanagedFunction<CBreakableDieDelegate> CBreakableDieGetUnmanagedFunction()
    {
        if (CBreakableDieUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBreakable", "CBreakableDie");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBreakable::CBreakableDie.");
            }
            CBreakableDieUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBreakableDieDelegate>(address);
        }
        return CBreakableDieUnmanagedFunction;
    }

    internal static Guid HookCBreakableDie()
    {
        CBreakableDieHookGuid = CBreakableDieGetUnmanagedFunction().AddHook(next => (a1) => CBreakableDiePipeline(a1, () => next()(a1)));
        return CBreakableDieHookGuid;
    }

    internal static Guid UnhookCBreakableDie()
    {
        CBreakableDieGetUnmanagedFunction().RemoveHook(CBreakableDieHookGuid);
        return Guid.Empty;
    }

    private static void CBreakableDiePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBreakable>(a1);

            var preCtx = new CBreakableDiePreContext { SchemaObject = schemaObject };
            InvokeCBreakableDiePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBreakableDiePostContext { SchemaObject = schemaObject };
            InvokeCBreakableDiePost(ref postCtx);
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    internal static void InvokeCBreakableDie(nint a1)
    {
        CBreakableDieGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBreakableDiePre(ref CBreakableDiePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBreakableDiePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBreakableDiePost(ref CBreakableDiePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBreakableDiePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBreakableDieHook : ICBreakableDieHook
{
    private event OnCBreakableDiePreDelegate? _Pre;
    private event OnCBreakableDiePostDelegate? _Post;

    public event OnCBreakableDiePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBreakableDie);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBreakableDie);
            }
        }
    }

    public event OnCBreakableDiePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBreakableDie);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBreakableDie);
            }
        }
    }

    public void InvokePre(ref CBreakableDiePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBreakableDiePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBreakableDie);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBreakableDie);
        }
    }

    public void Invoke(CBreakable schemaObject) => DatamapHooksPublisher.InvokeCBreakableDie(schemaObject.Address);
}