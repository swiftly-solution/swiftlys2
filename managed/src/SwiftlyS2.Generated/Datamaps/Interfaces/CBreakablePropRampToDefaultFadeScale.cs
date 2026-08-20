using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBreakablePropRampToDefaultFadeScalePreContext
{
    public CBreakableProp SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBreakablePropRampToDefaultFadeScalePostContext
{
    public CBreakableProp SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBreakablePropRampToDefaultFadeScalePreDelegate(ref CBreakablePropRampToDefaultFadeScalePreContext ctx);
public delegate void OnCBreakablePropRampToDefaultFadeScalePostDelegate(ref CBreakablePropRampToDefaultFadeScalePostContext ctx);

public interface ICBreakablePropRampToDefaultFadeScaleHook
{
    public event OnCBreakablePropRampToDefaultFadeScalePreDelegate Pre;
    public event OnCBreakablePropRampToDefaultFadeScalePostDelegate Post;

    public void Invoke(CBreakableProp schemaObject);
}