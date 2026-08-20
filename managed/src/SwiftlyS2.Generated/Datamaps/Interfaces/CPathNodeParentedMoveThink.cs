using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPathNodeParentedMoveThinkPreContext
{
    public CPathNode SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPathNodeParentedMoveThinkPostContext
{
    public CPathNode SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPathNodeParentedMoveThinkPreDelegate(ref CPathNodeParentedMoveThinkPreContext ctx);
public delegate void OnCPathNodeParentedMoveThinkPostDelegate(ref CPathNodeParentedMoveThinkPostContext ctx);

public interface ICPathNodeParentedMoveThinkHook
{
    public event OnCPathNodeParentedMoveThinkPreDelegate Pre;
    public event OnCPathNodeParentedMoveThinkPostDelegate Post;

    public void Invoke(CPathNode schemaObject);
}