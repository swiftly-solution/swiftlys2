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
    private delegate void CPlantedC4C4ThinkDelegate(nint a1);

    private static IUnmanagedFunction<CPlantedC4C4ThinkDelegate>? CPlantedC4C4ThinkUnmanagedFunction;
    private static Guid CPlantedC4C4ThinkHookGuid;

    private static IUnmanagedFunction<CPlantedC4C4ThinkDelegate> CPlantedC4C4ThinkGetUnmanagedFunction()
    {
        if (CPlantedC4C4ThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPlantedC4", "CPlantedC4C4Think");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPlantedC4::CPlantedC4C4Think.");
            }
            CPlantedC4C4ThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPlantedC4C4ThinkDelegate>(address);
        }
        return CPlantedC4C4ThinkUnmanagedFunction;
    }

    internal static Guid HookCPlantedC4C4Think()
    {
        CPlantedC4C4ThinkHookGuid = CPlantedC4C4ThinkGetUnmanagedFunction().AddHook(next => (a1) => CPlantedC4C4ThinkPipeline(a1, () => next()(a1)));
        return CPlantedC4C4ThinkHookGuid;
    }

    internal static Guid UnhookCPlantedC4C4Think()
    {
        CPlantedC4C4ThinkGetUnmanagedFunction().RemoveHook(CPlantedC4C4ThinkHookGuid);
        return Guid.Empty;
    }

    private static void CPlantedC4C4ThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPlantedC4>(a1);

            var preCtx = new CPlantedC4C4ThinkPreContext { SchemaObject = schemaObject };
            InvokeCPlantedC4C4ThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPlantedC4C4ThinkPostContext { SchemaObject = schemaObject };
            InvokeCPlantedC4C4ThinkPost(ref postCtx);
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

    internal static void InvokeCPlantedC4C4Think(nint a1)
    {
        CPlantedC4C4ThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPlantedC4C4ThinkPre(ref CPlantedC4C4ThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPlantedC4C4ThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPlantedC4C4ThinkPost(ref CPlantedC4C4ThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPlantedC4C4ThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPlantedC4C4ThinkHook : ICPlantedC4C4ThinkHook
{
    private event OnCPlantedC4C4ThinkPreDelegate? _Pre;
    private event OnCPlantedC4C4ThinkPostDelegate? _Post;

    public event OnCPlantedC4C4ThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPlantedC4C4Think);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPlantedC4C4Think);
            }
        }
    }

    public event OnCPlantedC4C4ThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPlantedC4C4Think);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPlantedC4C4Think);
            }
        }
    }

    public void InvokePre(ref CPlantedC4C4ThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPlantedC4C4ThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPlantedC4C4Think);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPlantedC4C4Think);
        }
    }

    public void Invoke(CPlantedC4 schemaObject) => DatamapHooksPublisher.InvokeCPlantedC4C4Think(schemaObject.Address);
}