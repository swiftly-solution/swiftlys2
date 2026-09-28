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
    private delegate void CPhysicalButtonButtonReturnDelegate(nint a1);

    private static IUnmanagedFunction<CPhysicalButtonButtonReturnDelegate>? CPhysicalButtonButtonReturnUnmanagedFunction;
    private static Guid CPhysicalButtonButtonReturnHookGuid;

    private static IUnmanagedFunction<CPhysicalButtonButtonReturnDelegate> CPhysicalButtonButtonReturnGetUnmanagedFunction()
    {
        if (CPhysicalButtonButtonReturnUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPhysicalButton", "CPhysicalButtonButtonReturn");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPhysicalButton::CPhysicalButtonButtonReturn.");
            }
            CPhysicalButtonButtonReturnUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPhysicalButtonButtonReturnDelegate>(address);
        }
        return CPhysicalButtonButtonReturnUnmanagedFunction;
    }

    internal static Guid HookCPhysicalButtonButtonReturn()
    {
        CPhysicalButtonButtonReturnHookGuid = CPhysicalButtonButtonReturnGetUnmanagedFunction().AddHook(next => (a1) => CPhysicalButtonButtonReturnPipeline(a1, () => next()(a1)));
        return CPhysicalButtonButtonReturnHookGuid;
    }

    internal static Guid UnhookCPhysicalButtonButtonReturn()
    {
        CPhysicalButtonButtonReturnGetUnmanagedFunction().RemoveHook(CPhysicalButtonButtonReturnHookGuid);
        return Guid.Empty;
    }

    private static void CPhysicalButtonButtonReturnPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPhysicalButton>(a1);

            var preCtx = new CPhysicalButtonButtonReturnPreContext { SchemaObject = schemaObject };
            InvokeCPhysicalButtonButtonReturnPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPhysicalButtonButtonReturnPostContext { SchemaObject = schemaObject };
            InvokeCPhysicalButtonButtonReturnPost(ref postCtx);
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

    internal static void InvokeCPhysicalButtonButtonReturn(nint a1)
    {
        CPhysicalButtonButtonReturnGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPhysicalButtonButtonReturnPre(ref CPhysicalButtonButtonReturnPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysicalButtonButtonReturnPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPhysicalButtonButtonReturnPost(ref CPhysicalButtonButtonReturnPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysicalButtonButtonReturnPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPhysicalButtonButtonReturnHook : ICPhysicalButtonButtonReturnHook
{
    private event OnCPhysicalButtonButtonReturnPreDelegate? _Pre;
    private event OnCPhysicalButtonButtonReturnPostDelegate? _Post;

    public event OnCPhysicalButtonButtonReturnPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysicalButtonButtonReturn);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonButtonReturn);
            }
        }
    }

    public event OnCPhysicalButtonButtonReturnPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysicalButtonButtonReturn);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonButtonReturn);
            }
        }
    }

    public void InvokePre(ref CPhysicalButtonButtonReturnPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPhysicalButtonButtonReturnPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonButtonReturn);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonButtonReturn);
        }
    }

    public void Invoke(CPhysicalButton schemaObject) => DatamapHooksPublisher.InvokeCPhysicalButtonButtonReturn(schemaObject.Address);
}