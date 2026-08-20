using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPreContext
{
    public CInfoSpawnGroupLoadUnload SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPostContext
{
    public CInfoSpawnGroupLoadUnload SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPreDelegate(ref CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPreContext ctx);
public delegate void OnCInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPostDelegate(ref CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPostContext ctx);

public interface ICInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkHook
{
    public event OnCInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPreDelegate Pre;
    public event OnCInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPostDelegate Post;

    public void Invoke(CInfoSpawnGroupLoadUnload schemaObject);
}