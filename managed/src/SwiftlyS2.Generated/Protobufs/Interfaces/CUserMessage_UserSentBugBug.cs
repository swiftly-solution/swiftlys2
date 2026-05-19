using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMessage_UserSentBugBug : ITypedProtobuf<CUserMessage_UserSentBugBug>
{
    static CUserMessage_UserSentBugBug ITypedProtobuf<CUserMessage_UserSentBugBug>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessage_UserSentBugBugImpl(handle, isManuallyAllocated);

    public string CommandLine { get; set; }
    public string AutoexecCfg { get; set; }
    public CMsgSource2SystemSpecs SystemSpecs { get; }
    public uint BuildId { get; set; }
    public int Osversion { get; set; }
    public string CommandLogs { get; set; }
}