using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBombTargetBombTargetUsePreContext
{
    public CBombTarget SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBombTargetBombTargetUsePostContext
{
    public CBombTarget SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBombTargetBombTargetUsePreDelegate(ref CBombTargetBombTargetUsePreContext ctx);
public delegate void OnCBombTargetBombTargetUsePostDelegate(ref CBombTargetBombTargetUsePostContext ctx);

public interface ICBombTargetBombTargetUseHook
{
    public event OnCBombTargetBombTargetUsePreDelegate Pre;
    public event OnCBombTargetBombTargetUsePostDelegate Post;

    public void Invoke(CBombTarget schemaObject);
}