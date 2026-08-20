using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CCSWeaponBaseDefaultTouchPreContext
{
    public CCSWeaponBase SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CCSWeaponBaseDefaultTouchPostContext
{
    public CCSWeaponBase SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCCSWeaponBaseDefaultTouchPreDelegate(ref CCSWeaponBaseDefaultTouchPreContext ctx);
public delegate void OnCCSWeaponBaseDefaultTouchPostDelegate(ref CCSWeaponBaseDefaultTouchPostContext ctx);

public interface ICCSWeaponBaseDefaultTouchHook
{
    public event OnCCSWeaponBaseDefaultTouchPreDelegate Pre;
    public event OnCCSWeaponBaseDefaultTouchPostDelegate Post;

    public void Invoke(CCSWeaponBase schemaObject);
}