
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgAdjustItemEquippedStateMultiImpl : TypedProtobuf<CMsgAdjustItemEquippedStateMulti>, CMsgAdjustItemEquippedStateMulti
{
  public CMsgAdjustItemEquippedStateMultiImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldValueType<ulong> TEquips
  { get => new ProtobufRepeatedFieldValueType<ulong>(Accessor, "t_equips"); }


  public IProtobufRepeatedFieldValueType<ulong> CtEquips
  { get => new ProtobufRepeatedFieldValueType<ulong>(Accessor, "ct_equips"); }


  public IProtobufRepeatedFieldValueType<ulong> NoteamEquips
  { get => new ProtobufRepeatedFieldValueType<ulong>(Accessor, "noteam_equips"); }

}
