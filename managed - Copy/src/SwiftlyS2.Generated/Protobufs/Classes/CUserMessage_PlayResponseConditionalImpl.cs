
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessage_PlayResponseConditionalImpl : NetMessage<CUserMessage_PlayResponseConditional>, CUserMessage_PlayResponseConditional
{
  public CUserMessage_PlayResponseConditionalImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int EntIndex
  { get => Accessor.GetInt32("ent_index"); set => Accessor.SetInt32("ent_index", value); }


  public IProtobufRepeatedFieldValueType<int> PlayerSlots
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "player_slots"); }


  public string Response
  { get => Accessor.GetString("response"); set => Accessor.SetString("response", value); }


  public Vector EntOrigin
  { get => Accessor.GetVector("ent_origin"); set => Accessor.SetVector("ent_origin", value); }


  public float PreDelay
  { get => Accessor.GetFloat("pre_delay"); set => Accessor.SetFloat("pre_delay", value); }


  public int MixPriority
  { get => Accessor.GetInt32("mix_priority"); set => Accessor.SetInt32("mix_priority", value); }

}
