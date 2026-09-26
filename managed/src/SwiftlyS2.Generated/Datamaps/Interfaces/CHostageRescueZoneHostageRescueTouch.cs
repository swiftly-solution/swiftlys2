using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CHostageRescueZoneHostageRescueTouchPreContext
{
    public CHostageRescueZone SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CHostageRescueZoneHostageRescueTouchPostContext
{
    public CHostageRescueZone SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCHostageRescueZoneHostageRescueTouchPreDelegate(ref CHostageRescueZoneHostageRescueTouchPreContext ctx);
public delegate void OnCHostageRescueZoneHostageRescueTouchPostDelegate(ref CHostageRescueZoneHostageRescueTouchPostContext ctx);

public interface ICHostageRescueZoneHostageRescueTouchHook
{
    public event OnCHostageRescueZoneHostageRescueTouchPreDelegate Pre;
    public event OnCHostageRescueZoneHostageRescueTouchPostDelegate Post;

    public void Invoke(CHostageRescueZone schemaObject);
}