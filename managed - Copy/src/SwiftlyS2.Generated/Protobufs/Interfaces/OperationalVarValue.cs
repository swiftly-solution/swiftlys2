
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface OperationalVarValue : ITypedProtobuf<OperationalVarValue>
{
  static OperationalVarValue ITypedProtobuf<OperationalVarValue>.Wrap(nint handle, bool isManuallyAllocated) => new OperationalVarValueImpl(handle, isManuallyAllocated);


  public string Name { get; set; }


  public int Ivalue { get; set; }


  public float Fvalue { get; set; }


  public byte[] Svalue { get; set; }

}
