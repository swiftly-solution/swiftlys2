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
    private delegate void CItemComeToRestDelegate(nint a1);

    private static IUnmanagedFunction<CItemComeToRestDelegate>? CItemComeToRestUnmanagedFunction;
    private static Guid CItemComeToRestHookGuid;

    private static IUnmanagedFunction<CItemComeToRestDelegate> CItemComeToRestGetUnmanagedFunction()
    {
        if (CItemComeToRestUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CItem", "CItemComeToRest");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CItem::CItemComeToRest.");
            }
            CItemComeToRestUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CItemComeToRestDelegate>(address);
        }
        return CItemComeToRestUnmanagedFunction;
    }

    internal static Guid HookCItemComeToRest()
    {
        CItemComeToRestHookGuid = CItemComeToRestGetUnmanagedFunction().AddHook(next => (a1) => CItemComeToRestPipeline(a1, () => next()(a1)));
        return CItemComeToRestHookGuid;
    }

    internal static Guid UnhookCItemComeToRest()
    {
        CItemComeToRestGetUnmanagedFunction().RemoveHook(CItemComeToRestHookGuid);
        return Guid.Empty;
    }

    private static void CItemComeToRestPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CItem>(a1);

            var preCtx = new CItemComeToRestPreContext { SchemaObject = schemaObject };
            InvokeCItemComeToRestPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CItemComeToRestPostContext { SchemaObject = schemaObject };
            InvokeCItemComeToRestPost(ref postCtx);
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

    internal static void InvokeCItemComeToRest(nint a1)
    {
        CItemComeToRestGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCItemComeToRestPre(ref CItemComeToRestPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCItemComeToRestPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCItemComeToRestPost(ref CItemComeToRestPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCItemComeToRestPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CItemComeToRestHook : ICItemComeToRestHook
{
    private event OnCItemComeToRestPreDelegate? _Pre;
    private event OnCItemComeToRestPostDelegate? _Post;

    public event OnCItemComeToRestPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CItemComeToRest);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemComeToRest);
            }
        }
    }

    public event OnCItemComeToRestPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CItemComeToRest);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemComeToRest);
            }
        }
    }

    public void InvokePre(ref CItemComeToRestPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CItemComeToRestPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemComeToRest);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemComeToRest);
        }
    }

    public void Invoke(CItem schemaObject) => DatamapHooksPublisher.InvokeCItemComeToRest(schemaObject.Address);
}