using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPointHurtHurtThinkPreContext
{
    public CPointHurt SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPointHurtHurtThinkPostContext
{
    public CPointHurt SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPointHurtHurtThinkPreDelegate(ref CPointHurtHurtThinkPreContext ctx);
public delegate void OnCPointHurtHurtThinkPostDelegate(ref CPointHurtHurtThinkPostContext ctx);

public interface ICPointHurtHurtThinkHook
{
    public event OnCPointHurtHurtThinkPreDelegate Pre;
    public event OnCPointHurtHurtThinkPostDelegate Post;

    public void Invoke(CPointHurt schemaObject);
}