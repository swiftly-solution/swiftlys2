using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseButtonButtonBackHomePreContext
{
    public CBaseButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseButtonButtonBackHomePostContext
{
    public CBaseButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseButtonButtonBackHomePreDelegate(ref CBaseButtonButtonBackHomePreContext ctx);
public delegate void OnCBaseButtonButtonBackHomePostDelegate(ref CBaseButtonButtonBackHomePostContext ctx);

public interface ICBaseButtonButtonBackHomeHook
{
    public event OnCBaseButtonButtonBackHomePreDelegate Pre;
    public event OnCBaseButtonButtonBackHomePostDelegate Post;

    public void Invoke(CBaseButton schemaObject);
}