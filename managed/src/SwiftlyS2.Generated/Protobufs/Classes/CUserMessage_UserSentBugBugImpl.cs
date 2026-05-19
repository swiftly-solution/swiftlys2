using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessage_UserSentBugBugImpl : TypedProtobuf<CUserMessage_UserSentBugBug>, CUserMessage_UserSentBugBug
{
    public CUserMessage_UserSentBugBugImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public string CommandLine
    { get => Accessor.GetString("command_line"); set => Accessor.SetString("command_line", value); }
    public string AutoexecCfg
    { get => Accessor.GetString("autoexec_cfg"); set => Accessor.SetString("autoexec_cfg", value); }
    public CMsgSource2SystemSpecs SystemSpecs
    { get => new CMsgSource2SystemSpecsImpl(NativeNetMessages.GetNestedMessage(Address, "system_specs"), false); }
    public uint BuildId
    { get => Accessor.GetUInt32("build_id"); set => Accessor.SetUInt32("build_id", value); }
    public int Osversion
    { get => Accessor.GetInt32("osversion"); set => Accessor.SetInt32("osversion", value); }
    public string CommandLogs
    { get => Accessor.GetString("command_logs"); set => Accessor.SetString("command_logs", value); }
}