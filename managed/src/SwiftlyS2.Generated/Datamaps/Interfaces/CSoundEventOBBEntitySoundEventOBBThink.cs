using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CSoundEventOBBEntitySoundEventOBBThinkPreContext
{
    public CSoundEventOBBEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CSoundEventOBBEntitySoundEventOBBThinkPostContext
{
    public CSoundEventOBBEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCSoundEventOBBEntitySoundEventOBBThinkPreDelegate(ref CSoundEventOBBEntitySoundEventOBBThinkPreContext ctx);
public delegate void OnCSoundEventOBBEntitySoundEventOBBThinkPostDelegate(ref CSoundEventOBBEntitySoundEventOBBThinkPostContext ctx);

public interface ICSoundEventOBBEntitySoundEventOBBThinkHook
{
    public event OnCSoundEventOBBEntitySoundEventOBBThinkPreDelegate Pre;
    public event OnCSoundEventOBBEntitySoundEventOBBThinkPostDelegate Post;

    public void Invoke(CSoundEventOBBEntity schemaObject);
}