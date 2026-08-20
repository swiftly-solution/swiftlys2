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
    private delegate void CPhysForceInitialThinkDelegate(nint a1);

    private static IUnmanagedFunction<CPhysForceInitialThinkDelegate>? CPhysForceInitialThinkUnmanagedFunction;
    private static Guid CPhysForceInitialThinkHookGuid;

    private static IUnmanagedFunction<CPhysForceInitialThinkDelegate> CPhysForceInitialThinkGetUnmanagedFunction()
    {
        if (CPhysForceInitialThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPhysForce", "CPhysForceInitialThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPhysForce::CPhysForceInitialThink.");
            }
            CPhysForceInitialThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPhysForceInitialThinkDelegate>(address);
        }
        return CPhysForceInitialThinkUnmanagedFunction;
    }

    internal static Guid HookCPhysForceInitialThink()
    {
        CPhysForceInitialThinkHookGuid = CPhysForceInitialThinkGetUnmanagedFunction().AddHook(next => (a1) => CPhysForceInitialThinkPipeline(a1, () => next()(a1)));
        return CPhysForceInitialThinkHookGuid;
    }

    internal static Guid UnhookCPhysForceInitialThink()
    {
        CPhysForceInitialThinkGetUnmanagedFunction().RemoveHook(CPhysForceInitialThinkHookGuid);
        return Guid.Empty;
    }

    private static void CPhysForceInitialThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPhysForce>(a1);

            var preCtx = new CPhysForceInitialThinkPreContext { SchemaObject = schemaObject };
            InvokeCPhysForceInitialThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPhysForceInitialThinkPostContext { SchemaObject = schemaObject };
            InvokeCPhysForceInitialThinkPost(ref postCtx);
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

    internal static void InvokeCPhysForceInitialThink(nint a1)
    {
        CPhysForceInitialThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPhysForceInitialThinkPre(ref CPhysForceInitialThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysForceInitialThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPhysForceInitialThinkPost(ref CPhysForceInitialThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysForceInitialThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPhysForceInitialThinkHook : ICPhysForceInitialThinkHook
{
    private event OnCPhysForceInitialThinkPreDelegate? _Pre;
    private event OnCPhysForceInitialThinkPostDelegate? _Post;

    public event OnCPhysForceInitialThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysForceInitialThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysForceInitialThink);
            }
        }
    }

    public event OnCPhysForceInitialThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysForceInitialThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysForceInitialThink);
            }
        }
    }

    public void InvokePre(ref CPhysForceInitialThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPhysForceInitialThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysForceInitialThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysForceInitialThink);
        }
    }

    public void Invoke(CPhysForce schemaObject) => DatamapHooksPublisher.InvokeCPhysForceInitialThink(schemaObject.Address);
}