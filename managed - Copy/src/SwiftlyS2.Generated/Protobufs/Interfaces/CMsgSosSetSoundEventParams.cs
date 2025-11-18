
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgSosSetSoundEventParams : ITypedProtobuf<CMsgSosSetSoundEventParams>, INetMessage<CMsgSosSetSoundEventParams>, IDisposable
{
  static int INetMessage<CMsgSosSetSoundEventParams>.MessageId => 210;
  
  static string INetMessage<CMsgSosSetSoundEventParams>.MessageName => "CMsgSosSetSoundEventParams";

  static CMsgSosSetSoundEventParams ITypedProtobuf<CMsgSosSetSoundEventParams>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSosSetSoundEventParamsImpl(handle, isManuallyAllocated);


  public int SoundeventGuid { get; set; }


  public byte[] PackedParams { get; set; }

}
