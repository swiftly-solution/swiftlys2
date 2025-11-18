
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgTEBeamPointsImpl : NetMessage<CMsgTEBeamPoints>, CMsgTEBeamPoints
{
  public CMsgTEBeamPointsImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public CMsgTEBaseBeam Base
  { get => new CMsgTEBaseBeamImpl(NativeNetMessages.GetNestedMessage(Address, "base"), false); }


  public Vector Start
  { get => Accessor.GetVector("start"); set => Accessor.SetVector("start", value); }


  public Vector End
  { get => Accessor.GetVector("end"); set => Accessor.SetVector("end", value); }

}
