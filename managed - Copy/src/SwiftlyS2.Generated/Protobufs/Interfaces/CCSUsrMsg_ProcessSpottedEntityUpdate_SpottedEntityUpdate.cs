
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCSUsrMsg_ProcessSpottedEntityUpdate_SpottedEntityUpdate : ITypedProtobuf<CCSUsrMsg_ProcessSpottedEntityUpdate_SpottedEntityUpdate>
{
  static CCSUsrMsg_ProcessSpottedEntityUpdate_SpottedEntityUpdate ITypedProtobuf<CCSUsrMsg_ProcessSpottedEntityUpdate_SpottedEntityUpdate>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_ProcessSpottedEntityUpdate_SpottedEntityUpdateImpl(handle, isManuallyAllocated);


  public int EntityIdx { get; set; }


  public int ClassId { get; set; }


  public int OriginX { get; set; }


  public int OriginY { get; set; }


  public int OriginZ { get; set; }


  public int AngleY { get; set; }


  public bool Defuser { get; set; }


  public bool PlayerHasDefuser { get; set; }


  public bool PlayerHasC4 { get; set; }

}
