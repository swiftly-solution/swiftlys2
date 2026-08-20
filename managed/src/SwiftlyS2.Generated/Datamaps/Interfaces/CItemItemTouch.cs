using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CItemItemTouchPreContext
{
    public CItem SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CItemItemTouchPostContext
{
    public CItem SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCItemItemTouchPreDelegate(ref CItemItemTouchPreContext ctx);
public delegate void OnCItemItemTouchPostDelegate(ref CItemItemTouchPostContext ctx);

public interface ICItemItemTouchHook
{
    public event OnCItemItemTouchPreDelegate Pre;
    public event OnCItemItemTouchPostDelegate Post;

    public void Invoke(CItem schemaObject);
}