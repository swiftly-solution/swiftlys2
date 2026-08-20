using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBombTargetBombTargetTouchPreContext
{
    public CBombTarget SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBombTargetBombTargetTouchPostContext
{
    public CBombTarget SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBombTargetBombTargetTouchPreDelegate(ref CBombTargetBombTargetTouchPreContext ctx);
public delegate void OnCBombTargetBombTargetTouchPostDelegate(ref CBombTargetBombTargetTouchPostContext ctx);

public interface ICBombTargetBombTargetTouchHook
{
    public event OnCBombTargetBombTargetTouchPreDelegate Pre;
    public event OnCBombTargetBombTargetTouchPostDelegate Post;

    public void Invoke(CBombTarget schemaObject);
}