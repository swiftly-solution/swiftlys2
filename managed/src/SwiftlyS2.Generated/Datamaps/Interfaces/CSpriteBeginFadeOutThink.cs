using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CSpriteBeginFadeOutThinkPreContext
{
    public CSprite SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CSpriteBeginFadeOutThinkPostContext
{
    public CSprite SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCSpriteBeginFadeOutThinkPreDelegate(ref CSpriteBeginFadeOutThinkPreContext ctx);
public delegate void OnCSpriteBeginFadeOutThinkPostDelegate(ref CSpriteBeginFadeOutThinkPostContext ctx);

public interface ICSpriteBeginFadeOutThinkHook
{
    public event OnCSpriteBeginFadeOutThinkPreDelegate Pre;
    public event OnCSpriteBeginFadeOutThinkPostDelegate Post;

    public void Invoke(CSprite schemaObject);
}