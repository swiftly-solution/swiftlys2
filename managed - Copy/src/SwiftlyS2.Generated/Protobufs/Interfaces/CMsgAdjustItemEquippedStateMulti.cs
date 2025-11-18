
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgAdjustItemEquippedStateMulti : ITypedProtobuf<CMsgAdjustItemEquippedStateMulti>
{
  static CMsgAdjustItemEquippedStateMulti ITypedProtobuf<CMsgAdjustItemEquippedStateMulti>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgAdjustItemEquippedStateMultiImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldValueType<ulong> TEquips { get; }


  public IProtobufRepeatedFieldValueType<ulong> CtEquips { get; }


  public IProtobufRepeatedFieldValueType<ulong> NoteamEquips { get; }

}
