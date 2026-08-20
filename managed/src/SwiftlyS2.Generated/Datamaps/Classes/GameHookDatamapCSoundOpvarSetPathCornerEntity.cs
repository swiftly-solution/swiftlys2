using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSoundOpvarSetPathCornerEntity : IGameHookDatamapCSoundOpvarSetPathCornerEntity
{
    internal readonly CSoundOpvarSetPathCornerEntitySetOpvarThinkHook CSoundOpvarSetPathCornerEntitySetOpvarThinkHook = new();

    public ICSoundOpvarSetPathCornerEntitySetOpvarThinkHook SetOpvarThink => CSoundOpvarSetPathCornerEntitySetOpvarThinkHook;

    internal void UnregisterListeners()
    {
        CSoundOpvarSetPathCornerEntitySetOpvarThinkHook.UnregisterListeners();
    }
}