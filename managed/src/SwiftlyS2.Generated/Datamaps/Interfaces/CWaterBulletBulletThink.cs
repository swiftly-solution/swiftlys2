using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CWaterBulletBulletThinkPreContext
{
    public CWaterBullet SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CWaterBulletBulletThinkPostContext
{
    public CWaterBullet SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCWaterBulletBulletThinkPreDelegate(ref CWaterBulletBulletThinkPreContext ctx);
public delegate void OnCWaterBulletBulletThinkPostDelegate(ref CWaterBulletBulletThinkPostContext ctx);

public interface ICWaterBulletBulletThinkHook
{
    public event OnCWaterBulletBulletThinkPreDelegate Pre;
    public event OnCWaterBulletBulletThinkPostDelegate Post;

    public void Invoke(CWaterBullet schemaObject);
}