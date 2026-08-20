using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CMomentaryRotButtonUseMoveDonePreContext
{
    public CMomentaryRotButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CMomentaryRotButtonUseMoveDonePostContext
{
    public CMomentaryRotButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCMomentaryRotButtonUseMoveDonePreDelegate(ref CMomentaryRotButtonUseMoveDonePreContext ctx);
public delegate void OnCMomentaryRotButtonUseMoveDonePostDelegate(ref CMomentaryRotButtonUseMoveDonePostContext ctx);

public interface ICMomentaryRotButtonUseMoveDoneHook
{
    public event OnCMomentaryRotButtonUseMoveDonePreDelegate Pre;
    public event OnCMomentaryRotButtonUseMoveDonePostDelegate Post;

    public void Invoke(CMomentaryRotButton schemaObject);
}