using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Events;

internal class OnEntityFireOutputHookEvent : IOnEntityFireOutputHookEvent
{
    public required CEntityIOOutput EntityIO { get; init; }
    public required string OutputName { get; init; }
    public required CEntityInstance? Activator { get; init; }
    public required CEntityInstance? Caller { get; init; }
    public required CVariant VariantValue { get; init; }
    public required float Delay { get; init; }
    public required HookResult Result { get; set; }
}