
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface MLDict : ITypedProtobuf<MLDict>
{
  static MLDict ITypedProtobuf<MLDict>.Wrap(nint handle, bool isManuallyAllocated) => new MLDictImpl(handle, isManuallyAllocated);


  public string Key { get; set; }


  public string ValString { get; set; }


  public int ValInt { get; set; }


  public float ValFloat { get; set; }

}
