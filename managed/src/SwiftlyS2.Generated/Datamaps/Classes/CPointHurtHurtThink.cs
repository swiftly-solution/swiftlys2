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
    private delegate void CPointHurtHurtThinkDelegate(nint a1);

    private static IUnmanagedFunction<CPointHurtHurtThinkDelegate>? CPointHurtHurtThinkUnmanagedFunction;
    private static Guid CPointHurtHurtThinkHookGuid;

    private static IUnmanagedFunction<CPointHurtHurtThinkDelegate> CPointHurtHurtThinkGetUnmanagedFunction()
    {
        if (CPointHurtHurtThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPointHurt", "CPointHurtHurtThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPointHurt::CPointHurtHurtThink.");
            }
            CPointHurtHurtThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPointHurtHurtThinkDelegate>(address);
        }
        return CPointHurtHurtThinkUnmanagedFunction;
    }

    internal static Guid HookCPointHurtHurtThink()
    {
        CPointHurtHurtThinkHookGuid = CPointHurtHurtThinkGetUnmanagedFunction().AddHook(next => (a1) => CPointHurtHurtThinkPipeline(a1, () => next()(a1)));
        return CPointHurtHurtThinkHookGuid;
    }

    internal static Guid UnhookCPointHurtHurtThink()
    {
        CPointHurtHurtThinkGetUnmanagedFunction().RemoveHook(CPointHurtHurtThinkHookGuid);
        return Guid.Empty;
    }

    private static void CPointHurtHurtThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPointHurt>(a1);

            var preCtx = new CPointHurtHurtThinkPreContext { SchemaObject = schemaObject };
            InvokeCPointHurtHurtThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPointHurtHurtThinkPostContext { SchemaObject = schemaObject };
            InvokeCPointHurtHurtThinkPost(ref postCtx);
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

    internal static void InvokeCPointHurtHurtThink(nint a1)
    {
        CPointHurtHurtThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPointHurtHurtThinkPre(ref CPointHurtHurtThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPointHurtHurtThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPointHurtHurtThinkPost(ref CPointHurtHurtThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPointHurtHurtThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPointHurtHurtThinkHook : ICPointHurtHurtThinkHook
{
    private event OnCPointHurtHurtThinkPreDelegate? _Pre;
    private event OnCPointHurtHurtThinkPostDelegate? _Post;

    public event OnCPointHurtHurtThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPointHurtHurtThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointHurtHurtThink);
            }
        }
    }

    public event OnCPointHurtHurtThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPointHurtHurtThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointHurtHurtThink);
            }
        }
    }

    public void InvokePre(ref CPointHurtHurtThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPointHurtHurtThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointHurtHurtThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointHurtHurtThink);
        }
    }

    public void Invoke(CPointHurt schemaObject) => DatamapHooksPublisher.InvokeCPointHurtHurtThink(schemaObject.Address);
}