
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientRequestPlayersProfileImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientRequestPlayersProfile>, CMsgGCCStrike15_v2_ClientRequestPlayersProfile
{
  public CMsgGCCStrike15_v2_ClientRequestPlayersProfileImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint RequestIdDeprecated
  { get => Accessor.GetUInt32("request_id__deprecated"); set => Accessor.SetUInt32("request_id__deprecated", value); }


  public IProtobufRepeatedFieldValueType<uint> AccountIdsDeprecated
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "account_ids__deprecated"); }


  public uint AccountId
  { get => Accessor.GetUInt32("account_id"); set => Accessor.SetUInt32("account_id", value); }


  public uint RequestLevel
  { get => Accessor.GetUInt32("request_level"); set => Accessor.SetUInt32("request_level", value); }

}
