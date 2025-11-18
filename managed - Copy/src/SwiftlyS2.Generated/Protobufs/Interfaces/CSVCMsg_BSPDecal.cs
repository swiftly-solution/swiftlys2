
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_BSPDecal : ITypedProtobuf<CSVCMsg_BSPDecal>, INetMessage<CSVCMsg_BSPDecal>, IDisposable
{
  static int INetMessage<CSVCMsg_BSPDecal>.MessageId => 53;
  
  static string INetMessage<CSVCMsg_BSPDecal>.MessageName => "CSVCMsg_BSPDecal";

  static CSVCMsg_BSPDecal ITypedProtobuf<CSVCMsg_BSPDecal>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_BSPDecalImpl(handle, isManuallyAllocated);


  public Vector Pos { get; set; }


  public int DecalTextureIndex { get; set; }


  public int EntityIndex { get; set; }


  public int ModelIndex { get; set; }


  public bool LowPriority { get; set; }

}
