using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Events;

internal class OnPlayerPawnPostThinkHookEvent : IOnPlayerPawnPostThinkHookEvent
{
    public required CCSPlayerPawn PlayerPawn { get; init; }
}
