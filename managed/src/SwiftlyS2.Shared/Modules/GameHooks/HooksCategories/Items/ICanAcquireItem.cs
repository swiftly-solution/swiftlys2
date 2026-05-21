using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public struct CanAcquireItemParams
{
    public required IPlayer Player { get; set; }
    public required CEconItemView EconItemView { get; init; }
    public required CCSWeaponBaseVData? WeaponVData { get; init; }
    public required AcquireMethod AcquireMethod { get; init; }
}

public ref struct CanAcquireItemPreContext
{
    public CanAcquireItemParams Params;
    private AcquireResult _return;
    private bool _returnSet;
    private HookResult _hookResult;

    public void SetReturn( AcquireResult result ) { _return = result; _returnSet = true; }
    public void SetHookResult( HookResult result ) => _hookResult = result;

    internal bool IsReturnSet => _returnSet;
    internal AcquireResult Return => _return;
    internal HookResult HookResult => _hookResult;
}

public ref struct CanAcquireItemPostContext
{
    public CanAcquireItemParams Params;
    public AcquireResult Return { get; set; }
    private HookResult _hookResult;

    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCanAcquireItemPreDelegate( ref CanAcquireItemPreContext ctx );
public delegate void OnCanAcquireItemPostDelegate( ref CanAcquireItemPostContext ctx );

public interface ICanAcquireItemHook
{
    public event OnCanAcquireItemPreDelegate Pre;
    public event OnCanAcquireItemPostDelegate Post;
}
