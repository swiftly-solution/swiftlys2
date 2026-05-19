using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgAppRights : ITypedProtobuf<CMsgAppRights>
{
    static CMsgAppRights ITypedProtobuf<CMsgAppRights>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgAppRightsImpl(handle, isManuallyAllocated);

    public bool EditInfo { get; set; }
    public bool Publish { get; set; }
    public bool ViewErrorData { get; set; }
    public bool Download { get; set; }
    public bool UploadCdkeys { get; set; }
    public bool GenerateCdkeys { get; set; }
    public bool ViewFinancials { get; set; }
    public bool ManageCeg { get; set; }
    public bool ManageSigning { get; set; }
    public bool ManageCdkeys { get; set; }
    public bool EditMarketing { get; set; }
    public bool EconomySupport { get; set; }
    public bool EconomySupportSupervisor { get; set; }
    public bool ManagePricing { get; set; }
    public bool BroadcastLive { get; set; }
    public bool ViewMarketingTraffic { get; set; }
    public bool EditStoreDisplayContent { get; set; }
}