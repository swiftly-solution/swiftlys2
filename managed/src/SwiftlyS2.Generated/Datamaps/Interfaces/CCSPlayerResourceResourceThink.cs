using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CCSPlayerResourceResourceThinkPreContext
{
    public CCSPlayerResource SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CCSPlayerResourceResourceThinkPostContext
{
    public CCSPlayerResource SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCCSPlayerResourceResourceThinkPreDelegate(ref CCSPlayerResourceResourceThinkPreContext ctx);
public delegate void OnCCSPlayerResourceResourceThinkPostDelegate(ref CCSPlayerResourceResourceThinkPostContext ctx);

public interface ICCSPlayerResourceResourceThinkHook
{
    public event OnCCSPlayerResourceResourceThinkPreDelegate Pre;
    public event OnCCSPlayerResourceResourceThinkPostDelegate Post;

    public void Invoke(CCSPlayerResource schemaObject);
}