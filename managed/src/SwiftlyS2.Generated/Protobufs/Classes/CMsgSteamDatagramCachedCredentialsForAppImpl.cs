using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramCachedCredentialsForAppImpl : TypedProtobuf<CMsgSteamDatagramCachedCredentialsForApp>, CMsgSteamDatagramCachedCredentialsForApp
{
    public CMsgSteamDatagramCachedCredentialsForAppImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public byte[] PrivateKey
    { get => Accessor.GetBytes("private_key"); set => Accessor.SetBytes("private_key", value); }
    public byte[] Cert
    { get => Accessor.GetBytes("cert"); set => Accessor.SetBytes("cert", value); }
    public IProtobufRepeatedFieldValueType<byte[]> RelayTickets
    { get => new ProtobufRepeatedFieldValueType<byte[]>(Accessor, "relay_tickets"); }
}