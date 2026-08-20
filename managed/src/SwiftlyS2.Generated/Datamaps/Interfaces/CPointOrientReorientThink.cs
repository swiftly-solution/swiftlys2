using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPointOrientReorientThinkPreContext
{
    public CPointOrient SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPointOrientReorientThinkPostContext
{
    public CPointOrient SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPointOrientReorientThinkPreDelegate(ref CPointOrientReorientThinkPreContext ctx);
public delegate void OnCPointOrientReorientThinkPostDelegate(ref CPointOrientReorientThinkPostContext ctx);

public interface ICPointOrientReorientThinkHook
{
    public event OnCPointOrientReorientThinkPreDelegate Pre;
    public event OnCPointOrientReorientThinkPostDelegate Post;

    public void Invoke(CPointOrient schemaObject);
}