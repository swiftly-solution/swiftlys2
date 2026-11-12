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
    private delegate void CMomentaryRotButtonReturnMoveDoneDelegate(nint a1);

    private static IUnmanagedFunction<CMomentaryRotButtonReturnMoveDoneDelegate>? CMomentaryRotButtonReturnMoveDoneUnmanagedFunction;
    private static Guid CMomentaryRotButtonReturnMoveDoneHookGuid;

    private static IUnmanagedFunction<CMomentaryRotButtonReturnMoveDoneDelegate> CMomentaryRotButtonReturnMoveDoneGetUnmanagedFunction()
    {
        if (CMomentaryRotButtonReturnMoveDoneUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CMomentaryRotButton", "CMomentaryRotButtonReturnMoveDone");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CMomentaryRotButton::CMomentaryRotButtonReturnMoveDone.");
            }
            CMomentaryRotButtonReturnMoveDoneUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CMomentaryRotButtonReturnMoveDoneDelegate>(address);
        }
        return CMomentaryRotButtonReturnMoveDoneUnmanagedFunction;
    }

    internal static Guid HookCMomentaryRotButtonReturnMoveDone()
    {
        CMomentaryRotButtonReturnMoveDoneHookGuid = CMomentaryRotButtonReturnMoveDoneGetUnmanagedFunction().AddHook(next => (a1) => CMomentaryRotButtonReturnMoveDonePipeline(a1, () => next()(a1)));
        return CMomentaryRotButtonReturnMoveDoneHookGuid;
    }

    internal static Guid UnhookCMomentaryRotButtonReturnMoveDone()
    {
        CMomentaryRotButtonReturnMoveDoneGetUnmanagedFunction().RemoveHook(CMomentaryRotButtonReturnMoveDoneHookGuid);
        return Guid.Empty;
    }

    private static void CMomentaryRotButtonReturnMoveDonePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CMomentaryRotButton>(a1);

            var preCtx = new CMomentaryRotButtonReturnMoveDonePreContext { SchemaObject = schemaObject };
            InvokeCMomentaryRotButtonReturnMoveDonePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CMomentaryRotButtonReturnMoveDonePostContext { SchemaObject = schemaObject };
            InvokeCMomentaryRotButtonReturnMoveDonePost(ref postCtx);
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

    internal static void InvokeCMomentaryRotButtonReturnMoveDone(nint a1)
    {
        CMomentaryRotButtonReturnMoveDoneGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCMomentaryRotButtonReturnMoveDonePre(ref CMomentaryRotButtonReturnMoveDonePreContext ctx)
    {
        var subs = GetSubscribers(DatamapHookListener.CMomentaryRotButtonReturnMoveDone);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeCMomentaryRotButtonReturnMoveDonePre(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
            {
                return;
            }
        }
    }

    internal static void InvokeCMomentaryRotButtonReturnMoveDonePost(ref CMomentaryRotButtonReturnMoveDonePostContext ctx)
    {
        var subs = GetSubscribers(DatamapHookListener.CMomentaryRotButtonReturnMoveDone);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeCMomentaryRotButtonReturnMoveDonePost(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
            {
                return;
            }
        }
    }
}

internal sealed class CMomentaryRotButtonReturnMoveDoneHook(GameHooksService owner) : ICMomentaryRotButtonReturnMoveDoneHook
{
    private event OnCMomentaryRotButtonReturnMoveDonePreDelegate? _Pre;
    private event OnCMomentaryRotButtonReturnMoveDonePostDelegate? _Post;

    public event OnCMomentaryRotButtonReturnMoveDonePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CMomentaryRotButtonReturnMoveDone, owner);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMomentaryRotButtonReturnMoveDone, owner);
            }
        }
    }

    public event OnCMomentaryRotButtonReturnMoveDonePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CMomentaryRotButtonReturnMoveDone, owner);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMomentaryRotButtonReturnMoveDone, owner);
            }
        }
    }

    public void InvokePre(ref CMomentaryRotButtonReturnMoveDonePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CMomentaryRotButtonReturnMoveDonePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMomentaryRotButtonReturnMoveDone, owner);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMomentaryRotButtonReturnMoveDone, owner);
        }
    }

    public void Invoke(CMomentaryRotButton schemaObject) => DatamapHooksPublisher.InvokeCMomentaryRotButtonReturnMoveDone(schemaObject.Address);
}