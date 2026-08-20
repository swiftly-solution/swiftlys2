using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBasePropDoorDoorOpenMoveDonePreContext
{
    public CBasePropDoor SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBasePropDoorDoorOpenMoveDonePostContext
{
    public CBasePropDoor SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBasePropDoorDoorOpenMoveDonePreDelegate(ref CBasePropDoorDoorOpenMoveDonePreContext ctx);
public delegate void OnCBasePropDoorDoorOpenMoveDonePostDelegate(ref CBasePropDoorDoorOpenMoveDonePostContext ctx);

public interface ICBasePropDoorDoorOpenMoveDoneHook
{
    public event OnCBasePropDoorDoorOpenMoveDonePreDelegate Pre;
    public event OnCBasePropDoorDoorOpenMoveDonePostDelegate Post;

    public void Invoke(CBasePropDoor schemaObject);
}