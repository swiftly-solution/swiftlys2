
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_CloseCaptionImpl : NetMessage<CCSUsrMsg_CloseCaption>, CCSUsrMsg_CloseCaption
{
  public CCSUsrMsg_CloseCaptionImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public uint Hash
  { get => Accessor.GetUInt32("hash"); set => Accessor.SetUInt32("hash", value); }


  public int Duration
  { get => Accessor.GetInt32("duration"); set => Accessor.SetInt32("duration", value); }


  public bool FromPlayer
  { get => Accessor.GetBool("from_player"); set => Accessor.SetBool("from_player", value); }


  public string Cctoken
  { get => Accessor.GetString("cctoken"); set => Accessor.SetString("cctoken", value); }

}
