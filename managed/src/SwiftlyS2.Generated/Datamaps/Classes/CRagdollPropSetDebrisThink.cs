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
    private delegate void CRagdollPropSetDebrisThinkDelegate(nint a1);

    private static IUnmanagedFunction<CRagdollPropSetDebrisThinkDelegate>? CRagdollPropSetDebrisThinkUnmanagedFunction;
    private static Guid CRagdollPropSetDebrisThinkHookGuid;

    private static IUnmanagedFunction<CRagdollPropSetDebrisThinkDelegate> CRagdollPropSetDebrisThinkGetUnmanagedFunction()
    {
        if (CRagdollPropSetDebrisThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CRagdollProp", "CRagdollPropSetDebrisThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CRagdollProp::CRagdollPropSetDebrisThink.");
            }
            CRagdollPropSetDebrisThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CRagdollPropSetDebrisThinkDelegate>(address);
        }
        return CRagdollPropSetDebrisThinkUnmanagedFunction;
    }

    internal static Guid HookCRagdollPropSetDebrisThink()
    {
        CRagdollPropSetDebrisThinkHookGuid = CRagdollPropSetDebrisThinkGetUnmanagedFunction().AddHook(next => (a1) => CRagdollPropSetDebrisThinkPipeline(a1, () => next()(a1)));
        return CRagdollPropSetDebrisThinkHookGuid;
    }

    internal static Guid UnhookCRagdollPropSetDebrisThink()
    {
        CRagdollPropSetDebrisThinkGetUnmanagedFunction().RemoveHook(CRagdollPropSetDebrisThinkHookGuid);
        return Guid.Empty;
    }

    private static void CRagdollPropSetDebrisThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CRagdollProp>(a1);

            var preCtx = new CRagdollPropSetDebrisThinkPreContext { SchemaObject = schemaObject };
            InvokeCRagdollPropSetDebrisThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CRagdollPropSetDebrisThinkPostContext { SchemaObject = schemaObject };
            InvokeCRagdollPropSetDebrisThinkPost(ref postCtx);
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

    internal static void InvokeCRagdollPropSetDebrisThink(nint a1)
    {
        CRagdollPropSetDebrisThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCRagdollPropSetDebrisThinkPre(ref CRagdollPropSetDebrisThinkPreContext ctx)
    {
        var subs = GetSubscribers(DatamapHookListener.CRagdollPropSetDebrisThink);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeCRagdollPropSetDebrisThinkPre(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
            {
                return;
            }
        }
    }

    internal static void InvokeCRagdollPropSetDebrisThinkPost(ref CRagdollPropSetDebrisThinkPostContext ctx)
    {
        var subs = GetSubscribers(DatamapHookListener.CRagdollPropSetDebrisThink);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeCRagdollPropSetDebrisThinkPost(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
            {
                return;
            }
        }
    }
}

internal sealed class CRagdollPropSetDebrisThinkHook(GameHooksService owner) : ICRagdollPropSetDebrisThinkHook
{
    private event OnCRagdollPropSetDebrisThinkPreDelegate? _Pre;
    private event OnCRagdollPropSetDebrisThinkPostDelegate? _Post;

    public event OnCRagdollPropSetDebrisThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CRagdollPropSetDebrisThink, owner);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CRagdollPropSetDebrisThink, owner);
            }
        }
    }

    public event OnCRagdollPropSetDebrisThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CRagdollPropSetDebrisThink, owner);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CRagdollPropSetDebrisThink, owner);
            }
        }
    }

    public void InvokePre(ref CRagdollPropSetDebrisThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CRagdollPropSetDebrisThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CRagdollPropSetDebrisThink, owner);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CRagdollPropSetDebrisThink, owner);
        }
    }

    public void Invoke(CRagdollProp schemaObject) => DatamapHooksPublisher.InvokeCRagdollPropSetDebrisThink(schemaObject.Address);
}