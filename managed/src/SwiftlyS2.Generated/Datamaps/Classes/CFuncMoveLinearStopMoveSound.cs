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
    private delegate void CFuncMoveLinearStopMoveSoundDelegate(nint a1);

    private static IUnmanagedFunction<CFuncMoveLinearStopMoveSoundDelegate>? CFuncMoveLinearStopMoveSoundUnmanagedFunction;
    private static Guid CFuncMoveLinearStopMoveSoundHookGuid;

    private static IUnmanagedFunction<CFuncMoveLinearStopMoveSoundDelegate> CFuncMoveLinearStopMoveSoundGetUnmanagedFunction()
    {
        if (CFuncMoveLinearStopMoveSoundUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CFuncMoveLinear", "CFuncMoveLinearStopMoveSound");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CFuncMoveLinear::CFuncMoveLinearStopMoveSound.");
            }
            CFuncMoveLinearStopMoveSoundUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CFuncMoveLinearStopMoveSoundDelegate>(address);
        }
        return CFuncMoveLinearStopMoveSoundUnmanagedFunction;
    }

    internal static Guid HookCFuncMoveLinearStopMoveSound()
    {
        CFuncMoveLinearStopMoveSoundHookGuid = CFuncMoveLinearStopMoveSoundGetUnmanagedFunction().AddHook(next => (a1) => CFuncMoveLinearStopMoveSoundPipeline(a1, () => next()(a1)));
        return CFuncMoveLinearStopMoveSoundHookGuid;
    }

    internal static Guid UnhookCFuncMoveLinearStopMoveSound()
    {
        CFuncMoveLinearStopMoveSoundGetUnmanagedFunction().RemoveHook(CFuncMoveLinearStopMoveSoundHookGuid);
        return Guid.Empty;
    }

    private static void CFuncMoveLinearStopMoveSoundPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CFuncMoveLinear>(a1);

            var preCtx = new CFuncMoveLinearStopMoveSoundPreContext { SchemaObject = schemaObject };
            InvokeCFuncMoveLinearStopMoveSoundPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CFuncMoveLinearStopMoveSoundPostContext { SchemaObject = schemaObject };
            InvokeCFuncMoveLinearStopMoveSoundPost(ref postCtx);
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

    internal static void InvokeCFuncMoveLinearStopMoveSound(nint a1)
    {
        CFuncMoveLinearStopMoveSoundGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCFuncMoveLinearStopMoveSoundPre(ref CFuncMoveLinearStopMoveSoundPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncMoveLinearStopMoveSoundPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCFuncMoveLinearStopMoveSoundPost(ref CFuncMoveLinearStopMoveSoundPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncMoveLinearStopMoveSoundPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CFuncMoveLinearStopMoveSoundHook : ICFuncMoveLinearStopMoveSoundHook
{
    private event OnCFuncMoveLinearStopMoveSoundPreDelegate? _Pre;
    private event OnCFuncMoveLinearStopMoveSoundPostDelegate? _Post;

    public event OnCFuncMoveLinearStopMoveSoundPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncMoveLinearStopMoveSound);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncMoveLinearStopMoveSound);
            }
        }
    }

    public event OnCFuncMoveLinearStopMoveSoundPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncMoveLinearStopMoveSound);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncMoveLinearStopMoveSound);
            }
        }
    }

    public void InvokePre(ref CFuncMoveLinearStopMoveSoundPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CFuncMoveLinearStopMoveSoundPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncMoveLinearStopMoveSound);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncMoveLinearStopMoveSound);
        }
    }

    public void Invoke(CFuncMoveLinear schemaObject) => DatamapHooksPublisher.InvokeCFuncMoveLinearStopMoveSound(schemaObject.Address);
}