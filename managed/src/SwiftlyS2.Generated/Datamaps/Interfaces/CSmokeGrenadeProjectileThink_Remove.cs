using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CSmokeGrenadeProjectileThink_RemovePreContext
{
    public CSmokeGrenadeProjectile SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CSmokeGrenadeProjectileThink_RemovePostContext
{
    public CSmokeGrenadeProjectile SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCSmokeGrenadeProjectileThink_RemovePreDelegate(ref CSmokeGrenadeProjectileThink_RemovePreContext ctx);
public delegate void OnCSmokeGrenadeProjectileThink_RemovePostDelegate(ref CSmokeGrenadeProjectileThink_RemovePostContext ctx);

public interface ICSmokeGrenadeProjectileThink_RemoveHook
{
    public event OnCSmokeGrenadeProjectileThink_RemovePreDelegate Pre;
    public event OnCSmokeGrenadeProjectileThink_RemovePostDelegate Post;

    public void Invoke(CSmokeGrenadeProjectile schemaObject);
}