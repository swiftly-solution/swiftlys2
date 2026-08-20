using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseDoorMovingSoundThinkPreContext
{
    public CBaseDoor SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseDoorMovingSoundThinkPostContext
{
    public CBaseDoor SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseDoorMovingSoundThinkPreDelegate(ref CBaseDoorMovingSoundThinkPreContext ctx);
public delegate void OnCBaseDoorMovingSoundThinkPostDelegate(ref CBaseDoorMovingSoundThinkPostContext ctx);

public interface ICBaseDoorMovingSoundThinkHook
{
    public event OnCBaseDoorMovingSoundThinkPreDelegate Pre;
    public event OnCBaseDoorMovingSoundThinkPostDelegate Post;

    public void Invoke(CBaseDoor schemaObject);
}