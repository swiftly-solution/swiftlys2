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
    private delegate void CBaseEntityFakeScriptThinkFuncDelegate(nint a1);

    private static IUnmanagedFunction<CBaseEntityFakeScriptThinkFuncDelegate>? CBaseEntityFakeScriptThinkFuncUnmanagedFunction;
    private static Guid CBaseEntityFakeScriptThinkFuncHookGuid;

    private static IUnmanagedFunction<CBaseEntityFakeScriptThinkFuncDelegate> CBaseEntityFakeScriptThinkFuncGetUnmanagedFunction()
    {
        if (CBaseEntityFakeScriptThinkFuncUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseEntity", "CBaseEntityFakeScriptThinkFunc");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseEntity::CBaseEntityFakeScriptThinkFunc.");
            }
            CBaseEntityFakeScriptThinkFuncUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseEntityFakeScriptThinkFuncDelegate>(address);
        }
        return CBaseEntityFakeScriptThinkFuncUnmanagedFunction;
    }

    internal static Guid HookCBaseEntityFakeScriptThinkFunc()
    {
        CBaseEntityFakeScriptThinkFuncHookGuid = CBaseEntityFakeScriptThinkFuncGetUnmanagedFunction().AddHook(next => (a1) => CBaseEntityFakeScriptThinkFuncPipeline(a1, () => next()(a1)));
        return CBaseEntityFakeScriptThinkFuncHookGuid;
    }

    internal static Guid UnhookCBaseEntityFakeScriptThinkFunc()
    {
        CBaseEntityFakeScriptThinkFuncGetUnmanagedFunction().RemoveHook(CBaseEntityFakeScriptThinkFuncHookGuid);
        return Guid.Empty;
    }

    private static void CBaseEntityFakeScriptThinkFuncPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseEntity>(a1);

            var preCtx = new CBaseEntityFakeScriptThinkFuncPreContext { SchemaObject = schemaObject };
            InvokeCBaseEntityFakeScriptThinkFuncPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseEntityFakeScriptThinkFuncPostContext { SchemaObject = schemaObject };
            InvokeCBaseEntityFakeScriptThinkFuncPost(ref postCtx);
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

    internal static void InvokeCBaseEntityFakeScriptThinkFunc(nint a1)
    {
        CBaseEntityFakeScriptThinkFuncGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseEntityFakeScriptThinkFuncPre(ref CBaseEntityFakeScriptThinkFuncPreContext ctx)
    {
        var subs = GetSubscribers(DatamapHookListener.CBaseEntityFakeScriptThinkFunc);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeCBaseEntityFakeScriptThinkFuncPre(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
            {
                return;
            }
        }
    }

    internal static void InvokeCBaseEntityFakeScriptThinkFuncPost(ref CBaseEntityFakeScriptThinkFuncPostContext ctx)
    {
        var subs = GetSubscribers(DatamapHookListener.CBaseEntityFakeScriptThinkFunc);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeCBaseEntityFakeScriptThinkFuncPost(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
            {
                return;
            }
        }
    }
}

internal sealed class CBaseEntityFakeScriptThinkFuncHook(GameHooksService owner) : ICBaseEntityFakeScriptThinkFuncHook
{
    private event OnCBaseEntityFakeScriptThinkFuncPreDelegate? _Pre;
    private event OnCBaseEntityFakeScriptThinkFuncPostDelegate? _Post;

    public event OnCBaseEntityFakeScriptThinkFuncPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseEntityFakeScriptThinkFunc, owner);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseEntityFakeScriptThinkFunc, owner);
            }
        }
    }

    public event OnCBaseEntityFakeScriptThinkFuncPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseEntityFakeScriptThinkFunc, owner);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseEntityFakeScriptThinkFunc, owner);
            }
        }
    }

    public void InvokePre(ref CBaseEntityFakeScriptThinkFuncPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseEntityFakeScriptThinkFuncPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseEntityFakeScriptThinkFunc, owner);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseEntityFakeScriptThinkFunc, owner);
        }
    }

    public void Invoke(CBaseEntity schemaObject) => DatamapHooksPublisher.InvokeCBaseEntityFakeScriptThinkFunc(schemaObject.Address);
}