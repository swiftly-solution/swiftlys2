using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseCSGrenadeProjectileDangerSoundThinkPreContext
{
    public CBaseCSGrenadeProjectile SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseCSGrenadeProjectileDangerSoundThinkPostContext
{
    public CBaseCSGrenadeProjectile SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseCSGrenadeProjectileDangerSoundThinkPreDelegate(ref CBaseCSGrenadeProjectileDangerSoundThinkPreContext ctx);
public delegate void OnCBaseCSGrenadeProjectileDangerSoundThinkPostDelegate(ref CBaseCSGrenadeProjectileDangerSoundThinkPostContext ctx);

public interface ICBaseCSGrenadeProjectileDangerSoundThinkHook
{
    public event OnCBaseCSGrenadeProjectileDangerSoundThinkPreDelegate Pre;
    public event OnCBaseCSGrenadeProjectileDangerSoundThinkPostDelegate Post;

    public void Invoke(CBaseCSGrenadeProjectile schemaObject);
}