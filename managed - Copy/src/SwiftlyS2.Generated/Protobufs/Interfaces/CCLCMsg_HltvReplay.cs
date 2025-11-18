
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCLCMsg_HltvReplay : ITypedProtobuf<CCLCMsg_HltvReplay>, INetMessage<CCLCMsg_HltvReplay>, IDisposable
{
  static int INetMessage<CCLCMsg_HltvReplay>.MessageId => 36;
  
  static string INetMessage<CCLCMsg_HltvReplay>.MessageName => "CCLCMsg_HltvReplay";

  static CCLCMsg_HltvReplay ITypedProtobuf<CCLCMsg_HltvReplay>.Wrap(nint handle, bool isManuallyAllocated) => new CCLCMsg_HltvReplayImpl(handle, isManuallyAllocated);


  public int Request { get; set; }


  public float SlowdownLength { get; set; }


  public float SlowdownRate { get; set; }


  public int PrimaryTarget { get; set; }


  public float EventTime { get; set; }

}
