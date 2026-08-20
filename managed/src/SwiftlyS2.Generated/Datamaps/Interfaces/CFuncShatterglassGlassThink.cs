using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CFuncShatterglassGlassThinkPreContext
{
    public CFuncShatterglass SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CFuncShatterglassGlassThinkPostContext
{
    public CFuncShatterglass SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCFuncShatterglassGlassThinkPreDelegate(ref CFuncShatterglassGlassThinkPreContext ctx);
public delegate void OnCFuncShatterglassGlassThinkPostDelegate(ref CFuncShatterglassGlassThinkPostContext ctx);

public interface ICFuncShatterglassGlassThinkHook
{
    public event OnCFuncShatterglassGlassThinkPreDelegate Pre;
    public event OnCFuncShatterglassGlassThinkPostDelegate Post;

    public void Invoke(CFuncShatterglass schemaObject);
}