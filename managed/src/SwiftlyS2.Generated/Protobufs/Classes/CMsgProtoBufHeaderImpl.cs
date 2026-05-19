using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgProtoBufHeaderImpl : TypedProtobuf<CMsgProtoBufHeader>, CMsgProtoBufHeader
{
    public CMsgProtoBufHeaderImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public ulong Steamid
    { get => Accessor.GetUInt64("steamid"); set => Accessor.SetUInt64("steamid", value); }
    public int ClientSessionid
    { get => Accessor.GetInt32("client_sessionid"); set => Accessor.SetInt32("client_sessionid", value); }
    public uint RoutingAppid
    { get => Accessor.GetUInt32("routing_appid"); set => Accessor.SetUInt32("routing_appid", value); }
    public ulong JobidSource
    { get => Accessor.GetUInt64("jobid_source"); set => Accessor.SetUInt64("jobid_source", value); }
    public ulong JobidTarget
    { get => Accessor.GetUInt64("jobid_target"); set => Accessor.SetUInt64("jobid_target", value); }
    public string TargetJobName
    { get => Accessor.GetString("target_job_name"); set => Accessor.SetString("target_job_name", value); }
    public int SeqNum
    { get => Accessor.GetInt32("seq_num"); set => Accessor.SetInt32("seq_num", value); }
    public int Eresult
    { get => Accessor.GetInt32("eresult"); set => Accessor.SetInt32("eresult", value); }
    public string ErrorMessage
    { get => Accessor.GetString("error_message"); set => Accessor.SetString("error_message", value); }
    public uint AuthAccountFlags
    { get => Accessor.GetUInt32("auth_account_flags"); set => Accessor.SetUInt32("auth_account_flags", value); }
    public uint TokenSource
    { get => Accessor.GetUInt32("token_source"); set => Accessor.SetUInt32("token_source", value); }
    public bool AdminSpoofingUser
    { get => Accessor.GetBool("admin_spoofing_user"); set => Accessor.SetBool("admin_spoofing_user", value); }
    public int TransportError
    { get => Accessor.GetInt32("transport_error"); set => Accessor.SetInt32("transport_error", value); }
    public ulong Messageid
    { get => Accessor.GetUInt64("messageid"); set => Accessor.SetUInt64("messageid", value); }
    public uint PublisherGroupId
    { get => Accessor.GetUInt32("publisher_group_id"); set => Accessor.SetUInt32("publisher_group_id", value); }
    public uint Sysid
    { get => Accessor.GetUInt32("sysid"); set => Accessor.SetUInt32("sysid", value); }
    public ulong TraceTag
    { get => Accessor.GetUInt64("trace_tag"); set => Accessor.SetUInt64("trace_tag", value); }
    public uint WebapiKeyId
    { get => Accessor.GetUInt32("webapi_key_id"); set => Accessor.SetUInt32("webapi_key_id", value); }
    public bool IsFromExternalSource
    { get => Accessor.GetBool("is_from_external_source"); set => Accessor.SetBool("is_from_external_source", value); }
    public IProtobufRepeatedFieldValueType<uint> ForwardToSysid
    { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "forward_to_sysid"); }
    public uint CmSysid
    { get => Accessor.GetUInt32("cm_sysid"); set => Accessor.SetUInt32("cm_sysid", value); }
    public uint LauncherType
    { get => Accessor.GetUInt32("launcher_type"); set => Accessor.SetUInt32("launcher_type", value); }
    public uint Realm
    { get => Accessor.GetUInt32("realm"); set => Accessor.SetUInt32("realm", value); }
    public int TimeoutMs
    { get => Accessor.GetInt32("timeout_ms"); set => Accessor.SetInt32("timeout_ms", value); }
    public string DebugSource
    { get => Accessor.GetString("debug_source"); set => Accessor.SetString("debug_source", value); }
    public uint Ip
    { get => Accessor.GetUInt32("ip"); set => Accessor.SetUInt32("ip", value); }
    public byte[] IpV6
    { get => Accessor.GetBytes("ip_v6"); set => Accessor.SetBytes("ip_v6", value); }
}