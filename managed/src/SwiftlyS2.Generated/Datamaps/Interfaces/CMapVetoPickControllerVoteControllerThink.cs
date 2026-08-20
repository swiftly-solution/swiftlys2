using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CMapVetoPickControllerVoteControllerThinkPreContext
{
    public CMapVetoPickController SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CMapVetoPickControllerVoteControllerThinkPostContext
{
    public CMapVetoPickController SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCMapVetoPickControllerVoteControllerThinkPreDelegate(ref CMapVetoPickControllerVoteControllerThinkPreContext ctx);
public delegate void OnCMapVetoPickControllerVoteControllerThinkPostDelegate(ref CMapVetoPickControllerVoteControllerThinkPostContext ctx);

public interface ICMapVetoPickControllerVoteControllerThinkHook
{
    public event OnCMapVetoPickControllerVoteControllerThinkPreDelegate Pre;
    public event OnCMapVetoPickControllerVoteControllerThinkPostDelegate Post;

    public void Invoke(CMapVetoPickController schemaObject);
}