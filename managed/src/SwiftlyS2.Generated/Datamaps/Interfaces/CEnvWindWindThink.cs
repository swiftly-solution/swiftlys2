using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CEnvWindWindThinkPreContext
{
    public CEnvWind SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CEnvWindWindThinkPostContext
{
    public CEnvWind SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCEnvWindWindThinkPreDelegate(ref CEnvWindWindThinkPreContext ctx);
public delegate void OnCEnvWindWindThinkPostDelegate(ref CEnvWindWindThinkPostContext ctx);

public interface ICEnvWindWindThinkHook
{
    public event OnCEnvWindWindThinkPreDelegate Pre;
    public event OnCEnvWindWindThinkPostDelegate Post;

    public void Invoke(CEnvWind schemaObject);
}