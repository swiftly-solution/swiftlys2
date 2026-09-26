using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CDecoyProjectileGunfireThinkPreContext
{
    public CDecoyProjectile SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CDecoyProjectileGunfireThinkPostContext
{
    public CDecoyProjectile SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCDecoyProjectileGunfireThinkPreDelegate(ref CDecoyProjectileGunfireThinkPreContext ctx);
public delegate void OnCDecoyProjectileGunfireThinkPostDelegate(ref CDecoyProjectileGunfireThinkPostContext ctx);

public interface ICDecoyProjectileGunfireThinkHook
{
    public event OnCDecoyProjectileGunfireThinkPreDelegate Pre;
    public event OnCDecoyProjectileGunfireThinkPostDelegate Post;

    public void Invoke(CDecoyProjectile schemaObject);
}