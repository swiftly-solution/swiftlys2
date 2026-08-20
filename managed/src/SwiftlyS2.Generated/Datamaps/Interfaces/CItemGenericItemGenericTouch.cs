using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CItemGenericItemGenericTouchPreContext
{
    public CItemGeneric SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CItemGenericItemGenericTouchPostContext
{
    public CItemGeneric SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCItemGenericItemGenericTouchPreDelegate(ref CItemGenericItemGenericTouchPreContext ctx);
public delegate void OnCItemGenericItemGenericTouchPostDelegate(ref CItemGenericItemGenericTouchPostContext ctx);

public interface ICItemGenericItemGenericTouchHook
{
    public event OnCItemGenericItemGenericTouchPreDelegate Pre;
    public event OnCItemGenericItemGenericTouchPostDelegate Post;

    public void Invoke(CItemGeneric schemaObject);
}