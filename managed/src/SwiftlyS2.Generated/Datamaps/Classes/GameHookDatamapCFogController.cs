using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCFogController : IGameHookDatamapCFogController
{
    internal readonly CFogControllerSetLerpValuesHook CFogControllerSetLerpValuesHook = new();

    public ICFogControllerSetLerpValuesHook SetLerpValues => CFogControllerSetLerpValuesHook;

    internal void UnregisterListeners()
    {
        CFogControllerSetLerpValuesHook.UnregisterListeners();
    }
}