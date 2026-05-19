using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramCachedCredentialsForApp : ITypedProtobuf<CMsgSteamDatagramCachedCredentialsForApp>
{
    static CMsgSteamDatagramCachedCredentialsForApp ITypedProtobuf<CMsgSteamDatagramCachedCredentialsForApp>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramCachedCredentialsForAppImpl(handle, isManuallyAllocated);

    public byte[] PrivateKey { get; set; }
    public byte[] Cert { get; set; }
    public IProtobufRepeatedFieldValueType<byte[]> RelayTickets { get; }
}