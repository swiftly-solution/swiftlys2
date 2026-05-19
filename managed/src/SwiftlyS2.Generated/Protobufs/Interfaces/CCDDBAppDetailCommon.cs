using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCDDBAppDetailCommon : ITypedProtobuf<CCDDBAppDetailCommon>
{
    static CCDDBAppDetailCommon ITypedProtobuf<CCDDBAppDetailCommon>.Wrap(nint handle, bool isManuallyAllocated) => new CCDDBAppDetailCommonImpl(handle, isManuallyAllocated);

    public uint Appid { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public bool Tool { get; set; }
    public bool Demo { get; set; }
    public bool Media { get; set; }
    public bool CommunityVisibleStats { get; set; }
    public string FriendlyName { get; set; }
    public string Propagation { get; set; }
    public bool HasAdultContent { get; set; }
    public bool IsVisibleInSteamChina { get; set; }
    public uint AppType { get; set; }
}