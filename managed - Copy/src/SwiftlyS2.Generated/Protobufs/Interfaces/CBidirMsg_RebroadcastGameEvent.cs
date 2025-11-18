
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CBidirMsg_RebroadcastGameEvent : ITypedProtobuf<CBidirMsg_RebroadcastGameEvent>
{
  static CBidirMsg_RebroadcastGameEvent ITypedProtobuf<CBidirMsg_RebroadcastGameEvent>.Wrap(nint handle, bool isManuallyAllocated) => new CBidirMsg_RebroadcastGameEventImpl(handle, isManuallyAllocated);


  public bool Posttoserver { get; set; }


  public int Buftype { get; set; }


  public uint Clientbitcount { get; set; }


  public ulong Receivingclients { get; set; }

}
