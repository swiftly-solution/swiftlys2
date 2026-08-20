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
    private delegate void CPhysForceForceOffDelegate(nint a1);

    private static IUnmanagedFunction<CPhysForceForceOffDelegate>? CPhysForceForceOffUnmanagedFunction;
    private static Guid CPhysForceForceOffHookGuid;

    private static IUnmanagedFunction<CPhysForceForceOffDelegate> CPhysForceForceOffGetUnmanagedFunction()
    {
        if (CPhysForceForceOffUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPhysForce", "CPhysForceForceOff");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPhysForce::CPhysForceForceOff.");
            }
            CPhysForceForceOffUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPhysForceForceOffDelegate>(address);
        }
        return CPhysForceForceOffUnmanagedFunction;
    }

    internal static Guid HookCPhysForceForceOff()
    {
        CPhysForceForceOffHookGuid = CPhysForceForceOffGetUnmanagedFunction().AddHook(next => (a1) => CPhysForceForceOffPipeline(a1, () => next()(a1)));
        return CPhysForceForceOffHookGuid;
    }

    internal static Guid UnhookCPhysForceForceOff()
    {
        CPhysForceForceOffGetUnmanagedFunction().RemoveHook(CPhysForceForceOffHookGuid);
        return Guid.Empty;
    }

    private static void CPhysForceForceOffPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPhysForce>(a1);

            var preCtx = new CPhysForceForceOffPreContext { SchemaObject = schemaObject };
            InvokeCPhysForceForceOffPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPhysForceForceOffPostContext { SchemaObject = schemaObject };
            InvokeCPhysForceForceOffPost(ref postCtx);
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

    internal static void InvokeCPhysForceForceOff(nint a1)
    {
        CPhysForceForceOffGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPhysForceForceOffPre(ref CPhysForceForceOffPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysForceForceOffPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPhysForceForceOffPost(ref CPhysForceForceOffPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysForceForceOffPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPhysForceForceOffHook : ICPhysForceForceOffHook
{
    private event OnCPhysForceForceOffPreDelegate? _Pre;
    private event OnCPhysForceForceOffPostDelegate? _Post;

    public event OnCPhysForceForceOffPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysForceForceOff);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysForceForceOff);
            }
        }
    }

    public event OnCPhysForceForceOffPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysForceForceOff);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysForceForceOff);
            }
        }
    }

    public void InvokePre(ref CPhysForceForceOffPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPhysForceForceOffPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysForceForceOff);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysForceForceOff);
        }
    }

    public void Invoke(CPhysForce schemaObject) => DatamapHooksPublisher.InvokeCPhysForceForceOff(schemaObject.Address);
}