using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBasePropDoorDoorCloseMoveDonePreContext
{
    public CBasePropDoor SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBasePropDoorDoorCloseMoveDonePostContext
{
    public CBasePropDoor SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBasePropDoorDoorCloseMoveDonePreDelegate(ref CBasePropDoorDoorCloseMoveDonePreContext ctx);
public delegate void OnCBasePropDoorDoorCloseMoveDonePostDelegate(ref CBasePropDoorDoorCloseMoveDonePostContext ctx);

public interface ICBasePropDoorDoorCloseMoveDoneHook
{
    public event OnCBasePropDoorDoorCloseMoveDonePreDelegate Pre;
    public event OnCBasePropDoorDoorCloseMoveDonePostDelegate Post;

    public void Invoke(CBasePropDoor schemaObject);
}