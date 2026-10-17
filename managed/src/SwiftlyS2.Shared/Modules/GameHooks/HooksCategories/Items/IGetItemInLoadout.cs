using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public struct GetItemInLoadoutParams
{
    public required IPlayer Player { get; init; }
    public required Team Team { get; init; }
    public required loadout_slot_t Slot { get; init; }
}

public ref struct GetItemInLoadoutPreContext
{
    public GetItemInLoadoutParams Params;
    private CEconItemView? _return;
    private bool _returnSet;
    private HookResult _hookResult;

    /// <summary>
    /// Sets the item the game gets back. Only used when the hook result is Stop or CancelOriginal.
    /// </summary>
    public void SetReturn( CEconItemView item ) { _return = item; _returnSet = true; }
    public void SetHookResult( HookResult result ) => _hookResult = result;

    internal bool IsReturnSet => _returnSet;
    internal CEconItemView? Return => _return;
    internal HookResult HookResult => _hookResult;
}

public ref struct GetItemInLoadoutPostContext
{
    public GetItemInLoadoutParams Params;

    /// <summary>
    /// The item the game gets back, null when the slot is empty. Replace it to answer with another item.
    /// The object is only valid during the callback; keep the item's address instead if you need it later.
    /// </summary>
    public CEconItemView? Return { get; set; }
    private HookResult _hookResult;

    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnGetItemInLoadoutPreDelegate( ref GetItemInLoadoutPreContext ctx );
public delegate void OnGetItemInLoadoutPostDelegate( ref GetItemInLoadoutPostContext ctx );

public interface IGetItemInLoadoutHook
{
    /// <summary>
    /// Throws when "FollowCS2ServerGuidelines" is enabled in core.jsonc.
    /// </summary>
    public event OnGetItemInLoadoutPreDelegate Pre;

    /// <summary>
    /// Throws when "FollowCS2ServerGuidelines" is enabled in core.jsonc.
    /// </summary>
    public event OnGetItemInLoadoutPostDelegate Post;
}
