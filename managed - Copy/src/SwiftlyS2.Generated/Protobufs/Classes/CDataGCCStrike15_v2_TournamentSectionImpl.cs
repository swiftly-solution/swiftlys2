
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDataGCCStrike15_v2_TournamentSectionImpl : TypedProtobuf<CDataGCCStrike15_v2_TournamentSection>, CDataGCCStrike15_v2_TournamentSection
{
  public CDataGCCStrike15_v2_TournamentSectionImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Sectionid
  { get => Accessor.GetUInt32("sectionid"); set => Accessor.SetUInt32("sectionid", value); }


  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }


  public string Desc
  { get => Accessor.GetString("desc"); set => Accessor.SetString("desc", value); }


  public IProtobufRepeatedFieldSubMessageType<CDataGCCStrike15_v2_TournamentGroup> Groups
  { get => new ProtobufRepeatedFieldSubMessageType<CDataGCCStrike15_v2_TournamentGroup>(Accessor, "groups"); }

}
