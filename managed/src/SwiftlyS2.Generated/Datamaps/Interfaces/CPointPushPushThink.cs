using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPointPushPushThinkPreContext
{
    public CPointPush SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPointPushPushThinkPostContext
{
    public CPointPush SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPointPushPushThinkPreDelegate(ref CPointPushPushThinkPreContext ctx);
public delegate void OnCPointPushPushThinkPostDelegate(ref CPointPushPushThinkPostContext ctx);

public interface ICPointPushPushThinkHook
{
    public event OnCPointPushPushThinkPreDelegate Pre;
    public event OnCPointPushPushThinkPostDelegate Post;

    public void Invoke(CPointPush schemaObject);
}