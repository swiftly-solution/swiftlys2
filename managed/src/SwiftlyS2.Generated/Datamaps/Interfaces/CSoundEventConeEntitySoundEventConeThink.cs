using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CSoundEventConeEntitySoundEventConeThinkPreContext
{
    public CSoundEventConeEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CSoundEventConeEntitySoundEventConeThinkPostContext
{
    public CSoundEventConeEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCSoundEventConeEntitySoundEventConeThinkPreDelegate(ref CSoundEventConeEntitySoundEventConeThinkPreContext ctx);
public delegate void OnCSoundEventConeEntitySoundEventConeThinkPostDelegate(ref CSoundEventConeEntitySoundEventConeThinkPostContext ctx);

public interface ICSoundEventConeEntitySoundEventConeThinkHook
{
    public event OnCSoundEventConeEntitySoundEventConeThinkPreDelegate Pre;
    public event OnCSoundEventConeEntitySoundEventConeThinkPostDelegate Post;

    public void Invoke(CSoundEventConeEntity schemaObject);
}