
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CGCToGCMsgMasterAck_ResponseImpl : TypedProtobuf<CGCToGCMsgMasterAck_Response>, CGCToGCMsgMasterAck_Response
{
  public CGCToGCMsgMasterAck_ResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Eresult
  { get => Accessor.GetInt32("eresult"); set => Accessor.SetInt32("eresult", value); }

}
