
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_XRankGetImpl : NetMessage<CCSUsrMsg_XRankGet>, CCSUsrMsg_XRankGet
{
  public CCSUsrMsg_XRankGetImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int ModeIdx
  { get => Accessor.GetInt32("mode_idx"); set => Accessor.SetInt32("mode_idx", value); }


  public int Controller
  { get => Accessor.GetInt32("controller"); set => Accessor.SetInt32("controller", value); }

}
