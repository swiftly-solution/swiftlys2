using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramCertificate : ITypedProtobuf<CMsgSteamDatagramCertificate>
{
    static CMsgSteamDatagramCertificate ITypedProtobuf<CMsgSteamDatagramCertificate>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramCertificateImpl(handle, isManuallyAllocated);

    public CMsgSteamDatagramCertificate_EKeyType KeyType { get; set; }
    public byte[] KeyData { get; set; }
    public ulong LegacySteamId { get; set; }
    public CMsgSteamNetworkingIdentityLegacyBinary LegacyIdentityBinary { get; }
    public string IdentityString { get; set; }
    public IProtobufRepeatedFieldValueType<uint> GameserverDatacenterIds { get; }
    public uint TimeCreated { get; set; }
    public uint TimeExpiry { get; set; }
    public IProtobufRepeatedFieldValueType<uint> AppIds { get; }
    public IProtobufRepeatedFieldValueType<string> IpAddresses { get; }
}