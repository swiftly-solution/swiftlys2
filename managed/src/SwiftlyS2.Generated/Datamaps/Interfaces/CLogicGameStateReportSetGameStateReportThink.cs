using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CLogicGameStateReportSetGameStateReportThinkPreContext
{
    public CLogicGameStateReport SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CLogicGameStateReportSetGameStateReportThinkPostContext
{
    public CLogicGameStateReport SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCLogicGameStateReportSetGameStateReportThinkPreDelegate(ref CLogicGameStateReportSetGameStateReportThinkPreContext ctx);
public delegate void OnCLogicGameStateReportSetGameStateReportThinkPostDelegate(ref CLogicGameStateReportSetGameStateReportThinkPostContext ctx);

public interface ICLogicGameStateReportSetGameStateReportThinkHook
{
    public event OnCLogicGameStateReportSetGameStateReportThinkPreDelegate Pre;
    public event OnCLogicGameStateReportSetGameStateReportThinkPostDelegate Post;

    public void Invoke(CLogicGameStateReport schemaObject);
}