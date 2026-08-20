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
    private delegate void CGenericConstraintUpdateThinkDelegate(nint a1);

    private static IUnmanagedFunction<CGenericConstraintUpdateThinkDelegate>? CGenericConstraintUpdateThinkUnmanagedFunction;
    private static Guid CGenericConstraintUpdateThinkHookGuid;

    private static IUnmanagedFunction<CGenericConstraintUpdateThinkDelegate> CGenericConstraintUpdateThinkGetUnmanagedFunction()
    {
        if (CGenericConstraintUpdateThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CGenericConstraint", "CGenericConstraintUpdateThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CGenericConstraint::CGenericConstraintUpdateThink.");
            }
            CGenericConstraintUpdateThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CGenericConstraintUpdateThinkDelegate>(address);
        }
        return CGenericConstraintUpdateThinkUnmanagedFunction;
    }

    internal static Guid HookCGenericConstraintUpdateThink()
    {
        CGenericConstraintUpdateThinkHookGuid = CGenericConstraintUpdateThinkGetUnmanagedFunction().AddHook(next => (a1) => CGenericConstraintUpdateThinkPipeline(a1, () => next()(a1)));
        return CGenericConstraintUpdateThinkHookGuid;
    }

    internal static Guid UnhookCGenericConstraintUpdateThink()
    {
        CGenericConstraintUpdateThinkGetUnmanagedFunction().RemoveHook(CGenericConstraintUpdateThinkHookGuid);
        return Guid.Empty;
    }

    private static void CGenericConstraintUpdateThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CGenericConstraint>(a1);

            var preCtx = new CGenericConstraintUpdateThinkPreContext { SchemaObject = schemaObject };
            InvokeCGenericConstraintUpdateThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CGenericConstraintUpdateThinkPostContext { SchemaObject = schemaObject };
            InvokeCGenericConstraintUpdateThinkPost(ref postCtx);
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

    internal static void InvokeCGenericConstraintUpdateThink(nint a1)
    {
        CGenericConstraintUpdateThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCGenericConstraintUpdateThinkPre(ref CGenericConstraintUpdateThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCGenericConstraintUpdateThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCGenericConstraintUpdateThinkPost(ref CGenericConstraintUpdateThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCGenericConstraintUpdateThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CGenericConstraintUpdateThinkHook : ICGenericConstraintUpdateThinkHook
{
    private event OnCGenericConstraintUpdateThinkPreDelegate? _Pre;
    private event OnCGenericConstraintUpdateThinkPostDelegate? _Post;

    public event OnCGenericConstraintUpdateThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CGenericConstraintUpdateThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CGenericConstraintUpdateThink);
            }
        }
    }

    public event OnCGenericConstraintUpdateThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CGenericConstraintUpdateThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CGenericConstraintUpdateThink);
            }
        }
    }

    public void InvokePre(ref CGenericConstraintUpdateThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CGenericConstraintUpdateThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CGenericConstraintUpdateThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CGenericConstraintUpdateThink);
        }
    }

    public void Invoke(CGenericConstraint schemaObject) => DatamapHooksPublisher.InvokeCGenericConstraintUpdateThink(schemaObject.Address);
}