using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPhysSlideConstraintSoundThinkPreContext
{
    public CPhysSlideConstraint SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPhysSlideConstraintSoundThinkPostContext
{
    public CPhysSlideConstraint SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPhysSlideConstraintSoundThinkPreDelegate(ref CPhysSlideConstraintSoundThinkPreContext ctx);
public delegate void OnCPhysSlideConstraintSoundThinkPostDelegate(ref CPhysSlideConstraintSoundThinkPostContext ctx);

public interface ICPhysSlideConstraintSoundThinkHook
{
    public event OnCPhysSlideConstraintSoundThinkPreDelegate Pre;
    public event OnCPhysSlideConstraintSoundThinkPostDelegate Post;

    public void Invoke(CPhysSlideConstraint schemaObject);
}