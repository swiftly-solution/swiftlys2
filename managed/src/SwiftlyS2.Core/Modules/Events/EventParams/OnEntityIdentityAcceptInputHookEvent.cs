using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Events;

internal class OnEntityIdentityAcceptInputHookEvent : IOnEntityIdentityAcceptInputHookEvent
{
    public required CEntityIdentity Identity { get; init; }
    public required CEntityInstance EntityInstance { get; init; }
    public required string InputName { get; init; }
    public required CEntityInstance? Activator { get; init; }
    public required CEntityInstance? Caller { get; init; }
    public required CVariant VariantValue { get; init; }
    public required int OutputId { get; init; }
    public required HookResult Result { get; set; }
}