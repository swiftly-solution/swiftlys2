
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientRequestSouvenirImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientRequestSouvenir>, CMsgGCCStrike15_v2_ClientRequestSouvenir
{
  public CMsgGCCStrike15_v2_ClientRequestSouvenirImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Itemid
  { get => Accessor.GetUInt64("itemid"); set => Accessor.SetUInt64("itemid", value); }


  public ulong Matchid
  { get => Accessor.GetUInt64("matchid"); set => Accessor.SetUInt64("matchid", value); }


  public int Eventid
  { get => Accessor.GetInt32("eventid"); set => Accessor.SetInt32("eventid", value); }

}
