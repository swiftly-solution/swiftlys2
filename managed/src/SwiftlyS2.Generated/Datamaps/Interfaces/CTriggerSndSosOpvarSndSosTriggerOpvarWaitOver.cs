using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPreContext
{
    public CTriggerSndSosOpvar SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPostContext
{
    public CTriggerSndSosOpvar SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPreDelegate(ref CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPreContext ctx);
public delegate void OnCTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPostDelegate(ref CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPostContext ctx);

public interface ICTriggerSndSosOpvarSndSosTriggerOpvarWaitOverHook
{
    public event OnCTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPreDelegate Pre;
    public event OnCTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPostDelegate Post;

    public void Invoke(CTriggerSndSosOpvar schemaObject);
}