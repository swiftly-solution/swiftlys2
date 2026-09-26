using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CVoteControllerVoteControllerThinkPreContext
{
    public CVoteController SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CVoteControllerVoteControllerThinkPostContext
{
    public CVoteController SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCVoteControllerVoteControllerThinkPreDelegate(ref CVoteControllerVoteControllerThinkPreContext ctx);
public delegate void OnCVoteControllerVoteControllerThinkPostDelegate(ref CVoteControllerVoteControllerThinkPostContext ctx);

public interface ICVoteControllerVoteControllerThinkHook
{
    public event OnCVoteControllerVoteControllerThinkPreDelegate Pre;
    public event OnCVoteControllerVoteControllerThinkPostDelegate Post;

    public void Invoke(CVoteController schemaObject);
}