
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageVoiceMaskImpl : NetMessage<CUserMessageVoiceMask>, CUserMessageVoiceMask
{
  public CUserMessageVoiceMaskImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public IProtobufRepeatedFieldValueType<uint> GamerulesMasks
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "gamerules_masks"); }


  public IProtobufRepeatedFieldValueType<uint> BanMasks
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "ban_masks"); }


  public bool ModEnable
  { get => Accessor.GetBool("mod_enable"); set => Accessor.SetBool("mod_enable", value); }

}
