
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class MLWeaponStateImpl : TypedProtobuf<MLWeaponState>, MLWeaponState
{
  public MLWeaponStateImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Index
  { get => Accessor.GetInt32("index"); set => Accessor.SetInt32("index", value); }


  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }


  public EWeaponType Type
  { get => (EWeaponType)Accessor.GetInt32("type"); set => Accessor.SetInt32("type", (int)value); }


  public int AmmoClip
  { get => Accessor.GetInt32("ammo_clip"); set => Accessor.SetInt32("ammo_clip", value); }


  public int AmmoClipMax
  { get => Accessor.GetInt32("ammo_clip_max"); set => Accessor.SetInt32("ammo_clip_max", value); }


  public int AmmoReserve
  { get => Accessor.GetInt32("ammo_reserve"); set => Accessor.SetInt32("ammo_reserve", value); }


  public string State
  { get => Accessor.GetString("state"); set => Accessor.SetString("state", value); }


  public float RecoilIndex
  { get => Accessor.GetFloat("recoil_index"); set => Accessor.SetFloat("recoil_index", value); }

}
