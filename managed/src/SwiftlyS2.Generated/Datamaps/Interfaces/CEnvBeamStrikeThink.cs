using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CEnvBeamStrikeThinkPreContext
{
    public CEnvBeam SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CEnvBeamStrikeThinkPostContext
{
    public CEnvBeam SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCEnvBeamStrikeThinkPreDelegate(ref CEnvBeamStrikeThinkPreContext ctx);
public delegate void OnCEnvBeamStrikeThinkPostDelegate(ref CEnvBeamStrikeThinkPostContext ctx);

public interface ICEnvBeamStrikeThinkHook
{
    public event OnCEnvBeamStrikeThinkPreDelegate Pre;
    public event OnCEnvBeamStrikeThinkPostDelegate Post;

    public void Invoke(CEnvBeam schemaObject);
}