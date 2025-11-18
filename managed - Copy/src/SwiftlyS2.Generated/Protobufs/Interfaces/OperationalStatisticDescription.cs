
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface OperationalStatisticDescription : ITypedProtobuf<OperationalStatisticDescription>
{
  static OperationalStatisticDescription ITypedProtobuf<OperationalStatisticDescription>.Wrap(nint handle, bool isManuallyAllocated) => new OperationalStatisticDescriptionImpl(handle, isManuallyAllocated);


  public string Name { get; set; }


  public uint Idkey { get; set; }

}
