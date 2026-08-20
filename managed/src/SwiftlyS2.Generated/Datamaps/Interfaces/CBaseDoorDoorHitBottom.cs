using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseDoorDoorHitBottomPreContext
{
    public CBaseDoor SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseDoorDoorHitBottomPostContext
{
    public CBaseDoor SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseDoorDoorHitBottomPreDelegate(ref CBaseDoorDoorHitBottomPreContext ctx);
public delegate void OnCBaseDoorDoorHitBottomPostDelegate(ref CBaseDoorDoorHitBottomPostContext ctx);

public interface ICBaseDoorDoorHitBottomHook
{
    public event OnCBaseDoorDoorHitBottomPreDelegate Pre;
    public event OnCBaseDoorDoorHitBottomPostDelegate Post;

    public void Invoke(CBaseDoor schemaObject);
}