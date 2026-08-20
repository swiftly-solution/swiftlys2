using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CSoundOpvarSetPointBaseSetOpvarThinkPreContext
{
    public CSoundOpvarSetPointBase SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CSoundOpvarSetPointBaseSetOpvarThinkPostContext
{
    public CSoundOpvarSetPointBase SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCSoundOpvarSetPointBaseSetOpvarThinkPreDelegate(ref CSoundOpvarSetPointBaseSetOpvarThinkPreContext ctx);
public delegate void OnCSoundOpvarSetPointBaseSetOpvarThinkPostDelegate(ref CSoundOpvarSetPointBaseSetOpvarThinkPostContext ctx);

public interface ICSoundOpvarSetPointBaseSetOpvarThinkHook
{
    public event OnCSoundOpvarSetPointBaseSetOpvarThinkPreDelegate Pre;
    public event OnCSoundOpvarSetPointBaseSetOpvarThinkPostDelegate Post;

    public void Invoke(CSoundOpvarSetPointBase schemaObject);
}