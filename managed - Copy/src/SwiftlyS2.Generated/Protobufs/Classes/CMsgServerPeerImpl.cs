
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgServerPeerImpl : TypedProtobuf<CMsgServerPeer>, CMsgServerPeer
{
  public CMsgServerPeerImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int PlayerSlot
  { get => Accessor.GetInt32("player_slot"); set => Accessor.SetInt32("player_slot", value); }


  public ulong Steamid
  { get => Accessor.GetUInt64("steamid"); set => Accessor.SetUInt64("steamid", value); }


  public CMsgIPCAddress Ipc
  { get => new CMsgIPCAddressImpl(NativeNetMessages.GetNestedMessage(Address, "ipc"), false); }


  public bool TheyHearYou
  { get => Accessor.GetBool("they_hear_you"); set => Accessor.SetBool("they_hear_you", value); }


  public bool YouHearThem
  { get => Accessor.GetBool("you_hear_them"); set => Accessor.SetBool("you_hear_them", value); }


  public bool IsListenserverHost
  { get => Accessor.GetBool("is_listenserver_host"); set => Accessor.SetBool("is_listenserver_host", value); }

}
