
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_ShakeImpl : NetMessage<CCSUsrMsg_Shake>, CCSUsrMsg_Shake
{
  public CCSUsrMsg_ShakeImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Command
  { get => Accessor.GetInt32("command"); set => Accessor.SetInt32("command", value); }


  public float LocalAmplitude
  { get => Accessor.GetFloat("local_amplitude"); set => Accessor.SetFloat("local_amplitude", value); }


  public float Frequency
  { get => Accessor.GetFloat("frequency"); set => Accessor.SetFloat("frequency", value); }


  public float Duration
  { get => Accessor.GetFloat("duration"); set => Accessor.SetFloat("duration", value); }

}
