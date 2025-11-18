
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGC_GlobalGame_PlayImpl : TypedProtobuf<CMsgGC_GlobalGame_Play>, CMsgGC_GlobalGame_Play
{
  public CMsgGC_GlobalGame_PlayImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Ticket
  { get => Accessor.GetUInt64("ticket"); set => Accessor.SetUInt64("ticket", value); }


  public uint Gametimems
  { get => Accessor.GetUInt32("gametimems"); set => Accessor.SetUInt32("gametimems", value); }


  public uint Msperpoint
  { get => Accessor.GetUInt32("msperpoint"); set => Accessor.SetUInt32("msperpoint", value); }

}
