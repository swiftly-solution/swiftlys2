using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseButtonButtonUsePreContext
{
    public CBaseButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseButtonButtonUsePostContext
{
    public CBaseButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseButtonButtonUsePreDelegate(ref CBaseButtonButtonUsePreContext ctx);
public delegate void OnCBaseButtonButtonUsePostDelegate(ref CBaseButtonButtonUsePostContext ctx);

public interface ICBaseButtonButtonUseHook
{
    public event OnCBaseButtonButtonUsePreDelegate Pre;
    public event OnCBaseButtonButtonUsePostDelegate Post;

    public void Invoke(CBaseButton schemaObject);
}