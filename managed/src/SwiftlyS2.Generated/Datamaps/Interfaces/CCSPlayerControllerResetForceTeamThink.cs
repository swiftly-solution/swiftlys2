using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CCSPlayerControllerResetForceTeamThinkPreContext
{
    public CCSPlayerController SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CCSPlayerControllerResetForceTeamThinkPostContext
{
    public CCSPlayerController SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCCSPlayerControllerResetForceTeamThinkPreDelegate(ref CCSPlayerControllerResetForceTeamThinkPreContext ctx);
public delegate void OnCCSPlayerControllerResetForceTeamThinkPostDelegate(ref CCSPlayerControllerResetForceTeamThinkPostContext ctx);

public interface ICCSPlayerControllerResetForceTeamThinkHook
{
    public event OnCCSPlayerControllerResetForceTeamThinkPreDelegate Pre;
    public event OnCCSPlayerControllerResetForceTeamThinkPostDelegate Post;

    public void Invoke(CCSPlayerController schemaObject);
}