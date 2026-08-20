using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBreakableDiePreContext
{
    public CBreakable SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBreakableDiePostContext
{
    public CBreakable SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBreakableDiePreDelegate(ref CBreakableDiePreContext ctx);
public delegate void OnCBreakableDiePostDelegate(ref CBreakableDiePostContext ctx);

public interface ICBreakableDieHook
{
    public event OnCBreakableDiePreDelegate Pre;
    public event OnCBreakableDiePostDelegate Post;

    public void Invoke(CBreakable schemaObject);
}