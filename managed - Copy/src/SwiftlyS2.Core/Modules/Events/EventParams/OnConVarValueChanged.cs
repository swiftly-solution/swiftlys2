using SwiftlyS2.Shared.Events;

namespace SwiftlyS2.Core.Events;

internal class OnConVarValueChanged : IOnConVarValueChanged
{
    public required string ConVarName { get; set; }

    public required int PlayerId { get; set; }

    public required string NewValue { get; set; }

    public required string OldValue { get; set; }
}