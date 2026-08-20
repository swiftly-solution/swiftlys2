using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CSoundOpvarSetPathCornerEntitySetOpvarThinkPreContext
{
    public CSoundOpvarSetPathCornerEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CSoundOpvarSetPathCornerEntitySetOpvarThinkPostContext
{
    public CSoundOpvarSetPathCornerEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCSoundOpvarSetPathCornerEntitySetOpvarThinkPreDelegate(ref CSoundOpvarSetPathCornerEntitySetOpvarThinkPreContext ctx);
public delegate void OnCSoundOpvarSetPathCornerEntitySetOpvarThinkPostDelegate(ref CSoundOpvarSetPathCornerEntitySetOpvarThinkPostContext ctx);

public interface ICSoundOpvarSetPathCornerEntitySetOpvarThinkHook
{
    public event OnCSoundOpvarSetPathCornerEntitySetOpvarThinkPreDelegate Pre;
    public event OnCSoundOpvarSetPathCornerEntitySetOpvarThinkPostDelegate Post;

    public void Invoke(CSoundOpvarSetPathCornerEntity schemaObject);
}