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
    private delegate void CMultiSourceRegisterDelegate(nint a1);

    private static IUnmanagedFunction<CMultiSourceRegisterDelegate>? CMultiSourceRegisterUnmanagedFunction;
    private static Guid CMultiSourceRegisterHookGuid;

    private static IUnmanagedFunction<CMultiSourceRegisterDelegate> CMultiSourceRegisterGetUnmanagedFunction()
    {
        if (CMultiSourceRegisterUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CMultiSource", "CMultiSourceRegister");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CMultiSource::CMultiSourceRegister.");
            }
            CMultiSourceRegisterUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CMultiSourceRegisterDelegate>(address);
        }
        return CMultiSourceRegisterUnmanagedFunction;
    }

    internal static Guid HookCMultiSourceRegister()
    {
        CMultiSourceRegisterHookGuid = CMultiSourceRegisterGetUnmanagedFunction().AddHook(next => (a1) => CMultiSourceRegisterPipeline(a1, () => next()(a1)));
        return CMultiSourceRegisterHookGuid;
    }

    internal static Guid UnhookCMultiSourceRegister()
    {
        CMultiSourceRegisterGetUnmanagedFunction().RemoveHook(CMultiSourceRegisterHookGuid);
        return Guid.Empty;
    }

    private static void CMultiSourceRegisterPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CMultiSource>(a1);

            var preCtx = new CMultiSourceRegisterPreContext { SchemaObject = schemaObject };
            InvokeCMultiSourceRegisterPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CMultiSourceRegisterPostContext { SchemaObject = schemaObject };
            InvokeCMultiSourceRegisterPost(ref postCtx);
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

    internal static void InvokeCMultiSourceRegister(nint a1)
    {
        CMultiSourceRegisterGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCMultiSourceRegisterPre(ref CMultiSourceRegisterPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCMultiSourceRegisterPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCMultiSourceRegisterPost(ref CMultiSourceRegisterPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCMultiSourceRegisterPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CMultiSourceRegisterHook : ICMultiSourceRegisterHook
{
    private event OnCMultiSourceRegisterPreDelegate? _Pre;
    private event OnCMultiSourceRegisterPostDelegate? _Post;

    public event OnCMultiSourceRegisterPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CMultiSourceRegister);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMultiSourceRegister);
            }
        }
    }

    public event OnCMultiSourceRegisterPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CMultiSourceRegister);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMultiSourceRegister);
            }
        }
    }

    public void InvokePre(ref CMultiSourceRegisterPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CMultiSourceRegisterPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMultiSourceRegister);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMultiSourceRegister);
        }
    }

    public void Invoke(CMultiSource schemaObject) => DatamapHooksPublisher.InvokeCMultiSourceRegister(schemaObject.Address);
}