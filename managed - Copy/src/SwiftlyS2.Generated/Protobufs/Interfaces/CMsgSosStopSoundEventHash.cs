
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgSosStopSoundEventHash : ITypedProtobuf<CMsgSosStopSoundEventHash>, INetMessage<CMsgSosStopSoundEventHash>, IDisposable
{
  static int INetMessage<CMsgSosStopSoundEventHash>.MessageId => 212;
  
  static string INetMessage<CMsgSosStopSoundEventHash>.MessageName => "CMsgSosStopSoundEventHash";

  static CMsgSosStopSoundEventHash ITypedProtobuf<CMsgSosStopSoundEventHash>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSosStopSoundEventHashImpl(handle, isManuallyAllocated);


  public uint SoundeventHash { get; set; }


  public int SourceEntityIndex { get; set; }

}
