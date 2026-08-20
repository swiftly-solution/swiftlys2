using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CRevertSavedLoadThinkPreContext
{
    public CRevertSaved SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CRevertSavedLoadThinkPostContext
{
    public CRevertSaved SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCRevertSavedLoadThinkPreDelegate(ref CRevertSavedLoadThinkPreContext ctx);
public delegate void OnCRevertSavedLoadThinkPostDelegate(ref CRevertSavedLoadThinkPostContext ctx);

public interface ICRevertSavedLoadThinkHook
{
    public event OnCRevertSavedLoadThinkPreDelegate Pre;
    public event OnCRevertSavedLoadThinkPostDelegate Post;

    public void Invoke(CRevertSaved schemaObject);
}