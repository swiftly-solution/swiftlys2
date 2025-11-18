
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCToGCIsTrustedServerResponseImpl : TypedProtobuf<CMsgGCToGCIsTrustedServerResponse>, CMsgGCToGCIsTrustedServerResponse
{
  public CMsgGCToGCIsTrustedServerResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool IsTrusted
  { get => Accessor.GetBool("is_trusted"); set => Accessor.SetBool("is_trusted", value); }

}
