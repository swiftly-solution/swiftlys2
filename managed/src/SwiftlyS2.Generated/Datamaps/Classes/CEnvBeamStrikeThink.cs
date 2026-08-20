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
    private delegate void CEnvBeamStrikeThinkDelegate(nint a1);

    private static IUnmanagedFunction<CEnvBeamStrikeThinkDelegate>? CEnvBeamStrikeThinkUnmanagedFunction;
    private static Guid CEnvBeamStrikeThinkHookGuid;

    private static IUnmanagedFunction<CEnvBeamStrikeThinkDelegate> CEnvBeamStrikeThinkGetUnmanagedFunction()
    {
        if (CEnvBeamStrikeThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CEnvBeam", "CEnvBeamStrikeThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CEnvBeam::CEnvBeamStrikeThink.");
            }
            CEnvBeamStrikeThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CEnvBeamStrikeThinkDelegate>(address);
        }
        return CEnvBeamStrikeThinkUnmanagedFunction;
    }

    internal static Guid HookCEnvBeamStrikeThink()
    {
        CEnvBeamStrikeThinkHookGuid = CEnvBeamStrikeThinkGetUnmanagedFunction().AddHook(next => (a1) => CEnvBeamStrikeThinkPipeline(a1, () => next()(a1)));
        return CEnvBeamStrikeThinkHookGuid;
    }

    internal static Guid UnhookCEnvBeamStrikeThink()
    {
        CEnvBeamStrikeThinkGetUnmanagedFunction().RemoveHook(CEnvBeamStrikeThinkHookGuid);
        return Guid.Empty;
    }

    private static void CEnvBeamStrikeThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CEnvBeam>(a1);

            var preCtx = new CEnvBeamStrikeThinkPreContext { SchemaObject = schemaObject };
            InvokeCEnvBeamStrikeThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CEnvBeamStrikeThinkPostContext { SchemaObject = schemaObject };
            InvokeCEnvBeamStrikeThinkPost(ref postCtx);
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

    internal static void InvokeCEnvBeamStrikeThink(nint a1)
    {
        CEnvBeamStrikeThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCEnvBeamStrikeThinkPre(ref CEnvBeamStrikeThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCEnvBeamStrikeThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCEnvBeamStrikeThinkPost(ref CEnvBeamStrikeThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCEnvBeamStrikeThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CEnvBeamStrikeThinkHook : ICEnvBeamStrikeThinkHook
{
    private event OnCEnvBeamStrikeThinkPreDelegate? _Pre;
    private event OnCEnvBeamStrikeThinkPostDelegate? _Post;

    public event OnCEnvBeamStrikeThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CEnvBeamStrikeThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvBeamStrikeThink);
            }
        }
    }

    public event OnCEnvBeamStrikeThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CEnvBeamStrikeThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvBeamStrikeThink);
            }
        }
    }

    public void InvokePre(ref CEnvBeamStrikeThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CEnvBeamStrikeThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvBeamStrikeThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvBeamStrikeThink);
        }
    }

    public void Invoke(CEnvBeam schemaObject) => DatamapHooksPublisher.InvokeCEnvBeamStrikeThink(schemaObject.Address);
}