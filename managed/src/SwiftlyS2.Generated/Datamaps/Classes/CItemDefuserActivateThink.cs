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
    private delegate void CItemDefuserActivateThinkDelegate(nint a1);

    private static IUnmanagedFunction<CItemDefuserActivateThinkDelegate>? CItemDefuserActivateThinkUnmanagedFunction;
    private static Guid CItemDefuserActivateThinkHookGuid;

    private static IUnmanagedFunction<CItemDefuserActivateThinkDelegate> CItemDefuserActivateThinkGetUnmanagedFunction()
    {
        if (CItemDefuserActivateThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CItemDefuser", "CItemDefuserActivateThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CItemDefuser::CItemDefuserActivateThink.");
            }
            CItemDefuserActivateThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CItemDefuserActivateThinkDelegate>(address);
        }
        return CItemDefuserActivateThinkUnmanagedFunction;
    }

    internal static Guid HookCItemDefuserActivateThink()
    {
        CItemDefuserActivateThinkHookGuid = CItemDefuserActivateThinkGetUnmanagedFunction().AddHook(next => (a1) => CItemDefuserActivateThinkPipeline(a1, () => next()(a1)));
        return CItemDefuserActivateThinkHookGuid;
    }

    internal static Guid UnhookCItemDefuserActivateThink()
    {
        CItemDefuserActivateThinkGetUnmanagedFunction().RemoveHook(CItemDefuserActivateThinkHookGuid);
        return Guid.Empty;
    }

    private static void CItemDefuserActivateThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CItemDefuser>(a1);

            var preCtx = new CItemDefuserActivateThinkPreContext { SchemaObject = schemaObject };
            InvokeCItemDefuserActivateThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CItemDefuserActivateThinkPostContext { SchemaObject = schemaObject };
            InvokeCItemDefuserActivateThinkPost(ref postCtx);
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

    internal static void InvokeCItemDefuserActivateThink(nint a1)
    {
        CItemDefuserActivateThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCItemDefuserActivateThinkPre(ref CItemDefuserActivateThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCItemDefuserActivateThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCItemDefuserActivateThinkPost(ref CItemDefuserActivateThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCItemDefuserActivateThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CItemDefuserActivateThinkHook : ICItemDefuserActivateThinkHook
{
    private event OnCItemDefuserActivateThinkPreDelegate? _Pre;
    private event OnCItemDefuserActivateThinkPostDelegate? _Post;

    public event OnCItemDefuserActivateThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CItemDefuserActivateThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemDefuserActivateThink);
            }
        }
    }

    public event OnCItemDefuserActivateThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CItemDefuserActivateThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemDefuserActivateThink);
            }
        }
    }

    public void InvokePre(ref CItemDefuserActivateThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CItemDefuserActivateThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemDefuserActivateThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemDefuserActivateThink);
        }
    }

    public void Invoke(CItemDefuser schemaObject) => DatamapHooksPublisher.InvokeCItemDefuserActivateThink(schemaObject.Address);
}