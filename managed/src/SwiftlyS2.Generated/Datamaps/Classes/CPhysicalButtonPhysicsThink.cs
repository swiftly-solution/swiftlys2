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
    private delegate void CPhysicalButtonPhysicsThinkDelegate(nint a1);

    private static IUnmanagedFunction<CPhysicalButtonPhysicsThinkDelegate>? CPhysicalButtonPhysicsThinkUnmanagedFunction;
    private static Guid CPhysicalButtonPhysicsThinkHookGuid;

    private static IUnmanagedFunction<CPhysicalButtonPhysicsThinkDelegate> CPhysicalButtonPhysicsThinkGetUnmanagedFunction()
    {
        if (CPhysicalButtonPhysicsThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPhysicalButton", "CPhysicalButtonPhysicsThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPhysicalButton::CPhysicalButtonPhysicsThink.");
            }
            CPhysicalButtonPhysicsThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPhysicalButtonPhysicsThinkDelegate>(address);
        }
        return CPhysicalButtonPhysicsThinkUnmanagedFunction;
    }

    internal static Guid HookCPhysicalButtonPhysicsThink()
    {
        CPhysicalButtonPhysicsThinkHookGuid = CPhysicalButtonPhysicsThinkGetUnmanagedFunction().AddHook(next => (a1) => CPhysicalButtonPhysicsThinkPipeline(a1, () => next()(a1)));
        return CPhysicalButtonPhysicsThinkHookGuid;
    }

    internal static Guid UnhookCPhysicalButtonPhysicsThink()
    {
        CPhysicalButtonPhysicsThinkGetUnmanagedFunction().RemoveHook(CPhysicalButtonPhysicsThinkHookGuid);
        return Guid.Empty;
    }

    private static void CPhysicalButtonPhysicsThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPhysicalButton>(a1);

            var preCtx = new CPhysicalButtonPhysicsThinkPreContext { SchemaObject = schemaObject };
            InvokeCPhysicalButtonPhysicsThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPhysicalButtonPhysicsThinkPostContext { SchemaObject = schemaObject };
            InvokeCPhysicalButtonPhysicsThinkPost(ref postCtx);
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

    internal static void InvokeCPhysicalButtonPhysicsThink(nint a1)
    {
        CPhysicalButtonPhysicsThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPhysicalButtonPhysicsThinkPre(ref CPhysicalButtonPhysicsThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysicalButtonPhysicsThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPhysicalButtonPhysicsThinkPost(ref CPhysicalButtonPhysicsThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysicalButtonPhysicsThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPhysicalButtonPhysicsThinkHook : ICPhysicalButtonPhysicsThinkHook
{
    private event OnCPhysicalButtonPhysicsThinkPreDelegate? _Pre;
    private event OnCPhysicalButtonPhysicsThinkPostDelegate? _Post;

    public event OnCPhysicalButtonPhysicsThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysicalButtonPhysicsThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonPhysicsThink);
            }
        }
    }

    public event OnCPhysicalButtonPhysicsThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysicalButtonPhysicsThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonPhysicsThink);
            }
        }
    }

    public void InvokePre(ref CPhysicalButtonPhysicsThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPhysicalButtonPhysicsThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonPhysicsThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonPhysicsThink);
        }
    }

    public void Invoke(CPhysicalButton schemaObject) => DatamapHooksPublisher.InvokeCPhysicalButtonPhysicsThink(schemaObject.Address);
}