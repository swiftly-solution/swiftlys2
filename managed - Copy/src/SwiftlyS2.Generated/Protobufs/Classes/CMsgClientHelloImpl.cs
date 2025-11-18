
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgClientHelloImpl : TypedProtobuf<CMsgClientHello>, CMsgClientHello
{
  public CMsgClientHelloImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Version
  { get => Accessor.GetUInt32("version"); set => Accessor.SetUInt32("version", value); }


  public IProtobufRepeatedFieldSubMessageType<CMsgSOCacheHaveVersion> SocacheHaveVersions
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgSOCacheHaveVersion>(Accessor, "socache_have_versions"); }


  public uint ClientSessionNeed
  { get => Accessor.GetUInt32("client_session_need"); set => Accessor.SetUInt32("client_session_need", value); }


  public uint ClientLauncher
  { get => Accessor.GetUInt32("client_launcher"); set => Accessor.SetUInt32("client_launcher", value); }


  public uint PartnerSrcid
  { get => Accessor.GetUInt32("partner_srcid"); set => Accessor.SetUInt32("partner_srcid", value); }


  public uint PartnerAccountid
  { get => Accessor.GetUInt32("partner_accountid"); set => Accessor.SetUInt32("partner_accountid", value); }


  public uint PartnerAccountflags
  { get => Accessor.GetUInt32("partner_accountflags"); set => Accessor.SetUInt32("partner_accountflags", value); }


  public uint PartnerAccountbalance
  { get => Accessor.GetUInt32("partner_accountbalance"); set => Accessor.SetUInt32("partner_accountbalance", value); }


  public uint SteamLauncher
  { get => Accessor.GetUInt32("steam_launcher"); set => Accessor.SetUInt32("steam_launcher", value); }

}
