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
    private delegate void CEnvWindWindThinkDelegate(nint a1);

    private static IUnmanagedFunction<CEnvWindWindThinkDelegate>? CEnvWindWindThinkUnmanagedFunction;
    private static Guid CEnvWindWindThinkHookGuid;

    private static IUnmanagedFunction<CEnvWindWindThinkDelegate> CEnvWindWindThinkGetUnmanagedFunction()
    {
        if (CEnvWindWindThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CEnvWind", "CEnvWindWindThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CEnvWind::CEnvWindWindThink.");
            }
            CEnvWindWindThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CEnvWindWindThinkDelegate>(address);
        }
        return CEnvWindWindThinkUnmanagedFunction;
    }

    internal static Guid HookCEnvWindWindThink()
    {
        CEnvWindWindThinkHookGuid = CEnvWindWindThinkGetUnmanagedFunction().AddHook(next => (a1) => CEnvWindWindThinkPipeline(a1, () => next()(a1)));
        return CEnvWindWindThinkHookGuid;
    }

    internal static Guid UnhookCEnvWindWindThink()
    {
        CEnvWindWindThinkGetUnmanagedFunction().RemoveHook(CEnvWindWindThinkHookGuid);
        return Guid.Empty;
    }

    private static void CEnvWindWindThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CEnvWind>(a1);

            var preCtx = new CEnvWindWindThinkPreContext { SchemaObject = schemaObject };
            InvokeCEnvWindWindThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CEnvWindWindThinkPostContext { SchemaObject = schemaObject };
            InvokeCEnvWindWindThinkPost(ref postCtx);
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

    internal static void InvokeCEnvWindWindThink(nint a1)
    {
        CEnvWindWindThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCEnvWindWindThinkPre(ref CEnvWindWindThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCEnvWindWindThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCEnvWindWindThinkPost(ref CEnvWindWindThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCEnvWindWindThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CEnvWindWindThinkHook : ICEnvWindWindThinkHook
{
    private event OnCEnvWindWindThinkPreDelegate? _Pre;
    private event OnCEnvWindWindThinkPostDelegate? _Post;

    public event OnCEnvWindWindThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CEnvWindWindThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvWindWindThink);
            }
        }
    }

    public event OnCEnvWindWindThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CEnvWindWindThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvWindWindThink);
            }
        }
    }

    public void InvokePre(ref CEnvWindWindThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CEnvWindWindThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvWindWindThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvWindWindThink);
        }
    }

    public void Invoke(CEnvWind schemaObject) => DatamapHooksPublisher.InvokeCEnvWindWindThink(schemaObject.Address);
}