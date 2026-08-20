using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CLogicMeasureMovementMeasureThinkPreContext
{
    public CLogicMeasureMovement SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CLogicMeasureMovementMeasureThinkPostContext
{
    public CLogicMeasureMovement SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCLogicMeasureMovementMeasureThinkPreDelegate(ref CLogicMeasureMovementMeasureThinkPreContext ctx);
public delegate void OnCLogicMeasureMovementMeasureThinkPostDelegate(ref CLogicMeasureMovementMeasureThinkPostContext ctx);

public interface ICLogicMeasureMovementMeasureThinkHook
{
    public event OnCLogicMeasureMovementMeasureThinkPreDelegate Pre;
    public event OnCLogicMeasureMovementMeasureThinkPostDelegate Post;

    public void Invoke(CLogicMeasureMovement schemaObject);
}