using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseAnimGraphChoreoServicesThinkPreContext
{
    public CBaseAnimGraph SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseAnimGraphChoreoServicesThinkPostContext
{
    public CBaseAnimGraph SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseAnimGraphChoreoServicesThinkPreDelegate(ref CBaseAnimGraphChoreoServicesThinkPreContext ctx);
public delegate void OnCBaseAnimGraphChoreoServicesThinkPostDelegate(ref CBaseAnimGraphChoreoServicesThinkPostContext ctx);

public interface ICBaseAnimGraphChoreoServicesThinkHook
{
    public event OnCBaseAnimGraphChoreoServicesThinkPreDelegate Pre;
    public event OnCBaseAnimGraphChoreoServicesThinkPostDelegate Post;

    public void Invoke(CBaseAnimGraph schemaObject);
}