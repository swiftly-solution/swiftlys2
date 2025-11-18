using SwiftlyS2.Shared.Events;

namespace SwiftlyS2.Core.Events;

internal class OnConVarCreated : IOnConVarCreated
{
    public required string ConVarName { get; set; }
}