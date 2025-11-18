
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface NetMessageSplitscreenUserChanged : ITypedProtobuf<NetMessageSplitscreenUserChanged>
{
  static NetMessageSplitscreenUserChanged ITypedProtobuf<NetMessageSplitscreenUserChanged>.Wrap(nint handle, bool isManuallyAllocated) => new NetMessageSplitscreenUserChangedImpl(handle, isManuallyAllocated);


  public uint Slot { get; set; }

}
