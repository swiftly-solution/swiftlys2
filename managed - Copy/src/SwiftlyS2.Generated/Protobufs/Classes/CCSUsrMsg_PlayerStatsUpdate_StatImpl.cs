
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_PlayerStatsUpdate_StatImpl : TypedProtobuf<CCSUsrMsg_PlayerStatsUpdate_Stat>, CCSUsrMsg_PlayerStatsUpdate_Stat
{
  public CCSUsrMsg_PlayerStatsUpdate_StatImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Idx
  { get => Accessor.GetInt32("idx"); set => Accessor.SetInt32("idx", value); }


  public int Delta
  { get => Accessor.GetInt32("delta"); set => Accessor.SetInt32("delta", value); }

}
