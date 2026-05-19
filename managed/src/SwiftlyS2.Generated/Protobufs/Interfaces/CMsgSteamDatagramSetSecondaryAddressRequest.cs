using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramSetSecondaryAddressRequest : ITypedProtobuf<CMsgSteamDatagramSetSecondaryAddressRequest>
{
    static CMsgSteamDatagramSetSecondaryAddressRequest ITypedProtobuf<CMsgSteamDatagramSetSecondaryAddressRequest>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramSetSecondaryAddressRequestImpl(handle, isManuallyAllocated);

    public uint ClientMainIp { get; set; }
    public uint ClientMainPort { get; set; }
    public uint ClientConnectionId { get; set; }
    public string ClientIdentity { get; set; }
    public bool RequestSendDuplication { get; set; }
    public byte[] KludgePad { get; set; }
}