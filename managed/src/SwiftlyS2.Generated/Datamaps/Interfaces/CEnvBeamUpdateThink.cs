using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CEnvBeamUpdateThinkPreContext
{
    public CEnvBeam SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CEnvBeamUpdateThinkPostContext
{
    public CEnvBeam SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCEnvBeamUpdateThinkPreDelegate(ref CEnvBeamUpdateThinkPreContext ctx);
public delegate void OnCEnvBeamUpdateThinkPostDelegate(ref CEnvBeamUpdateThinkPostContext ctx);

public interface ICEnvBeamUpdateThinkHook
{
    public event OnCEnvBeamUpdateThinkPreDelegate Pre;
    public event OnCEnvBeamUpdateThinkPostDelegate Post;

    public void Invoke(CEnvBeam schemaObject);
}