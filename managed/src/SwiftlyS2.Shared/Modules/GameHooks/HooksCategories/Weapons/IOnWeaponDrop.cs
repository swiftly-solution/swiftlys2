using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public struct WeaponDropParams
{
    public required IPlayer Player { get; set; }
    public required CBasePlayerWeapon? Weapon { get; init; }
    public required bool SwappingWeapon { get; init; }
}

public ref struct WeaponDropPreContext
{
    public WeaponDropParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct WeaponDropPostContext
{
    public WeaponDropParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnWeaponDropPreDelegate( ref WeaponDropPreContext ctx );
public delegate void OnWeaponDropPostDelegate( ref WeaponDropPostContext ctx );

public interface IWeaponDropHook
{
    public event OnWeaponDropPreDelegate Pre;
    public event OnWeaponDropPostDelegate Post;
}
