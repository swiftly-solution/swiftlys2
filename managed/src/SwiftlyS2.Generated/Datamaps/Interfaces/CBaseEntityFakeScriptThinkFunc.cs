using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseEntityFakeScriptThinkFuncPreContext
{
    public CBaseEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseEntityFakeScriptThinkFuncPostContext
{
    public CBaseEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseEntityFakeScriptThinkFuncPreDelegate(ref CBaseEntityFakeScriptThinkFuncPreContext ctx);
public delegate void OnCBaseEntityFakeScriptThinkFuncPostDelegate(ref CBaseEntityFakeScriptThinkFuncPostContext ctx);

public interface ICBaseEntityFakeScriptThinkFuncHook
{
    public event OnCBaseEntityFakeScriptThinkFuncPreDelegate Pre;
    public event OnCBaseEntityFakeScriptThinkFuncPostDelegate Post;

    public void Invoke(CBaseEntity schemaObject);
}