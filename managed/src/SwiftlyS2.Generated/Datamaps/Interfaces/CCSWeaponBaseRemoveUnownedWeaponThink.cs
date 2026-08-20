using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CCSWeaponBaseRemoveUnownedWeaponThinkPreContext
{
    public CCSWeaponBase SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CCSWeaponBaseRemoveUnownedWeaponThinkPostContext
{
    public CCSWeaponBase SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCCSWeaponBaseRemoveUnownedWeaponThinkPreDelegate(ref CCSWeaponBaseRemoveUnownedWeaponThinkPreContext ctx);
public delegate void OnCCSWeaponBaseRemoveUnownedWeaponThinkPostDelegate(ref CCSWeaponBaseRemoveUnownedWeaponThinkPostContext ctx);

public interface ICCSWeaponBaseRemoveUnownedWeaponThinkHook
{
    public event OnCCSWeaponBaseRemoveUnownedWeaponThinkPreDelegate Pre;
    public event OnCCSWeaponBaseRemoveUnownedWeaponThinkPostDelegate Post;

    public void Invoke(CCSWeaponBase schemaObject);
}