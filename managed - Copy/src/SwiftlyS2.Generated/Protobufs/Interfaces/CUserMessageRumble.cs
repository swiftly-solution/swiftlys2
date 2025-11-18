
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageRumble : ITypedProtobuf<CUserMessageRumble>, INetMessage<CUserMessageRumble>, IDisposable
{
  static int INetMessage<CUserMessageRumble>.MessageId => 116;
  
  static string INetMessage<CUserMessageRumble>.MessageName => "CUserMessageRumble";

  static CUserMessageRumble ITypedProtobuf<CUserMessageRumble>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageRumbleImpl(handle, isManuallyAllocated);


  public int Index { get; set; }


  public int Data { get; set; }


  public int Flags { get; set; }

}
