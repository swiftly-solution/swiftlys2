
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCHAccountPhoneNumberChangeImpl : TypedProtobuf<CMsgGCHAccountPhoneNumberChange>, CMsgGCHAccountPhoneNumberChange
{
  public CMsgGCHAccountPhoneNumberChangeImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Steamid
  { get => Accessor.GetUInt64("steamid"); set => Accessor.SetUInt64("steamid", value); }


  public uint Appid
  { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }


  public ulong PhoneId
  { get => Accessor.GetUInt64("phone_id"); set => Accessor.SetUInt64("phone_id", value); }


  public bool IsVerified
  { get => Accessor.GetBool("is_verified"); set => Accessor.SetBool("is_verified", value); }


  public bool IsIdentifying
  { get => Accessor.GetBool("is_identifying"); set => Accessor.SetBool("is_identifying", value); }

}
