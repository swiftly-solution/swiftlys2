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
    private delegate void CPhysicsPropClearThrownByPlayerThinkDelegate(nint a1);

    private static IUnmanagedFunction<CPhysicsPropClearThrownByPlayerThinkDelegate>? CPhysicsPropClearThrownByPlayerThinkUnmanagedFunction;
    private static Guid CPhysicsPropClearThrownByPlayerThinkHookGuid;

    private static IUnmanagedFunction<CPhysicsPropClearThrownByPlayerThinkDelegate> CPhysicsPropClearThrownByPlayerThinkGetUnmanagedFunction()
    {
        if (CPhysicsPropClearThrownByPlayerThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPhysicsProp", "CPhysicsPropClearThrownByPlayerThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPhysicsProp::CPhysicsPropClearThrownByPlayerThink.");
            }
            CPhysicsPropClearThrownByPlayerThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPhysicsPropClearThrownByPlayerThinkDelegate>(address);
        }
        return CPhysicsPropClearThrownByPlayerThinkUnmanagedFunction;
    }

    internal static Guid HookCPhysicsPropClearThrownByPlayerThink()
    {
        CPhysicsPropClearThrownByPlayerThinkHookGuid = CPhysicsPropClearThrownByPlayerThinkGetUnmanagedFunction().AddHook(next => (a1) => CPhysicsPropClearThrownByPlayerThinkPipeline(a1, () => next()(a1)));
        return CPhysicsPropClearThrownByPlayerThinkHookGuid;
    }

    internal static Guid UnhookCPhysicsPropClearThrownByPlayerThink()
    {
        CPhysicsPropClearThrownByPlayerThinkGetUnmanagedFunction().RemoveHook(CPhysicsPropClearThrownByPlayerThinkHookGuid);
        return Guid.Empty;
    }

    private static void CPhysicsPropClearThrownByPlayerThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPhysicsProp>(a1);

            var preCtx = new CPhysicsPropClearThrownByPlayerThinkPreContext { SchemaObject = schemaObject };
            InvokeCPhysicsPropClearThrownByPlayerThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPhysicsPropClearThrownByPlayerThinkPostContext { SchemaObject = schemaObject };
            InvokeCPhysicsPropClearThrownByPlayerThinkPost(ref postCtx);
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

    internal static void InvokeCPhysicsPropClearThrownByPlayerThink(nint a1)
    {
        CPhysicsPropClearThrownByPlayerThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPhysicsPropClearThrownByPlayerThinkPre(ref CPhysicsPropClearThrownByPlayerThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysicsPropClearThrownByPlayerThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPhysicsPropClearThrownByPlayerThinkPost(ref CPhysicsPropClearThrownByPlayerThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysicsPropClearThrownByPlayerThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPhysicsPropClearThrownByPlayerThinkHook : ICPhysicsPropClearThrownByPlayerThinkHook
{
    private event OnCPhysicsPropClearThrownByPlayerThinkPreDelegate? _Pre;
    private event OnCPhysicsPropClearThrownByPlayerThinkPostDelegate? _Post;

    public event OnCPhysicsPropClearThrownByPlayerThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysicsPropClearThrownByPlayerThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicsPropClearThrownByPlayerThink);
            }
        }
    }

    public event OnCPhysicsPropClearThrownByPlayerThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysicsPropClearThrownByPlayerThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicsPropClearThrownByPlayerThink);
            }
        }
    }

    public void InvokePre(ref CPhysicsPropClearThrownByPlayerThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPhysicsPropClearThrownByPlayerThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicsPropClearThrownByPlayerThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicsPropClearThrownByPlayerThink);
        }
    }

    public void Invoke(CPhysicsProp schemaObject) => DatamapHooksPublisher.InvokeCPhysicsPropClearThrownByPlayerThink(schemaObject.Address);
}