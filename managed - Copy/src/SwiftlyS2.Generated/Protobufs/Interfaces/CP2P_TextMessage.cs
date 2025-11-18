
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CP2P_TextMessage : ITypedProtobuf<CP2P_TextMessage>
{
  static CP2P_TextMessage ITypedProtobuf<CP2P_TextMessage>.Wrap(nint handle, bool isManuallyAllocated) => new CP2P_TextMessageImpl(handle, isManuallyAllocated);


  public byte[] Text { get; set; }

}
