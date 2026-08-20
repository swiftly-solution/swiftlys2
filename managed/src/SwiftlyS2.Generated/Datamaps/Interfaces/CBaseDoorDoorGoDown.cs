using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseDoorDoorGoDownPreContext
{
    public CBaseDoor SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseDoorDoorGoDownPostContext
{
    public CBaseDoor SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseDoorDoorGoDownPreDelegate(ref CBaseDoorDoorGoDownPreContext ctx);
public delegate void OnCBaseDoorDoorGoDownPostDelegate(ref CBaseDoorDoorGoDownPostContext ctx);

public interface ICBaseDoorDoorGoDownHook
{
    public event OnCBaseDoorDoorGoDownPreDelegate Pre;
    public event OnCBaseDoorDoorGoDownPostDelegate Post;

    public void Invoke(CBaseDoor schemaObject);
}