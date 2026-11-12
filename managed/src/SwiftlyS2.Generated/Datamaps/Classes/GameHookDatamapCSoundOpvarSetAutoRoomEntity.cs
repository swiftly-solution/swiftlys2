using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSoundOpvarSetAutoRoomEntity(GameHooksService owner) : IGameHookDatamapCSoundOpvarSetAutoRoomEntity
{
    internal readonly CSoundOpvarSetAutoRoomEntitySetOpvarThinkHook CSoundOpvarSetAutoRoomEntitySetOpvarThinkHook = new(owner);

    public ICSoundOpvarSetAutoRoomEntitySetOpvarThinkHook SetOpvarThink => CSoundOpvarSetAutoRoomEntitySetOpvarThinkHook;

    internal void UnregisterListeners()
    {
        CSoundOpvarSetAutoRoomEntitySetOpvarThinkHook.UnregisterListeners();
    }
}