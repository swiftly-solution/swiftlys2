
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_ReloadEffect : ITypedProtobuf<CCSUsrMsg_ReloadEffect>, INetMessage<CCSUsrMsg_ReloadEffect>, IDisposable
{
  static int INetMessage<CCSUsrMsg_ReloadEffect>.MessageId => 326;
  
  static string INetMessage<CCSUsrMsg_ReloadEffect>.MessageName => "CCSUsrMsg_ReloadEffect";

  static CCSUsrMsg_ReloadEffect ITypedProtobuf<CCSUsrMsg_ReloadEffect>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_ReloadEffectImpl(handle, isManuallyAllocated);


  public int Entidx { get; set; }


  public int Actanim { get; set; }


  public float OriginX { get; set; }


  public float OriginY { get; set; }


  public float OriginZ { get; set; }

}
