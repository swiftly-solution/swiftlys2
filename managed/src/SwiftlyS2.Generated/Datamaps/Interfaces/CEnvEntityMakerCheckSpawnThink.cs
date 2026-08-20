using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CEnvEntityMakerCheckSpawnThinkPreContext
{
    public CEnvEntityMaker SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CEnvEntityMakerCheckSpawnThinkPostContext
{
    public CEnvEntityMaker SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCEnvEntityMakerCheckSpawnThinkPreDelegate(ref CEnvEntityMakerCheckSpawnThinkPreContext ctx);
public delegate void OnCEnvEntityMakerCheckSpawnThinkPostDelegate(ref CEnvEntityMakerCheckSpawnThinkPostContext ctx);

public interface ICEnvEntityMakerCheckSpawnThinkHook
{
    public event OnCEnvEntityMakerCheckSpawnThinkPreDelegate Pre;
    public event OnCEnvEntityMakerCheckSpawnThinkPostDelegate Post;

    public void Invoke(CEnvEntityMaker schemaObject);
}