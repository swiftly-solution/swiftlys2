using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPlantedC4C4ThinkPreContext
{
    public CPlantedC4 SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPlantedC4C4ThinkPostContext
{
    public CPlantedC4 SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPlantedC4C4ThinkPreDelegate(ref CPlantedC4C4ThinkPreContext ctx);
public delegate void OnCPlantedC4C4ThinkPostDelegate(ref CPlantedC4C4ThinkPostContext ctx);

public interface ICPlantedC4C4ThinkHook
{
    public event OnCPlantedC4C4ThinkPreDelegate Pre;
    public event OnCPlantedC4C4ThinkPostDelegate Post;

    public void Invoke(CPlantedC4 schemaObject);
}