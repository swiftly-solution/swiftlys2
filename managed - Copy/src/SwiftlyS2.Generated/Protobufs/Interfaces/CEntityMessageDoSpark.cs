
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CEntityMessageDoSpark : ITypedProtobuf<CEntityMessageDoSpark>
{
  static CEntityMessageDoSpark ITypedProtobuf<CEntityMessageDoSpark>.Wrap(nint handle, bool isManuallyAllocated) => new CEntityMessageDoSparkImpl(handle, isManuallyAllocated);


  public Vector Origin { get; set; }


  public int Entityindex { get; set; }


  public float Radius { get; set; }


  public uint Color { get; set; }


  public uint Beams { get; set; }


  public float Thick { get; set; }


  public float Duration { get; set; }


  public CEntityMsg EntityMsg { get; }

}
