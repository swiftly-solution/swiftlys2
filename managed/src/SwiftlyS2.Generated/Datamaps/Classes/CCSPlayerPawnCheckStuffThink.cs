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
    private delegate void CCSPlayerPawnCheckStuffThinkDelegate(nint a1);

    private static IUnmanagedFunction<CCSPlayerPawnCheckStuffThinkDelegate>? CCSPlayerPawnCheckStuffThinkUnmanagedFunction;
    private static Guid CCSPlayerPawnCheckStuffThinkHookGuid;

    private static IUnmanagedFunction<CCSPlayerPawnCheckStuffThinkDelegate> CCSPlayerPawnCheckStuffThinkGetUnmanagedFunction()
    {
        if (CCSPlayerPawnCheckStuffThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CCSPlayerPawn", "CCSPlayerPawnCheckStuffThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CCSPlayerPawn::CCSPlayerPawnCheckStuffThink.");
            }
            CCSPlayerPawnCheckStuffThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerPawnCheckStuffThinkDelegate>(address);
        }
        return CCSPlayerPawnCheckStuffThinkUnmanagedFunction;
    }

    internal static Guid HookCCSPlayerPawnCheckStuffThink()
    {
        CCSPlayerPawnCheckStuffThinkHookGuid = CCSPlayerPawnCheckStuffThinkGetUnmanagedFunction().AddHook(next => (a1) => CCSPlayerPawnCheckStuffThinkPipeline(a1, () => next()(a1)));
        return CCSPlayerPawnCheckStuffThinkHookGuid;
    }

    internal static Guid UnhookCCSPlayerPawnCheckStuffThink()
    {
        CCSPlayerPawnCheckStuffThinkGetUnmanagedFunction().RemoveHook(CCSPlayerPawnCheckStuffThinkHookGuid);
        return Guid.Empty;
    }

    private static void CCSPlayerPawnCheckStuffThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CCSPlayerPawn>(a1);

            var preCtx = new CCSPlayerPawnCheckStuffThinkPreContext { SchemaObject = schemaObject };
            InvokeCCSPlayerPawnCheckStuffThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CCSPlayerPawnCheckStuffThinkPostContext { SchemaObject = schemaObject };
            InvokeCCSPlayerPawnCheckStuffThinkPost(ref postCtx);
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

    internal static void InvokeCCSPlayerPawnCheckStuffThink(nint a1)
    {
        CCSPlayerPawnCheckStuffThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCCSPlayerPawnCheckStuffThinkPre(ref CCSPlayerPawnCheckStuffThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCCSPlayerPawnCheckStuffThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCCSPlayerPawnCheckStuffThinkPost(ref CCSPlayerPawnCheckStuffThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCCSPlayerPawnCheckStuffThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CCSPlayerPawnCheckStuffThinkHook : ICCSPlayerPawnCheckStuffThinkHook
{
    private event OnCCSPlayerPawnCheckStuffThinkPreDelegate? _Pre;
    private event OnCCSPlayerPawnCheckStuffThinkPostDelegate? _Post;

    public event OnCCSPlayerPawnCheckStuffThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CCSPlayerPawnCheckStuffThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerPawnCheckStuffThink);
            }
        }
    }

    public event OnCCSPlayerPawnCheckStuffThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CCSPlayerPawnCheckStuffThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerPawnCheckStuffThink);
            }
        }
    }

    public void InvokePre(ref CCSPlayerPawnCheckStuffThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CCSPlayerPawnCheckStuffThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerPawnCheckStuffThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerPawnCheckStuffThink);
        }
    }

    public void Invoke(CCSPlayerPawn schemaObject) => DatamapHooksPublisher.InvokeCCSPlayerPawnCheckStuffThink(schemaObject.Address);
}