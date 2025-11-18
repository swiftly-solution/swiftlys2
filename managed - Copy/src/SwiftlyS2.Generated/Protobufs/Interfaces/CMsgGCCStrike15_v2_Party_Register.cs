
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_Party_Register : ITypedProtobuf<CMsgGCCStrike15_v2_Party_Register>
{
  static CMsgGCCStrike15_v2_Party_Register ITypedProtobuf<CMsgGCCStrike15_v2_Party_Register>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_Party_RegisterImpl(handle, isManuallyAllocated);


  public uint Id { get; set; }


  public uint Ver { get; set; }


  public uint Apr { get; set; }


  public uint Ark { get; set; }


  public uint Nby { get; set; }


  public uint Grp { get; set; }


  public uint Slots { get; set; }


  public uint Launcher { get; set; }


  public uint GameType { get; set; }

}
