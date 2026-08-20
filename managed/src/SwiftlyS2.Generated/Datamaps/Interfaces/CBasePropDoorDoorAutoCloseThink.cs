using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBasePropDoorDoorAutoCloseThinkPreContext
{
    public CBasePropDoor SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBasePropDoorDoorAutoCloseThinkPostContext
{
    public CBasePropDoor SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBasePropDoorDoorAutoCloseThinkPreDelegate(ref CBasePropDoorDoorAutoCloseThinkPreContext ctx);
public delegate void OnCBasePropDoorDoorAutoCloseThinkPostDelegate(ref CBasePropDoorDoorAutoCloseThinkPostContext ctx);

public interface ICBasePropDoorDoorAutoCloseThinkHook
{
    public event OnCBasePropDoorDoorAutoCloseThinkPreDelegate Pre;
    public event OnCBasePropDoorDoorAutoCloseThinkPostDelegate Post;

    public void Invoke(CBasePropDoor schemaObject);
}