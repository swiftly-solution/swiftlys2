
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgServerHelloImpl : TypedProtobuf<CMsgServerHello>, CMsgServerHello
{
  public CMsgServerHelloImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Version
  { get => Accessor.GetUInt32("version"); set => Accessor.SetUInt32("version", value); }


  public IProtobufRepeatedFieldSubMessageType<CMsgSOCacheHaveVersion> SocacheHaveVersions
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgSOCacheHaveVersion>(Accessor, "socache_have_versions"); }


  public uint LegacyClientSessionNeed
  { get => Accessor.GetUInt32("legacy_client_session_need"); set => Accessor.SetUInt32("legacy_client_session_need", value); }


  public uint ClientLauncher
  { get => Accessor.GetUInt32("client_launcher"); set => Accessor.SetUInt32("client_launcher", value); }


  public byte[] LegacySteamdatagramRouting
  { get => Accessor.GetBytes("legacy_steamdatagram_routing"); set => Accessor.SetBytes("legacy_steamdatagram_routing", value); }


  public uint RequiredInternalAddr
  { get => Accessor.GetUInt32("required_internal_addr"); set => Accessor.SetUInt32("required_internal_addr", value); }


  public byte[] SteamdatagramLogin
  { get => Accessor.GetBytes("steamdatagram_login"); set => Accessor.SetBytes("steamdatagram_login", value); }


  public uint SocacheControl
  { get => Accessor.GetUInt32("socache_control"); set => Accessor.SetUInt32("socache_control", value); }

}
