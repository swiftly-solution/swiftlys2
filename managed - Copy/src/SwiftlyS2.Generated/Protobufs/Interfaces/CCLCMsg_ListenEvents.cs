
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCLCMsg_ListenEvents : ITypedProtobuf<CCLCMsg_ListenEvents>
{
  static CCLCMsg_ListenEvents ITypedProtobuf<CCLCMsg_ListenEvents>.Wrap(nint handle, bool isManuallyAllocated) => new CCLCMsg_ListenEventsImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldValueType<uint> EventMask { get; }

}
