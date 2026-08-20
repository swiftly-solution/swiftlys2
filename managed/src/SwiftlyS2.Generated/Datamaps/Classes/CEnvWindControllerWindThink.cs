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
    private delegate void CEnvWindControllerWindThinkDelegate(nint a1);

    private static IUnmanagedFunction<CEnvWindControllerWindThinkDelegate>? CEnvWindControllerWindThinkUnmanagedFunction;
    private static Guid CEnvWindControllerWindThinkHookGuid;

    private static IUnmanagedFunction<CEnvWindControllerWindThinkDelegate> CEnvWindControllerWindThinkGetUnmanagedFunction()
    {
        if (CEnvWindControllerWindThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CEnvWindController", "CEnvWindControllerWindThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CEnvWindController::CEnvWindControllerWindThink.");
            }
            CEnvWindControllerWindThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CEnvWindControllerWindThinkDelegate>(address);
        }
        return CEnvWindControllerWindThinkUnmanagedFunction;
    }

    internal static Guid HookCEnvWindControllerWindThink()
    {
        CEnvWindControllerWindThinkHookGuid = CEnvWindControllerWindThinkGetUnmanagedFunction().AddHook(next => (a1) => CEnvWindControllerWindThinkPipeline(a1, () => next()(a1)));
        return CEnvWindControllerWindThinkHookGuid;
    }

    internal static Guid UnhookCEnvWindControllerWindThink()
    {
        CEnvWindControllerWindThinkGetUnmanagedFunction().RemoveHook(CEnvWindControllerWindThinkHookGuid);
        return Guid.Empty;
    }

    private static void CEnvWindControllerWindThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CEnvWindController>(a1);

            var preCtx = new CEnvWindControllerWindThinkPreContext { SchemaObject = schemaObject };
            InvokeCEnvWindControllerWindThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CEnvWindControllerWindThinkPostContext { SchemaObject = schemaObject };
            InvokeCEnvWindControllerWindThinkPost(ref postCtx);
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

    internal static void InvokeCEnvWindControllerWindThink(nint a1)
    {
        CEnvWindControllerWindThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCEnvWindControllerWindThinkPre(ref CEnvWindControllerWindThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCEnvWindControllerWindThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCEnvWindControllerWindThinkPost(ref CEnvWindControllerWindThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCEnvWindControllerWindThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CEnvWindControllerWindThinkHook : ICEnvWindControllerWindThinkHook
{
    private event OnCEnvWindControllerWindThinkPreDelegate? _Pre;
    private event OnCEnvWindControllerWindThinkPostDelegate? _Post;

    public event OnCEnvWindControllerWindThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CEnvWindControllerWindThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvWindControllerWindThink);
            }
        }
    }

    public event OnCEnvWindControllerWindThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CEnvWindControllerWindThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvWindControllerWindThink);
            }
        }
    }

    public void InvokePre(ref CEnvWindControllerWindThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CEnvWindControllerWindThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvWindControllerWindThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvWindControllerWindThink);
        }
    }

    public void Invoke(CEnvWindController schemaObject) => DatamapHooksPublisher.InvokeCEnvWindControllerWindThink(schemaObject.Address);
}