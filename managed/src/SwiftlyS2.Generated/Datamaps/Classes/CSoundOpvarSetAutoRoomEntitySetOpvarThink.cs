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
    private delegate void CSoundOpvarSetAutoRoomEntitySetOpvarThinkDelegate(nint a1);

    private static IUnmanagedFunction<CSoundOpvarSetAutoRoomEntitySetOpvarThinkDelegate>? CSoundOpvarSetAutoRoomEntitySetOpvarThinkUnmanagedFunction;
    private static Guid CSoundOpvarSetAutoRoomEntitySetOpvarThinkHookGuid;

    private static IUnmanagedFunction<CSoundOpvarSetAutoRoomEntitySetOpvarThinkDelegate> CSoundOpvarSetAutoRoomEntitySetOpvarThinkGetUnmanagedFunction()
    {
        if (CSoundOpvarSetAutoRoomEntitySetOpvarThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CSoundOpvarSetAutoRoomEntity", "CSoundOpvarSetAutoRoomEntitySetOpvarThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CSoundOpvarSetAutoRoomEntity::CSoundOpvarSetAutoRoomEntitySetOpvarThink.");
            }
            CSoundOpvarSetAutoRoomEntitySetOpvarThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CSoundOpvarSetAutoRoomEntitySetOpvarThinkDelegate>(address);
        }
        return CSoundOpvarSetAutoRoomEntitySetOpvarThinkUnmanagedFunction;
    }

    internal static Guid HookCSoundOpvarSetAutoRoomEntitySetOpvarThink()
    {
        CSoundOpvarSetAutoRoomEntitySetOpvarThinkHookGuid = CSoundOpvarSetAutoRoomEntitySetOpvarThinkGetUnmanagedFunction().AddHook(next => (a1) => CSoundOpvarSetAutoRoomEntitySetOpvarThinkPipeline(a1, () => next()(a1)));
        return CSoundOpvarSetAutoRoomEntitySetOpvarThinkHookGuid;
    }

    internal static Guid UnhookCSoundOpvarSetAutoRoomEntitySetOpvarThink()
    {
        CSoundOpvarSetAutoRoomEntitySetOpvarThinkGetUnmanagedFunction().RemoveHook(CSoundOpvarSetAutoRoomEntitySetOpvarThinkHookGuid);
        return Guid.Empty;
    }

    private static void CSoundOpvarSetAutoRoomEntitySetOpvarThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CSoundOpvarSetAutoRoomEntity>(a1);

            var preCtx = new CSoundOpvarSetAutoRoomEntitySetOpvarThinkPreContext { SchemaObject = schemaObject };
            InvokeCSoundOpvarSetAutoRoomEntitySetOpvarThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CSoundOpvarSetAutoRoomEntitySetOpvarThinkPostContext { SchemaObject = schemaObject };
            InvokeCSoundOpvarSetAutoRoomEntitySetOpvarThinkPost(ref postCtx);
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

    internal static void InvokeCSoundOpvarSetAutoRoomEntitySetOpvarThink(nint a1)
    {
        CSoundOpvarSetAutoRoomEntitySetOpvarThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCSoundOpvarSetAutoRoomEntitySetOpvarThinkPre(ref CSoundOpvarSetAutoRoomEntitySetOpvarThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSoundOpvarSetAutoRoomEntitySetOpvarThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCSoundOpvarSetAutoRoomEntitySetOpvarThinkPost(ref CSoundOpvarSetAutoRoomEntitySetOpvarThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSoundOpvarSetAutoRoomEntitySetOpvarThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CSoundOpvarSetAutoRoomEntitySetOpvarThinkHook : ICSoundOpvarSetAutoRoomEntitySetOpvarThinkHook
{
    private event OnCSoundOpvarSetAutoRoomEntitySetOpvarThinkPreDelegate? _Pre;
    private event OnCSoundOpvarSetAutoRoomEntitySetOpvarThinkPostDelegate? _Post;

    public event OnCSoundOpvarSetAutoRoomEntitySetOpvarThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSoundOpvarSetAutoRoomEntitySetOpvarThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundOpvarSetAutoRoomEntitySetOpvarThink);
            }
        }
    }

    public event OnCSoundOpvarSetAutoRoomEntitySetOpvarThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSoundOpvarSetAutoRoomEntitySetOpvarThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundOpvarSetAutoRoomEntitySetOpvarThink);
            }
        }
    }

    public void InvokePre(ref CSoundOpvarSetAutoRoomEntitySetOpvarThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CSoundOpvarSetAutoRoomEntitySetOpvarThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundOpvarSetAutoRoomEntitySetOpvarThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundOpvarSetAutoRoomEntitySetOpvarThink);
        }
    }

    public void Invoke(CSoundOpvarSetAutoRoomEntity schemaObject) => DatamapHooksPublisher.InvokeCSoundOpvarSetAutoRoomEntitySetOpvarThink(schemaObject.Address);
}