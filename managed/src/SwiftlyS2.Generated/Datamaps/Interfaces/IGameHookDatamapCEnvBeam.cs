namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCEnvBeam
{
    public ICEnvBeamStrikeThinkHook StrikeThink { get; }
    public ICEnvBeamUpdateThinkHook UpdateThink { get; }
}