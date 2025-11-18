
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageCurrentTimescaleImpl : NetMessage<CUserMessageCurrentTimescale>, CUserMessageCurrentTimescale
{
  public CUserMessageCurrentTimescaleImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public float Current
  { get => Accessor.GetFloat("current"); set => Accessor.SetFloat("current", value); }

}
