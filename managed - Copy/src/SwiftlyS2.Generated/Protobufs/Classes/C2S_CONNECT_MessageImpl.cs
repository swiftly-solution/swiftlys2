
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class C2S_CONNECT_MessageImpl : TypedProtobuf<C2S_CONNECT_Message>, C2S_CONNECT_Message
{
  public C2S_CONNECT_MessageImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint HostVersion
  { get => Accessor.GetUInt32("host_version"); set => Accessor.SetUInt32("host_version", value); }


  public uint AuthProtocol
  { get => Accessor.GetUInt32("auth_protocol"); set => Accessor.SetUInt32("auth_protocol", value); }


  public uint ChallengeNumber
  { get => Accessor.GetUInt32("challenge_number"); set => Accessor.SetUInt32("challenge_number", value); }


  public ulong ReservationCookie
  { get => Accessor.GetUInt64("reservation_cookie"); set => Accessor.SetUInt64("reservation_cookie", value); }


  public bool LowViolence
  { get => Accessor.GetBool("low_violence"); set => Accessor.SetBool("low_violence", value); }


  public byte[] EncryptedPassword
  { get => Accessor.GetBytes("encrypted_password"); set => Accessor.SetBytes("encrypted_password", value); }


  public IProtobufRepeatedFieldSubMessageType<CCLCMsg_SplitPlayerConnect> Splitplayers
  { get => new ProtobufRepeatedFieldSubMessageType<CCLCMsg_SplitPlayerConnect>(Accessor, "splitplayers"); }


  public byte[] AuthSteam
  { get => Accessor.GetBytes("auth_steam"); set => Accessor.SetBytes("auth_steam", value); }


  public string ChallengeContext
  { get => Accessor.GetString("challenge_context"); set => Accessor.SetString("challenge_context", value); }


  public C2S_CONNECT_SameProcessCheck LocalhostSameProcessCheck
  { get => new C2S_CONNECT_SameProcessCheckImpl(NativeNetMessages.GetNestedMessage(Address, "localhost_same_process_check"), false); }

}
