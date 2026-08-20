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
    private delegate void CBaseDoorDoorGoUpDelegate(nint a1);

    private static IUnmanagedFunction<CBaseDoorDoorGoUpDelegate>? CBaseDoorDoorGoUpUnmanagedFunction;
    private static Guid CBaseDoorDoorGoUpHookGuid;

    private static IUnmanagedFunction<CBaseDoorDoorGoUpDelegate> CBaseDoorDoorGoUpGetUnmanagedFunction()
    {
        if (CBaseDoorDoorGoUpUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseDoor", "CBaseDoorDoorGoUp");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseDoor::CBaseDoorDoorGoUp.");
            }
            CBaseDoorDoorGoUpUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseDoorDoorGoUpDelegate>(address);
        }
        return CBaseDoorDoorGoUpUnmanagedFunction;
    }

    internal static Guid HookCBaseDoorDoorGoUp()
    {
        CBaseDoorDoorGoUpHookGuid = CBaseDoorDoorGoUpGetUnmanagedFunction().AddHook(next => (a1) => CBaseDoorDoorGoUpPipeline(a1, () => next()(a1)));
        return CBaseDoorDoorGoUpHookGuid;
    }

    internal static Guid UnhookCBaseDoorDoorGoUp()
    {
        CBaseDoorDoorGoUpGetUnmanagedFunction().RemoveHook(CBaseDoorDoorGoUpHookGuid);
        return Guid.Empty;
    }

    private static void CBaseDoorDoorGoUpPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseDoor>(a1);

            var preCtx = new CBaseDoorDoorGoUpPreContext { SchemaObject = schemaObject };
            InvokeCBaseDoorDoorGoUpPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseDoorDoorGoUpPostContext { SchemaObject = schemaObject };
            InvokeCBaseDoorDoorGoUpPost(ref postCtx);
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

    internal static void InvokeCBaseDoorDoorGoUp(nint a1)
    {
        CBaseDoorDoorGoUpGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseDoorDoorGoUpPre(ref CBaseDoorDoorGoUpPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseDoorDoorGoUpPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseDoorDoorGoUpPost(ref CBaseDoorDoorGoUpPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseDoorDoorGoUpPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseDoorDoorGoUpHook : ICBaseDoorDoorGoUpHook
{
    private event OnCBaseDoorDoorGoUpPreDelegate? _Pre;
    private event OnCBaseDoorDoorGoUpPostDelegate? _Post;

    public event OnCBaseDoorDoorGoUpPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseDoorDoorGoUp);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorDoorGoUp);
            }
        }
    }

    public event OnCBaseDoorDoorGoUpPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseDoorDoorGoUp);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorDoorGoUp);
            }
        }
    }

    public void InvokePre(ref CBaseDoorDoorGoUpPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseDoorDoorGoUpPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorDoorGoUp);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorDoorGoUp);
        }
    }

    public void Invoke(CBaseDoor schemaObject) => DatamapHooksPublisher.InvokeCBaseDoorDoorGoUp(schemaObject.Address);
}