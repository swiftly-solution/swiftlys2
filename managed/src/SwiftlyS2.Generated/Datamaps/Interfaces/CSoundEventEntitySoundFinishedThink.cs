using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CSoundEventEntitySoundFinishedThinkPreContext
{
    public CSoundEventEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CSoundEventEntitySoundFinishedThinkPostContext
{
    public CSoundEventEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCSoundEventEntitySoundFinishedThinkPreDelegate(ref CSoundEventEntitySoundFinishedThinkPreContext ctx);
public delegate void OnCSoundEventEntitySoundFinishedThinkPostDelegate(ref CSoundEventEntitySoundFinishedThinkPostContext ctx);

public interface ICSoundEventEntitySoundFinishedThinkHook
{
    public event OnCSoundEventEntitySoundFinishedThinkPreDelegate Pre;
    public event OnCSoundEventEntitySoundFinishedThinkPostDelegate Post;

    public void Invoke(CSoundEventEntity schemaObject);
}