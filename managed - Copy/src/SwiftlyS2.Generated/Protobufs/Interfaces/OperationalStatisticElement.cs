
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface OperationalStatisticElement : ITypedProtobuf<OperationalStatisticElement>
{
  static OperationalStatisticElement ITypedProtobuf<OperationalStatisticElement>.Wrap(nint handle, bool isManuallyAllocated) => new OperationalStatisticElementImpl(handle, isManuallyAllocated);


  public uint Idkey { get; set; }


  public IProtobufRepeatedFieldValueType<int> Values { get; }

}
