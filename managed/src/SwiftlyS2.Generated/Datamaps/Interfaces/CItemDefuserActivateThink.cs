using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CItemDefuserActivateThinkPreContext
{
    public CItemDefuser SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CItemDefuserActivateThinkPostContext
{
    public CItemDefuser SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCItemDefuserActivateThinkPreDelegate(ref CItemDefuserActivateThinkPreContext ctx);
public delegate void OnCItemDefuserActivateThinkPostDelegate(ref CItemDefuserActivateThinkPostContext ctx);

public interface ICItemDefuserActivateThinkHook
{
    public event OnCItemDefuserActivateThinkPreDelegate Pre;
    public event OnCItemDefuserActivateThinkPostDelegate Post;

    public void Invoke(CItemDefuser schemaObject);
}