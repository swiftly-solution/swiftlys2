
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgTEDecal : ITypedProtobuf<CMsgTEDecal>, INetMessage<CMsgTEDecal>, IDisposable
{
  static int INetMessage<CMsgTEDecal>.MessageId => 410;
  
  static string INetMessage<CMsgTEDecal>.MessageName => "CMsgTEDecal";

  static CMsgTEDecal ITypedProtobuf<CMsgTEDecal>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgTEDecalImpl(handle, isManuallyAllocated);


  public Vector Origin { get; set; }


  public Vector Start { get; set; }


  public int Entity { get; set; }


  public uint Hitbox { get; set; }


  public uint Index { get; set; }

}
