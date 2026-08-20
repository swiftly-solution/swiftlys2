using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CCSPlayerControllerInventoryUpdateThinkPreContext
{
    public CCSPlayerController SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CCSPlayerControllerInventoryUpdateThinkPostContext
{
    public CCSPlayerController SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCCSPlayerControllerInventoryUpdateThinkPreDelegate(ref CCSPlayerControllerInventoryUpdateThinkPreContext ctx);
public delegate void OnCCSPlayerControllerInventoryUpdateThinkPostDelegate(ref CCSPlayerControllerInventoryUpdateThinkPostContext ctx);

public interface ICCSPlayerControllerInventoryUpdateThinkHook
{
    public event OnCCSPlayerControllerInventoryUpdateThinkPreDelegate Pre;
    public event OnCCSPlayerControllerInventoryUpdateThinkPostDelegate Post;

    public void Invoke(CCSPlayerController schemaObject);
}