using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCEnvBeam(GameHooksService owner) : IGameHookDatamapCEnvBeam
{
    internal readonly CEnvBeamStrikeThinkHook CEnvBeamStrikeThinkHook = new(owner);
    internal readonly CEnvBeamUpdateThinkHook CEnvBeamUpdateThinkHook = new(owner);

    public ICEnvBeamStrikeThinkHook StrikeThink => CEnvBeamStrikeThinkHook;
    public ICEnvBeamUpdateThinkHook UpdateThink => CEnvBeamUpdateThinkHook;

    internal void UnregisterListeners()
    {
        CEnvBeamStrikeThinkHook.UnregisterListeners();
        CEnvBeamUpdateThinkHook.UnregisterListeners();
    }
}