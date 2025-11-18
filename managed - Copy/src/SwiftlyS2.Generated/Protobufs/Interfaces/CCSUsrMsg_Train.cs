
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_Train : ITypedProtobuf<CCSUsrMsg_Train>, INetMessage<CCSUsrMsg_Train>, IDisposable
{
  static int INetMessage<CCSUsrMsg_Train>.MessageId => 303;
  
  static string INetMessage<CCSUsrMsg_Train>.MessageName => "CCSUsrMsg_Train";

  static CCSUsrMsg_Train ITypedProtobuf<CCSUsrMsg_Train>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_TrainImpl(handle, isManuallyAllocated);


  public int Train { get; set; }

}
