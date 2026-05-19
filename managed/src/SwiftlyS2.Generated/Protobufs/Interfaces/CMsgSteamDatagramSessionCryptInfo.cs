using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramSessionCryptInfo : ITypedProtobuf<CMsgSteamDatagramSessionCryptInfo>
{
    static CMsgSteamDatagramSessionCryptInfo ITypedProtobuf<CMsgSteamDatagramSessionCryptInfo>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramSessionCryptInfoImpl(handle, isManuallyAllocated);

    public CMsgSteamDatagramSessionCryptInfo_EKeyType KeyType { get; set; }
    public byte[] KeyData { get; set; }
    public ulong Nonce { get; set; }
    public uint ProtocolVersion { get; set; }
    public ESteamNetworkingSocketsCipher Ciphers { get; set; }
}