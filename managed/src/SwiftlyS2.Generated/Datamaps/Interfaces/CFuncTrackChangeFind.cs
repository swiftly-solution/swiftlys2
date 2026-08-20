using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CFuncTrackChangeFindPreContext
{
    public CFuncTrackChange SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CFuncTrackChangeFindPostContext
{
    public CFuncTrackChange SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCFuncTrackChangeFindPreDelegate(ref CFuncTrackChangeFindPreContext ctx);
public delegate void OnCFuncTrackChangeFindPostDelegate(ref CFuncTrackChangeFindPostContext ctx);

public interface ICFuncTrackChangeFindHook
{
    public event OnCFuncTrackChangeFindPreDelegate Pre;
    public event OnCFuncTrackChangeFindPostDelegate Post;

    public void Invoke(CFuncTrackChange schemaObject);
}