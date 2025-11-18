
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageLagCompensationErrorImpl : NetMessage<CUserMessageLagCompensationError>, CUserMessageLagCompensationError
{
  public CUserMessageLagCompensationErrorImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public float Distance
  { get => Accessor.GetFloat("distance"); set => Accessor.SetFloat("distance", value); }

}
