using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgProtoBufHeader : ITypedProtobuf<CMsgProtoBufHeader>
{
    static CMsgProtoBufHeader ITypedProtobuf<CMsgProtoBufHeader>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgProtoBufHeaderImpl(handle, isManuallyAllocated);

    public ulong Steamid { get; set; }
    public int ClientSessionid { get; set; }
    public uint RoutingAppid { get; set; }
    public ulong JobidSource { get; set; }
    public ulong JobidTarget { get; set; }
    public string TargetJobName { get; set; }
    public int SeqNum { get; set; }
    public int Eresult { get; set; }
    public string ErrorMessage { get; set; }
    public uint AuthAccountFlags { get; set; }
    public uint TokenSource { get; set; }
    public bool AdminSpoofingUser { get; set; }
    public int TransportError { get; set; }
    public ulong Messageid { get; set; }
    public uint PublisherGroupId { get; set; }
    public uint Sysid { get; set; }
    public ulong TraceTag { get; set; }
    public uint WebapiKeyId { get; set; }
    public bool IsFromExternalSource { get; set; }
    public IProtobufRepeatedFieldValueType<uint> ForwardToSysid { get; }
    public uint CmSysid { get; set; }
    public uint LauncherType { get; set; }
    public uint Realm { get; set; }
    public int TimeoutMs { get; set; }
    public string DebugSource { get; set; }
    public uint Ip { get; set; }
    public byte[] IpV6 { get; set; }
}