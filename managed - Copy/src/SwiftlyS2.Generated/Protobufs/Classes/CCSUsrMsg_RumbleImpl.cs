
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_RumbleImpl : NetMessage<CCSUsrMsg_Rumble>, CCSUsrMsg_Rumble
{
  public CCSUsrMsg_RumbleImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Index
  { get => Accessor.GetInt32("index"); set => Accessor.SetInt32("index", value); }


  public int Data
  { get => Accessor.GetInt32("data"); set => Accessor.SetInt32("data", value); }


  public int Flags
  { get => Accessor.GetInt32("flags"); set => Accessor.SetInt32("flags", value); }

}
