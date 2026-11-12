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
    private delegate void CBaseDoorDoorGoDownDelegate(nint a1);

    private static IUnmanagedFunction<CBaseDoorDoorGoDownDelegate>? CBaseDoorDoorGoDownUnmanagedFunction;
    private static Guid CBaseDoorDoorGoDownHookGuid;

    private static IUnmanagedFunction<CBaseDoorDoorGoDownDelegate> CBaseDoorDoorGoDownGetUnmanagedFunction()
    {
        if (CBaseDoorDoorGoDownUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseDoor", "CBaseDoorDoorGoDown");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseDoor::CBaseDoorDoorGoDown.");
            }
            CBaseDoorDoorGoDownUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseDoorDoorGoDownDelegate>(address);
        }
        return CBaseDoorDoorGoDownUnmanagedFunction;
    }

    internal static Guid HookCBaseDoorDoorGoDown()
    {
        CBaseDoorDoorGoDownHookGuid = CBaseDoorDoorGoDownGetUnmanagedFunction().AddHook(next => (a1) => CBaseDoorDoorGoDownPipeline(a1, () => next()(a1)));
        return CBaseDoorDoorGoDownHookGuid;
    }

    internal static Guid UnhookCBaseDoorDoorGoDown()
    {
        CBaseDoorDoorGoDownGetUnmanagedFunction().RemoveHook(CBaseDoorDoorGoDownHookGuid);
        return Guid.Empty;
    }

    private static void CBaseDoorDoorGoDownPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseDoor>(a1);

            var preCtx = new CBaseDoorDoorGoDownPreContext { SchemaObject = schemaObject };
            InvokeCBaseDoorDoorGoDownPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseDoorDoorGoDownPostContext { SchemaObject = schemaObject };
            InvokeCBaseDoorDoorGoDownPost(ref postCtx);
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

    internal static void InvokeCBaseDoorDoorGoDown(nint a1)
    {
        CBaseDoorDoorGoDownGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseDoorDoorGoDownPre(ref CBaseDoorDoorGoDownPreContext ctx)
    {
        var subs = GetSubscribers(DatamapHookListener.CBaseDoorDoorGoDown);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeCBaseDoorDoorGoDownPre(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
            {
                return;
            }
        }
    }

    internal static void InvokeCBaseDoorDoorGoDownPost(ref CBaseDoorDoorGoDownPostContext ctx)
    {
        var subs = GetSubscribers(DatamapHookListener.CBaseDoorDoorGoDown);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeCBaseDoorDoorGoDownPost(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
            {
                return;
            }
        }
    }
}

internal sealed class CBaseDoorDoorGoDownHook(GameHooksService owner) : ICBaseDoorDoorGoDownHook
{
    private event OnCBaseDoorDoorGoDownPreDelegate? _Pre;
    private event OnCBaseDoorDoorGoDownPostDelegate? _Post;

    public event OnCBaseDoorDoorGoDownPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseDoorDoorGoDown, owner);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorDoorGoDown, owner);
            }
        }
    }

    public event OnCBaseDoorDoorGoDownPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseDoorDoorGoDown, owner);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorDoorGoDown, owner);
            }
        }
    }

    public void InvokePre(ref CBaseDoorDoorGoDownPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseDoorDoorGoDownPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorDoorGoDown, owner);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorDoorGoDown, owner);
        }
    }

    public void Invoke(CBaseDoor schemaObject) => DatamapHooksPublisher.InvokeCBaseDoorDoorGoDown(schemaObject.Address);
}