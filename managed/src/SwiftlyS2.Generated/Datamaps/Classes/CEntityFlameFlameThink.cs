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
    private delegate void CEntityFlameFlameThinkDelegate(nint a1);

    private static IUnmanagedFunction<CEntityFlameFlameThinkDelegate>? CEntityFlameFlameThinkUnmanagedFunction;
    private static Guid CEntityFlameFlameThinkHookGuid;

    private static IUnmanagedFunction<CEntityFlameFlameThinkDelegate> CEntityFlameFlameThinkGetUnmanagedFunction()
    {
        if (CEntityFlameFlameThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CEntityFlame", "CEntityFlameFlameThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CEntityFlame::CEntityFlameFlameThink.");
            }
            CEntityFlameFlameThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CEntityFlameFlameThinkDelegate>(address);
        }
        return CEntityFlameFlameThinkUnmanagedFunction;
    }

    internal static Guid HookCEntityFlameFlameThink()
    {
        CEntityFlameFlameThinkHookGuid = CEntityFlameFlameThinkGetUnmanagedFunction().AddHook(next => (a1) => CEntityFlameFlameThinkPipeline(a1, () => next()(a1)));
        return CEntityFlameFlameThinkHookGuid;
    }

    internal static Guid UnhookCEntityFlameFlameThink()
    {
        CEntityFlameFlameThinkGetUnmanagedFunction().RemoveHook(CEntityFlameFlameThinkHookGuid);
        return Guid.Empty;
    }

    private static void CEntityFlameFlameThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CEntityFlame>(a1);

            var preCtx = new CEntityFlameFlameThinkPreContext { SchemaObject = schemaObject };
            InvokeCEntityFlameFlameThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CEntityFlameFlameThinkPostContext { SchemaObject = schemaObject };
            InvokeCEntityFlameFlameThinkPost(ref postCtx);
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

    internal static void InvokeCEntityFlameFlameThink(nint a1)
    {
        CEntityFlameFlameThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCEntityFlameFlameThinkPre(ref CEntityFlameFlameThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCEntityFlameFlameThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCEntityFlameFlameThinkPost(ref CEntityFlameFlameThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCEntityFlameFlameThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CEntityFlameFlameThinkHook : ICEntityFlameFlameThinkHook
{
    private event OnCEntityFlameFlameThinkPreDelegate? _Pre;
    private event OnCEntityFlameFlameThinkPostDelegate? _Post;

    public event OnCEntityFlameFlameThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CEntityFlameFlameThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEntityFlameFlameThink);
            }
        }
    }

    public event OnCEntityFlameFlameThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CEntityFlameFlameThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEntityFlameFlameThink);
            }
        }
    }

    public void InvokePre(ref CEntityFlameFlameThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CEntityFlameFlameThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEntityFlameFlameThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEntityFlameFlameThink);
        }
    }

    public void Invoke(CEntityFlame schemaObject) => DatamapHooksPublisher.InvokeCEntityFlameFlameThink(schemaObject.Address);
}