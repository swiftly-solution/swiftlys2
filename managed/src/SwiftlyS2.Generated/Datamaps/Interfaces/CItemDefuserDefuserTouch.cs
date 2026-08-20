using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CItemDefuserDefuserTouchPreContext
{
    public CItemDefuser SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CItemDefuserDefuserTouchPostContext
{
    public CItemDefuser SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCItemDefuserDefuserTouchPreDelegate(ref CItemDefuserDefuserTouchPreContext ctx);
public delegate void OnCItemDefuserDefuserTouchPostDelegate(ref CItemDefuserDefuserTouchPostContext ctx);

public interface ICItemDefuserDefuserTouchHook
{
    public event OnCItemDefuserDefuserTouchPreDelegate Pre;
    public event OnCItemDefuserDefuserTouchPostDelegate Post;

    public void Invoke(CItemDefuser schemaObject);
}