
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_Party_SearchResults_EntryImpl : TypedProtobuf<CMsgGCCStrike15_v2_Party_SearchResults_Entry>, CMsgGCCStrike15_v2_Party_SearchResults_Entry
{
  public CMsgGCCStrike15_v2_Party_SearchResults_EntryImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Id
  { get => Accessor.GetUInt32("id"); set => Accessor.SetUInt32("id", value); }


  public uint Grp
  { get => Accessor.GetUInt32("grp"); set => Accessor.SetUInt32("grp", value); }


  public uint GameType
  { get => Accessor.GetUInt32("game_type"); set => Accessor.SetUInt32("game_type", value); }


  public uint Apr
  { get => Accessor.GetUInt32("apr"); set => Accessor.SetUInt32("apr", value); }


  public uint Ark
  { get => Accessor.GetUInt32("ark"); set => Accessor.SetUInt32("ark", value); }


  public uint Loc
  { get => Accessor.GetUInt32("loc"); set => Accessor.SetUInt32("loc", value); }


  public uint Accountid
  { get => Accessor.GetUInt32("accountid"); set => Accessor.SetUInt32("accountid", value); }

}
