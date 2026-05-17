using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookController : IGameHookController
{
    internal readonly ProcessUsercmdsEvents ProcessUsercmdsEvents = new();

    public IProcessUsercmdsEvents ProcessUsercmds => ProcessUsercmdsEvents;

}
