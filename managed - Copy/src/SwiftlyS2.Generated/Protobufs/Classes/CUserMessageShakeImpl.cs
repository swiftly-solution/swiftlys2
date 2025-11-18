
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageShakeImpl : NetMessage<CUserMessageShake>, CUserMessageShake
{
  public CUserMessageShakeImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public uint Command
  { get => Accessor.GetUInt32("command"); set => Accessor.SetUInt32("command", value); }


  public float Amplitude
  { get => Accessor.GetFloat("amplitude"); set => Accessor.SetFloat("amplitude", value); }


  public float Frequency
  { get => Accessor.GetFloat("frequency"); set => Accessor.SetFloat("frequency", value); }


  public float Duration
  { get => Accessor.GetFloat("duration"); set => Accessor.SetFloat("duration", value); }

}
