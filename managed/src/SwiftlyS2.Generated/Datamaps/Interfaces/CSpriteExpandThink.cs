using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CSpriteExpandThinkPreContext
{
    public CSprite SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CSpriteExpandThinkPostContext
{
    public CSprite SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCSpriteExpandThinkPreDelegate(ref CSpriteExpandThinkPreContext ctx);
public delegate void OnCSpriteExpandThinkPostDelegate(ref CSpriteExpandThinkPostContext ctx);

public interface ICSpriteExpandThinkHook
{
    public event OnCSpriteExpandThinkPreDelegate Pre;
    public event OnCSpriteExpandThinkPostDelegate Post;

    public void Invoke(CSprite schemaObject);
}