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
    private delegate void CMomentaryRotButtonUseMoveDoneDelegate(nint a1);

    private static IUnmanagedFunction<CMomentaryRotButtonUseMoveDoneDelegate>? CMomentaryRotButtonUseMoveDoneUnmanagedFunction;
    private static Guid CMomentaryRotButtonUseMoveDoneHookGuid;

    private static IUnmanagedFunction<CMomentaryRotButtonUseMoveDoneDelegate> CMomentaryRotButtonUseMoveDoneGetUnmanagedFunction()
    {
        if (CMomentaryRotButtonUseMoveDoneUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CMomentaryRotButton", "CMomentaryRotButtonUseMoveDone");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CMomentaryRotButton::CMomentaryRotButtonUseMoveDone.");
            }
            CMomentaryRotButtonUseMoveDoneUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CMomentaryRotButtonUseMoveDoneDelegate>(address);
        }
        return CMomentaryRotButtonUseMoveDoneUnmanagedFunction;
    }

    internal static Guid HookCMomentaryRotButtonUseMoveDone()
    {
        CMomentaryRotButtonUseMoveDoneHookGuid = CMomentaryRotButtonUseMoveDoneGetUnmanagedFunction().AddHook(next => (a1) => CMomentaryRotButtonUseMoveDonePipeline(a1, () => next()(a1)));
        return CMomentaryRotButtonUseMoveDoneHookGuid;
    }

    internal static Guid UnhookCMomentaryRotButtonUseMoveDone()
    {
        CMomentaryRotButtonUseMoveDoneGetUnmanagedFunction().RemoveHook(CMomentaryRotButtonUseMoveDoneHookGuid);
        return Guid.Empty;
    }

    private static void CMomentaryRotButtonUseMoveDonePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CMomentaryRotButton>(a1);

            var preCtx = new CMomentaryRotButtonUseMoveDonePreContext { SchemaObject = schemaObject };
            InvokeCMomentaryRotButtonUseMoveDonePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CMomentaryRotButtonUseMoveDonePostContext { SchemaObject = schemaObject };
            InvokeCMomentaryRotButtonUseMoveDonePost(ref postCtx);
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

    internal static void InvokeCMomentaryRotButtonUseMoveDone(nint a1)
    {
        CMomentaryRotButtonUseMoveDoneGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCMomentaryRotButtonUseMoveDonePre(ref CMomentaryRotButtonUseMoveDonePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCMomentaryRotButtonUseMoveDonePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCMomentaryRotButtonUseMoveDonePost(ref CMomentaryRotButtonUseMoveDonePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCMomentaryRotButtonUseMoveDonePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CMomentaryRotButtonUseMoveDoneHook : ICMomentaryRotButtonUseMoveDoneHook
{
    private event OnCMomentaryRotButtonUseMoveDonePreDelegate? _Pre;
    private event OnCMomentaryRotButtonUseMoveDonePostDelegate? _Post;

    public event OnCMomentaryRotButtonUseMoveDonePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CMomentaryRotButtonUseMoveDone);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMomentaryRotButtonUseMoveDone);
            }
        }
    }

    public event OnCMomentaryRotButtonUseMoveDonePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CMomentaryRotButtonUseMoveDone);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMomentaryRotButtonUseMoveDone);
            }
        }
    }

    public void InvokePre(ref CMomentaryRotButtonUseMoveDonePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CMomentaryRotButtonUseMoveDonePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMomentaryRotButtonUseMoveDone);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMomentaryRotButtonUseMoveDone);
        }
    }

    public void Invoke(CMomentaryRotButton schemaObject) => DatamapHooksPublisher.InvokeCMomentaryRotButtonUseMoveDone(schemaObject.Address);
}