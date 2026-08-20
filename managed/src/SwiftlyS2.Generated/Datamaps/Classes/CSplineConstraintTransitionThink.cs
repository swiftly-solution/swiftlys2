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
    private delegate void CSplineConstraintTransitionThinkDelegate(nint a1);

    private static IUnmanagedFunction<CSplineConstraintTransitionThinkDelegate>? CSplineConstraintTransitionThinkUnmanagedFunction;
    private static Guid CSplineConstraintTransitionThinkHookGuid;

    private static IUnmanagedFunction<CSplineConstraintTransitionThinkDelegate> CSplineConstraintTransitionThinkGetUnmanagedFunction()
    {
        if (CSplineConstraintTransitionThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CSplineConstraint", "CSplineConstraintTransitionThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CSplineConstraint::CSplineConstraintTransitionThink.");
            }
            CSplineConstraintTransitionThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CSplineConstraintTransitionThinkDelegate>(address);
        }
        return CSplineConstraintTransitionThinkUnmanagedFunction;
    }

    internal static Guid HookCSplineConstraintTransitionThink()
    {
        CSplineConstraintTransitionThinkHookGuid = CSplineConstraintTransitionThinkGetUnmanagedFunction().AddHook(next => (a1) => CSplineConstraintTransitionThinkPipeline(a1, () => next()(a1)));
        return CSplineConstraintTransitionThinkHookGuid;
    }

    internal static Guid UnhookCSplineConstraintTransitionThink()
    {
        CSplineConstraintTransitionThinkGetUnmanagedFunction().RemoveHook(CSplineConstraintTransitionThinkHookGuid);
        return Guid.Empty;
    }

    private static void CSplineConstraintTransitionThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CSplineConstraint>(a1);

            var preCtx = new CSplineConstraintTransitionThinkPreContext { SchemaObject = schemaObject };
            InvokeCSplineConstraintTransitionThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CSplineConstraintTransitionThinkPostContext { SchemaObject = schemaObject };
            InvokeCSplineConstraintTransitionThinkPost(ref postCtx);
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

    internal static void InvokeCSplineConstraintTransitionThink(nint a1)
    {
        CSplineConstraintTransitionThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCSplineConstraintTransitionThinkPre(ref CSplineConstraintTransitionThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSplineConstraintTransitionThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCSplineConstraintTransitionThinkPost(ref CSplineConstraintTransitionThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSplineConstraintTransitionThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CSplineConstraintTransitionThinkHook : ICSplineConstraintTransitionThinkHook
{
    private event OnCSplineConstraintTransitionThinkPreDelegate? _Pre;
    private event OnCSplineConstraintTransitionThinkPostDelegate? _Post;

    public event OnCSplineConstraintTransitionThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSplineConstraintTransitionThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSplineConstraintTransitionThink);
            }
        }
    }

    public event OnCSplineConstraintTransitionThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSplineConstraintTransitionThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSplineConstraintTransitionThink);
            }
        }
    }

    public void InvokePre(ref CSplineConstraintTransitionThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CSplineConstraintTransitionThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSplineConstraintTransitionThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSplineConstraintTransitionThink);
        }
    }

    public void Invoke(CSplineConstraint schemaObject) => DatamapHooksPublisher.InvokeCSplineConstraintTransitionThink(schemaObject.Address);
}