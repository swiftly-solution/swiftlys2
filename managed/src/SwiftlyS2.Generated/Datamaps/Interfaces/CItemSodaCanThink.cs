using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CItemSodaCanThinkPreContext
{
    public CItemSoda SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CItemSodaCanThinkPostContext
{
    public CItemSoda SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCItemSodaCanThinkPreDelegate(ref CItemSodaCanThinkPreContext ctx);
public delegate void OnCItemSodaCanThinkPostDelegate(ref CItemSodaCanThinkPostContext ctx);

public interface ICItemSodaCanThinkHook
{
    public event OnCItemSodaCanThinkPreDelegate Pre;
    public event OnCItemSodaCanThinkPostDelegate Post;

    public void Invoke(CItemSoda schemaObject);
}