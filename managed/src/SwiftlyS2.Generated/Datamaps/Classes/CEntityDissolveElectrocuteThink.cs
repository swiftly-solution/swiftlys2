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
    private delegate void CEntityDissolveElectrocuteThinkDelegate(nint a1);

    private static IUnmanagedFunction<CEntityDissolveElectrocuteThinkDelegate>? CEntityDissolveElectrocuteThinkUnmanagedFunction;
    private static Guid CEntityDissolveElectrocuteThinkHookGuid;

    private static IUnmanagedFunction<CEntityDissolveElectrocuteThinkDelegate> CEntityDissolveElectrocuteThinkGetUnmanagedFunction()
    {
        if (CEntityDissolveElectrocuteThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CEntityDissolve", "CEntityDissolveElectrocuteThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CEntityDissolve::CEntityDissolveElectrocuteThink.");
            }
            CEntityDissolveElectrocuteThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CEntityDissolveElectrocuteThinkDelegate>(address);
        }
        return CEntityDissolveElectrocuteThinkUnmanagedFunction;
    }

    internal static Guid HookCEntityDissolveElectrocuteThink()
    {
        CEntityDissolveElectrocuteThinkHookGuid = CEntityDissolveElectrocuteThinkGetUnmanagedFunction().AddHook(next => (a1) => CEntityDissolveElectrocuteThinkPipeline(a1, () => next()(a1)));
        return CEntityDissolveElectrocuteThinkHookGuid;
    }

    internal static Guid UnhookCEntityDissolveElectrocuteThink()
    {
        CEntityDissolveElectrocuteThinkGetUnmanagedFunction().RemoveHook(CEntityDissolveElectrocuteThinkHookGuid);
        return Guid.Empty;
    }

    private static void CEntityDissolveElectrocuteThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CEntityDissolve>(a1);

            var preCtx = new CEntityDissolveElectrocuteThinkPreContext { SchemaObject = schemaObject };
            InvokeCEntityDissolveElectrocuteThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CEntityDissolveElectrocuteThinkPostContext { SchemaObject = schemaObject };
            InvokeCEntityDissolveElectrocuteThinkPost(ref postCtx);
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

    internal static void InvokeCEntityDissolveElectrocuteThink(nint a1)
    {
        CEntityDissolveElectrocuteThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCEntityDissolveElectrocuteThinkPre(ref CEntityDissolveElectrocuteThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCEntityDissolveElectrocuteThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCEntityDissolveElectrocuteThinkPost(ref CEntityDissolveElectrocuteThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCEntityDissolveElectrocuteThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CEntityDissolveElectrocuteThinkHook : ICEntityDissolveElectrocuteThinkHook
{
    private event OnCEntityDissolveElectrocuteThinkPreDelegate? _Pre;
    private event OnCEntityDissolveElectrocuteThinkPostDelegate? _Post;

    public event OnCEntityDissolveElectrocuteThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CEntityDissolveElectrocuteThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEntityDissolveElectrocuteThink);
            }
        }
    }

    public event OnCEntityDissolveElectrocuteThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CEntityDissolveElectrocuteThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEntityDissolveElectrocuteThink);
            }
        }
    }

    public void InvokePre(ref CEntityDissolveElectrocuteThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CEntityDissolveElectrocuteThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEntityDissolveElectrocuteThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEntityDissolveElectrocuteThink);
        }
    }

    public void Invoke(CEntityDissolve schemaObject) => DatamapHooksPublisher.InvokeCEntityDissolveElectrocuteThink(schemaObject.Address);
}