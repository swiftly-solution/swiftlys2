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
    private delegate void CSpriteBeginFadeOutThinkDelegate(nint a1);

    private static IUnmanagedFunction<CSpriteBeginFadeOutThinkDelegate>? CSpriteBeginFadeOutThinkUnmanagedFunction;
    private static Guid CSpriteBeginFadeOutThinkHookGuid;

    private static IUnmanagedFunction<CSpriteBeginFadeOutThinkDelegate> CSpriteBeginFadeOutThinkGetUnmanagedFunction()
    {
        if (CSpriteBeginFadeOutThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CSprite", "CSpriteBeginFadeOutThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CSprite::CSpriteBeginFadeOutThink.");
            }
            CSpriteBeginFadeOutThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CSpriteBeginFadeOutThinkDelegate>(address);
        }
        return CSpriteBeginFadeOutThinkUnmanagedFunction;
    }

    internal static Guid HookCSpriteBeginFadeOutThink()
    {
        CSpriteBeginFadeOutThinkHookGuid = CSpriteBeginFadeOutThinkGetUnmanagedFunction().AddHook(next => (a1) => CSpriteBeginFadeOutThinkPipeline(a1, () => next()(a1)));
        return CSpriteBeginFadeOutThinkHookGuid;
    }

    internal static Guid UnhookCSpriteBeginFadeOutThink()
    {
        CSpriteBeginFadeOutThinkGetUnmanagedFunction().RemoveHook(CSpriteBeginFadeOutThinkHookGuid);
        return Guid.Empty;
    }

    private static void CSpriteBeginFadeOutThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CSprite>(a1);

            var preCtx = new CSpriteBeginFadeOutThinkPreContext { SchemaObject = schemaObject };
            InvokeCSpriteBeginFadeOutThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CSpriteBeginFadeOutThinkPostContext { SchemaObject = schemaObject };
            InvokeCSpriteBeginFadeOutThinkPost(ref postCtx);
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

    internal static void InvokeCSpriteBeginFadeOutThink(nint a1)
    {
        CSpriteBeginFadeOutThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCSpriteBeginFadeOutThinkPre(ref CSpriteBeginFadeOutThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSpriteBeginFadeOutThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCSpriteBeginFadeOutThinkPost(ref CSpriteBeginFadeOutThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSpriteBeginFadeOutThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CSpriteBeginFadeOutThinkHook : ICSpriteBeginFadeOutThinkHook
{
    private event OnCSpriteBeginFadeOutThinkPreDelegate? _Pre;
    private event OnCSpriteBeginFadeOutThinkPostDelegate? _Post;

    public event OnCSpriteBeginFadeOutThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSpriteBeginFadeOutThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSpriteBeginFadeOutThink);
            }
        }
    }

    public event OnCSpriteBeginFadeOutThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSpriteBeginFadeOutThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSpriteBeginFadeOutThink);
            }
        }
    }

    public void InvokePre(ref CSpriteBeginFadeOutThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CSpriteBeginFadeOutThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSpriteBeginFadeOutThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSpriteBeginFadeOutThink);
        }
    }

    public void Invoke(CSprite schemaObject) => DatamapHooksPublisher.InvokeCSpriteBeginFadeOutThink(schemaObject.Address);
}