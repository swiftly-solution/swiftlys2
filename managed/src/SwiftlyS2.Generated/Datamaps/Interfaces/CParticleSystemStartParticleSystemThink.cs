using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CParticleSystemStartParticleSystemThinkPreContext
{
    public CParticleSystem SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CParticleSystemStartParticleSystemThinkPostContext
{
    public CParticleSystem SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCParticleSystemStartParticleSystemThinkPreDelegate(ref CParticleSystemStartParticleSystemThinkPreContext ctx);
public delegate void OnCParticleSystemStartParticleSystemThinkPostDelegate(ref CParticleSystemStartParticleSystemThinkPostContext ctx);

public interface ICParticleSystemStartParticleSystemThinkHook
{
    public event OnCParticleSystemStartParticleSystemThinkPreDelegate Pre;
    public event OnCParticleSystemStartParticleSystemThinkPostDelegate Post;

    public void Invoke(CParticleSystem schemaObject);
}