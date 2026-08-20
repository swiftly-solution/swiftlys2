using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CMomentaryRotButtonUpdateThinkPreContext
{
    public CMomentaryRotButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CMomentaryRotButtonUpdateThinkPostContext
{
    public CMomentaryRotButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCMomentaryRotButtonUpdateThinkPreDelegate(ref CMomentaryRotButtonUpdateThinkPreContext ctx);
public delegate void OnCMomentaryRotButtonUpdateThinkPostDelegate(ref CMomentaryRotButtonUpdateThinkPostContext ctx);

public interface ICMomentaryRotButtonUpdateThinkHook
{
    public event OnCMomentaryRotButtonUpdateThinkPreDelegate Pre;
    public event OnCMomentaryRotButtonUpdateThinkPostDelegate Post;

    public void Invoke(CMomentaryRotButton schemaObject);
}