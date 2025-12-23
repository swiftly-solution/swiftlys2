using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Events;

internal class OnEntityTakeDamageEvent : IOnEntityTakeDamageEvent
{
    public required CEntityInstance Entity { get; set; }
    public unsafe nint _infoPtr;
    public unsafe nint _resultPtr;
    public ref CTakeDamageInfo Info => ref _infoPtr.AsRef<CTakeDamageInfo>();
    public ref CTakeDamageResult DamageResult => ref _resultPtr.AsRef<CTakeDamageResult>();

    public HookResult Result { get; set; } = HookResult.Continue;
}
