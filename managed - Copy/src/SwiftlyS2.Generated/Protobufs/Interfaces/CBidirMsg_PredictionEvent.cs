
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CBidirMsg_PredictionEvent : ITypedProtobuf<CBidirMsg_PredictionEvent>
{
  static CBidirMsg_PredictionEvent ITypedProtobuf<CBidirMsg_PredictionEvent>.Wrap(nint handle, bool isManuallyAllocated) => new CBidirMsg_PredictionEventImpl(handle, isManuallyAllocated);


  public uint EventId { get; set; }


  public byte[] EventData { get; set; }


  public uint SyncType { get; set; }


  public uint SyncValUint32 { get; set; }

}
