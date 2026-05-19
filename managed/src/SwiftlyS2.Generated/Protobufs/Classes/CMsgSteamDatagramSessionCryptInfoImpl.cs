using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramSessionCryptInfoImpl : TypedProtobuf<CMsgSteamDatagramSessionCryptInfo>, CMsgSteamDatagramSessionCryptInfo
{
    public CMsgSteamDatagramSessionCryptInfoImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public CMsgSteamDatagramSessionCryptInfo_EKeyType KeyType
    { get => (CMsgSteamDatagramSessionCryptInfo_EKeyType)Accessor.GetInt32("key_type"); set => Accessor.SetInt32("key_type", (int)value); }
    public byte[] KeyData
    { get => Accessor.GetBytes("key_data"); set => Accessor.SetBytes("key_data", value); }
    public ulong Nonce
    { get => Accessor.GetUInt64("nonce"); set => Accessor.SetUInt64("nonce", value); }
    public uint ProtocolVersion
    { get => Accessor.GetUInt32("protocol_version"); set => Accessor.SetUInt32("protocol_version", value); }
    public ESteamNetworkingSocketsCipher Ciphers
    { get => (ESteamNetworkingSocketsCipher)Accessor.GetInt32("ciphers"); set => Accessor.SetInt32("ciphers", (int)value); }
}