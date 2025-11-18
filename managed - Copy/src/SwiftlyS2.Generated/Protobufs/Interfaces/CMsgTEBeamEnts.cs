
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgTEBeamEnts : ITypedProtobuf<CMsgTEBeamEnts>, INetMessage<CMsgTEBeamEnts>, IDisposable
{
  static int INetMessage<CMsgTEBeamEnts>.MessageId => 403;
  
  static string INetMessage<CMsgTEBeamEnts>.MessageName => "CMsgTEBeamEnts";

  static CMsgTEBeamEnts ITypedProtobuf<CMsgTEBeamEnts>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgTEBeamEntsImpl(handle, isManuallyAllocated);


  public CMsgTEBaseBeam Base { get; }


  public uint Startentity { get; set; }


  public uint Endentity { get; set; }

}
