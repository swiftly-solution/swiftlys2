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
    private delegate void CCSWeaponBaseRemoveUnownedWeaponThinkDelegate(nint a1);

    private static IUnmanagedFunction<CCSWeaponBaseRemoveUnownedWeaponThinkDelegate>? CCSWeaponBaseRemoveUnownedWeaponThinkUnmanagedFunction;
    private static Guid CCSWeaponBaseRemoveUnownedWeaponThinkHookGuid;

    private static IUnmanagedFunction<CCSWeaponBaseRemoveUnownedWeaponThinkDelegate> CCSWeaponBaseRemoveUnownedWeaponThinkGetUnmanagedFunction()
    {
        if (CCSWeaponBaseRemoveUnownedWeaponThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CCSWeaponBase", "CCSWeaponBaseRemoveUnownedWeaponThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CCSWeaponBase::CCSWeaponBaseRemoveUnownedWeaponThink.");
            }
            CCSWeaponBaseRemoveUnownedWeaponThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSWeaponBaseRemoveUnownedWeaponThinkDelegate>(address);
        }
        return CCSWeaponBaseRemoveUnownedWeaponThinkUnmanagedFunction;
    }

    internal static Guid HookCCSWeaponBaseRemoveUnownedWeaponThink()
    {
        CCSWeaponBaseRemoveUnownedWeaponThinkHookGuid = CCSWeaponBaseRemoveUnownedWeaponThinkGetUnmanagedFunction().AddHook(next => (a1) => CCSWeaponBaseRemoveUnownedWeaponThinkPipeline(a1, () => next()(a1)));
        return CCSWeaponBaseRemoveUnownedWeaponThinkHookGuid;
    }

    internal static Guid UnhookCCSWeaponBaseRemoveUnownedWeaponThink()
    {
        CCSWeaponBaseRemoveUnownedWeaponThinkGetUnmanagedFunction().RemoveHook(CCSWeaponBaseRemoveUnownedWeaponThinkHookGuid);
        return Guid.Empty;
    }

    private static void CCSWeaponBaseRemoveUnownedWeaponThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CCSWeaponBase>(a1);

            var preCtx = new CCSWeaponBaseRemoveUnownedWeaponThinkPreContext { SchemaObject = schemaObject };
            InvokeCCSWeaponBaseRemoveUnownedWeaponThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CCSWeaponBaseRemoveUnownedWeaponThinkPostContext { SchemaObject = schemaObject };
            InvokeCCSWeaponBaseRemoveUnownedWeaponThinkPost(ref postCtx);
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

    internal static void InvokeCCSWeaponBaseRemoveUnownedWeaponThink(nint a1)
    {
        CCSWeaponBaseRemoveUnownedWeaponThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCCSWeaponBaseRemoveUnownedWeaponThinkPre(ref CCSWeaponBaseRemoveUnownedWeaponThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCCSWeaponBaseRemoveUnownedWeaponThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCCSWeaponBaseRemoveUnownedWeaponThinkPost(ref CCSWeaponBaseRemoveUnownedWeaponThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCCSWeaponBaseRemoveUnownedWeaponThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CCSWeaponBaseRemoveUnownedWeaponThinkHook : ICCSWeaponBaseRemoveUnownedWeaponThinkHook
{
    private event OnCCSWeaponBaseRemoveUnownedWeaponThinkPreDelegate? _Pre;
    private event OnCCSWeaponBaseRemoveUnownedWeaponThinkPostDelegate? _Post;

    public event OnCCSWeaponBaseRemoveUnownedWeaponThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CCSWeaponBaseRemoveUnownedWeaponThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSWeaponBaseRemoveUnownedWeaponThink);
            }
        }
    }

    public event OnCCSWeaponBaseRemoveUnownedWeaponThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CCSWeaponBaseRemoveUnownedWeaponThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSWeaponBaseRemoveUnownedWeaponThink);
            }
        }
    }

    public void InvokePre(ref CCSWeaponBaseRemoveUnownedWeaponThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CCSWeaponBaseRemoveUnownedWeaponThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSWeaponBaseRemoveUnownedWeaponThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSWeaponBaseRemoveUnownedWeaponThink);
        }
    }

    public void Invoke(CCSWeaponBase schemaObject) => DatamapHooksPublisher.InvokeCCSWeaponBaseRemoveUnownedWeaponThink(schemaObject.Address);
}