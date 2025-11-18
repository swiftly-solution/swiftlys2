
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCUpdateSessionIPImpl : TypedProtobuf<CMsgGCUpdateSessionIP>, CMsgGCUpdateSessionIP
{
  public CMsgGCUpdateSessionIPImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Steamid
  { get => Accessor.GetUInt64("steamid"); set => Accessor.SetUInt64("steamid", value); }


  public uint Ip
  { get => Accessor.GetUInt32("ip"); set => Accessor.SetUInt32("ip", value); }

}
