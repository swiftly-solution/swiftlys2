using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CCSPlayerPawnPushawayThinkPreContext
{
    public CCSPlayerPawn SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CCSPlayerPawnPushawayThinkPostContext
{
    public CCSPlayerPawn SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCCSPlayerPawnPushawayThinkPreDelegate(ref CCSPlayerPawnPushawayThinkPreContext ctx);
public delegate void OnCCSPlayerPawnPushawayThinkPostDelegate(ref CCSPlayerPawnPushawayThinkPostContext ctx);

public interface ICCSPlayerPawnPushawayThinkHook
{
    public event OnCCSPlayerPawnPushawayThinkPreDelegate Pre;
    public event OnCCSPlayerPawnPushawayThinkPostDelegate Post;

    public void Invoke(CCSPlayerPawn schemaObject);
}