
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgTEArmorRicochetImpl : NetMessage<CMsgTEArmorRicochet>, CMsgTEArmorRicochet
{
  public CMsgTEArmorRicochetImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public Vector Pos
  { get => Accessor.GetVector("pos"); set => Accessor.SetVector("pos", value); }


  public Vector Dir
  { get => Accessor.GetVector("dir"); set => Accessor.SetVector("dir", value); }

}
