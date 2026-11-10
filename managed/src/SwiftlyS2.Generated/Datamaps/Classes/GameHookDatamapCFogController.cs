using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCFogController(GameHooksService owner) : IGameHookDatamapCFogController
{
    internal readonly CFogControllerSetLerpValuesHook CFogControllerSetLerpValuesHook = new(owner);

    public ICFogControllerSetLerpValuesHook SetLerpValues => CFogControllerSetLerpValuesHook;

    internal void UnregisterListeners()
    {
        CFogControllerSetLerpValuesHook.UnregisterListeners();
    }
}