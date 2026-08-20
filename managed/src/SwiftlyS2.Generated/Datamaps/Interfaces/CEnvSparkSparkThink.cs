using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CEnvSparkSparkThinkPreContext
{
    public CEnvSpark SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CEnvSparkSparkThinkPostContext
{
    public CEnvSpark SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCEnvSparkSparkThinkPreDelegate(ref CEnvSparkSparkThinkPreContext ctx);
public delegate void OnCEnvSparkSparkThinkPostDelegate(ref CEnvSparkSparkThinkPostContext ctx);

public interface ICEnvSparkSparkThinkHook
{
    public event OnCEnvSparkSparkThinkPreDelegate Pre;
    public event OnCEnvSparkSparkThinkPostDelegate Post;

    public void Invoke(CEnvSpark schemaObject);
}