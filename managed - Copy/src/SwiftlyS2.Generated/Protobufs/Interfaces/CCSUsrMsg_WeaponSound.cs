
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_WeaponSound : ITypedProtobuf<CCSUsrMsg_WeaponSound>, INetMessage<CCSUsrMsg_WeaponSound>, IDisposable
{
  static int INetMessage<CCSUsrMsg_WeaponSound>.MessageId => 369;
  
  static string INetMessage<CCSUsrMsg_WeaponSound>.MessageName => "CCSUsrMsg_WeaponSound";

  static CCSUsrMsg_WeaponSound ITypedProtobuf<CCSUsrMsg_WeaponSound>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_WeaponSoundImpl(handle, isManuallyAllocated);


  public int Entidx { get; set; }


  public float OriginX { get; set; }


  public float OriginY { get; set; }


  public float OriginZ { get; set; }


  public string Sound { get; set; }


  public float GameTimestamp { get; set; }


  public uint SourceSoundscapeid { get; set; }

}
