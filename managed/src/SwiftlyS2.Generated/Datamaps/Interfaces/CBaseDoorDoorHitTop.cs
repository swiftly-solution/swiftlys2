using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseDoorDoorHitTopPreContext
{
    public CBaseDoor SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseDoorDoorHitTopPostContext
{
    public CBaseDoor SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseDoorDoorHitTopPreDelegate(ref CBaseDoorDoorHitTopPreContext ctx);
public delegate void OnCBaseDoorDoorHitTopPostDelegate(ref CBaseDoorDoorHitTopPostContext ctx);

public interface ICBaseDoorDoorHitTopHook
{
    public event OnCBaseDoorDoorHitTopPreDelegate Pre;
    public event OnCBaseDoorDoorHitTopPostDelegate Post;

    public void Invoke(CBaseDoor schemaObject);
}