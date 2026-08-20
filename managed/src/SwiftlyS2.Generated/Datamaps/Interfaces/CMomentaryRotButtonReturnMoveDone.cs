using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CMomentaryRotButtonReturnMoveDonePreContext
{
    public CMomentaryRotButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CMomentaryRotButtonReturnMoveDonePostContext
{
    public CMomentaryRotButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCMomentaryRotButtonReturnMoveDonePreDelegate(ref CMomentaryRotButtonReturnMoveDonePreContext ctx);
public delegate void OnCMomentaryRotButtonReturnMoveDonePostDelegate(ref CMomentaryRotButtonReturnMoveDonePostContext ctx);

public interface ICMomentaryRotButtonReturnMoveDoneHook
{
    public event OnCMomentaryRotButtonReturnMoveDonePreDelegate Pre;
    public event OnCMomentaryRotButtonReturnMoveDonePostDelegate Post;

    public void Invoke(CMomentaryRotButton schemaObject);
}