
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgTEExplosion : ITypedProtobuf<CMsgTEExplosion>, INetMessage<CMsgTEExplosion>, IDisposable
{
  static int INetMessage<CMsgTEExplosion>.MessageId => 419;
  
  static string INetMessage<CMsgTEExplosion>.MessageName => "CMsgTEExplosion";

  static CMsgTEExplosion ITypedProtobuf<CMsgTEExplosion>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgTEExplosionImpl(handle, isManuallyAllocated);


  public Vector Origin { get; set; }


  public uint Flags { get; set; }


  public Vector Normal { get; set; }


  public uint Radius { get; set; }


  public uint Magnitude { get; set; }


  public bool AffectRagdolls { get; set; }


  public string SoundName { get; set; }


  public uint ExplosionType { get; set; }


  public bool CreateDebris { get; set; }


  public Vector DebrisOrigin { get; set; }


  public uint DebrisSurfaceprop { get; set; }

}
