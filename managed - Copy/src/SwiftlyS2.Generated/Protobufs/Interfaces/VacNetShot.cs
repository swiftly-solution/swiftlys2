
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface VacNetShot : ITypedProtobuf<VacNetShot>
{
  static VacNetShot ITypedProtobuf<VacNetShot>.Wrap(nint handle, bool isManuallyAllocated) => new VacNetShotImpl(handle, isManuallyAllocated);


  public ulong SteamidPlayer { get; set; }


  public int RoundNumber { get; set; }


  public int HitType { get; set; }


  public int WeaponType { get; set; }


  public float DistanceToHurtTarget { get; set; }


  public IProtobufRepeatedFieldValueType<float> DeltaYawWindow { get; }


  public IProtobufRepeatedFieldValueType<float> DeltaPitchWindow { get; }

}
