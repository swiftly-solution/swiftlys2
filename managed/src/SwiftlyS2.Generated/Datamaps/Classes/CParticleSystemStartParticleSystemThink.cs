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
    private delegate void CParticleSystemStartParticleSystemThinkDelegate(nint a1);

    private static IUnmanagedFunction<CParticleSystemStartParticleSystemThinkDelegate>? CParticleSystemStartParticleSystemThinkUnmanagedFunction;
    private static Guid CParticleSystemStartParticleSystemThinkHookGuid;

    private static IUnmanagedFunction<CParticleSystemStartParticleSystemThinkDelegate> CParticleSystemStartParticleSystemThinkGetUnmanagedFunction()
    {
        if (CParticleSystemStartParticleSystemThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CParticleSystem", "CParticleSystemStartParticleSystemThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CParticleSystem::CParticleSystemStartParticleSystemThink.");
            }
            CParticleSystemStartParticleSystemThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CParticleSystemStartParticleSystemThinkDelegate>(address);
        }
        return CParticleSystemStartParticleSystemThinkUnmanagedFunction;
    }

    internal static Guid HookCParticleSystemStartParticleSystemThink()
    {
        CParticleSystemStartParticleSystemThinkHookGuid = CParticleSystemStartParticleSystemThinkGetUnmanagedFunction().AddHook(next => (a1) => CParticleSystemStartParticleSystemThinkPipeline(a1, () => next()(a1)));
        return CParticleSystemStartParticleSystemThinkHookGuid;
    }

    internal static Guid UnhookCParticleSystemStartParticleSystemThink()
    {
        CParticleSystemStartParticleSystemThinkGetUnmanagedFunction().RemoveHook(CParticleSystemStartParticleSystemThinkHookGuid);
        return Guid.Empty;
    }

    private static void CParticleSystemStartParticleSystemThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CParticleSystem>(a1);

            var preCtx = new CParticleSystemStartParticleSystemThinkPreContext { SchemaObject = schemaObject };
            InvokeCParticleSystemStartParticleSystemThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CParticleSystemStartParticleSystemThinkPostContext { SchemaObject = schemaObject };
            InvokeCParticleSystemStartParticleSystemThinkPost(ref postCtx);
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

    internal static void InvokeCParticleSystemStartParticleSystemThink(nint a1)
    {
        CParticleSystemStartParticleSystemThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCParticleSystemStartParticleSystemThinkPre(ref CParticleSystemStartParticleSystemThinkPreContext ctx)
    {
        var subs = GetSubscribers(DatamapHookListener.CParticleSystemStartParticleSystemThink);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeCParticleSystemStartParticleSystemThinkPre(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
            {
                return;
            }
        }
    }

    internal static void InvokeCParticleSystemStartParticleSystemThinkPost(ref CParticleSystemStartParticleSystemThinkPostContext ctx)
    {
        var subs = GetSubscribers(DatamapHookListener.CParticleSystemStartParticleSystemThink);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeCParticleSystemStartParticleSystemThinkPost(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
            {
                return;
            }
        }
    }
}

internal sealed class CParticleSystemStartParticleSystemThinkHook(GameHooksService owner) : ICParticleSystemStartParticleSystemThinkHook
{
    private event OnCParticleSystemStartParticleSystemThinkPreDelegate? _Pre;
    private event OnCParticleSystemStartParticleSystemThinkPostDelegate? _Post;

    public event OnCParticleSystemStartParticleSystemThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CParticleSystemStartParticleSystemThink, owner);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CParticleSystemStartParticleSystemThink, owner);
            }
        }
    }

    public event OnCParticleSystemStartParticleSystemThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CParticleSystemStartParticleSystemThink, owner);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CParticleSystemStartParticleSystemThink, owner);
            }
        }
    }

    public void InvokePre(ref CParticleSystemStartParticleSystemThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CParticleSystemStartParticleSystemThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CParticleSystemStartParticleSystemThink, owner);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CParticleSystemStartParticleSystemThink, owner);
        }
    }

    public void Invoke(CParticleSystem schemaObject) => DatamapHooksPublisher.InvokeCParticleSystemStartParticleSystemThink(schemaObject.Address);
}