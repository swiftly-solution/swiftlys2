
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgAccountDetailsImpl : TypedProtobuf<CMsgAccountDetails>, CMsgAccountDetails
{
  public CMsgAccountDetailsImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool Valid
  { get => Accessor.GetBool("valid"); set => Accessor.SetBool("valid", value); }


  public string AccountName
  { get => Accessor.GetString("account_name"); set => Accessor.SetString("account_name", value); }


  public bool PublicProfile
  { get => Accessor.GetBool("public_profile"); set => Accessor.SetBool("public_profile", value); }


  public bool PublicInventory
  { get => Accessor.GetBool("public_inventory"); set => Accessor.SetBool("public_inventory", value); }


  public bool VacBanned
  { get => Accessor.GetBool("vac_banned"); set => Accessor.SetBool("vac_banned", value); }


  public bool CyberCafe
  { get => Accessor.GetBool("cyber_cafe"); set => Accessor.SetBool("cyber_cafe", value); }


  public bool SchoolAccount
  { get => Accessor.GetBool("school_account"); set => Accessor.SetBool("school_account", value); }


  public bool FreeTrialAccount
  { get => Accessor.GetBool("free_trial_account"); set => Accessor.SetBool("free_trial_account", value); }


  public bool Subscribed
  { get => Accessor.GetBool("subscribed"); set => Accessor.SetBool("subscribed", value); }


  public bool LowViolence
  { get => Accessor.GetBool("low_violence"); set => Accessor.SetBool("low_violence", value); }


  public bool Limited
  { get => Accessor.GetBool("limited"); set => Accessor.SetBool("limited", value); }


  public bool Trusted
  { get => Accessor.GetBool("trusted"); set => Accessor.SetBool("trusted", value); }


  public uint Package
  { get => Accessor.GetUInt32("package"); set => Accessor.SetUInt32("package", value); }


  public uint TimeCached
  { get => Accessor.GetUInt32("time_cached"); set => Accessor.SetUInt32("time_cached", value); }


  public bool AccountLocked
  { get => Accessor.GetBool("account_locked"); set => Accessor.SetBool("account_locked", value); }


  public bool CommunityBanned
  { get => Accessor.GetBool("community_banned"); set => Accessor.SetBool("community_banned", value); }


  public bool TradeBanned
  { get => Accessor.GetBool("trade_banned"); set => Accessor.SetBool("trade_banned", value); }


  public bool EligibleForCommunityMarket
  { get => Accessor.GetBool("eligible_for_community_market"); set => Accessor.SetBool("eligible_for_community_market", value); }

}
