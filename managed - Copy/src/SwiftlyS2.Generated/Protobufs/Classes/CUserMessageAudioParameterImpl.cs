
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageAudioParameterImpl : NetMessage<CUserMessageAudioParameter>, CUserMessageAudioParameter
{
  public CUserMessageAudioParameterImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public uint ParameterType
  { get => Accessor.GetUInt32("parameter_type"); set => Accessor.SetUInt32("parameter_type", value); }


  public uint NameHashCode
  { get => Accessor.GetUInt32("name_hash_code"); set => Accessor.SetUInt32("name_hash_code", value); }


  public float Value
  { get => Accessor.GetFloat("value"); set => Accessor.SetFloat("value", value); }


  public uint IntValue
  { get => Accessor.GetUInt32("int_value"); set => Accessor.SetUInt32("int_value", value); }

}
