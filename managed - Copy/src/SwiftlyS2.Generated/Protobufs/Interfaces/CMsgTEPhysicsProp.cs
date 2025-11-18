
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgTEPhysicsProp : ITypedProtobuf<CMsgTEPhysicsProp>, INetMessage<CMsgTEPhysicsProp>, IDisposable
{
  static int INetMessage<CMsgTEPhysicsProp>.MessageId => 423;
  
  static string INetMessage<CMsgTEPhysicsProp>.MessageName => "CMsgTEPhysicsProp";

  static CMsgTEPhysicsProp ITypedProtobuf<CMsgTEPhysicsProp>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgTEPhysicsPropImpl(handle, isManuallyAllocated);


  public Vector Origin { get; set; }


  public Vector Velocity { get; set; }


  public QAngle Angles { get; set; }


  public uint Skin { get; set; }


  public uint Flags { get; set; }


  public uint Effects { get; set; }


  public uint Color { get; set; }


  public ulong Modelindex { get; set; }


  public uint UnusedBreakmodelsnottomake { get; set; }


  public float Scale { get; set; }


  public Vector Dmgpos { get; set; }


  public Vector Dmgdir { get; set; }


  public int Dmgtype { get; set; }

}
