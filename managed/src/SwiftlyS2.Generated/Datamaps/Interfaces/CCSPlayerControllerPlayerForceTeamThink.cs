using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CCSPlayerControllerPlayerForceTeamThinkPreContext
{
    public CCSPlayerController SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CCSPlayerControllerPlayerForceTeamThinkPostContext
{
    public CCSPlayerController SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCCSPlayerControllerPlayerForceTeamThinkPreDelegate(ref CCSPlayerControllerPlayerForceTeamThinkPreContext ctx);
public delegate void OnCCSPlayerControllerPlayerForceTeamThinkPostDelegate(ref CCSPlayerControllerPlayerForceTeamThinkPostContext ctx);

public interface ICCSPlayerControllerPlayerForceTeamThinkHook
{
    public event OnCCSPlayerControllerPlayerForceTeamThinkPreDelegate Pre;
    public event OnCCSPlayerControllerPlayerForceTeamThinkPostDelegate Post;

    public void Invoke(CCSPlayerController schemaObject);
}