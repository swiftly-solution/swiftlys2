
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCLCMsg_CmdKeyValues : ITypedProtobuf<CCLCMsg_CmdKeyValues>, INetMessage<CCLCMsg_CmdKeyValues>, IDisposable
{
  static int INetMessage<CCLCMsg_CmdKeyValues>.MessageId => 34;
  
  static string INetMessage<CCLCMsg_CmdKeyValues>.MessageName => "CCLCMsg_CmdKeyValues";

  static CCLCMsg_CmdKeyValues ITypedProtobuf<CCLCMsg_CmdKeyValues>.Wrap(nint handle, bool isManuallyAllocated) => new CCLCMsg_CmdKeyValuesImpl(handle, isManuallyAllocated);


  public byte[] Data { get; set; }

}
