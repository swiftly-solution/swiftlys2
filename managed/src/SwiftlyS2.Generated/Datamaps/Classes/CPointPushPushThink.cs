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
    private delegate void CPointPushPushThinkDelegate(nint a1);

    private static IUnmanagedFunction<CPointPushPushThinkDelegate>? CPointPushPushThinkUnmanagedFunction;
    private static Guid CPointPushPushThinkHookGuid;

    private static IUnmanagedFunction<CPointPushPushThinkDelegate> CPointPushPushThinkGetUnmanagedFunction()
    {
        if (CPointPushPushThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPointPush", "CPointPushPushThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPointPush::CPointPushPushThink.");
            }
            CPointPushPushThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPointPushPushThinkDelegate>(address);
        }
        return CPointPushPushThinkUnmanagedFunction;
    }

    internal static Guid HookCPointPushPushThink()
    {
        CPointPushPushThinkHookGuid = CPointPushPushThinkGetUnmanagedFunction().AddHook(next => (a1) => CPointPushPushThinkPipeline(a1, () => next()(a1)));
        return CPointPushPushThinkHookGuid;
    }

    internal static Guid UnhookCPointPushPushThink()
    {
        CPointPushPushThinkGetUnmanagedFunction().RemoveHook(CPointPushPushThinkHookGuid);
        return Guid.Empty;
    }

    private static void CPointPushPushThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPointPush>(a1);

            var preCtx = new CPointPushPushThinkPreContext { SchemaObject = schemaObject };
            InvokeCPointPushPushThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPointPushPushThinkPostContext { SchemaObject = schemaObject };
            InvokeCPointPushPushThinkPost(ref postCtx);
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

    internal static void InvokeCPointPushPushThink(nint a1)
    {
        CPointPushPushThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPointPushPushThinkPre(ref CPointPushPushThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPointPushPushThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPointPushPushThinkPost(ref CPointPushPushThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPointPushPushThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPointPushPushThinkHook : ICPointPushPushThinkHook
{
    private event OnCPointPushPushThinkPreDelegate? _Pre;
    private event OnCPointPushPushThinkPostDelegate? _Post;

    public event OnCPointPushPushThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPointPushPushThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointPushPushThink);
            }
        }
    }

    public event OnCPointPushPushThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPointPushPushThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointPushPushThink);
            }
        }
    }

    public void InvokePre(ref CPointPushPushThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPointPushPushThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointPushPushThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointPushPushThink);
        }
    }

    public void Invoke(CPointPush schemaObject) => DatamapHooksPublisher.InvokeCPointPushPushThink(schemaObject.Address);
}