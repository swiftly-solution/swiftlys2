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
    private delegate void CWaterBulletBulletThinkDelegate(nint a1);

    private static IUnmanagedFunction<CWaterBulletBulletThinkDelegate>? CWaterBulletBulletThinkUnmanagedFunction;
    private static Guid CWaterBulletBulletThinkHookGuid;

    private static IUnmanagedFunction<CWaterBulletBulletThinkDelegate> CWaterBulletBulletThinkGetUnmanagedFunction()
    {
        if (CWaterBulletBulletThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CWaterBullet", "CWaterBulletBulletThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CWaterBullet::CWaterBulletBulletThink.");
            }
            CWaterBulletBulletThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CWaterBulletBulletThinkDelegate>(address);
        }
        return CWaterBulletBulletThinkUnmanagedFunction;
    }

    internal static Guid HookCWaterBulletBulletThink()
    {
        CWaterBulletBulletThinkHookGuid = CWaterBulletBulletThinkGetUnmanagedFunction().AddHook(next => (a1) => CWaterBulletBulletThinkPipeline(a1, () => next()(a1)));
        return CWaterBulletBulletThinkHookGuid;
    }

    internal static Guid UnhookCWaterBulletBulletThink()
    {
        CWaterBulletBulletThinkGetUnmanagedFunction().RemoveHook(CWaterBulletBulletThinkHookGuid);
        return Guid.Empty;
    }

    private static void CWaterBulletBulletThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CWaterBullet>(a1);

            var preCtx = new CWaterBulletBulletThinkPreContext { SchemaObject = schemaObject };
            InvokeCWaterBulletBulletThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CWaterBulletBulletThinkPostContext { SchemaObject = schemaObject };
            InvokeCWaterBulletBulletThinkPost(ref postCtx);
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

    internal static void InvokeCWaterBulletBulletThink(nint a1)
    {
        CWaterBulletBulletThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCWaterBulletBulletThinkPre(ref CWaterBulletBulletThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCWaterBulletBulletThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCWaterBulletBulletThinkPost(ref CWaterBulletBulletThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCWaterBulletBulletThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CWaterBulletBulletThinkHook : ICWaterBulletBulletThinkHook
{
    private event OnCWaterBulletBulletThinkPreDelegate? _Pre;
    private event OnCWaterBulletBulletThinkPostDelegate? _Post;

    public event OnCWaterBulletBulletThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CWaterBulletBulletThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CWaterBulletBulletThink);
            }
        }
    }

    public event OnCWaterBulletBulletThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CWaterBulletBulletThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CWaterBulletBulletThink);
            }
        }
    }

    public void InvokePre(ref CWaterBulletBulletThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CWaterBulletBulletThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CWaterBulletBulletThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CWaterBulletBulletThink);
        }
    }

    public void Invoke(CWaterBullet schemaObject) => DatamapHooksPublisher.InvokeCWaterBulletBulletThink(schemaObject.Address);
}