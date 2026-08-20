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
    private delegate void CEnvLaserStrikeThinkDelegate(nint a1);

    private static IUnmanagedFunction<CEnvLaserStrikeThinkDelegate>? CEnvLaserStrikeThinkUnmanagedFunction;
    private static Guid CEnvLaserStrikeThinkHookGuid;

    private static IUnmanagedFunction<CEnvLaserStrikeThinkDelegate> CEnvLaserStrikeThinkGetUnmanagedFunction()
    {
        if (CEnvLaserStrikeThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CEnvLaser", "CEnvLaserStrikeThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CEnvLaser::CEnvLaserStrikeThink.");
            }
            CEnvLaserStrikeThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CEnvLaserStrikeThinkDelegate>(address);
        }
        return CEnvLaserStrikeThinkUnmanagedFunction;
    }

    internal static Guid HookCEnvLaserStrikeThink()
    {
        CEnvLaserStrikeThinkHookGuid = CEnvLaserStrikeThinkGetUnmanagedFunction().AddHook(next => (a1) => CEnvLaserStrikeThinkPipeline(a1, () => next()(a1)));
        return CEnvLaserStrikeThinkHookGuid;
    }

    internal static Guid UnhookCEnvLaserStrikeThink()
    {
        CEnvLaserStrikeThinkGetUnmanagedFunction().RemoveHook(CEnvLaserStrikeThinkHookGuid);
        return Guid.Empty;
    }

    private static void CEnvLaserStrikeThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CEnvLaser>(a1);

            var preCtx = new CEnvLaserStrikeThinkPreContext { SchemaObject = schemaObject };
            InvokeCEnvLaserStrikeThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CEnvLaserStrikeThinkPostContext { SchemaObject = schemaObject };
            InvokeCEnvLaserStrikeThinkPost(ref postCtx);
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

    internal static void InvokeCEnvLaserStrikeThink(nint a1)
    {
        CEnvLaserStrikeThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCEnvLaserStrikeThinkPre(ref CEnvLaserStrikeThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCEnvLaserStrikeThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCEnvLaserStrikeThinkPost(ref CEnvLaserStrikeThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCEnvLaserStrikeThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CEnvLaserStrikeThinkHook : ICEnvLaserStrikeThinkHook
{
    private event OnCEnvLaserStrikeThinkPreDelegate? _Pre;
    private event OnCEnvLaserStrikeThinkPostDelegate? _Post;

    public event OnCEnvLaserStrikeThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CEnvLaserStrikeThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvLaserStrikeThink);
            }
        }
    }

    public event OnCEnvLaserStrikeThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CEnvLaserStrikeThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvLaserStrikeThink);
            }
        }
    }

    public void InvokePre(ref CEnvLaserStrikeThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CEnvLaserStrikeThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvLaserStrikeThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvLaserStrikeThink);
        }
    }

    public void Invoke(CEnvLaser schemaObject) => DatamapHooksPublisher.InvokeCEnvLaserStrikeThink(schemaObject.Address);
}