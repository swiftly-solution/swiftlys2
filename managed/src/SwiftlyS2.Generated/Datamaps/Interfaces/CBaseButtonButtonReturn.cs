using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseButtonButtonReturnPreContext
{
    public CBaseButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseButtonButtonReturnPostContext
{
    public CBaseButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseButtonButtonReturnPreDelegate(ref CBaseButtonButtonReturnPreContext ctx);
public delegate void OnCBaseButtonButtonReturnPostDelegate(ref CBaseButtonButtonReturnPostContext ctx);

public interface ICBaseButtonButtonReturnHook
{
    public event OnCBaseButtonButtonReturnPreDelegate Pre;
    public event OnCBaseButtonButtonReturnPostDelegate Post;

    public void Invoke(CBaseButton schemaObject);
}