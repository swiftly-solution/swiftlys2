using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CSmokeGrenadeProjectileThink_UpdatePreContext
{
    public CSmokeGrenadeProjectile SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CSmokeGrenadeProjectileThink_UpdatePostContext
{
    public CSmokeGrenadeProjectile SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCSmokeGrenadeProjectileThink_UpdatePreDelegate(ref CSmokeGrenadeProjectileThink_UpdatePreContext ctx);
public delegate void OnCSmokeGrenadeProjectileThink_UpdatePostDelegate(ref CSmokeGrenadeProjectileThink_UpdatePostContext ctx);

public interface ICSmokeGrenadeProjectileThink_UpdateHook
{
    public event OnCSmokeGrenadeProjectileThink_UpdatePreDelegate Pre;
    public event OnCSmokeGrenadeProjectileThink_UpdatePostDelegate Post;

    public void Invoke(CSmokeGrenadeProjectile schemaObject);
}