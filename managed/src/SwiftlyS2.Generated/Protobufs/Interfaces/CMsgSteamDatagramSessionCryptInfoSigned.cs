using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramSessionCryptInfoSigned : ITypedProtobuf<CMsgSteamDatagramSessionCryptInfoSigned>
{
    static CMsgSteamDatagramSessionCryptInfoSigned ITypedProtobuf<CMsgSteamDatagramSessionCryptInfoSigned>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramSessionCryptInfoSignedImpl(handle, isManuallyAllocated);

    public byte[] Info { get; set; }
    public byte[] Signature { get; set; }
}