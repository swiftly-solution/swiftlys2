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
    private delegate void CTriggerLerpObjectUnsetWaitForEntityDelegate(nint a1);

    private static IUnmanagedFunction<CTriggerLerpObjectUnsetWaitForEntityDelegate>? CTriggerLerpObjectUnsetWaitForEntityUnmanagedFunction;
    private static Guid CTriggerLerpObjectUnsetWaitForEntityHookGuid;

    private static IUnmanagedFunction<CTriggerLerpObjectUnsetWaitForEntityDelegate> CTriggerLerpObjectUnsetWaitForEntityGetUnmanagedFunction()
    {
        if (CTriggerLerpObjectUnsetWaitForEntityUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CTriggerLerpObject", "CTriggerLerpObjectUnsetWaitForEntity");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CTriggerLerpObject::CTriggerLerpObjectUnsetWaitForEntity.");
            }
            CTriggerLerpObjectUnsetWaitForEntityUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CTriggerLerpObjectUnsetWaitForEntityDelegate>(address);
        }
        return CTriggerLerpObjectUnsetWaitForEntityUnmanagedFunction;
    }

    internal static Guid HookCTriggerLerpObjectUnsetWaitForEntity()
    {
        CTriggerLerpObjectUnsetWaitForEntityHookGuid = CTriggerLerpObjectUnsetWaitForEntityGetUnmanagedFunction().AddHook(next => (a1) => CTriggerLerpObjectUnsetWaitForEntityPipeline(a1, () => next()(a1)));
        return CTriggerLerpObjectUnsetWaitForEntityHookGuid;
    }

    internal static Guid UnhookCTriggerLerpObjectUnsetWaitForEntity()
    {
        CTriggerLerpObjectUnsetWaitForEntityGetUnmanagedFunction().RemoveHook(CTriggerLerpObjectUnsetWaitForEntityHookGuid);
        return Guid.Empty;
    }

    private static void CTriggerLerpObjectUnsetWaitForEntityPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CTriggerLerpObject>(a1);

            var preCtx = new CTriggerLerpObjectUnsetWaitForEntityPreContext { SchemaObject = schemaObject };
            InvokeCTriggerLerpObjectUnsetWaitForEntityPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CTriggerLerpObjectUnsetWaitForEntityPostContext { SchemaObject = schemaObject };
            InvokeCTriggerLerpObjectUnsetWaitForEntityPost(ref postCtx);
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

    internal static void InvokeCTriggerLerpObjectUnsetWaitForEntity(nint a1)
    {
        CTriggerLerpObjectUnsetWaitForEntityGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCTriggerLerpObjectUnsetWaitForEntityPre(ref CTriggerLerpObjectUnsetWaitForEntityPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerLerpObjectUnsetWaitForEntityPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCTriggerLerpObjectUnsetWaitForEntityPost(ref CTriggerLerpObjectUnsetWaitForEntityPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerLerpObjectUnsetWaitForEntityPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CTriggerLerpObjectUnsetWaitForEntityHook : ICTriggerLerpObjectUnsetWaitForEntityHook
{
    private event OnCTriggerLerpObjectUnsetWaitForEntityPreDelegate? _Pre;
    private event OnCTriggerLerpObjectUnsetWaitForEntityPostDelegate? _Post;

    public event OnCTriggerLerpObjectUnsetWaitForEntityPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerLerpObjectUnsetWaitForEntity);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerLerpObjectUnsetWaitForEntity);
            }
        }
    }

    public event OnCTriggerLerpObjectUnsetWaitForEntityPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerLerpObjectUnsetWaitForEntity);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerLerpObjectUnsetWaitForEntity);
            }
        }
    }

    public void InvokePre(ref CTriggerLerpObjectUnsetWaitForEntityPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CTriggerLerpObjectUnsetWaitForEntityPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerLerpObjectUnsetWaitForEntity);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerLerpObjectUnsetWaitForEntity);
        }
    }

    public void Invoke(CTriggerLerpObject schemaObject) => DatamapHooksPublisher.InvokeCTriggerLerpObjectUnsetWaitForEntity(schemaObject.Address);
}