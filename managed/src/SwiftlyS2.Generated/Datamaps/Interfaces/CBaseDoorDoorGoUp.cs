using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseDoorDoorGoUpPreContext
{
    public CBaseDoor SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseDoorDoorGoUpPostContext
{
    public CBaseDoor SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseDoorDoorGoUpPreDelegate(ref CBaseDoorDoorGoUpPreContext ctx);
public delegate void OnCBaseDoorDoorGoUpPostDelegate(ref CBaseDoorDoorGoUpPostContext ctx);

public interface ICBaseDoorDoorGoUpHook
{
    public event OnCBaseDoorDoorGoUpPreDelegate Pre;
    public event OnCBaseDoorDoorGoUpPostDelegate Post;

    public void Invoke(CBaseDoor schemaObject);
}