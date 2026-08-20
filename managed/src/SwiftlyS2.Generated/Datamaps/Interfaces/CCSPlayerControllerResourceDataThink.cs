using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CCSPlayerControllerResourceDataThinkPreContext
{
    public CCSPlayerController SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CCSPlayerControllerResourceDataThinkPostContext
{
    public CCSPlayerController SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCCSPlayerControllerResourceDataThinkPreDelegate(ref CCSPlayerControllerResourceDataThinkPreContext ctx);
public delegate void OnCCSPlayerControllerResourceDataThinkPostDelegate(ref CCSPlayerControllerResourceDataThinkPostContext ctx);

public interface ICCSPlayerControllerResourceDataThinkHook
{
    public event OnCCSPlayerControllerResourceDataThinkPreDelegate Pre;
    public event OnCCSPlayerControllerResourceDataThinkPostDelegate Post;

    public void Invoke(CCSPlayerController schemaObject);
}