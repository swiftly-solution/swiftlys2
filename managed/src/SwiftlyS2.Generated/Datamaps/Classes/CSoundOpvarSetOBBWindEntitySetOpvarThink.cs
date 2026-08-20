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
    private delegate void CSoundOpvarSetOBBWindEntitySetOpvarThinkDelegate(nint a1);

    private static IUnmanagedFunction<CSoundOpvarSetOBBWindEntitySetOpvarThinkDelegate>? CSoundOpvarSetOBBWindEntitySetOpvarThinkUnmanagedFunction;
    private static Guid CSoundOpvarSetOBBWindEntitySetOpvarThinkHookGuid;

    private static IUnmanagedFunction<CSoundOpvarSetOBBWindEntitySetOpvarThinkDelegate> CSoundOpvarSetOBBWindEntitySetOpvarThinkGetUnmanagedFunction()
    {
        if (CSoundOpvarSetOBBWindEntitySetOpvarThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CSoundOpvarSetOBBWindEntity", "CSoundOpvarSetOBBWindEntitySetOpvarThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CSoundOpvarSetOBBWindEntity::CSoundOpvarSetOBBWindEntitySetOpvarThink.");
            }
            CSoundOpvarSetOBBWindEntitySetOpvarThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CSoundOpvarSetOBBWindEntitySetOpvarThinkDelegate>(address);
        }
        return CSoundOpvarSetOBBWindEntitySetOpvarThinkUnmanagedFunction;
    }

    internal static Guid HookCSoundOpvarSetOBBWindEntitySetOpvarThink()
    {
        CSoundOpvarSetOBBWindEntitySetOpvarThinkHookGuid = CSoundOpvarSetOBBWindEntitySetOpvarThinkGetUnmanagedFunction().AddHook(next => (a1) => CSoundOpvarSetOBBWindEntitySetOpvarThinkPipeline(a1, () => next()(a1)));
        return CSoundOpvarSetOBBWindEntitySetOpvarThinkHookGuid;
    }

    internal static Guid UnhookCSoundOpvarSetOBBWindEntitySetOpvarThink()
    {
        CSoundOpvarSetOBBWindEntitySetOpvarThinkGetUnmanagedFunction().RemoveHook(CSoundOpvarSetOBBWindEntitySetOpvarThinkHookGuid);
        return Guid.Empty;
    }

    private static void CSoundOpvarSetOBBWindEntitySetOpvarThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CSoundOpvarSetOBBWindEntity>(a1);

            var preCtx = new CSoundOpvarSetOBBWindEntitySetOpvarThinkPreContext { SchemaObject = schemaObject };
            InvokeCSoundOpvarSetOBBWindEntitySetOpvarThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CSoundOpvarSetOBBWindEntitySetOpvarThinkPostContext { SchemaObject = schemaObject };
            InvokeCSoundOpvarSetOBBWindEntitySetOpvarThinkPost(ref postCtx);
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

    internal static void InvokeCSoundOpvarSetOBBWindEntitySetOpvarThink(nint a1)
    {
        CSoundOpvarSetOBBWindEntitySetOpvarThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCSoundOpvarSetOBBWindEntitySetOpvarThinkPre(ref CSoundOpvarSetOBBWindEntitySetOpvarThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSoundOpvarSetOBBWindEntitySetOpvarThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCSoundOpvarSetOBBWindEntitySetOpvarThinkPost(ref CSoundOpvarSetOBBWindEntitySetOpvarThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSoundOpvarSetOBBWindEntitySetOpvarThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CSoundOpvarSetOBBWindEntitySetOpvarThinkHook : ICSoundOpvarSetOBBWindEntitySetOpvarThinkHook
{
    private event OnCSoundOpvarSetOBBWindEntitySetOpvarThinkPreDelegate? _Pre;
    private event OnCSoundOpvarSetOBBWindEntitySetOpvarThinkPostDelegate? _Post;

    public event OnCSoundOpvarSetOBBWindEntitySetOpvarThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSoundOpvarSetOBBWindEntitySetOpvarThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundOpvarSetOBBWindEntitySetOpvarThink);
            }
        }
    }

    public event OnCSoundOpvarSetOBBWindEntitySetOpvarThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSoundOpvarSetOBBWindEntitySetOpvarThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundOpvarSetOBBWindEntitySetOpvarThink);
            }
        }
    }

    public void InvokePre(ref CSoundOpvarSetOBBWindEntitySetOpvarThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CSoundOpvarSetOBBWindEntitySetOpvarThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundOpvarSetOBBWindEntitySetOpvarThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundOpvarSetOBBWindEntitySetOpvarThink);
        }
    }

    public void Invoke(CSoundOpvarSetOBBWindEntity schemaObject) => DatamapHooksPublisher.InvokeCSoundOpvarSetOBBWindEntitySetOpvarThink(schemaObject.Address);
}