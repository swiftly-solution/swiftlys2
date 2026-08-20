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
    private delegate void CMomentaryRotButtonSetPositionMoveDoneDelegate(nint a1);

    private static IUnmanagedFunction<CMomentaryRotButtonSetPositionMoveDoneDelegate>? CMomentaryRotButtonSetPositionMoveDoneUnmanagedFunction;
    private static Guid CMomentaryRotButtonSetPositionMoveDoneHookGuid;

    private static IUnmanagedFunction<CMomentaryRotButtonSetPositionMoveDoneDelegate> CMomentaryRotButtonSetPositionMoveDoneGetUnmanagedFunction()
    {
        if (CMomentaryRotButtonSetPositionMoveDoneUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CMomentaryRotButton", "CMomentaryRotButtonSetPositionMoveDone");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CMomentaryRotButton::CMomentaryRotButtonSetPositionMoveDone.");
            }
            CMomentaryRotButtonSetPositionMoveDoneUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CMomentaryRotButtonSetPositionMoveDoneDelegate>(address);
        }
        return CMomentaryRotButtonSetPositionMoveDoneUnmanagedFunction;
    }

    internal static Guid HookCMomentaryRotButtonSetPositionMoveDone()
    {
        CMomentaryRotButtonSetPositionMoveDoneHookGuid = CMomentaryRotButtonSetPositionMoveDoneGetUnmanagedFunction().AddHook(next => (a1) => CMomentaryRotButtonSetPositionMoveDonePipeline(a1, () => next()(a1)));
        return CMomentaryRotButtonSetPositionMoveDoneHookGuid;
    }

    internal static Guid UnhookCMomentaryRotButtonSetPositionMoveDone()
    {
        CMomentaryRotButtonSetPositionMoveDoneGetUnmanagedFunction().RemoveHook(CMomentaryRotButtonSetPositionMoveDoneHookGuid);
        return Guid.Empty;
    }

    private static void CMomentaryRotButtonSetPositionMoveDonePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CMomentaryRotButton>(a1);

            var preCtx = new CMomentaryRotButtonSetPositionMoveDonePreContext { SchemaObject = schemaObject };
            InvokeCMomentaryRotButtonSetPositionMoveDonePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CMomentaryRotButtonSetPositionMoveDonePostContext { SchemaObject = schemaObject };
            InvokeCMomentaryRotButtonSetPositionMoveDonePost(ref postCtx);
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

    internal static void InvokeCMomentaryRotButtonSetPositionMoveDone(nint a1)
    {
        CMomentaryRotButtonSetPositionMoveDoneGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCMomentaryRotButtonSetPositionMoveDonePre(ref CMomentaryRotButtonSetPositionMoveDonePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCMomentaryRotButtonSetPositionMoveDonePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCMomentaryRotButtonSetPositionMoveDonePost(ref CMomentaryRotButtonSetPositionMoveDonePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCMomentaryRotButtonSetPositionMoveDonePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CMomentaryRotButtonSetPositionMoveDoneHook : ICMomentaryRotButtonSetPositionMoveDoneHook
{
    private event OnCMomentaryRotButtonSetPositionMoveDonePreDelegate? _Pre;
    private event OnCMomentaryRotButtonSetPositionMoveDonePostDelegate? _Post;

    public event OnCMomentaryRotButtonSetPositionMoveDonePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CMomentaryRotButtonSetPositionMoveDone);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMomentaryRotButtonSetPositionMoveDone);
            }
        }
    }

    public event OnCMomentaryRotButtonSetPositionMoveDonePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CMomentaryRotButtonSetPositionMoveDone);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMomentaryRotButtonSetPositionMoveDone);
            }
        }
    }

    public void InvokePre(ref CMomentaryRotButtonSetPositionMoveDonePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CMomentaryRotButtonSetPositionMoveDonePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMomentaryRotButtonSetPositionMoveDone);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMomentaryRotButtonSetPositionMoveDone);
        }
    }

    public void Invoke(CMomentaryRotButton schemaObject) => DatamapHooksPublisher.InvokeCMomentaryRotButtonSetPositionMoveDone(schemaObject.Address);
}