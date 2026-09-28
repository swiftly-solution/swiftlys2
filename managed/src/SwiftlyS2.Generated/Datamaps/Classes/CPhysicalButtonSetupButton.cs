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
    private delegate void CPhysicalButtonSetupButtonDelegate(nint a1);

    private static IUnmanagedFunction<CPhysicalButtonSetupButtonDelegate>? CPhysicalButtonSetupButtonUnmanagedFunction;
    private static Guid CPhysicalButtonSetupButtonHookGuid;

    private static IUnmanagedFunction<CPhysicalButtonSetupButtonDelegate> CPhysicalButtonSetupButtonGetUnmanagedFunction()
    {
        if (CPhysicalButtonSetupButtonUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPhysicalButton", "CPhysicalButtonSetupButton");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPhysicalButton::CPhysicalButtonSetupButton.");
            }
            CPhysicalButtonSetupButtonUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPhysicalButtonSetupButtonDelegate>(address);
        }
        return CPhysicalButtonSetupButtonUnmanagedFunction;
    }

    internal static Guid HookCPhysicalButtonSetupButton()
    {
        CPhysicalButtonSetupButtonHookGuid = CPhysicalButtonSetupButtonGetUnmanagedFunction().AddHook(next => (a1) => CPhysicalButtonSetupButtonPipeline(a1, () => next()(a1)));
        return CPhysicalButtonSetupButtonHookGuid;
    }

    internal static Guid UnhookCPhysicalButtonSetupButton()
    {
        CPhysicalButtonSetupButtonGetUnmanagedFunction().RemoveHook(CPhysicalButtonSetupButtonHookGuid);
        return Guid.Empty;
    }

    private static void CPhysicalButtonSetupButtonPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPhysicalButton>(a1);

            var preCtx = new CPhysicalButtonSetupButtonPreContext { SchemaObject = schemaObject };
            InvokeCPhysicalButtonSetupButtonPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPhysicalButtonSetupButtonPostContext { SchemaObject = schemaObject };
            InvokeCPhysicalButtonSetupButtonPost(ref postCtx);
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

    internal static void InvokeCPhysicalButtonSetupButton(nint a1)
    {
        CPhysicalButtonSetupButtonGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPhysicalButtonSetupButtonPre(ref CPhysicalButtonSetupButtonPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysicalButtonSetupButtonPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPhysicalButtonSetupButtonPost(ref CPhysicalButtonSetupButtonPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysicalButtonSetupButtonPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPhysicalButtonSetupButtonHook : ICPhysicalButtonSetupButtonHook
{
    private event OnCPhysicalButtonSetupButtonPreDelegate? _Pre;
    private event OnCPhysicalButtonSetupButtonPostDelegate? _Post;

    public event OnCPhysicalButtonSetupButtonPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysicalButtonSetupButton);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonSetupButton);
            }
        }
    }

    public event OnCPhysicalButtonSetupButtonPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysicalButtonSetupButton);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonSetupButton);
            }
        }
    }

    public void InvokePre(ref CPhysicalButtonSetupButtonPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPhysicalButtonSetupButtonPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonSetupButton);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonSetupButton);
        }
    }

    public void Invoke(CPhysicalButton schemaObject) => DatamapHooksPublisher.InvokeCPhysicalButtonSetupButton(schemaObject.Address);
}