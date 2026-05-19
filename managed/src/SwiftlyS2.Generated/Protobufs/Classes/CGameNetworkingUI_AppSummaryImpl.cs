using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CGameNetworkingUI_AppSummaryImpl : TypedProtobuf<CGameNetworkingUI_AppSummary>, CGameNetworkingUI_AppSummary
{
    public CGameNetworkingUI_AppSummaryImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint Appid
    { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }
    public bool IpWasSharedWithFriend
    { get => Accessor.GetBool("ip_was_shared_with_friend"); set => Accessor.SetBool("ip_was_shared_with_friend", value); }
    public bool IpWasSharedWithNonfriend
    { get => Accessor.GetBool("ip_was_shared_with_nonfriend"); set => Accessor.SetBool("ip_was_shared_with_nonfriend", value); }
    public uint ActiveConnections
    { get => Accessor.GetUInt32("active_connections"); set => Accessor.SetUInt32("active_connections", value); }
    public CGameNetworkingUI_ConnectionSummary MainCxn
    { get => new CGameNetworkingUI_ConnectionSummaryImpl(NativeNetMessages.GetNestedMessage(Address, "main_cxn"), false); }
}