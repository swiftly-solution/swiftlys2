using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CEnvLaserStrikeThinkPreContext
{
    public CEnvLaser SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CEnvLaserStrikeThinkPostContext
{
    public CEnvLaser SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCEnvLaserStrikeThinkPreDelegate(ref CEnvLaserStrikeThinkPreContext ctx);
public delegate void OnCEnvLaserStrikeThinkPostDelegate(ref CEnvLaserStrikeThinkPostContext ctx);

public interface ICEnvLaserStrikeThinkHook
{
    public event OnCEnvLaserStrikeThinkPreDelegate Pre;
    public event OnCEnvLaserStrikeThinkPostDelegate Post;

    public void Invoke(CEnvLaser schemaObject);
}