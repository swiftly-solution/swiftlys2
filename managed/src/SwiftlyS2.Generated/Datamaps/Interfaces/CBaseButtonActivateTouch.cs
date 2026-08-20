using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseButtonActivateTouchPreContext
{
    public CBaseButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseButtonActivateTouchPostContext
{
    public CBaseButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseButtonActivateTouchPreDelegate(ref CBaseButtonActivateTouchPreContext ctx);
public delegate void OnCBaseButtonActivateTouchPostDelegate(ref CBaseButtonActivateTouchPostContext ctx);

public interface ICBaseButtonActivateTouchHook
{
    public event OnCBaseButtonActivateTouchPreDelegate Pre;
    public event OnCBaseButtonActivateTouchPostDelegate Post;

    public void Invoke(CBaseButton schemaObject);
}