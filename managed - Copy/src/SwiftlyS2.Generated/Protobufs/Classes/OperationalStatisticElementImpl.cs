
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class OperationalStatisticElementImpl : TypedProtobuf<OperationalStatisticElement>, OperationalStatisticElement
{
  public OperationalStatisticElementImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Idkey
  { get => Accessor.GetUInt32("idkey"); set => Accessor.SetUInt32("idkey", value); }


  public IProtobufRepeatedFieldValueType<int> Values
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "values"); }

}
