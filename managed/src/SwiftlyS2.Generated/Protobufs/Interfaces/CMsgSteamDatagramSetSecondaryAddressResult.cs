using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramSetSecondaryAddressResult : ITypedProtobuf<CMsgSteamDatagramSetSecondaryAddressResult>
{
    static CMsgSteamDatagramSetSecondaryAddressResult ITypedProtobuf<CMsgSteamDatagramSetSecondaryAddressResult>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramSetSecondaryAddressResultImpl(handle, isManuallyAllocated);

    public bool Success { get; set; }
    public string Message { get; set; }
}