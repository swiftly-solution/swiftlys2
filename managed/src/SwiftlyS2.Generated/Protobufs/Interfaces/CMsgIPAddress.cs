using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgIPAddress : ITypedProtobuf<CMsgIPAddress>
{
    static CMsgIPAddress ITypedProtobuf<CMsgIPAddress>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgIPAddressImpl(handle, isManuallyAllocated);

    public uint V4 { get; set; }
    public byte[] V6 { get; set; }
}