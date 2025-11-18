
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageCloseCaptionImpl : NetMessage<CUserMessageCloseCaption>, CUserMessageCloseCaption
{
  public CUserMessageCloseCaptionImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public uint Hash
  { get => Accessor.GetUInt32("hash"); set => Accessor.SetUInt32("hash", value); }


  public float Duration
  { get => Accessor.GetFloat("duration"); set => Accessor.SetFloat("duration", value); }


  public bool FromPlayer
  { get => Accessor.GetBool("from_player"); set => Accessor.SetBool("from_player", value); }


  public int EntIndex
  { get => Accessor.GetInt32("ent_index"); set => Accessor.SetInt32("ent_index", value); }

}
