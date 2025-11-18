
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientLogonFatalErrorImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientLogonFatalError>, CMsgGCCStrike15_v2_ClientLogonFatalError
{
  public CMsgGCCStrike15_v2_ClientLogonFatalErrorImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Errorcode
  { get => Accessor.GetUInt32("errorcode"); set => Accessor.SetUInt32("errorcode", value); }


  public string Message
  { get => Accessor.GetString("message"); set => Accessor.SetString("message", value); }


  public string Country
  { get => Accessor.GetString("country"); set => Accessor.SetString("country", value); }

}
