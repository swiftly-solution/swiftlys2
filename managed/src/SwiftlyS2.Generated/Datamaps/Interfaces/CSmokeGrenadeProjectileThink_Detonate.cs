using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CSmokeGrenadeProjectileThink_DetonatePreContext
{
    public CSmokeGrenadeProjectile SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CSmokeGrenadeProjectileThink_DetonatePostContext
{
    public CSmokeGrenadeProjectile SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCSmokeGrenadeProjectileThink_DetonatePreDelegate(ref CSmokeGrenadeProjectileThink_DetonatePreContext ctx);
public delegate void OnCSmokeGrenadeProjectileThink_DetonatePostDelegate(ref CSmokeGrenadeProjectileThink_DetonatePostContext ctx);

public interface ICSmokeGrenadeProjectileThink_DetonateHook
{
    public event OnCSmokeGrenadeProjectileThink_DetonatePreDelegate Pre;
    public event OnCSmokeGrenadeProjectileThink_DetonatePostDelegate Post;

    public void Invoke(CSmokeGrenadeProjectile schemaObject);
}