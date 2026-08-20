using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseButtonButtonSparkPreContext
{
    public CBaseButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseButtonButtonSparkPostContext
{
    public CBaseButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseButtonButtonSparkPreDelegate(ref CBaseButtonButtonSparkPreContext ctx);
public delegate void OnCBaseButtonButtonSparkPostDelegate(ref CBaseButtonButtonSparkPostContext ctx);

public interface ICBaseButtonButtonSparkHook
{
    public event OnCBaseButtonButtonSparkPreDelegate Pre;
    public event OnCBaseButtonButtonSparkPostDelegate Post;

    public void Invoke(CBaseButton schemaObject);
}