using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSoundOpvarSetAutoRoomEntity : IGameHookDatamapCSoundOpvarSetAutoRoomEntity
{
    internal readonly CSoundOpvarSetAutoRoomEntitySetOpvarThinkHook CSoundOpvarSetAutoRoomEntitySetOpvarThinkHook = new();

    public ICSoundOpvarSetAutoRoomEntitySetOpvarThinkHook SetOpvarThink => CSoundOpvarSetAutoRoomEntitySetOpvarThinkHook;

    internal void UnregisterListeners()
    {
        CSoundOpvarSetAutoRoomEntitySetOpvarThinkHook.UnregisterListeners();
    }
}