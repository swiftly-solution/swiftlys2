using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CSoundOpvarSetAutoRoomEntitySetOpvarThinkPreContext
{
    public CSoundOpvarSetAutoRoomEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CSoundOpvarSetAutoRoomEntitySetOpvarThinkPostContext
{
    public CSoundOpvarSetAutoRoomEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCSoundOpvarSetAutoRoomEntitySetOpvarThinkPreDelegate(ref CSoundOpvarSetAutoRoomEntitySetOpvarThinkPreContext ctx);
public delegate void OnCSoundOpvarSetAutoRoomEntitySetOpvarThinkPostDelegate(ref CSoundOpvarSetAutoRoomEntitySetOpvarThinkPostContext ctx);

public interface ICSoundOpvarSetAutoRoomEntitySetOpvarThinkHook
{
    public event OnCSoundOpvarSetAutoRoomEntitySetOpvarThinkPreDelegate Pre;
    public event OnCSoundOpvarSetAutoRoomEntitySetOpvarThinkPostDelegate Post;

    public void Invoke(CSoundOpvarSetAutoRoomEntity schemaObject);
}