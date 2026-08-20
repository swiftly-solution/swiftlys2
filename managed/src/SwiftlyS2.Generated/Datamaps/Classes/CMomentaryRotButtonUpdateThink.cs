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
    private delegate void CMomentaryRotButtonUpdateThinkDelegate(nint a1);

    private static IUnmanagedFunction<CMomentaryRotButtonUpdateThinkDelegate>? CMomentaryRotButtonUpdateThinkUnmanagedFunction;
    private static Guid CMomentaryRotButtonUpdateThinkHookGuid;

    private static IUnmanagedFunction<CMomentaryRotButtonUpdateThinkDelegate> CMomentaryRotButtonUpdateThinkGetUnmanagedFunction()
    {
        if (CMomentaryRotButtonUpdateThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CMomentaryRotButton", "CMomentaryRotButtonUpdateThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CMomentaryRotButton::CMomentaryRotButtonUpdateThink.");
            }
            CMomentaryRotButtonUpdateThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CMomentaryRotButtonUpdateThinkDelegate>(address);
        }
        return CMomentaryRotButtonUpdateThinkUnmanagedFunction;
    }

    internal static Guid HookCMomentaryRotButtonUpdateThink()
    {
        CMomentaryRotButtonUpdateThinkHookGuid = CMomentaryRotButtonUpdateThinkGetUnmanagedFunction().AddHook(next => (a1) => CMomentaryRotButtonUpdateThinkPipeline(a1, () => next()(a1)));
        return CMomentaryRotButtonUpdateThinkHookGuid;
    }

    internal static Guid UnhookCMomentaryRotButtonUpdateThink()
    {
        CMomentaryRotButtonUpdateThinkGetUnmanagedFunction().RemoveHook(CMomentaryRotButtonUpdateThinkHookGuid);
        return Guid.Empty;
    }

    private static void CMomentaryRotButtonUpdateThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CMomentaryRotButton>(a1);

            var preCtx = new CMomentaryRotButtonUpdateThinkPreContext { SchemaObject = schemaObject };
            InvokeCMomentaryRotButtonUpdateThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CMomentaryRotButtonUpdateThinkPostContext { SchemaObject = schemaObject };
            InvokeCMomentaryRotButtonUpdateThinkPost(ref postCtx);
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

    internal static void InvokeCMomentaryRotButtonUpdateThink(nint a1)
    {
        CMomentaryRotButtonUpdateThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCMomentaryRotButtonUpdateThinkPre(ref CMomentaryRotButtonUpdateThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCMomentaryRotButtonUpdateThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCMomentaryRotButtonUpdateThinkPost(ref CMomentaryRotButtonUpdateThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCMomentaryRotButtonUpdateThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CMomentaryRotButtonUpdateThinkHook : ICMomentaryRotButtonUpdateThinkHook
{
    private event OnCMomentaryRotButtonUpdateThinkPreDelegate? _Pre;
    private event OnCMomentaryRotButtonUpdateThinkPostDelegate? _Post;

    public event OnCMomentaryRotButtonUpdateThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CMomentaryRotButtonUpdateThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMomentaryRotButtonUpdateThink);
            }
        }
    }

    public event OnCMomentaryRotButtonUpdateThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CMomentaryRotButtonUpdateThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMomentaryRotButtonUpdateThink);
            }
        }
    }

    public void InvokePre(ref CMomentaryRotButtonUpdateThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CMomentaryRotButtonUpdateThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMomentaryRotButtonUpdateThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMomentaryRotButtonUpdateThink);
        }
    }

    public void Invoke(CMomentaryRotButton schemaObject) => DatamapHooksPublisher.InvokeCMomentaryRotButtonUpdateThink(schemaObject.Address);
}