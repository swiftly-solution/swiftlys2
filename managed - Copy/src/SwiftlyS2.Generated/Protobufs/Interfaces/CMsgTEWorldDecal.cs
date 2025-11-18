
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgTEWorldDecal : ITypedProtobuf<CMsgTEWorldDecal>, INetMessage<CMsgTEWorldDecal>, IDisposable
{
  static int INetMessage<CMsgTEWorldDecal>.MessageId => 411;
  
  static string INetMessage<CMsgTEWorldDecal>.MessageName => "CMsgTEWorldDecal";

  static CMsgTEWorldDecal ITypedProtobuf<CMsgTEWorldDecal>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgTEWorldDecalImpl(handle, isManuallyAllocated);


  public Vector Origin { get; set; }


  public Vector Normal { get; set; }


  public uint Index { get; set; }

}
