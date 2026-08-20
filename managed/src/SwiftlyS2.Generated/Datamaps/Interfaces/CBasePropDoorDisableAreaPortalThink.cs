using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBasePropDoorDisableAreaPortalThinkPreContext
{
    public CBasePropDoor SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBasePropDoorDisableAreaPortalThinkPostContext
{
    public CBasePropDoor SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBasePropDoorDisableAreaPortalThinkPreDelegate(ref CBasePropDoorDisableAreaPortalThinkPreContext ctx);
public delegate void OnCBasePropDoorDisableAreaPortalThinkPostDelegate(ref CBasePropDoorDisableAreaPortalThinkPostContext ctx);

public interface ICBasePropDoorDisableAreaPortalThinkHook
{
    public event OnCBasePropDoorDisableAreaPortalThinkPreDelegate Pre;
    public event OnCBasePropDoorDisableAreaPortalThinkPostDelegate Post;

    public void Invoke(CBasePropDoor schemaObject);
}