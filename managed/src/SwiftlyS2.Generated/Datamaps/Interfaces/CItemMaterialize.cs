using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CItemMaterializePreContext
{
    public CItem SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CItemMaterializePostContext
{
    public CItem SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCItemMaterializePreDelegate(ref CItemMaterializePreContext ctx);
public delegate void OnCItemMaterializePostDelegate(ref CItemMaterializePostContext ctx);

public interface ICItemMaterializeHook
{
    public event OnCItemMaterializePreDelegate Pre;
    public event OnCItemMaterializePostDelegate Post;

    public void Invoke(CItem schemaObject);
}