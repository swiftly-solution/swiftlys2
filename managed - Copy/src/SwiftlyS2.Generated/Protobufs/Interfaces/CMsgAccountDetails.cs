
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgAccountDetails : ITypedProtobuf<CMsgAccountDetails>
{
  static CMsgAccountDetails ITypedProtobuf<CMsgAccountDetails>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgAccountDetailsImpl(handle, isManuallyAllocated);


  public bool Valid { get; set; }


  public string AccountName { get; set; }


  public bool PublicProfile { get; set; }


  public bool PublicInventory { get; set; }


  public bool VacBanned { get; set; }


  public bool CyberCafe { get; set; }


  public bool SchoolAccount { get; set; }


  public bool FreeTrialAccount { get; set; }


  public bool Subscribed { get; set; }


  public bool LowViolence { get; set; }


  public bool Limited { get; set; }


  public bool Trusted { get; set; }


  public uint Package { get; set; }


  public uint TimeCached { get; set; }


  public bool AccountLocked { get; set; }


  public bool CommunityBanned { get; set; }


  public bool TradeBanned { get; set; }


  public bool EligibleForCommunityMarket { get; set; }

}
