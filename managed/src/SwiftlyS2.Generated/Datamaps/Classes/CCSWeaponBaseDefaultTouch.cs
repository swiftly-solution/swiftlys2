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
    private delegate void CCSWeaponBaseDefaultTouchDelegate(nint a1);

    private static IUnmanagedFunction<CCSWeaponBaseDefaultTouchDelegate>? CCSWeaponBaseDefaultTouchUnmanagedFunction;
    private static Guid CCSWeaponBaseDefaultTouchHookGuid;

    private static IUnmanagedFunction<CCSWeaponBaseDefaultTouchDelegate> CCSWeaponBaseDefaultTouchGetUnmanagedFunction()
    {
        if (CCSWeaponBaseDefaultTouchUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CCSWeaponBase", "CCSWeaponBaseDefaultTouch");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CCSWeaponBase::CCSWeaponBaseDefaultTouch.");
            }
            CCSWeaponBaseDefaultTouchUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSWeaponBaseDefaultTouchDelegate>(address);
        }
        return CCSWeaponBaseDefaultTouchUnmanagedFunction;
    }

    internal static Guid HookCCSWeaponBaseDefaultTouch()
    {
        CCSWeaponBaseDefaultTouchHookGuid = CCSWeaponBaseDefaultTouchGetUnmanagedFunction().AddHook(next => (a1) => CCSWeaponBaseDefaultTouchPipeline(a1, () => next()(a1)));
        return CCSWeaponBaseDefaultTouchHookGuid;
    }

    internal static Guid UnhookCCSWeaponBaseDefaultTouch()
    {
        CCSWeaponBaseDefaultTouchGetUnmanagedFunction().RemoveHook(CCSWeaponBaseDefaultTouchHookGuid);
        return Guid.Empty;
    }

    private static void CCSWeaponBaseDefaultTouchPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CCSWeaponBase>(a1);

            var preCtx = new CCSWeaponBaseDefaultTouchPreContext { SchemaObject = schemaObject };
            InvokeCCSWeaponBaseDefaultTouchPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CCSWeaponBaseDefaultTouchPostContext { SchemaObject = schemaObject };
            InvokeCCSWeaponBaseDefaultTouchPost(ref postCtx);
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

    internal static void InvokeCCSWeaponBaseDefaultTouch(nint a1)
    {
        CCSWeaponBaseDefaultTouchGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCCSWeaponBaseDefaultTouchPre(ref CCSWeaponBaseDefaultTouchPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCCSWeaponBaseDefaultTouchPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCCSWeaponBaseDefaultTouchPost(ref CCSWeaponBaseDefaultTouchPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCCSWeaponBaseDefaultTouchPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CCSWeaponBaseDefaultTouchHook : ICCSWeaponBaseDefaultTouchHook
{
    private event OnCCSWeaponBaseDefaultTouchPreDelegate? _Pre;
    private event OnCCSWeaponBaseDefaultTouchPostDelegate? _Post;

    public event OnCCSWeaponBaseDefaultTouchPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CCSWeaponBaseDefaultTouch);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSWeaponBaseDefaultTouch);
            }
        }
    }

    public event OnCCSWeaponBaseDefaultTouchPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CCSWeaponBaseDefaultTouch);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSWeaponBaseDefaultTouch);
            }
        }
    }

    public void InvokePre(ref CCSWeaponBaseDefaultTouchPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CCSWeaponBaseDefaultTouchPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSWeaponBaseDefaultTouch);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSWeaponBaseDefaultTouch);
        }
    }

    public void Invoke(CCSWeaponBase schemaObject) => DatamapHooksPublisher.InvokeCCSWeaponBaseDefaultTouch(schemaObject.Address);
}