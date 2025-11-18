
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgSource1LegacyListenEvents : ITypedProtobuf<CMsgSource1LegacyListenEvents>, INetMessage<CMsgSource1LegacyListenEvents>, IDisposable
{
  static int INetMessage<CMsgSource1LegacyListenEvents>.MessageId => 206;
  
  static string INetMessage<CMsgSource1LegacyListenEvents>.MessageName => "CMsgSource1LegacyListenEvents";

  static CMsgSource1LegacyListenEvents ITypedProtobuf<CMsgSource1LegacyListenEvents>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSource1LegacyListenEventsImpl(handle, isManuallyAllocated);


  public int Playerslot { get; set; }


  public IProtobufRepeatedFieldValueType<uint> Eventarraybits { get; }

}
