using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgAppRightsImpl : TypedProtobuf<CMsgAppRights>, CMsgAppRights
{
    public CMsgAppRightsImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public bool EditInfo
    { get => Accessor.GetBool("edit_info"); set => Accessor.SetBool("edit_info", value); }
    public bool Publish
    { get => Accessor.GetBool("publish"); set => Accessor.SetBool("publish", value); }
    public bool ViewErrorData
    { get => Accessor.GetBool("view_error_data"); set => Accessor.SetBool("view_error_data", value); }
    public bool Download
    { get => Accessor.GetBool("download"); set => Accessor.SetBool("download", value); }
    public bool UploadCdkeys
    { get => Accessor.GetBool("upload_cdkeys"); set => Accessor.SetBool("upload_cdkeys", value); }
    public bool GenerateCdkeys
    { get => Accessor.GetBool("generate_cdkeys"); set => Accessor.SetBool("generate_cdkeys", value); }
    public bool ViewFinancials
    { get => Accessor.GetBool("view_financials"); set => Accessor.SetBool("view_financials", value); }
    public bool ManageCeg
    { get => Accessor.GetBool("manage_ceg"); set => Accessor.SetBool("manage_ceg", value); }
    public bool ManageSigning
    { get => Accessor.GetBool("manage_signing"); set => Accessor.SetBool("manage_signing", value); }
    public bool ManageCdkeys
    { get => Accessor.GetBool("manage_cdkeys"); set => Accessor.SetBool("manage_cdkeys", value); }
    public bool EditMarketing
    { get => Accessor.GetBool("edit_marketing"); set => Accessor.SetBool("edit_marketing", value); }
    public bool EconomySupport
    { get => Accessor.GetBool("economy_support"); set => Accessor.SetBool("economy_support", value); }
    public bool EconomySupportSupervisor
    { get => Accessor.GetBool("economy_support_supervisor"); set => Accessor.SetBool("economy_support_supervisor", value); }
    public bool ManagePricing
    { get => Accessor.GetBool("manage_pricing"); set => Accessor.SetBool("manage_pricing", value); }
    public bool BroadcastLive
    { get => Accessor.GetBool("broadcast_live"); set => Accessor.SetBool("broadcast_live", value); }
    public bool ViewMarketingTraffic
    { get => Accessor.GetBool("view_marketing_traffic"); set => Accessor.SetBool("view_marketing_traffic", value); }
    public bool EditStoreDisplayContent
    { get => Accessor.GetBool("edit_store_display_content"); set => Accessor.SetBool("edit_store_display_content", value); }
}