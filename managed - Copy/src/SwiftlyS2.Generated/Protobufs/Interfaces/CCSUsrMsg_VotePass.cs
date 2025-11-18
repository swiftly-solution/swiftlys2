
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_VotePass : ITypedProtobuf<CCSUsrMsg_VotePass>, INetMessage<CCSUsrMsg_VotePass>, IDisposable
{
  static int INetMessage<CCSUsrMsg_VotePass>.MessageId => 347;
  
  static string INetMessage<CCSUsrMsg_VotePass>.MessageName => "CCSUsrMsg_VotePass";

  static CCSUsrMsg_VotePass ITypedProtobuf<CCSUsrMsg_VotePass>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_VotePassImpl(handle, isManuallyAllocated);


  public int Team { get; set; }


  public int VoteType { get; set; }


  public string DispStr { get; set; }


  public string DetailsStr { get; set; }

}
