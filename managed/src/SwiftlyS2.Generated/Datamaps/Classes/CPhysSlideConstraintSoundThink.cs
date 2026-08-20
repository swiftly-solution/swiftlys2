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
    private delegate void CPhysSlideConstraintSoundThinkDelegate(nint a1);

    private static IUnmanagedFunction<CPhysSlideConstraintSoundThinkDelegate>? CPhysSlideConstraintSoundThinkUnmanagedFunction;
    private static Guid CPhysSlideConstraintSoundThinkHookGuid;

    private static IUnmanagedFunction<CPhysSlideConstraintSoundThinkDelegate> CPhysSlideConstraintSoundThinkGetUnmanagedFunction()
    {
        if (CPhysSlideConstraintSoundThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPhysSlideConstraint", "CPhysSlideConstraintSoundThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPhysSlideConstraint::CPhysSlideConstraintSoundThink.");
            }
            CPhysSlideConstraintSoundThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPhysSlideConstraintSoundThinkDelegate>(address);
        }
        return CPhysSlideConstraintSoundThinkUnmanagedFunction;
    }

    internal static Guid HookCPhysSlideConstraintSoundThink()
    {
        CPhysSlideConstraintSoundThinkHookGuid = CPhysSlideConstraintSoundThinkGetUnmanagedFunction().AddHook(next => (a1) => CPhysSlideConstraintSoundThinkPipeline(a1, () => next()(a1)));
        return CPhysSlideConstraintSoundThinkHookGuid;
    }

    internal static Guid UnhookCPhysSlideConstraintSoundThink()
    {
        CPhysSlideConstraintSoundThinkGetUnmanagedFunction().RemoveHook(CPhysSlideConstraintSoundThinkHookGuid);
        return Guid.Empty;
    }

    private static void CPhysSlideConstraintSoundThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPhysSlideConstraint>(a1);

            var preCtx = new CPhysSlideConstraintSoundThinkPreContext { SchemaObject = schemaObject };
            InvokeCPhysSlideConstraintSoundThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPhysSlideConstraintSoundThinkPostContext { SchemaObject = schemaObject };
            InvokeCPhysSlideConstraintSoundThinkPost(ref postCtx);
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

    internal static void InvokeCPhysSlideConstraintSoundThink(nint a1)
    {
        CPhysSlideConstraintSoundThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPhysSlideConstraintSoundThinkPre(ref CPhysSlideConstraintSoundThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysSlideConstraintSoundThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPhysSlideConstraintSoundThinkPost(ref CPhysSlideConstraintSoundThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysSlideConstraintSoundThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPhysSlideConstraintSoundThinkHook : ICPhysSlideConstraintSoundThinkHook
{
    private event OnCPhysSlideConstraintSoundThinkPreDelegate? _Pre;
    private event OnCPhysSlideConstraintSoundThinkPostDelegate? _Post;

    public event OnCPhysSlideConstraintSoundThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysSlideConstraintSoundThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysSlideConstraintSoundThink);
            }
        }
    }

    public event OnCPhysSlideConstraintSoundThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysSlideConstraintSoundThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysSlideConstraintSoundThink);
            }
        }
    }

    public void InvokePre(ref CPhysSlideConstraintSoundThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPhysSlideConstraintSoundThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysSlideConstraintSoundThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysSlideConstraintSoundThink);
        }
    }

    public void Invoke(CPhysSlideConstraint schemaObject) => DatamapHooksPublisher.InvokeCPhysSlideConstraintSoundThink(schemaObject.Address);
}