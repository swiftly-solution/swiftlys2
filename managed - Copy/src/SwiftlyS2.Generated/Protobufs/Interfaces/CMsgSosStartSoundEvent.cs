
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgSosStartSoundEvent : ITypedProtobuf<CMsgSosStartSoundEvent>, INetMessage<CMsgSosStartSoundEvent>, IDisposable
{
  static int INetMessage<CMsgSosStartSoundEvent>.MessageId => 208;
  
  static string INetMessage<CMsgSosStartSoundEvent>.MessageName => "CMsgSosStartSoundEvent";

  static CMsgSosStartSoundEvent ITypedProtobuf<CMsgSosStartSoundEvent>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSosStartSoundEventImpl(handle, isManuallyAllocated);


  public int SoundeventGuid { get; set; }


  public uint SoundeventHash { get; set; }


  public int SourceEntityIndex { get; set; }


  public int Seed { get; set; }


  public byte[] PackedParams { get; set; }


  public float StartTime { get; set; }

}
