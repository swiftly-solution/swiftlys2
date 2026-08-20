using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CSpriteAnimateThinkPreContext
{
    public CSprite SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CSpriteAnimateThinkPostContext
{
    public CSprite SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCSpriteAnimateThinkPreDelegate(ref CSpriteAnimateThinkPreContext ctx);
public delegate void OnCSpriteAnimateThinkPostDelegate(ref CSpriteAnimateThinkPostContext ctx);

public interface ICSpriteAnimateThinkHook
{
    public event OnCSpriteAnimateThinkPreDelegate Pre;
    public event OnCSpriteAnimateThinkPostDelegate Post;

    public void Invoke(CSprite schemaObject);
}