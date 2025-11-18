using SwiftlyS2.Shared.Events;

namespace SwiftlyS2.Core.Events;

internal class OnConCommandCreated : IOnConCommandCreated
{
    public required string CommandName { get; set; }
}