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
    private delegate void CBaseGrenadeDangerSoundThinkDelegate(nint a1);

    private static IUnmanagedFunction<CBaseGrenadeDangerSoundThinkDelegate>? CBaseGrenadeDangerSoundThinkUnmanagedFunction;
    private static Guid CBaseGrenadeDangerSoundThinkHookGuid;

    private static IUnmanagedFunction<CBaseGrenadeDangerSoundThinkDelegate> CBaseGrenadeDangerSoundThinkGetUnmanagedFunction()
    {
        if (CBaseGrenadeDangerSoundThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseGrenade", "CBaseGrenadeDangerSoundThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseGrenade::CBaseGrenadeDangerSoundThink.");
            }
            CBaseGrenadeDangerSoundThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseGrenadeDangerSoundThinkDelegate>(address);
        }
        return CBaseGrenadeDangerSoundThinkUnmanagedFunction;
    }

    internal static Guid HookCBaseGrenadeDangerSoundThink()
    {
        CBaseGrenadeDangerSoundThinkHookGuid = CBaseGrenadeDangerSoundThinkGetUnmanagedFunction().AddHook(next => (a1) => CBaseGrenadeDangerSoundThinkPipeline(a1, () => next()(a1)));
        return CBaseGrenadeDangerSoundThinkHookGuid;
    }

    internal static Guid UnhookCBaseGrenadeDangerSoundThink()
    {
        CBaseGrenadeDangerSoundThinkGetUnmanagedFunction().RemoveHook(CBaseGrenadeDangerSoundThinkHookGuid);
        return Guid.Empty;
    }

    private static void CBaseGrenadeDangerSoundThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseGrenade>(a1);

            var preCtx = new CBaseGrenadeDangerSoundThinkPreContext { SchemaObject = schemaObject };
            InvokeCBaseGrenadeDangerSoundThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseGrenadeDangerSoundThinkPostContext { SchemaObject = schemaObject };
            InvokeCBaseGrenadeDangerSoundThinkPost(ref postCtx);
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

    internal static void InvokeCBaseGrenadeDangerSoundThink(nint a1)
    {
        CBaseGrenadeDangerSoundThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseGrenadeDangerSoundThinkPre(ref CBaseGrenadeDangerSoundThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseGrenadeDangerSoundThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseGrenadeDangerSoundThinkPost(ref CBaseGrenadeDangerSoundThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseGrenadeDangerSoundThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseGrenadeDangerSoundThinkHook : ICBaseGrenadeDangerSoundThinkHook
{
    private event OnCBaseGrenadeDangerSoundThinkPreDelegate? _Pre;
    private event OnCBaseGrenadeDangerSoundThinkPostDelegate? _Post;

    public event OnCBaseGrenadeDangerSoundThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseGrenadeDangerSoundThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeDangerSoundThink);
            }
        }
    }

    public event OnCBaseGrenadeDangerSoundThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseGrenadeDangerSoundThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeDangerSoundThink);
            }
        }
    }

    public void InvokePre(ref CBaseGrenadeDangerSoundThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseGrenadeDangerSoundThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeDangerSoundThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeDangerSoundThink);
        }
    }

    public void Invoke(CBaseGrenade schemaObject) => DatamapHooksPublisher.InvokeCBaseGrenadeDangerSoundThink(schemaObject.Address);
}