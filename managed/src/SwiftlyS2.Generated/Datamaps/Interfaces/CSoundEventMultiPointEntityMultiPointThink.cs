using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CSoundEventMultiPointEntityMultiPointThinkPreContext
{
    public CSoundEventMultiPointEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CSoundEventMultiPointEntityMultiPointThinkPostContext
{
    public CSoundEventMultiPointEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCSoundEventMultiPointEntityMultiPointThinkPreDelegate(ref CSoundEventMultiPointEntityMultiPointThinkPreContext ctx);
public delegate void OnCSoundEventMultiPointEntityMultiPointThinkPostDelegate(ref CSoundEventMultiPointEntityMultiPointThinkPostContext ctx);

public interface ICSoundEventMultiPointEntityMultiPointThinkHook
{
    public event OnCSoundEventMultiPointEntityMultiPointThinkPreDelegate Pre;
    public event OnCSoundEventMultiPointEntityMultiPointThinkPostDelegate Post;

    public void Invoke(CSoundEventMultiPointEntity schemaObject);
}