
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_Party_SearchResults_Entry : ITypedProtobuf<CMsgGCCStrike15_v2_Party_SearchResults_Entry>
{
  static CMsgGCCStrike15_v2_Party_SearchResults_Entry ITypedProtobuf<CMsgGCCStrike15_v2_Party_SearchResults_Entry>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_Party_SearchResults_EntryImpl(handle, isManuallyAllocated);


  public uint Id { get; set; }


  public uint Grp { get; set; }


  public uint GameType { get; set; }


  public uint Apr { get; set; }


  public uint Ark { get; set; }


  public uint Loc { get; set; }


  public uint Accountid { get; set; }

}
