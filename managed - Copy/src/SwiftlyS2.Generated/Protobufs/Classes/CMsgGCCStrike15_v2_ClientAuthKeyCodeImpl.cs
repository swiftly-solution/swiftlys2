
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientAuthKeyCodeImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientAuthKeyCode>, CMsgGCCStrike15_v2_ClientAuthKeyCode
{
  public CMsgGCCStrike15_v2_ClientAuthKeyCodeImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Eventid
  { get => Accessor.GetUInt32("eventid"); set => Accessor.SetUInt32("eventid", value); }


  public string Code
  { get => Accessor.GetString("code"); set => Accessor.SetString("code", value); }

}
