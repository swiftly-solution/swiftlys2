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
    private delegate void CChickenChickenThinkDelegate(nint a1);

    private static IUnmanagedFunction<CChickenChickenThinkDelegate>? CChickenChickenThinkUnmanagedFunction;
    private static Guid CChickenChickenThinkHookGuid;

    private static IUnmanagedFunction<CChickenChickenThinkDelegate> CChickenChickenThinkGetUnmanagedFunction()
    {
        if (CChickenChickenThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CChicken", "CChickenChickenThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CChicken::CChickenChickenThink.");
            }
            CChickenChickenThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CChickenChickenThinkDelegate>(address);
        }
        return CChickenChickenThinkUnmanagedFunction;
    }

    internal static Guid HookCChickenChickenThink()
    {
        CChickenChickenThinkHookGuid = CChickenChickenThinkGetUnmanagedFunction().AddHook(next => (a1) => CChickenChickenThinkPipeline(a1, () => next()(a1)));
        return CChickenChickenThinkHookGuid;
    }

    internal static Guid UnhookCChickenChickenThink()
    {
        CChickenChickenThinkGetUnmanagedFunction().RemoveHook(CChickenChickenThinkHookGuid);
        return Guid.Empty;
    }

    private static void CChickenChickenThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CChicken>(a1);

            var preCtx = new CChickenChickenThinkPreContext { SchemaObject = schemaObject };
            InvokeCChickenChickenThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CChickenChickenThinkPostContext { SchemaObject = schemaObject };
            InvokeCChickenChickenThinkPost(ref postCtx);
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

    internal static void InvokeCChickenChickenThink(nint a1)
    {
        CChickenChickenThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCChickenChickenThinkPre(ref CChickenChickenThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCChickenChickenThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCChickenChickenThinkPost(ref CChickenChickenThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCChickenChickenThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CChickenChickenThinkHook : ICChickenChickenThinkHook
{
    private event OnCChickenChickenThinkPreDelegate? _Pre;
    private event OnCChickenChickenThinkPostDelegate? _Post;

    public event OnCChickenChickenThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CChickenChickenThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CChickenChickenThink);
            }
        }
    }

    public event OnCChickenChickenThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CChickenChickenThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CChickenChickenThink);
            }
        }
    }

    public void InvokePre(ref CChickenChickenThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CChickenChickenThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CChickenChickenThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CChickenChickenThink);
        }
    }

    public void Invoke(CChicken schemaObject) => DatamapHooksPublisher.InvokeCChickenChickenThink(schemaObject.Address);
}