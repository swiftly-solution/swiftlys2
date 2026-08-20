using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseDoorCloseAreaPortalsThinkPreContext
{
    public CBaseDoor SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseDoorCloseAreaPortalsThinkPostContext
{
    public CBaseDoor SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseDoorCloseAreaPortalsThinkPreDelegate(ref CBaseDoorCloseAreaPortalsThinkPreContext ctx);
public delegate void OnCBaseDoorCloseAreaPortalsThinkPostDelegate(ref CBaseDoorCloseAreaPortalsThinkPostContext ctx);

public interface ICBaseDoorCloseAreaPortalsThinkHook
{
    public event OnCBaseDoorCloseAreaPortalsThinkPreDelegate Pre;
    public event OnCBaseDoorCloseAreaPortalsThinkPostDelegate Post;

    public void Invoke(CBaseDoor schemaObject);
}