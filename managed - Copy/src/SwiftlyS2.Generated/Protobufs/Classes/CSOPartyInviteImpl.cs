
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSOPartyInviteImpl : TypedProtobuf<CSOPartyInvite>, CSOPartyInvite
{
  public CSOPartyInviteImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong GroupId
  { get => Accessor.GetUInt64("group_id"); set => Accessor.SetUInt64("group_id", value); }


  public ulong SenderId
  { get => Accessor.GetUInt64("sender_id"); set => Accessor.SetUInt64("sender_id", value); }


  public string SenderName
  { get => Accessor.GetString("sender_name"); set => Accessor.SetString("sender_name", value); }

}
