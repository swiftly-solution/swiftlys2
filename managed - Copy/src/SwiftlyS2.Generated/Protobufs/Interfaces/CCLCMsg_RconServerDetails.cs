
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCLCMsg_RconServerDetails : ITypedProtobuf<CCLCMsg_RconServerDetails>, INetMessage<CCLCMsg_RconServerDetails>, IDisposable
{
  static int INetMessage<CCLCMsg_RconServerDetails>.MessageId => 35;
  
  static string INetMessage<CCLCMsg_RconServerDetails>.MessageName => "CCLCMsg_RconServerDetails";

  static CCLCMsg_RconServerDetails ITypedProtobuf<CCLCMsg_RconServerDetails>.Wrap(nint handle, bool isManuallyAllocated) => new CCLCMsg_RconServerDetailsImpl(handle, isManuallyAllocated);


  public byte[] Token { get; set; }

}
