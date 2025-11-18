
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgTEBaseBeam : ITypedProtobuf<CMsgTEBaseBeam>
{
  static CMsgTEBaseBeam ITypedProtobuf<CMsgTEBaseBeam>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgTEBaseBeamImpl(handle, isManuallyAllocated);


  public ulong Modelindex { get; set; }


  public ulong Haloindex { get; set; }


  public uint Startframe { get; set; }


  public uint Framerate { get; set; }


  public float Life { get; set; }


  public float Width { get; set; }


  public float Endwidth { get; set; }


  public uint Fadelength { get; set; }


  public float Amplitude { get; set; }


  public uint Color { get; set; }


  public uint Speed { get; set; }


  public uint Flags { get; set; }

}
