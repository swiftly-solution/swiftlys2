
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageDesiredTimescaleImpl : NetMessage<CUserMessageDesiredTimescale>, CUserMessageDesiredTimescale
{
  public CUserMessageDesiredTimescaleImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public float Desired
  { get => Accessor.GetFloat("desired"); set => Accessor.SetFloat("desired", value); }


  public float Acceleration
  { get => Accessor.GetFloat("acceleration"); set => Accessor.SetFloat("acceleration", value); }


  public float Minblendrate
  { get => Accessor.GetFloat("minblendrate"); set => Accessor.SetFloat("minblendrate", value); }


  public float Blenddeltamultiplier
  { get => Accessor.GetFloat("blenddeltamultiplier"); set => Accessor.SetFloat("blenddeltamultiplier", value); }

}
