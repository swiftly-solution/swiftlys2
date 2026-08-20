using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CMomentaryRotButtonSetPositionMoveDonePreContext
{
    public CMomentaryRotButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CMomentaryRotButtonSetPositionMoveDonePostContext
{
    public CMomentaryRotButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCMomentaryRotButtonSetPositionMoveDonePreDelegate(ref CMomentaryRotButtonSetPositionMoveDonePreContext ctx);
public delegate void OnCMomentaryRotButtonSetPositionMoveDonePostDelegate(ref CMomentaryRotButtonSetPositionMoveDonePostContext ctx);

public interface ICMomentaryRotButtonSetPositionMoveDoneHook
{
    public event OnCMomentaryRotButtonSetPositionMoveDonePreDelegate Pre;
    public event OnCMomentaryRotButtonSetPositionMoveDonePostDelegate Post;

    public void Invoke(CMomentaryRotButton schemaObject);
}