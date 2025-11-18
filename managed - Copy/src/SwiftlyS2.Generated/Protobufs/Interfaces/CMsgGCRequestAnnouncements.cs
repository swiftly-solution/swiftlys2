
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCRequestAnnouncements : ITypedProtobuf<CMsgGCRequestAnnouncements>
{
  static CMsgGCRequestAnnouncements ITypedProtobuf<CMsgGCRequestAnnouncements>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCRequestAnnouncementsImpl(handle, isManuallyAllocated);


}
