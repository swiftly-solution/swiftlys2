using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CLogicNPCCounterSetNPCCounterThinkPreContext
{
    public CLogicNPCCounter SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CLogicNPCCounterSetNPCCounterThinkPostContext
{
    public CLogicNPCCounter SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCLogicNPCCounterSetNPCCounterThinkPreDelegate(ref CLogicNPCCounterSetNPCCounterThinkPreContext ctx);
public delegate void OnCLogicNPCCounterSetNPCCounterThinkPostDelegate(ref CLogicNPCCounterSetNPCCounterThinkPostContext ctx);

public interface ICLogicNPCCounterSetNPCCounterThinkHook
{
    public event OnCLogicNPCCounterSetNPCCounterThinkPreDelegate Pre;
    public event OnCLogicNPCCounterSetNPCCounterThinkPostDelegate Post;

    public void Invoke(CLogicNPCCounter schemaObject);
}