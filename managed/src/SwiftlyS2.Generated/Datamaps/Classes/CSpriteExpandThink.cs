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
    private delegate void CSpriteExpandThinkDelegate(nint a1);

    private static IUnmanagedFunction<CSpriteExpandThinkDelegate>? CSpriteExpandThinkUnmanagedFunction;
    private static Guid CSpriteExpandThinkHookGuid;

    private static IUnmanagedFunction<CSpriteExpandThinkDelegate> CSpriteExpandThinkGetUnmanagedFunction()
    {
        if (CSpriteExpandThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CSprite", "CSpriteExpandThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CSprite::CSpriteExpandThink.");
            }
            CSpriteExpandThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CSpriteExpandThinkDelegate>(address);
        }
        return CSpriteExpandThinkUnmanagedFunction;
    }

    internal static Guid HookCSpriteExpandThink()
    {
        CSpriteExpandThinkHookGuid = CSpriteExpandThinkGetUnmanagedFunction().AddHook(next => (a1) => CSpriteExpandThinkPipeline(a1, () => next()(a1)));
        return CSpriteExpandThinkHookGuid;
    }

    internal static Guid UnhookCSpriteExpandThink()
    {
        CSpriteExpandThinkGetUnmanagedFunction().RemoveHook(CSpriteExpandThinkHookGuid);
        return Guid.Empty;
    }

    private static void CSpriteExpandThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CSprite>(a1);

            var preCtx = new CSpriteExpandThinkPreContext { SchemaObject = schemaObject };
            InvokeCSpriteExpandThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CSpriteExpandThinkPostContext { SchemaObject = schemaObject };
            InvokeCSpriteExpandThinkPost(ref postCtx);
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

    internal static void InvokeCSpriteExpandThink(nint a1)
    {
        CSpriteExpandThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCSpriteExpandThinkPre(ref CSpriteExpandThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSpriteExpandThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCSpriteExpandThinkPost(ref CSpriteExpandThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSpriteExpandThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CSpriteExpandThinkHook : ICSpriteExpandThinkHook
{
    private event OnCSpriteExpandThinkPreDelegate? _Pre;
    private event OnCSpriteExpandThinkPostDelegate? _Post;

    public event OnCSpriteExpandThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSpriteExpandThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSpriteExpandThink);
            }
        }
    }

    public event OnCSpriteExpandThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSpriteExpandThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSpriteExpandThink);
            }
        }
    }

    public void InvokePre(ref CSpriteExpandThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CSpriteExpandThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSpriteExpandThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSpriteExpandThink);
        }
    }

    public void Invoke(CSprite schemaObject) => DatamapHooksPublisher.InvokeCSpriteExpandThink(schemaObject.Address);
}