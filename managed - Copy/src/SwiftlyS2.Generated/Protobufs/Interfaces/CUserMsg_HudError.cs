
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_HudError : ITypedProtobuf<CUserMsg_HudError>
{
  static CUserMsg_HudError ITypedProtobuf<CUserMsg_HudError>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_HudErrorImpl(handle, isManuallyAllocated);


  public int OrderId { get; set; }

}
