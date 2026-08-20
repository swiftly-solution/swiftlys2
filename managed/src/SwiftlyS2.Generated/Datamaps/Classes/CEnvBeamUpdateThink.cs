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
    private delegate void CEnvBeamUpdateThinkDelegate(nint a1);

    private static IUnmanagedFunction<CEnvBeamUpdateThinkDelegate>? CEnvBeamUpdateThinkUnmanagedFunction;
    private static Guid CEnvBeamUpdateThinkHookGuid;

    private static IUnmanagedFunction<CEnvBeamUpdateThinkDelegate> CEnvBeamUpdateThinkGetUnmanagedFunction()
    {
        if (CEnvBeamUpdateThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CEnvBeam", "CEnvBeamUpdateThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CEnvBeam::CEnvBeamUpdateThink.");
            }
            CEnvBeamUpdateThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CEnvBeamUpdateThinkDelegate>(address);
        }
        return CEnvBeamUpdateThinkUnmanagedFunction;
    }

    internal static Guid HookCEnvBeamUpdateThink()
    {
        CEnvBeamUpdateThinkHookGuid = CEnvBeamUpdateThinkGetUnmanagedFunction().AddHook(next => (a1) => CEnvBeamUpdateThinkPipeline(a1, () => next()(a1)));
        return CEnvBeamUpdateThinkHookGuid;
    }

    internal static Guid UnhookCEnvBeamUpdateThink()
    {
        CEnvBeamUpdateThinkGetUnmanagedFunction().RemoveHook(CEnvBeamUpdateThinkHookGuid);
        return Guid.Empty;
    }

    private static void CEnvBeamUpdateThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CEnvBeam>(a1);

            var preCtx = new CEnvBeamUpdateThinkPreContext { SchemaObject = schemaObject };
            InvokeCEnvBeamUpdateThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CEnvBeamUpdateThinkPostContext { SchemaObject = schemaObject };
            InvokeCEnvBeamUpdateThinkPost(ref postCtx);
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

    internal static void InvokeCEnvBeamUpdateThink(nint a1)
    {
        CEnvBeamUpdateThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCEnvBeamUpdateThinkPre(ref CEnvBeamUpdateThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCEnvBeamUpdateThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCEnvBeamUpdateThinkPost(ref CEnvBeamUpdateThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCEnvBeamUpdateThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CEnvBeamUpdateThinkHook : ICEnvBeamUpdateThinkHook
{
    private event OnCEnvBeamUpdateThinkPreDelegate? _Pre;
    private event OnCEnvBeamUpdateThinkPostDelegate? _Post;

    public event OnCEnvBeamUpdateThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CEnvBeamUpdateThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvBeamUpdateThink);
            }
        }
    }

    public event OnCEnvBeamUpdateThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CEnvBeamUpdateThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvBeamUpdateThink);
            }
        }
    }

    public void InvokePre(ref CEnvBeamUpdateThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CEnvBeamUpdateThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvBeamUpdateThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvBeamUpdateThink);
        }
    }

    public void Invoke(CEnvBeam schemaObject) => DatamapHooksPublisher.InvokeCEnvBeamUpdateThink(schemaObject.Address);
}