using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSoundOpvarSetPathCornerEntity(GameHooksService owner) : IGameHookDatamapCSoundOpvarSetPathCornerEntity
{
    internal readonly CSoundOpvarSetPathCornerEntitySetOpvarThinkHook CSoundOpvarSetPathCornerEntitySetOpvarThinkHook = new(owner);

    public ICSoundOpvarSetPathCornerEntitySetOpvarThinkHook SetOpvarThink => CSoundOpvarSetPathCornerEntitySetOpvarThinkHook;

    internal void UnregisterListeners()
    {
        CSoundOpvarSetPathCornerEntitySetOpvarThinkHook.UnregisterListeners();
    }
}