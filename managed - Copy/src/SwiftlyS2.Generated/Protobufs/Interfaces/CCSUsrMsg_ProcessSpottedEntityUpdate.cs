
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_ProcessSpottedEntityUpdate : ITypedProtobuf<CCSUsrMsg_ProcessSpottedEntityUpdate>, INetMessage<CCSUsrMsg_ProcessSpottedEntityUpdate>, IDisposable
{
  static int INetMessage<CCSUsrMsg_ProcessSpottedEntityUpdate>.MessageId => 325;
  
  static string INetMessage<CCSUsrMsg_ProcessSpottedEntityUpdate>.MessageName => "CCSUsrMsg_ProcessSpottedEntityUpdate";

  static CCSUsrMsg_ProcessSpottedEntityUpdate ITypedProtobuf<CCSUsrMsg_ProcessSpottedEntityUpdate>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_ProcessSpottedEntityUpdateImpl(handle, isManuallyAllocated);


  public bool NewUpdate { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CCSUsrMsg_ProcessSpottedEntityUpdate_SpottedEntityUpdate> EntityUpdates { get; }

}
