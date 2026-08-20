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
    private delegate void CItemMaterializeDelegate(nint a1);

    private static IUnmanagedFunction<CItemMaterializeDelegate>? CItemMaterializeUnmanagedFunction;
    private static Guid CItemMaterializeHookGuid;

    private static IUnmanagedFunction<CItemMaterializeDelegate> CItemMaterializeGetUnmanagedFunction()
    {
        if (CItemMaterializeUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CItem", "CItemMaterialize");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CItem::CItemMaterialize.");
            }
            CItemMaterializeUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CItemMaterializeDelegate>(address);
        }
        return CItemMaterializeUnmanagedFunction;
    }

    internal static Guid HookCItemMaterialize()
    {
        CItemMaterializeHookGuid = CItemMaterializeGetUnmanagedFunction().AddHook(next => (a1) => CItemMaterializePipeline(a1, () => next()(a1)));
        return CItemMaterializeHookGuid;
    }

    internal static Guid UnhookCItemMaterialize()
    {
        CItemMaterializeGetUnmanagedFunction().RemoveHook(CItemMaterializeHookGuid);
        return Guid.Empty;
    }

    private static void CItemMaterializePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CItem>(a1);

            var preCtx = new CItemMaterializePreContext { SchemaObject = schemaObject };
            InvokeCItemMaterializePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CItemMaterializePostContext { SchemaObject = schemaObject };
            InvokeCItemMaterializePost(ref postCtx);
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

    internal static void InvokeCItemMaterialize(nint a1)
    {
        CItemMaterializeGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCItemMaterializePre(ref CItemMaterializePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCItemMaterializePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCItemMaterializePost(ref CItemMaterializePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCItemMaterializePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CItemMaterializeHook : ICItemMaterializeHook
{
    private event OnCItemMaterializePreDelegate? _Pre;
    private event OnCItemMaterializePostDelegate? _Post;

    public event OnCItemMaterializePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CItemMaterialize);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemMaterialize);
            }
        }
    }

    public event OnCItemMaterializePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CItemMaterialize);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemMaterialize);
            }
        }
    }

    public void InvokePre(ref CItemMaterializePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CItemMaterializePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemMaterialize);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemMaterialize);
        }
    }

    public void Invoke(CItem schemaObject) => DatamapHooksPublisher.InvokeCItemMaterialize(schemaObject.Address);
}