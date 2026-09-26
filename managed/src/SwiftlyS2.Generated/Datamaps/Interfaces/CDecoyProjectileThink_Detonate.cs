using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CDecoyProjectileThink_DetonatePreContext
{
    public CDecoyProjectile SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CDecoyProjectileThink_DetonatePostContext
{
    public CDecoyProjectile SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCDecoyProjectileThink_DetonatePreDelegate(ref CDecoyProjectileThink_DetonatePreContext ctx);
public delegate void OnCDecoyProjectileThink_DetonatePostDelegate(ref CDecoyProjectileThink_DetonatePostContext ctx);

public interface ICDecoyProjectileThink_DetonateHook
{
    public event OnCDecoyProjectileThink_DetonatePreDelegate Pre;
    public event OnCDecoyProjectileThink_DetonatePostDelegate Post;

    public void Invoke(CDecoyProjectile schemaObject);
}