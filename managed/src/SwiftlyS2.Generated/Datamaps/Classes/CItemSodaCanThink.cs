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
    private delegate void CItemSodaCanThinkDelegate(nint a1);

    private static IUnmanagedFunction<CItemSodaCanThinkDelegate>? CItemSodaCanThinkUnmanagedFunction;
    private static Guid CItemSodaCanThinkHookGuid;

    private static IUnmanagedFunction<CItemSodaCanThinkDelegate> CItemSodaCanThinkGetUnmanagedFunction()
    {
        if (CItemSodaCanThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CItemSoda", "CItemSodaCanThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CItemSoda::CItemSodaCanThink.");
            }
            CItemSodaCanThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CItemSodaCanThinkDelegate>(address);
        }
        return CItemSodaCanThinkUnmanagedFunction;
    }

    internal static Guid HookCItemSodaCanThink()
    {
        CItemSodaCanThinkHookGuid = CItemSodaCanThinkGetUnmanagedFunction().AddHook(next => (a1) => CItemSodaCanThinkPipeline(a1, () => next()(a1)));
        return CItemSodaCanThinkHookGuid;
    }

    internal static Guid UnhookCItemSodaCanThink()
    {
        CItemSodaCanThinkGetUnmanagedFunction().RemoveHook(CItemSodaCanThinkHookGuid);
        return Guid.Empty;
    }

    private static void CItemSodaCanThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CItemSoda>(a1);

            var preCtx = new CItemSodaCanThinkPreContext { SchemaObject = schemaObject };
            InvokeCItemSodaCanThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CItemSodaCanThinkPostContext { SchemaObject = schemaObject };
            InvokeCItemSodaCanThinkPost(ref postCtx);
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

    internal static void InvokeCItemSodaCanThink(nint a1)
    {
        CItemSodaCanThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCItemSodaCanThinkPre(ref CItemSodaCanThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCItemSodaCanThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCItemSodaCanThinkPost(ref CItemSodaCanThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCItemSodaCanThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CItemSodaCanThinkHook : ICItemSodaCanThinkHook
{
    private event OnCItemSodaCanThinkPreDelegate? _Pre;
    private event OnCItemSodaCanThinkPostDelegate? _Post;

    public event OnCItemSodaCanThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CItemSodaCanThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemSodaCanThink);
            }
        }
    }

    public event OnCItemSodaCanThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CItemSodaCanThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemSodaCanThink);
            }
        }
    }

    public void InvokePre(ref CItemSodaCanThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CItemSodaCanThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemSodaCanThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemSodaCanThink);
        }
    }

    public void Invoke(CItemSoda schemaObject) => DatamapHooksPublisher.InvokeCItemSodaCanThink(schemaObject.Address);
}