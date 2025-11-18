
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgTEBeamEntPoint : ITypedProtobuf<CMsgTEBeamEntPoint>, INetMessage<CMsgTEBeamEntPoint>, IDisposable
{
  static int INetMessage<CMsgTEBeamEntPoint>.MessageId => 402;
  
  static string INetMessage<CMsgTEBeamEntPoint>.MessageName => "CMsgTEBeamEntPoint";

  static CMsgTEBeamEntPoint ITypedProtobuf<CMsgTEBeamEntPoint>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgTEBeamEntPointImpl(handle, isManuallyAllocated);


  public CMsgTEBaseBeam Base { get; }


  public uint Startentity { get; set; }


  public uint Endentity { get; set; }


  public Vector Start { get; set; }


  public Vector End { get; set; }

}
