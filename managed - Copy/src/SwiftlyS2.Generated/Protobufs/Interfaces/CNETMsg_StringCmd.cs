
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CNETMsg_StringCmd : ITypedProtobuf<CNETMsg_StringCmd>, INetMessage<CNETMsg_StringCmd>, IDisposable
{
  static int INetMessage<CNETMsg_StringCmd>.MessageId => 5;
  
  static string INetMessage<CNETMsg_StringCmd>.MessageName => "CNETMsg_StringCmd";

  static CNETMsg_StringCmd ITypedProtobuf<CNETMsg_StringCmd>.Wrap(nint handle, bool isManuallyAllocated) => new CNETMsg_StringCmdImpl(handle, isManuallyAllocated);


  public string Command { get; set; }


  public uint PredictionSync { get; set; }

}
