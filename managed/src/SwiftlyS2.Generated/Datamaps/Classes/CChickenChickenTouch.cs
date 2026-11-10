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
    private delegate void CChickenChickenTouchDelegate(nint a1);

    private static IUnmanagedFunction<CChickenChickenTouchDelegate>? CChickenChickenTouchUnmanagedFunction;
    private static Guid CChickenChickenTouchHookGuid;

    private static IUnmanagedFunction<CChickenChickenTouchDelegate> CChickenChickenTouchGetUnmanagedFunction()
    {
        if (CChickenChickenTouchUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CChicken", "CChickenChickenTouch");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CChicken::CChickenChickenTouch.");
            }
            CChickenChickenTouchUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CChickenChickenTouchDelegate>(address);
        }
        return CChickenChickenTouchUnmanagedFunction;
    }

    internal static Guid HookCChickenChickenTouch()
    {
        CChickenChickenTouchHookGuid = CChickenChickenTouchGetUnmanagedFunction().AddHook(next => (a1) => CChickenChickenTouchPipeline(a1, () => next()(a1)));
        return CChickenChickenTouchHookGuid;
    }

    internal static Guid UnhookCChickenChickenTouch()
    {
        CChickenChickenTouchGetUnmanagedFunction().RemoveHook(CChickenChickenTouchHookGuid);
        return Guid.Empty;
    }

    private static void CChickenChickenTouchPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CChicken>(a1);

            var preCtx = new CChickenChickenTouchPreContext { SchemaObject = schemaObject };
            InvokeCChickenChickenTouchPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CChickenChickenTouchPostContext { SchemaObject = schemaObject };
            InvokeCChickenChickenTouchPost(ref postCtx);
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

    internal static void InvokeCChickenChickenTouch(nint a1)
    {
        CChickenChickenTouchGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCChickenChickenTouchPre(ref CChickenChickenTouchPreContext ctx)
    {
        var subs = GetSubscribers(DatamapHookListener.CChickenChickenTouch);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeCChickenChickenTouchPre(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
            {
                return;
            }
        }
    }

    internal static void InvokeCChickenChickenTouchPost(ref CChickenChickenTouchPostContext ctx)
    {
        var subs = GetSubscribers(DatamapHookListener.CChickenChickenTouch);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeCChickenChickenTouchPost(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
            {
                return;
            }
        }
    }
}

internal sealed class CChickenChickenTouchHook(GameHooksService owner) : ICChickenChickenTouchHook
{
    private event OnCChickenChickenTouchPreDelegate? _Pre;
    private event OnCChickenChickenTouchPostDelegate? _Post;

    public event OnCChickenChickenTouchPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CChickenChickenTouch, owner);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CChickenChickenTouch, owner);
            }
        }
    }

    public event OnCChickenChickenTouchPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CChickenChickenTouch, owner);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CChickenChickenTouch, owner);
            }
        }
    }

    public void InvokePre(ref CChickenChickenTouchPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CChickenChickenTouchPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CChickenChickenTouch, owner);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CChickenChickenTouch, owner);
        }
    }

    public void Invoke(CChicken schemaObject) => DatamapHooksPublisher.InvokeCChickenChickenTouch(schemaObject.Address);
}