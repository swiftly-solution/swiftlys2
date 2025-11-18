
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgTEEnergySplash : ITypedProtobuf<CMsgTEEnergySplash>, INetMessage<CMsgTEEnergySplash>, IDisposable
{
  static int INetMessage<CMsgTEEnergySplash>.MessageId => 412;
  
  static string INetMessage<CMsgTEEnergySplash>.MessageName => "CMsgTEEnergySplash";

  static CMsgTEEnergySplash ITypedProtobuf<CMsgTEEnergySplash>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgTEEnergySplashImpl(handle, isManuallyAllocated);


  public Vector Pos { get; set; }


  public Vector Dir { get; set; }


  public bool Explosive { get; set; }

}
