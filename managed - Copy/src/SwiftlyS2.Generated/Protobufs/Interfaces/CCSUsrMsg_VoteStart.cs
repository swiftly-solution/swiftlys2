
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_VoteStart : ITypedProtobuf<CCSUsrMsg_VoteStart>, INetMessage<CCSUsrMsg_VoteStart>, IDisposable
{
  static int INetMessage<CCSUsrMsg_VoteStart>.MessageId => 346;
  
  static string INetMessage<CCSUsrMsg_VoteStart>.MessageName => "CCSUsrMsg_VoteStart";

  static CCSUsrMsg_VoteStart ITypedProtobuf<CCSUsrMsg_VoteStart>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_VoteStartImpl(handle, isManuallyAllocated);


  public int Team { get; set; }


  public int PlayerSlot { get; set; }


  public int VoteType { get; set; }


  public string DispStr { get; set; }


  public string DetailsStr { get; set; }


  public string OtherTeamStr { get; set; }


  public bool IsYesNoVote { get; set; }


  public int PlayerSlotTarget { get; set; }

}
