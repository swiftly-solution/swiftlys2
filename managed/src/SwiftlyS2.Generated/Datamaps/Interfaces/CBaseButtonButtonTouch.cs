using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseButtonButtonTouchPreContext
{
    public CBaseButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseButtonButtonTouchPostContext
{
    public CBaseButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseButtonButtonTouchPreDelegate(ref CBaseButtonButtonTouchPreContext ctx);
public delegate void OnCBaseButtonButtonTouchPostDelegate(ref CBaseButtonButtonTouchPostContext ctx);

public interface ICBaseButtonButtonTouchHook
{
    public event OnCBaseButtonButtonTouchPreDelegate Pre;
    public event OnCBaseButtonButtonTouchPostDelegate Post;

    public void Invoke(CBaseButton schemaObject);
}