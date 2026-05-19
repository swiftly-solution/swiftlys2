using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramSetSecondaryAddressRequestImpl : TypedProtobuf<CMsgSteamDatagramSetSecondaryAddressRequest>, CMsgSteamDatagramSetSecondaryAddressRequest
{
    public CMsgSteamDatagramSetSecondaryAddressRequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint ClientMainIp
    { get => Accessor.GetUInt32("client_main_ip"); set => Accessor.SetUInt32("client_main_ip", value); }
    public uint ClientMainPort
    { get => Accessor.GetUInt32("client_main_port"); set => Accessor.SetUInt32("client_main_port", value); }
    public uint ClientConnectionId
    { get => Accessor.GetUInt32("client_connection_id"); set => Accessor.SetUInt32("client_connection_id", value); }
    public string ClientIdentity
    { get => Accessor.GetString("client_identity"); set => Accessor.SetString("client_identity", value); }
    public bool RequestSendDuplication
    { get => Accessor.GetBool("request_send_duplication"); set => Accessor.SetBool("request_send_duplication", value); }
    public byte[] KludgePad
    { get => Accessor.GetBytes("kludge_pad"); set => Accessor.SetBytes("kludge_pad", value); }
}