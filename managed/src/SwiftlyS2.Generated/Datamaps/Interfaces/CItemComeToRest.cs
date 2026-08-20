using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CItemComeToRestPreContext
{
    public CItem SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CItemComeToRestPostContext
{
    public CItem SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCItemComeToRestPreDelegate(ref CItemComeToRestPreContext ctx);
public delegate void OnCItemComeToRestPostDelegate(ref CItemComeToRestPostContext ctx);

public interface ICItemComeToRestHook
{
    public event OnCItemComeToRestPreDelegate Pre;
    public event OnCItemComeToRestPostDelegate Post;

    public void Invoke(CItem schemaObject);
}