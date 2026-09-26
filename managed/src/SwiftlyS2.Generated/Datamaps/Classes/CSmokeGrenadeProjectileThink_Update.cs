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
    private delegate void CSmokeGrenadeProjectileThink_UpdateDelegate(nint a1);

    private static IUnmanagedFunction<CSmokeGrenadeProjectileThink_UpdateDelegate>? CSmokeGrenadeProjectileThink_UpdateUnmanagedFunction;
    private static Guid CSmokeGrenadeProjectileThink_UpdateHookGuid;

    private static IUnmanagedFunction<CSmokeGrenadeProjectileThink_UpdateDelegate> CSmokeGrenadeProjectileThink_UpdateGetUnmanagedFunction()
    {
        if (CSmokeGrenadeProjectileThink_UpdateUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CSmokeGrenadeProjectile", "CSmokeGrenadeProjectileThink_Update");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CSmokeGrenadeProjectile::CSmokeGrenadeProjectileThink_Update.");
            }
            CSmokeGrenadeProjectileThink_UpdateUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CSmokeGrenadeProjectileThink_UpdateDelegate>(address);
        }
        return CSmokeGrenadeProjectileThink_UpdateUnmanagedFunction;
    }

    internal static Guid HookCSmokeGrenadeProjectileThink_Update()
    {
        CSmokeGrenadeProjectileThink_UpdateHookGuid = CSmokeGrenadeProjectileThink_UpdateGetUnmanagedFunction().AddHook(next => (a1) => CSmokeGrenadeProjectileThink_UpdatePipeline(a1, () => next()(a1)));
        return CSmokeGrenadeProjectileThink_UpdateHookGuid;
    }

    internal static Guid UnhookCSmokeGrenadeProjectileThink_Update()
    {
        CSmokeGrenadeProjectileThink_UpdateGetUnmanagedFunction().RemoveHook(CSmokeGrenadeProjectileThink_UpdateHookGuid);
        return Guid.Empty;
    }

    private static void CSmokeGrenadeProjectileThink_UpdatePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CSmokeGrenadeProjectile>(a1);

            var preCtx = new CSmokeGrenadeProjectileThink_UpdatePreContext { SchemaObject = schemaObject };
            InvokeCSmokeGrenadeProjectileThink_UpdatePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CSmokeGrenadeProjectileThink_UpdatePostContext { SchemaObject = schemaObject };
            InvokeCSmokeGrenadeProjectileThink_UpdatePost(ref postCtx);
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

    internal static void InvokeCSmokeGrenadeProjectileThink_Update(nint a1)
    {
        CSmokeGrenadeProjectileThink_UpdateGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCSmokeGrenadeProjectileThink_UpdatePre(ref CSmokeGrenadeProjectileThink_UpdatePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSmokeGrenadeProjectileThink_UpdatePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCSmokeGrenadeProjectileThink_UpdatePost(ref CSmokeGrenadeProjectileThink_UpdatePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSmokeGrenadeProjectileThink_UpdatePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CSmokeGrenadeProjectileThink_UpdateHook : ICSmokeGrenadeProjectileThink_UpdateHook
{
    private event OnCSmokeGrenadeProjectileThink_UpdatePreDelegate? _Pre;
    private event OnCSmokeGrenadeProjectileThink_UpdatePostDelegate? _Post;

    public event OnCSmokeGrenadeProjectileThink_UpdatePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSmokeGrenadeProjectileThink_Update);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSmokeGrenadeProjectileThink_Update);
            }
        }
    }

    public event OnCSmokeGrenadeProjectileThink_UpdatePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSmokeGrenadeProjectileThink_Update);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSmokeGrenadeProjectileThink_Update);
            }
        }
    }

    public void InvokePre(ref CSmokeGrenadeProjectileThink_UpdatePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CSmokeGrenadeProjectileThink_UpdatePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSmokeGrenadeProjectileThink_Update);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSmokeGrenadeProjectileThink_Update);
        }
    }

    public void Invoke(CSmokeGrenadeProjectile schemaObject) => DatamapHooksPublisher.InvokeCSmokeGrenadeProjectileThink_Update(schemaObject.Address);
}