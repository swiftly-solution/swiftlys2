
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_WeaponSoundImpl : NetMessage<CCSUsrMsg_WeaponSound>, CCSUsrMsg_WeaponSound
{
  public CCSUsrMsg_WeaponSoundImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Entidx
  { get => Accessor.GetInt32("entidx"); set => Accessor.SetInt32("entidx", value); }


  public float OriginX
  { get => Accessor.GetFloat("origin_x"); set => Accessor.SetFloat("origin_x", value); }


  public float OriginY
  { get => Accessor.GetFloat("origin_y"); set => Accessor.SetFloat("origin_y", value); }


  public float OriginZ
  { get => Accessor.GetFloat("origin_z"); set => Accessor.SetFloat("origin_z", value); }


  public string Sound
  { get => Accessor.GetString("sound"); set => Accessor.SetString("sound", value); }


  public float GameTimestamp
  { get => Accessor.GetFloat("game_timestamp"); set => Accessor.SetFloat("game_timestamp", value); }


  public uint SourceSoundscapeid
  { get => Accessor.GetUInt32("source_soundscapeid"); set => Accessor.SetUInt32("source_soundscapeid", value); }

}
