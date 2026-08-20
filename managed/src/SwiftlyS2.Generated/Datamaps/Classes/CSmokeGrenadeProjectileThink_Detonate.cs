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
    private delegate void CSmokeGrenadeProjectileThink_DetonateDelegate(nint a1);

    private static IUnmanagedFunction<CSmokeGrenadeProjectileThink_DetonateDelegate>? CSmokeGrenadeProjectileThink_DetonateUnmanagedFunction;
    private static Guid CSmokeGrenadeProjectileThink_DetonateHookGuid;

    private static IUnmanagedFunction<CSmokeGrenadeProjectileThink_DetonateDelegate> CSmokeGrenadeProjectileThink_DetonateGetUnmanagedFunction()
    {
        if (CSmokeGrenadeProjectileThink_DetonateUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CSmokeGrenadeProjectile", "CSmokeGrenadeProjectileThink_Detonate");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CSmokeGrenadeProjectile::CSmokeGrenadeProjectileThink_Detonate.");
            }
            CSmokeGrenadeProjectileThink_DetonateUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CSmokeGrenadeProjectileThink_DetonateDelegate>(address);
        }
        return CSmokeGrenadeProjectileThink_DetonateUnmanagedFunction;
    }

    internal static Guid HookCSmokeGrenadeProjectileThink_Detonate()
    {
        CSmokeGrenadeProjectileThink_DetonateHookGuid = CSmokeGrenadeProjectileThink_DetonateGetUnmanagedFunction().AddHook(next => (a1) => CSmokeGrenadeProjectileThink_DetonatePipeline(a1, () => next()(a1)));
        return CSmokeGrenadeProjectileThink_DetonateHookGuid;
    }

    internal static Guid UnhookCSmokeGrenadeProjectileThink_Detonate()
    {
        CSmokeGrenadeProjectileThink_DetonateGetUnmanagedFunction().RemoveHook(CSmokeGrenadeProjectileThink_DetonateHookGuid);
        return Guid.Empty;
    }

    private static void CSmokeGrenadeProjectileThink_DetonatePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CSmokeGrenadeProjectile>(a1);

            var preCtx = new CSmokeGrenadeProjectileThink_DetonatePreContext { SchemaObject = schemaObject };
            InvokeCSmokeGrenadeProjectileThink_DetonatePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CSmokeGrenadeProjectileThink_DetonatePostContext { SchemaObject = schemaObject };
            InvokeCSmokeGrenadeProjectileThink_DetonatePost(ref postCtx);
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

    internal static void InvokeCSmokeGrenadeProjectileThink_Detonate(nint a1)
    {
        CSmokeGrenadeProjectileThink_DetonateGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCSmokeGrenadeProjectileThink_DetonatePre(ref CSmokeGrenadeProjectileThink_DetonatePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSmokeGrenadeProjectileThink_DetonatePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCSmokeGrenadeProjectileThink_DetonatePost(ref CSmokeGrenadeProjectileThink_DetonatePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSmokeGrenadeProjectileThink_DetonatePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CSmokeGrenadeProjectileThink_DetonateHook : ICSmokeGrenadeProjectileThink_DetonateHook
{
    private event OnCSmokeGrenadeProjectileThink_DetonatePreDelegate? _Pre;
    private event OnCSmokeGrenadeProjectileThink_DetonatePostDelegate? _Post;

    public event OnCSmokeGrenadeProjectileThink_DetonatePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSmokeGrenadeProjectileThink_Detonate);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSmokeGrenadeProjectileThink_Detonate);
            }
        }
    }

    public event OnCSmokeGrenadeProjectileThink_DetonatePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSmokeGrenadeProjectileThink_Detonate);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSmokeGrenadeProjectileThink_Detonate);
            }
        }
    }

    public void InvokePre(ref CSmokeGrenadeProjectileThink_DetonatePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CSmokeGrenadeProjectileThink_DetonatePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSmokeGrenadeProjectileThink_Detonate);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSmokeGrenadeProjectileThink_Detonate);
        }
    }

    public void Invoke(CSmokeGrenadeProjectile schemaObject) => DatamapHooksPublisher.InvokeCSmokeGrenadeProjectileThink_Detonate(schemaObject.Address);
}