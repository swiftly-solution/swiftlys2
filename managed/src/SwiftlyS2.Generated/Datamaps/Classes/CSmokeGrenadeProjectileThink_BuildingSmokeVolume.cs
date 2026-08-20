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
    private delegate void CSmokeGrenadeProjectileThink_BuildingSmokeVolumeDelegate(nint a1);

    private static IUnmanagedFunction<CSmokeGrenadeProjectileThink_BuildingSmokeVolumeDelegate>? CSmokeGrenadeProjectileThink_BuildingSmokeVolumeUnmanagedFunction;
    private static Guid CSmokeGrenadeProjectileThink_BuildingSmokeVolumeHookGuid;

    private static IUnmanagedFunction<CSmokeGrenadeProjectileThink_BuildingSmokeVolumeDelegate> CSmokeGrenadeProjectileThink_BuildingSmokeVolumeGetUnmanagedFunction()
    {
        if (CSmokeGrenadeProjectileThink_BuildingSmokeVolumeUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CSmokeGrenadeProjectile", "CSmokeGrenadeProjectileThink_BuildingSmokeVolume");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CSmokeGrenadeProjectile::CSmokeGrenadeProjectileThink_BuildingSmokeVolume.");
            }
            CSmokeGrenadeProjectileThink_BuildingSmokeVolumeUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CSmokeGrenadeProjectileThink_BuildingSmokeVolumeDelegate>(address);
        }
        return CSmokeGrenadeProjectileThink_BuildingSmokeVolumeUnmanagedFunction;
    }

    internal static Guid HookCSmokeGrenadeProjectileThink_BuildingSmokeVolume()
    {
        CSmokeGrenadeProjectileThink_BuildingSmokeVolumeHookGuid = CSmokeGrenadeProjectileThink_BuildingSmokeVolumeGetUnmanagedFunction().AddHook(next => (a1) => CSmokeGrenadeProjectileThink_BuildingSmokeVolumePipeline(a1, () => next()(a1)));
        return CSmokeGrenadeProjectileThink_BuildingSmokeVolumeHookGuid;
    }

    internal static Guid UnhookCSmokeGrenadeProjectileThink_BuildingSmokeVolume()
    {
        CSmokeGrenadeProjectileThink_BuildingSmokeVolumeGetUnmanagedFunction().RemoveHook(CSmokeGrenadeProjectileThink_BuildingSmokeVolumeHookGuid);
        return Guid.Empty;
    }

    private static void CSmokeGrenadeProjectileThink_BuildingSmokeVolumePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CSmokeGrenadeProjectile>(a1);

            var preCtx = new CSmokeGrenadeProjectileThink_BuildingSmokeVolumePreContext { SchemaObject = schemaObject };
            InvokeCSmokeGrenadeProjectileThink_BuildingSmokeVolumePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CSmokeGrenadeProjectileThink_BuildingSmokeVolumePostContext { SchemaObject = schemaObject };
            InvokeCSmokeGrenadeProjectileThink_BuildingSmokeVolumePost(ref postCtx);
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

    internal static void InvokeCSmokeGrenadeProjectileThink_BuildingSmokeVolume(nint a1)
    {
        CSmokeGrenadeProjectileThink_BuildingSmokeVolumeGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCSmokeGrenadeProjectileThink_BuildingSmokeVolumePre(ref CSmokeGrenadeProjectileThink_BuildingSmokeVolumePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSmokeGrenadeProjectileThink_BuildingSmokeVolumePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCSmokeGrenadeProjectileThink_BuildingSmokeVolumePost(ref CSmokeGrenadeProjectileThink_BuildingSmokeVolumePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSmokeGrenadeProjectileThink_BuildingSmokeVolumePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CSmokeGrenadeProjectileThink_BuildingSmokeVolumeHook : ICSmokeGrenadeProjectileThink_BuildingSmokeVolumeHook
{
    private event OnCSmokeGrenadeProjectileThink_BuildingSmokeVolumePreDelegate? _Pre;
    private event OnCSmokeGrenadeProjectileThink_BuildingSmokeVolumePostDelegate? _Post;

    public event OnCSmokeGrenadeProjectileThink_BuildingSmokeVolumePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSmokeGrenadeProjectileThink_BuildingSmokeVolume);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSmokeGrenadeProjectileThink_BuildingSmokeVolume);
            }
        }
    }

    public event OnCSmokeGrenadeProjectileThink_BuildingSmokeVolumePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSmokeGrenadeProjectileThink_BuildingSmokeVolume);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSmokeGrenadeProjectileThink_BuildingSmokeVolume);
            }
        }
    }

    public void InvokePre(ref CSmokeGrenadeProjectileThink_BuildingSmokeVolumePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CSmokeGrenadeProjectileThink_BuildingSmokeVolumePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSmokeGrenadeProjectileThink_BuildingSmokeVolume);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSmokeGrenadeProjectileThink_BuildingSmokeVolume);
        }
    }

    public void Invoke(CSmokeGrenadeProjectile schemaObject) => DatamapHooksPublisher.InvokeCSmokeGrenadeProjectileThink_BuildingSmokeVolume(schemaObject.Address);
}