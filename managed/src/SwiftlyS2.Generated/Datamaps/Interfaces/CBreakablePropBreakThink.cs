using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBreakablePropBreakThinkPreContext
{
    public CBreakableProp SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBreakablePropBreakThinkPostContext
{
    public CBreakableProp SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBreakablePropBreakThinkPreDelegate(ref CBreakablePropBreakThinkPreContext ctx);
public delegate void OnCBreakablePropBreakThinkPostDelegate(ref CBreakablePropBreakThinkPostContext ctx);

public interface ICBreakablePropBreakThinkHook
{
    public event OnCBreakablePropBreakThinkPreDelegate Pre;
    public event OnCBreakablePropBreakThinkPostDelegate Post;

    public void Invoke(CBreakableProp schemaObject);
}