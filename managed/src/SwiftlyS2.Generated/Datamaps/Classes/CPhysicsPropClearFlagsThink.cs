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
    private delegate void CPhysicsPropClearFlagsThinkDelegate(nint a1);

    private static IUnmanagedFunction<CPhysicsPropClearFlagsThinkDelegate>? CPhysicsPropClearFlagsThinkUnmanagedFunction;
    private static Guid CPhysicsPropClearFlagsThinkHookGuid;

    private static IUnmanagedFunction<CPhysicsPropClearFlagsThinkDelegate> CPhysicsPropClearFlagsThinkGetUnmanagedFunction()
    {
        if (CPhysicsPropClearFlagsThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPhysicsProp", "CPhysicsPropClearFlagsThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPhysicsProp::CPhysicsPropClearFlagsThink.");
            }
            CPhysicsPropClearFlagsThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPhysicsPropClearFlagsThinkDelegate>(address);
        }
        return CPhysicsPropClearFlagsThinkUnmanagedFunction;
    }

    internal static Guid HookCPhysicsPropClearFlagsThink()
    {
        CPhysicsPropClearFlagsThinkHookGuid = CPhysicsPropClearFlagsThinkGetUnmanagedFunction().AddHook(next => (a1) => CPhysicsPropClearFlagsThinkPipeline(a1, () => next()(a1)));
        return CPhysicsPropClearFlagsThinkHookGuid;
    }

    internal static Guid UnhookCPhysicsPropClearFlagsThink()
    {
        CPhysicsPropClearFlagsThinkGetUnmanagedFunction().RemoveHook(CPhysicsPropClearFlagsThinkHookGuid);
        return Guid.Empty;
    }

    private static void CPhysicsPropClearFlagsThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPhysicsProp>(a1);

            var preCtx = new CPhysicsPropClearFlagsThinkPreContext { SchemaObject = schemaObject };
            InvokeCPhysicsPropClearFlagsThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPhysicsPropClearFlagsThinkPostContext { SchemaObject = schemaObject };
            InvokeCPhysicsPropClearFlagsThinkPost(ref postCtx);
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

    internal static void InvokeCPhysicsPropClearFlagsThink(nint a1)
    {
        CPhysicsPropClearFlagsThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPhysicsPropClearFlagsThinkPre(ref CPhysicsPropClearFlagsThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysicsPropClearFlagsThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPhysicsPropClearFlagsThinkPost(ref CPhysicsPropClearFlagsThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysicsPropClearFlagsThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPhysicsPropClearFlagsThinkHook : ICPhysicsPropClearFlagsThinkHook
{
    private event OnCPhysicsPropClearFlagsThinkPreDelegate? _Pre;
    private event OnCPhysicsPropClearFlagsThinkPostDelegate? _Post;

    public event OnCPhysicsPropClearFlagsThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysicsPropClearFlagsThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicsPropClearFlagsThink);
            }
        }
    }

    public event OnCPhysicsPropClearFlagsThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysicsPropClearFlagsThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicsPropClearFlagsThink);
            }
        }
    }

    public void InvokePre(ref CPhysicsPropClearFlagsThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPhysicsPropClearFlagsThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicsPropClearFlagsThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicsPropClearFlagsThink);
        }
    }

    public void Invoke(CPhysicsProp schemaObject) => DatamapHooksPublisher.InvokeCPhysicsPropClearFlagsThink(schemaObject.Address);
}