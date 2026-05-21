using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public struct CanUseWeaponParams
{
    public required IPlayer Player { get; set; }
    public required CCSWeaponBase Weapon { get; init; }
}

public ref struct CanUseWeaponPreContext
{
    public CanUseWeaponParams Params;
    private bool _return;
    private bool _returnSet;
    private HookResult _hookResult;

    public void SetReturn( bool result ) { _return = result; _returnSet = true; }
    public void SetHookResult( HookResult result ) => _hookResult = result;

    internal bool IsReturnSet => _returnSet;
    internal bool Return => _return;
    internal HookResult HookResult => _hookResult;
}

public ref struct CanUseWeaponPostContext
{
    public CanUseWeaponParams Params;
    public bool Return { get; set; }
    private HookResult _hookResult;

    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCanUseWeaponPreDelegate( ref CanUseWeaponPreContext ctx );
public delegate void OnCanUseWeaponPostDelegate( ref CanUseWeaponPostContext ctx );

public interface ICanUseWeaponHook
{
    public event OnCanUseWeaponPreDelegate Pre;
    public event OnCanUseWeaponPostDelegate Post;
}
