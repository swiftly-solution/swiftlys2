using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CSpriteAnimateUntilDeadPreContext
{
    public CSprite SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CSpriteAnimateUntilDeadPostContext
{
    public CSprite SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCSpriteAnimateUntilDeadPreDelegate(ref CSpriteAnimateUntilDeadPreContext ctx);
public delegate void OnCSpriteAnimateUntilDeadPostDelegate(ref CSpriteAnimateUntilDeadPostContext ctx);

public interface ICSpriteAnimateUntilDeadHook
{
    public event OnCSpriteAnimateUntilDeadPreDelegate Pre;
    public event OnCSpriteAnimateUntilDeadPostDelegate Post;

    public void Invoke(CSprite schemaObject);
}