
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCMultiplexMessage : ITypedProtobuf<CMsgGCMultiplexMessage>
{
  static CMsgGCMultiplexMessage ITypedProtobuf<CMsgGCMultiplexMessage>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCMultiplexMessageImpl(handle, isManuallyAllocated);


  public uint Msgtype { get; set; }


  public byte[] Payload { get; set; }


  public IProtobufRepeatedFieldValueType<ulong> Steamids { get; }


  public bool Replytogc { get; set; }

}
