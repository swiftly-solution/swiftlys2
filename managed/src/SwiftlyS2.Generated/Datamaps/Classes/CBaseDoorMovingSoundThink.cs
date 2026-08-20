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
    private delegate void CBaseDoorMovingSoundThinkDelegate(nint a1);

    private static IUnmanagedFunction<CBaseDoorMovingSoundThinkDelegate>? CBaseDoorMovingSoundThinkUnmanagedFunction;
    private static Guid CBaseDoorMovingSoundThinkHookGuid;

    private static IUnmanagedFunction<CBaseDoorMovingSoundThinkDelegate> CBaseDoorMovingSoundThinkGetUnmanagedFunction()
    {
        if (CBaseDoorMovingSoundThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseDoor", "CBaseDoorMovingSoundThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseDoor::CBaseDoorMovingSoundThink.");
            }
            CBaseDoorMovingSoundThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseDoorMovingSoundThinkDelegate>(address);
        }
        return CBaseDoorMovingSoundThinkUnmanagedFunction;
    }

    internal static Guid HookCBaseDoorMovingSoundThink()
    {
        CBaseDoorMovingSoundThinkHookGuid = CBaseDoorMovingSoundThinkGetUnmanagedFunction().AddHook(next => (a1) => CBaseDoorMovingSoundThinkPipeline(a1, () => next()(a1)));
        return CBaseDoorMovingSoundThinkHookGuid;
    }

    internal static Guid UnhookCBaseDoorMovingSoundThink()
    {
        CBaseDoorMovingSoundThinkGetUnmanagedFunction().RemoveHook(CBaseDoorMovingSoundThinkHookGuid);
        return Guid.Empty;
    }

    private static void CBaseDoorMovingSoundThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseDoor>(a1);

            var preCtx = new CBaseDoorMovingSoundThinkPreContext { SchemaObject = schemaObject };
            InvokeCBaseDoorMovingSoundThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseDoorMovingSoundThinkPostContext { SchemaObject = schemaObject };
            InvokeCBaseDoorMovingSoundThinkPost(ref postCtx);
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

    internal static void InvokeCBaseDoorMovingSoundThink(nint a1)
    {
        CBaseDoorMovingSoundThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseDoorMovingSoundThinkPre(ref CBaseDoorMovingSoundThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseDoorMovingSoundThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseDoorMovingSoundThinkPost(ref CBaseDoorMovingSoundThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseDoorMovingSoundThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseDoorMovingSoundThinkHook : ICBaseDoorMovingSoundThinkHook
{
    private event OnCBaseDoorMovingSoundThinkPreDelegate? _Pre;
    private event OnCBaseDoorMovingSoundThinkPostDelegate? _Post;

    public event OnCBaseDoorMovingSoundThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseDoorMovingSoundThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorMovingSoundThink);
            }
        }
    }

    public event OnCBaseDoorMovingSoundThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseDoorMovingSoundThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorMovingSoundThink);
            }
        }
    }

    public void InvokePre(ref CBaseDoorMovingSoundThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseDoorMovingSoundThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorMovingSoundThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorMovingSoundThink);
        }
    }

    public void Invoke(CBaseDoor schemaObject) => DatamapHooksPublisher.InvokeCBaseDoorMovingSoundThink(schemaObject.Address);
}