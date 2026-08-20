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
    private delegate void CScriptedSequenceScriptThinkDelegate(nint a1);

    private static IUnmanagedFunction<CScriptedSequenceScriptThinkDelegate>? CScriptedSequenceScriptThinkUnmanagedFunction;
    private static Guid CScriptedSequenceScriptThinkHookGuid;

    private static IUnmanagedFunction<CScriptedSequenceScriptThinkDelegate> CScriptedSequenceScriptThinkGetUnmanagedFunction()
    {
        if (CScriptedSequenceScriptThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CScriptedSequence", "CScriptedSequenceScriptThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CScriptedSequence::CScriptedSequenceScriptThink.");
            }
            CScriptedSequenceScriptThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CScriptedSequenceScriptThinkDelegate>(address);
        }
        return CScriptedSequenceScriptThinkUnmanagedFunction;
    }

    internal static Guid HookCScriptedSequenceScriptThink()
    {
        CScriptedSequenceScriptThinkHookGuid = CScriptedSequenceScriptThinkGetUnmanagedFunction().AddHook(next => (a1) => CScriptedSequenceScriptThinkPipeline(a1, () => next()(a1)));
        return CScriptedSequenceScriptThinkHookGuid;
    }

    internal static Guid UnhookCScriptedSequenceScriptThink()
    {
        CScriptedSequenceScriptThinkGetUnmanagedFunction().RemoveHook(CScriptedSequenceScriptThinkHookGuid);
        return Guid.Empty;
    }

    private static void CScriptedSequenceScriptThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CScriptedSequence>(a1);

            var preCtx = new CScriptedSequenceScriptThinkPreContext { SchemaObject = schemaObject };
            InvokeCScriptedSequenceScriptThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CScriptedSequenceScriptThinkPostContext { SchemaObject = schemaObject };
            InvokeCScriptedSequenceScriptThinkPost(ref postCtx);
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

    internal static void InvokeCScriptedSequenceScriptThink(nint a1)
    {
        CScriptedSequenceScriptThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCScriptedSequenceScriptThinkPre(ref CScriptedSequenceScriptThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCScriptedSequenceScriptThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCScriptedSequenceScriptThinkPost(ref CScriptedSequenceScriptThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCScriptedSequenceScriptThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CScriptedSequenceScriptThinkHook : ICScriptedSequenceScriptThinkHook
{
    private event OnCScriptedSequenceScriptThinkPreDelegate? _Pre;
    private event OnCScriptedSequenceScriptThinkPostDelegate? _Post;

    public event OnCScriptedSequenceScriptThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CScriptedSequenceScriptThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CScriptedSequenceScriptThink);
            }
        }
    }

    public event OnCScriptedSequenceScriptThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CScriptedSequenceScriptThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CScriptedSequenceScriptThink);
            }
        }
    }

    public void InvokePre(ref CScriptedSequenceScriptThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CScriptedSequenceScriptThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CScriptedSequenceScriptThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CScriptedSequenceScriptThink);
        }
    }

    public void Invoke(CScriptedSequence schemaObject) => DatamapHooksPublisher.InvokeCScriptedSequenceScriptThink(schemaObject.Address);
}