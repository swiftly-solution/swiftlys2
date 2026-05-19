using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramGameserverPingRequestEnvelopeImpl : TypedProtobuf<CMsgSteamDatagramGameserverPingRequestEnvelope>, CMsgSteamDatagramGameserverPingRequestEnvelope
{
    public CMsgSteamDatagramGameserverPingRequestEnvelopeImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public CMsgSteamDatagramCertificateSigned Cert
    { get => new CMsgSteamDatagramCertificateSignedImpl(NativeNetMessages.GetNestedMessage(Address, "cert"), false); }
    public byte[] SignedData
    { get => Accessor.GetBytes("signed_data"); set => Accessor.SetBytes("signed_data", value); }
    public byte[] Signature
    { get => Accessor.GetBytes("signature"); set => Accessor.SetBytes("signature", value); }
    public uint LegacyYourPublicIp
    { get => Accessor.GetUInt32("legacy_your_public_ip"); set => Accessor.SetUInt32("legacy_your_public_ip", value); }
    public uint LegacyYourPublicPort
    { get => Accessor.GetUInt32("legacy_your_public_port"); set => Accessor.SetUInt32("legacy_your_public_port", value); }
    public uint LegacyRelayUnixTime
    { get => Accessor.GetUInt32("legacy_relay_unix_time"); set => Accessor.SetUInt32("legacy_relay_unix_time", value); }
    public ulong LegacyChallenge
    { get => Accessor.GetUInt64("legacy_challenge"); set => Accessor.SetUInt64("legacy_challenge", value); }
    public uint LegacyRouterTimestamp
    { get => Accessor.GetUInt32("legacy_router_timestamp"); set => Accessor.SetUInt32("legacy_router_timestamp", value); }
    public byte[] DummyPad
    { get => Accessor.GetBytes("dummy_pad"); set => Accessor.SetBytes("dummy_pad", value); }
}