using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CSmokeGrenadeProjectileThink_BuildingSmokeVolumePreContext
{
    public CSmokeGrenadeProjectile SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CSmokeGrenadeProjectileThink_BuildingSmokeVolumePostContext
{
    public CSmokeGrenadeProjectile SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCSmokeGrenadeProjectileThink_BuildingSmokeVolumePreDelegate(ref CSmokeGrenadeProjectileThink_BuildingSmokeVolumePreContext ctx);
public delegate void OnCSmokeGrenadeProjectileThink_BuildingSmokeVolumePostDelegate(ref CSmokeGrenadeProjectileThink_BuildingSmokeVolumePostContext ctx);

public interface ICSmokeGrenadeProjectileThink_BuildingSmokeVolumeHook
{
    public event OnCSmokeGrenadeProjectileThink_BuildingSmokeVolumePreDelegate Pre;
    public event OnCSmokeGrenadeProjectileThink_BuildingSmokeVolumePostDelegate Post;

    public void Invoke(CSmokeGrenadeProjectile schemaObject);
}