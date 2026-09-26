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
    private delegate void CCSPlayerResourceResourceThinkDelegate(nint a1);

    private static IUnmanagedFunction<CCSPlayerResourceResourceThinkDelegate>? CCSPlayerResourceResourceThinkUnmanagedFunction;
    private static Guid CCSPlayerResourceResourceThinkHookGuid;

    private static IUnmanagedFunction<CCSPlayerResourceResourceThinkDelegate> CCSPlayerResourceResourceThinkGetUnmanagedFunction()
    {
        if (CCSPlayerResourceResourceThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CCSPlayerResource", "CCSPlayerResourceResourceThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CCSPlayerResource::CCSPlayerResourceResourceThink.");
            }
            CCSPlayerResourceResourceThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerResourceResourceThinkDelegate>(address);
        }
        return CCSPlayerResourceResourceThinkUnmanagedFunction;
    }

    internal static Guid HookCCSPlayerResourceResourceThink()
    {
        CCSPlayerResourceResourceThinkHookGuid = CCSPlayerResourceResourceThinkGetUnmanagedFunction().AddHook(next => (a1) => CCSPlayerResourceResourceThinkPipeline(a1, () => next()(a1)));
        return CCSPlayerResourceResourceThinkHookGuid;
    }

    internal static Guid UnhookCCSPlayerResourceResourceThink()
    {
        CCSPlayerResourceResourceThinkGetUnmanagedFunction().RemoveHook(CCSPlayerResourceResourceThinkHookGuid);
        return Guid.Empty;
    }

    private static void CCSPlayerResourceResourceThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CCSPlayerResource>(a1);

            var preCtx = new CCSPlayerResourceResourceThinkPreContext { SchemaObject = schemaObject };
            InvokeCCSPlayerResourceResourceThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CCSPlayerResourceResourceThinkPostContext { SchemaObject = schemaObject };
            InvokeCCSPlayerResourceResourceThinkPost(ref postCtx);
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

    internal static void InvokeCCSPlayerResourceResourceThink(nint a1)
    {
        CCSPlayerResourceResourceThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCCSPlayerResourceResourceThinkPre(ref CCSPlayerResourceResourceThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCCSPlayerResourceResourceThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCCSPlayerResourceResourceThinkPost(ref CCSPlayerResourceResourceThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCCSPlayerResourceResourceThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CCSPlayerResourceResourceThinkHook : ICCSPlayerResourceResourceThinkHook
{
    private event OnCCSPlayerResourceResourceThinkPreDelegate? _Pre;
    private event OnCCSPlayerResourceResourceThinkPostDelegate? _Post;

    public event OnCCSPlayerResourceResourceThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CCSPlayerResourceResourceThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerResourceResourceThink);
            }
        }
    }

    public event OnCCSPlayerResourceResourceThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CCSPlayerResourceResourceThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerResourceResourceThink);
            }
        }
    }

    public void InvokePre(ref CCSPlayerResourceResourceThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CCSPlayerResourceResourceThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerResourceResourceThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerResourceResourceThink);
        }
    }

    public void Invoke(CCSPlayerResource schemaObject) => DatamapHooksPublisher.InvokeCCSPlayerResourceResourceThink(schemaObject.Address);
}