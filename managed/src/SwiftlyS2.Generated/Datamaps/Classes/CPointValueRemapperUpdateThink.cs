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
    private delegate void CPointValueRemapperUpdateThinkDelegate(nint a1);

    private static IUnmanagedFunction<CPointValueRemapperUpdateThinkDelegate>? CPointValueRemapperUpdateThinkUnmanagedFunction;
    private static Guid CPointValueRemapperUpdateThinkHookGuid;

    private static IUnmanagedFunction<CPointValueRemapperUpdateThinkDelegate> CPointValueRemapperUpdateThinkGetUnmanagedFunction()
    {
        if (CPointValueRemapperUpdateThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPointValueRemapper", "CPointValueRemapperUpdateThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPointValueRemapper::CPointValueRemapperUpdateThink.");
            }
            CPointValueRemapperUpdateThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPointValueRemapperUpdateThinkDelegate>(address);
        }
        return CPointValueRemapperUpdateThinkUnmanagedFunction;
    }

    internal static Guid HookCPointValueRemapperUpdateThink()
    {
        CPointValueRemapperUpdateThinkHookGuid = CPointValueRemapperUpdateThinkGetUnmanagedFunction().AddHook(next => (a1) => CPointValueRemapperUpdateThinkPipeline(a1, () => next()(a1)));
        return CPointValueRemapperUpdateThinkHookGuid;
    }

    internal static Guid UnhookCPointValueRemapperUpdateThink()
    {
        CPointValueRemapperUpdateThinkGetUnmanagedFunction().RemoveHook(CPointValueRemapperUpdateThinkHookGuid);
        return Guid.Empty;
    }

    private static void CPointValueRemapperUpdateThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPointValueRemapper>(a1);

            var preCtx = new CPointValueRemapperUpdateThinkPreContext { SchemaObject = schemaObject };
            InvokeCPointValueRemapperUpdateThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPointValueRemapperUpdateThinkPostContext { SchemaObject = schemaObject };
            InvokeCPointValueRemapperUpdateThinkPost(ref postCtx);
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

    internal static void InvokeCPointValueRemapperUpdateThink(nint a1)
    {
        CPointValueRemapperUpdateThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPointValueRemapperUpdateThinkPre(ref CPointValueRemapperUpdateThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPointValueRemapperUpdateThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPointValueRemapperUpdateThinkPost(ref CPointValueRemapperUpdateThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPointValueRemapperUpdateThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPointValueRemapperUpdateThinkHook : ICPointValueRemapperUpdateThinkHook
{
    private event OnCPointValueRemapperUpdateThinkPreDelegate? _Pre;
    private event OnCPointValueRemapperUpdateThinkPostDelegate? _Post;

    public event OnCPointValueRemapperUpdateThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPointValueRemapperUpdateThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointValueRemapperUpdateThink);
            }
        }
    }

    public event OnCPointValueRemapperUpdateThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPointValueRemapperUpdateThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointValueRemapperUpdateThink);
            }
        }
    }

    public void InvokePre(ref CPointValueRemapperUpdateThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPointValueRemapperUpdateThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointValueRemapperUpdateThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointValueRemapperUpdateThink);
        }
    }

    public void Invoke(CPointValueRemapper schemaObject) => DatamapHooksPublisher.InvokeCPointValueRemapperUpdateThink(schemaObject.Address);
}