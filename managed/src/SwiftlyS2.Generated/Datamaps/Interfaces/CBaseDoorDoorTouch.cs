using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseDoorDoorTouchPreContext
{
    public CBaseDoor SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseDoorDoorTouchPostContext
{
    public CBaseDoor SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseDoorDoorTouchPreDelegate(ref CBaseDoorDoorTouchPreContext ctx);
public delegate void OnCBaseDoorDoorTouchPostDelegate(ref CBaseDoorDoorTouchPostContext ctx);

public interface ICBaseDoorDoorTouchHook
{
    public event OnCBaseDoorDoorTouchPreDelegate Pre;
    public event OnCBaseDoorDoorTouchPostDelegate Post;

    public void Invoke(CBaseDoor schemaObject);
}