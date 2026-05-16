using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookMovement : IGameHookMovement
{
    internal readonly RunCommandMovementEvents RunCommandEvents = new();

    public IRunCommandMovementEvents RunCommand => RunCommandEvents;
}
