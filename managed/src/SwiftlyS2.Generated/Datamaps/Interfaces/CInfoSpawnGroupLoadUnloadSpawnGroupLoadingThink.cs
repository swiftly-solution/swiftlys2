using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPreContext
{
    public CInfoSpawnGroupLoadUnload SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPostContext
{
    public CInfoSpawnGroupLoadUnload SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPreDelegate(ref CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPreContext ctx);
public delegate void OnCInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPostDelegate(ref CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPostContext ctx);

public interface ICInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkHook
{
    public event OnCInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPreDelegate Pre;
    public event OnCInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPostDelegate Post;

    public void Invoke(CInfoSpawnGroupLoadUnload schemaObject);
}