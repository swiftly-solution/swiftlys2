using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CEntityFlameFlameThinkPreContext
{
    public CEntityFlame SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CEntityFlameFlameThinkPostContext
{
    public CEntityFlame SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCEntityFlameFlameThinkPreDelegate(ref CEntityFlameFlameThinkPreContext ctx);
public delegate void OnCEntityFlameFlameThinkPostDelegate(ref CEntityFlameFlameThinkPostContext ctx);

public interface ICEntityFlameFlameThinkHook
{
    public event OnCEntityFlameFlameThinkPreDelegate Pre;
    public event OnCEntityFlameFlameThinkPostDelegate Post;

    public void Invoke(CEntityFlame schemaObject);
}