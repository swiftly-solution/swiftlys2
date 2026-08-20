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
    private delegate void CSpriteAnimateUntilDeadDelegate(nint a1);

    private static IUnmanagedFunction<CSpriteAnimateUntilDeadDelegate>? CSpriteAnimateUntilDeadUnmanagedFunction;
    private static Guid CSpriteAnimateUntilDeadHookGuid;

    private static IUnmanagedFunction<CSpriteAnimateUntilDeadDelegate> CSpriteAnimateUntilDeadGetUnmanagedFunction()
    {
        if (CSpriteAnimateUntilDeadUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CSprite", "CSpriteAnimateUntilDead");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CSprite::CSpriteAnimateUntilDead.");
            }
            CSpriteAnimateUntilDeadUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CSpriteAnimateUntilDeadDelegate>(address);
        }
        return CSpriteAnimateUntilDeadUnmanagedFunction;
    }

    internal static Guid HookCSpriteAnimateUntilDead()
    {
        CSpriteAnimateUntilDeadHookGuid = CSpriteAnimateUntilDeadGetUnmanagedFunction().AddHook(next => (a1) => CSpriteAnimateUntilDeadPipeline(a1, () => next()(a1)));
        return CSpriteAnimateUntilDeadHookGuid;
    }

    internal static Guid UnhookCSpriteAnimateUntilDead()
    {
        CSpriteAnimateUntilDeadGetUnmanagedFunction().RemoveHook(CSpriteAnimateUntilDeadHookGuid);
        return Guid.Empty;
    }

    private static void CSpriteAnimateUntilDeadPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CSprite>(a1);

            var preCtx = new CSpriteAnimateUntilDeadPreContext { SchemaObject = schemaObject };
            InvokeCSpriteAnimateUntilDeadPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CSpriteAnimateUntilDeadPostContext { SchemaObject = schemaObject };
            InvokeCSpriteAnimateUntilDeadPost(ref postCtx);
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

    internal static void InvokeCSpriteAnimateUntilDead(nint a1)
    {
        CSpriteAnimateUntilDeadGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCSpriteAnimateUntilDeadPre(ref CSpriteAnimateUntilDeadPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSpriteAnimateUntilDeadPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCSpriteAnimateUntilDeadPost(ref CSpriteAnimateUntilDeadPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSpriteAnimateUntilDeadPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CSpriteAnimateUntilDeadHook : ICSpriteAnimateUntilDeadHook
{
    private event OnCSpriteAnimateUntilDeadPreDelegate? _Pre;
    private event OnCSpriteAnimateUntilDeadPostDelegate? _Post;

    public event OnCSpriteAnimateUntilDeadPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSpriteAnimateUntilDead);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSpriteAnimateUntilDead);
            }
        }
    }

    public event OnCSpriteAnimateUntilDeadPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSpriteAnimateUntilDead);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSpriteAnimateUntilDead);
            }
        }
    }

    public void InvokePre(ref CSpriteAnimateUntilDeadPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CSpriteAnimateUntilDeadPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSpriteAnimateUntilDead);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSpriteAnimateUntilDead);
        }
    }

    public void Invoke(CSprite schemaObject) => DatamapHooksPublisher.InvokeCSpriteAnimateUntilDead(schemaObject.Address);
}