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
    private delegate void CSmokeGrenadeProjectileThink_RemoveDelegate(nint a1);

    private static IUnmanagedFunction<CSmokeGrenadeProjectileThink_RemoveDelegate>? CSmokeGrenadeProjectileThink_RemoveUnmanagedFunction;
    private static Guid CSmokeGrenadeProjectileThink_RemoveHookGuid;

    private static IUnmanagedFunction<CSmokeGrenadeProjectileThink_RemoveDelegate> CSmokeGrenadeProjectileThink_RemoveGetUnmanagedFunction()
    {
        if (CSmokeGrenadeProjectileThink_RemoveUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CSmokeGrenadeProjectile", "CSmokeGrenadeProjectileThink_Remove");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CSmokeGrenadeProjectile::CSmokeGrenadeProjectileThink_Remove.");
            }
            CSmokeGrenadeProjectileThink_RemoveUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CSmokeGrenadeProjectileThink_RemoveDelegate>(address);
        }
        return CSmokeGrenadeProjectileThink_RemoveUnmanagedFunction;
    }

    internal static Guid HookCSmokeGrenadeProjectileThink_Remove()
    {
        CSmokeGrenadeProjectileThink_RemoveHookGuid = CSmokeGrenadeProjectileThink_RemoveGetUnmanagedFunction().AddHook(next => (a1) => CSmokeGrenadeProjectileThink_RemovePipeline(a1, () => next()(a1)));
        return CSmokeGrenadeProjectileThink_RemoveHookGuid;
    }

    internal static Guid UnhookCSmokeGrenadeProjectileThink_Remove()
    {
        CSmokeGrenadeProjectileThink_RemoveGetUnmanagedFunction().RemoveHook(CSmokeGrenadeProjectileThink_RemoveHookGuid);
        return Guid.Empty;
    }

    private static void CSmokeGrenadeProjectileThink_RemovePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CSmokeGrenadeProjectile>(a1);

            var preCtx = new CSmokeGrenadeProjectileThink_RemovePreContext { SchemaObject = schemaObject };
            InvokeCSmokeGrenadeProjectileThink_RemovePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CSmokeGrenadeProjectileThink_RemovePostContext { SchemaObject = schemaObject };
            InvokeCSmokeGrenadeProjectileThink_RemovePost(ref postCtx);
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

    internal static void InvokeCSmokeGrenadeProjectileThink_Remove(nint a1)
    {
        CSmokeGrenadeProjectileThink_RemoveGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCSmokeGrenadeProjectileThink_RemovePre(ref CSmokeGrenadeProjectileThink_RemovePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSmokeGrenadeProjectileThink_RemovePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCSmokeGrenadeProjectileThink_RemovePost(ref CSmokeGrenadeProjectileThink_RemovePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSmokeGrenadeProjectileThink_RemovePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CSmokeGrenadeProjectileThink_RemoveHook : ICSmokeGrenadeProjectileThink_RemoveHook
{
    private event OnCSmokeGrenadeProjectileThink_RemovePreDelegate? _Pre;
    private event OnCSmokeGrenadeProjectileThink_RemovePostDelegate? _Post;

    public event OnCSmokeGrenadeProjectileThink_RemovePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSmokeGrenadeProjectileThink_Remove);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSmokeGrenadeProjectileThink_Remove);
            }
        }
    }

    public event OnCSmokeGrenadeProjectileThink_RemovePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSmokeGrenadeProjectileThink_Remove);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSmokeGrenadeProjectileThink_Remove);
            }
        }
    }

    public void InvokePre(ref CSmokeGrenadeProjectileThink_RemovePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CSmokeGrenadeProjectileThink_RemovePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSmokeGrenadeProjectileThink_Remove);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSmokeGrenadeProjectileThink_Remove);
        }
    }

    public void Invoke(CSmokeGrenadeProjectile schemaObject) => DatamapHooksPublisher.InvokeCSmokeGrenadeProjectileThink_Remove(schemaObject.Address);
}