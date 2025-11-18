
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgTEBeamRing : ITypedProtobuf<CMsgTEBeamRing>, INetMessage<CMsgTEBeamRing>, IDisposable
{
  static int INetMessage<CMsgTEBeamRing>.MessageId => 405;
  
  static string INetMessage<CMsgTEBeamRing>.MessageName => "CMsgTEBeamRing";

  static CMsgTEBeamRing ITypedProtobuf<CMsgTEBeamRing>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgTEBeamRingImpl(handle, isManuallyAllocated);


  public CMsgTEBaseBeam Base { get; }


  public uint Startentity { get; set; }


  public uint Endentity { get; set; }

}
