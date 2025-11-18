
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CGCToGCMsgRouted : ITypedProtobuf<CGCToGCMsgRouted>
{
  static CGCToGCMsgRouted ITypedProtobuf<CGCToGCMsgRouted>.Wrap(nint handle, bool isManuallyAllocated) => new CGCToGCMsgRoutedImpl(handle, isManuallyAllocated);


  public uint MsgType { get; set; }


  public ulong SenderId { get; set; }


  public byte[] NetMessage { get; set; }


  public uint Ip { get; set; }

}
