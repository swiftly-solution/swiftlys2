
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_Party_Search : ITypedProtobuf<CMsgGCCStrike15_v2_Party_Search>
{
  static CMsgGCCStrike15_v2_Party_Search ITypedProtobuf<CMsgGCCStrike15_v2_Party_Search>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_Party_SearchImpl(handle, isManuallyAllocated);


  public uint Ver { get; set; }


  public uint Apr { get; set; }


  public uint Ark { get; set; }


  public IProtobufRepeatedFieldValueType<uint> Grps { get; }


  public uint Launcher { get; set; }


  public uint GameType { get; set; }

}
