using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CSoundEventPathCornerEntitySoundEventPathCornerThinkPreContext
{
    public CSoundEventPathCornerEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CSoundEventPathCornerEntitySoundEventPathCornerThinkPostContext
{
    public CSoundEventPathCornerEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCSoundEventPathCornerEntitySoundEventPathCornerThinkPreDelegate(ref CSoundEventPathCornerEntitySoundEventPathCornerThinkPreContext ctx);
public delegate void OnCSoundEventPathCornerEntitySoundEventPathCornerThinkPostDelegate(ref CSoundEventPathCornerEntitySoundEventPathCornerThinkPostContext ctx);

public interface ICSoundEventPathCornerEntitySoundEventPathCornerThinkHook
{
    public event OnCSoundEventPathCornerEntitySoundEventPathCornerThinkPreDelegate Pre;
    public event OnCSoundEventPathCornerEntitySoundEventPathCornerThinkPostDelegate Post;

    public void Invoke(CSoundEventPathCornerEntity schemaObject);
}