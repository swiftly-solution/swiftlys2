using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CEnvWindControllerWindThinkPreContext
{
    public CEnvWindController SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CEnvWindControllerWindThinkPostContext
{
    public CEnvWindController SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCEnvWindControllerWindThinkPreDelegate(ref CEnvWindControllerWindThinkPreContext ctx);
public delegate void OnCEnvWindControllerWindThinkPostDelegate(ref CEnvWindControllerWindThinkPostContext ctx);

public interface ICEnvWindControllerWindThinkHook
{
    public event OnCEnvWindControllerWindThinkPreDelegate Pre;
    public event OnCEnvWindControllerWindThinkPostDelegate Post;

    public void Invoke(CEnvWindController schemaObject);
}