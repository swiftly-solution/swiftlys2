using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramSignedMessageGeneric : ITypedProtobuf<CMsgSteamDatagramSignedMessageGeneric>
{
    static CMsgSteamDatagramSignedMessageGeneric ITypedProtobuf<CMsgSteamDatagramSignedMessageGeneric>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramSignedMessageGenericImpl(handle, isManuallyAllocated);

    public CMsgSteamDatagramCertificateSigned Cert { get; }
    public byte[] SignedData { get; set; }
    public byte[] Signature { get; set; }
    public byte[] DummyPad { get; set; }
}