
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgSosStopSoundEvent : ITypedProtobuf<CMsgSosStopSoundEvent>, INetMessage<CMsgSosStopSoundEvent>, IDisposable
{
  static int INetMessage<CMsgSosStopSoundEvent>.MessageId => 209;
  
  static string INetMessage<CMsgSosStopSoundEvent>.MessageName => "CMsgSosStopSoundEvent";

  static CMsgSosStopSoundEvent ITypedProtobuf<CMsgSosStopSoundEvent>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSosStopSoundEventImpl(handle, isManuallyAllocated);


  public int SoundeventGuid { get; set; }

}
