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
    private delegate void CSpriteAnimateThinkDelegate(nint a1);

    private static IUnmanagedFunction<CSpriteAnimateThinkDelegate>? CSpriteAnimateThinkUnmanagedFunction;
    private static Guid CSpriteAnimateThinkHookGuid;

    private static IUnmanagedFunction<CSpriteAnimateThinkDelegate> CSpriteAnimateThinkGetUnmanagedFunction()
    {
        if (CSpriteAnimateThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CSprite", "CSpriteAnimateThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CSprite::CSpriteAnimateThink.");
            }
            CSpriteAnimateThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CSpriteAnimateThinkDelegate>(address);
        }
        return CSpriteAnimateThinkUnmanagedFunction;
    }

    internal static Guid HookCSpriteAnimateThink()
    {
        CSpriteAnimateThinkHookGuid = CSpriteAnimateThinkGetUnmanagedFunction().AddHook(next => (a1) => CSpriteAnimateThinkPipeline(a1, () => next()(a1)));
        return CSpriteAnimateThinkHookGuid;
    }

    internal static Guid UnhookCSpriteAnimateThink()
    {
        CSpriteAnimateThinkGetUnmanagedFunction().RemoveHook(CSpriteAnimateThinkHookGuid);
        return Guid.Empty;
    }

    private static void CSpriteAnimateThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CSprite>(a1);

            var preCtx = new CSpriteAnimateThinkPreContext { SchemaObject = schemaObject };
            InvokeCSpriteAnimateThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CSpriteAnimateThinkPostContext { SchemaObject = schemaObject };
            InvokeCSpriteAnimateThinkPost(ref postCtx);
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

    internal static void InvokeCSpriteAnimateThink(nint a1)
    {
        CSpriteAnimateThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCSpriteAnimateThinkPre(ref CSpriteAnimateThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSpriteAnimateThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCSpriteAnimateThinkPost(ref CSpriteAnimateThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSpriteAnimateThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CSpriteAnimateThinkHook : ICSpriteAnimateThinkHook
{
    private event OnCSpriteAnimateThinkPreDelegate? _Pre;
    private event OnCSpriteAnimateThinkPostDelegate? _Post;

    public event OnCSpriteAnimateThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSpriteAnimateThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSpriteAnimateThink);
            }
        }
    }

    public event OnCSpriteAnimateThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSpriteAnimateThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSpriteAnimateThink);
            }
        }
    }

    public void InvokePre(ref CSpriteAnimateThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CSpriteAnimateThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSpriteAnimateThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSpriteAnimateThink);
        }
    }

    public void Invoke(CSprite schemaObject) => DatamapHooksPublisher.InvokeCSpriteAnimateThink(schemaObject.Address);
}