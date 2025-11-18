
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface MLWeaponState : ITypedProtobuf<MLWeaponState>
{
  static MLWeaponState ITypedProtobuf<MLWeaponState>.Wrap(nint handle, bool isManuallyAllocated) => new MLWeaponStateImpl(handle, isManuallyAllocated);


  public int Index { get; set; }


  public string Name { get; set; }


  public EWeaponType Type { get; set; }


  public int AmmoClip { get; set; }


  public int AmmoClipMax { get; set; }


  public int AmmoReserve { get; set; }


  public string State { get; set; }


  public float RecoilIndex { get; set; }

}
