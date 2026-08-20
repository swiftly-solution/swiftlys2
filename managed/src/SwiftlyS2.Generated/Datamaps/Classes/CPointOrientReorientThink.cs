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
    private delegate void CPointOrientReorientThinkDelegate(nint a1);

    private static IUnmanagedFunction<CPointOrientReorientThinkDelegate>? CPointOrientReorientThinkUnmanagedFunction;
    private static Guid CPointOrientReorientThinkHookGuid;

    private static IUnmanagedFunction<CPointOrientReorientThinkDelegate> CPointOrientReorientThinkGetUnmanagedFunction()
    {
        if (CPointOrientReorientThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPointOrient", "CPointOrientReorientThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPointOrient::CPointOrientReorientThink.");
            }
            CPointOrientReorientThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPointOrientReorientThinkDelegate>(address);
        }
        return CPointOrientReorientThinkUnmanagedFunction;
    }

    internal static Guid HookCPointOrientReorientThink()
    {
        CPointOrientReorientThinkHookGuid = CPointOrientReorientThinkGetUnmanagedFunction().AddHook(next => (a1) => CPointOrientReorientThinkPipeline(a1, () => next()(a1)));
        return CPointOrientReorientThinkHookGuid;
    }

    internal static Guid UnhookCPointOrientReorientThink()
    {
        CPointOrientReorientThinkGetUnmanagedFunction().RemoveHook(CPointOrientReorientThinkHookGuid);
        return Guid.Empty;
    }

    private static void CPointOrientReorientThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPointOrient>(a1);

            var preCtx = new CPointOrientReorientThinkPreContext { SchemaObject = schemaObject };
            InvokeCPointOrientReorientThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPointOrientReorientThinkPostContext { SchemaObject = schemaObject };
            InvokeCPointOrientReorientThinkPost(ref postCtx);
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

    internal static void InvokeCPointOrientReorientThink(nint a1)
    {
        CPointOrientReorientThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPointOrientReorientThinkPre(ref CPointOrientReorientThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPointOrientReorientThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPointOrientReorientThinkPost(ref CPointOrientReorientThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPointOrientReorientThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPointOrientReorientThinkHook : ICPointOrientReorientThinkHook
{
    private event OnCPointOrientReorientThinkPreDelegate? _Pre;
    private event OnCPointOrientReorientThinkPostDelegate? _Post;

    public event OnCPointOrientReorientThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPointOrientReorientThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointOrientReorientThink);
            }
        }
    }

    public event OnCPointOrientReorientThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPointOrientReorientThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointOrientReorientThink);
            }
        }
    }

    public void InvokePre(ref CPointOrientReorientThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPointOrientReorientThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointOrientReorientThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointOrientReorientThink);
        }
    }

    public void Invoke(CPointOrient schemaObject) => DatamapHooksPublisher.InvokeCPointOrientReorientThink(schemaObject.Address);
}