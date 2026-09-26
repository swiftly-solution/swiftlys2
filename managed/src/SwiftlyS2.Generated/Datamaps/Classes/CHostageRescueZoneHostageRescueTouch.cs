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
    private delegate void CHostageRescueZoneHostageRescueTouchDelegate(nint a1);

    private static IUnmanagedFunction<CHostageRescueZoneHostageRescueTouchDelegate>? CHostageRescueZoneHostageRescueTouchUnmanagedFunction;
    private static Guid CHostageRescueZoneHostageRescueTouchHookGuid;

    private static IUnmanagedFunction<CHostageRescueZoneHostageRescueTouchDelegate> CHostageRescueZoneHostageRescueTouchGetUnmanagedFunction()
    {
        if (CHostageRescueZoneHostageRescueTouchUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CHostageRescueZone", "CHostageRescueZoneHostageRescueTouch");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CHostageRescueZone::CHostageRescueZoneHostageRescueTouch.");
            }
            CHostageRescueZoneHostageRescueTouchUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CHostageRescueZoneHostageRescueTouchDelegate>(address);
        }
        return CHostageRescueZoneHostageRescueTouchUnmanagedFunction;
    }

    internal static Guid HookCHostageRescueZoneHostageRescueTouch()
    {
        CHostageRescueZoneHostageRescueTouchHookGuid = CHostageRescueZoneHostageRescueTouchGetUnmanagedFunction().AddHook(next => (a1) => CHostageRescueZoneHostageRescueTouchPipeline(a1, () => next()(a1)));
        return CHostageRescueZoneHostageRescueTouchHookGuid;
    }

    internal static Guid UnhookCHostageRescueZoneHostageRescueTouch()
    {
        CHostageRescueZoneHostageRescueTouchGetUnmanagedFunction().RemoveHook(CHostageRescueZoneHostageRescueTouchHookGuid);
        return Guid.Empty;
    }

    private static void CHostageRescueZoneHostageRescueTouchPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CHostageRescueZone>(a1);

            var preCtx = new CHostageRescueZoneHostageRescueTouchPreContext { SchemaObject = schemaObject };
            InvokeCHostageRescueZoneHostageRescueTouchPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CHostageRescueZoneHostageRescueTouchPostContext { SchemaObject = schemaObject };
            InvokeCHostageRescueZoneHostageRescueTouchPost(ref postCtx);
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

    internal static void InvokeCHostageRescueZoneHostageRescueTouch(nint a1)
    {
        CHostageRescueZoneHostageRescueTouchGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCHostageRescueZoneHostageRescueTouchPre(ref CHostageRescueZoneHostageRescueTouchPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCHostageRescueZoneHostageRescueTouchPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCHostageRescueZoneHostageRescueTouchPost(ref CHostageRescueZoneHostageRescueTouchPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCHostageRescueZoneHostageRescueTouchPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CHostageRescueZoneHostageRescueTouchHook : ICHostageRescueZoneHostageRescueTouchHook
{
    private event OnCHostageRescueZoneHostageRescueTouchPreDelegate? _Pre;
    private event OnCHostageRescueZoneHostageRescueTouchPostDelegate? _Post;

    public event OnCHostageRescueZoneHostageRescueTouchPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CHostageRescueZoneHostageRescueTouch);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CHostageRescueZoneHostageRescueTouch);
            }
        }
    }

    public event OnCHostageRescueZoneHostageRescueTouchPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CHostageRescueZoneHostageRescueTouch);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CHostageRescueZoneHostageRescueTouch);
            }
        }
    }

    public void InvokePre(ref CHostageRescueZoneHostageRescueTouchPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CHostageRescueZoneHostageRescueTouchPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CHostageRescueZoneHostageRescueTouch);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CHostageRescueZoneHostageRescueTouch);
        }
    }

    public void Invoke(CHostageRescueZone schemaObject) => DatamapHooksPublisher.InvokeCHostageRescueZoneHostageRescueTouch(schemaObject.Address);
}