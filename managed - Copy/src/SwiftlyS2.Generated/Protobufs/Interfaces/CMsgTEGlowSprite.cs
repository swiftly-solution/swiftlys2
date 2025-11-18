
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgTEGlowSprite : ITypedProtobuf<CMsgTEGlowSprite>, INetMessage<CMsgTEGlowSprite>, IDisposable
{
  static int INetMessage<CMsgTEGlowSprite>.MessageId => 415;
  
  static string INetMessage<CMsgTEGlowSprite>.MessageName => "CMsgTEGlowSprite";

  static CMsgTEGlowSprite ITypedProtobuf<CMsgTEGlowSprite>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgTEGlowSpriteImpl(handle, isManuallyAllocated);


  public Vector Origin { get; set; }


  public float Scale { get; set; }


  public float Life { get; set; }


  public uint Brightness { get; set; }

}
