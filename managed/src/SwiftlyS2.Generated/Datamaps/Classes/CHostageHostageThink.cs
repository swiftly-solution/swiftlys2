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
    private delegate void CHostageHostageThinkDelegate(nint a1);

    private static IUnmanagedFunction<CHostageHostageThinkDelegate>? CHostageHostageThinkUnmanagedFunction;
    private static Guid CHostageHostageThinkHookGuid;

    private static IUnmanagedFunction<CHostageHostageThinkDelegate> CHostageHostageThinkGetUnmanagedFunction()
    {
        if (CHostageHostageThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CHostage", "CHostageHostageThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CHostage::CHostageHostageThink.");
            }
            CHostageHostageThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CHostageHostageThinkDelegate>(address);
        }
        return CHostageHostageThinkUnmanagedFunction;
    }

    internal static Guid HookCHostageHostageThink()
    {
        CHostageHostageThinkHookGuid = CHostageHostageThinkGetUnmanagedFunction().AddHook(next => (a1) => CHostageHostageThinkPipeline(a1, () => next()(a1)));
        return CHostageHostageThinkHookGuid;
    }

    internal static Guid UnhookCHostageHostageThink()
    {
        CHostageHostageThinkGetUnmanagedFunction().RemoveHook(CHostageHostageThinkHookGuid);
        return Guid.Empty;
    }

    private static void CHostageHostageThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CHostage>(a1);

            var preCtx = new CHostageHostageThinkPreContext { SchemaObject = schemaObject };
            InvokeCHostageHostageThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CHostageHostageThinkPostContext { SchemaObject = schemaObject };
            InvokeCHostageHostageThinkPost(ref postCtx);
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

    internal static void InvokeCHostageHostageThink(nint a1)
    {
        CHostageHostageThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCHostageHostageThinkPre(ref CHostageHostageThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCHostageHostageThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCHostageHostageThinkPost(ref CHostageHostageThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCHostageHostageThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CHostageHostageThinkHook : ICHostageHostageThinkHook
{
    private event OnCHostageHostageThinkPreDelegate? _Pre;
    private event OnCHostageHostageThinkPostDelegate? _Post;

    public event OnCHostageHostageThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CHostageHostageThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CHostageHostageThink);
            }
        }
    }

    public event OnCHostageHostageThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CHostageHostageThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CHostageHostageThink);
            }
        }
    }

    public void InvokePre(ref CHostageHostageThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CHostageHostageThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CHostageHostageThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CHostageHostageThink);
        }
    }

    public void Invoke(CHostage schemaObject) => DatamapHooksPublisher.InvokeCHostageHostageThink(schemaObject.Address);
}