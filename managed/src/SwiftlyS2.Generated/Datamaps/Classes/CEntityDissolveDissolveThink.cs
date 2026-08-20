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
    private delegate void CEntityDissolveDissolveThinkDelegate(nint a1);

    private static IUnmanagedFunction<CEntityDissolveDissolveThinkDelegate>? CEntityDissolveDissolveThinkUnmanagedFunction;
    private static Guid CEntityDissolveDissolveThinkHookGuid;

    private static IUnmanagedFunction<CEntityDissolveDissolveThinkDelegate> CEntityDissolveDissolveThinkGetUnmanagedFunction()
    {
        if (CEntityDissolveDissolveThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CEntityDissolve", "CEntityDissolveDissolveThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CEntityDissolve::CEntityDissolveDissolveThink.");
            }
            CEntityDissolveDissolveThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CEntityDissolveDissolveThinkDelegate>(address);
        }
        return CEntityDissolveDissolveThinkUnmanagedFunction;
    }

    internal static Guid HookCEntityDissolveDissolveThink()
    {
        CEntityDissolveDissolveThinkHookGuid = CEntityDissolveDissolveThinkGetUnmanagedFunction().AddHook(next => (a1) => CEntityDissolveDissolveThinkPipeline(a1, () => next()(a1)));
        return CEntityDissolveDissolveThinkHookGuid;
    }

    internal static Guid UnhookCEntityDissolveDissolveThink()
    {
        CEntityDissolveDissolveThinkGetUnmanagedFunction().RemoveHook(CEntityDissolveDissolveThinkHookGuid);
        return Guid.Empty;
    }

    private static void CEntityDissolveDissolveThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CEntityDissolve>(a1);

            var preCtx = new CEntityDissolveDissolveThinkPreContext { SchemaObject = schemaObject };
            InvokeCEntityDissolveDissolveThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CEntityDissolveDissolveThinkPostContext { SchemaObject = schemaObject };
            InvokeCEntityDissolveDissolveThinkPost(ref postCtx);
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

    internal static void InvokeCEntityDissolveDissolveThink(nint a1)
    {
        CEntityDissolveDissolveThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCEntityDissolveDissolveThinkPre(ref CEntityDissolveDissolveThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCEntityDissolveDissolveThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCEntityDissolveDissolveThinkPost(ref CEntityDissolveDissolveThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCEntityDissolveDissolveThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CEntityDissolveDissolveThinkHook : ICEntityDissolveDissolveThinkHook
{
    private event OnCEntityDissolveDissolveThinkPreDelegate? _Pre;
    private event OnCEntityDissolveDissolveThinkPostDelegate? _Post;

    public event OnCEntityDissolveDissolveThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CEntityDissolveDissolveThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEntityDissolveDissolveThink);
            }
        }
    }

    public event OnCEntityDissolveDissolveThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CEntityDissolveDissolveThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEntityDissolveDissolveThink);
            }
        }
    }

    public void InvokePre(ref CEntityDissolveDissolveThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CEntityDissolveDissolveThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEntityDissolveDissolveThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEntityDissolveDissolveThink);
        }
    }

    public void Invoke(CEntityDissolve schemaObject) => DatamapHooksPublisher.InvokeCEntityDissolveDissolveThink(schemaObject.Address);
}