using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CHelpRequestLogs_UploadUserApplicationLog_Request : ITypedProtobuf<CHelpRequestLogs_UploadUserApplicationLog_Request>
{
    static CHelpRequestLogs_UploadUserApplicationLog_Request ITypedProtobuf<CHelpRequestLogs_UploadUserApplicationLog_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CHelpRequestLogs_UploadUserApplicationLog_RequestImpl(handle, isManuallyAllocated);

    public uint Appid { get; set; }
    public string LogType { get; set; }
    public string VersionString { get; set; }
    public string LogContents { get; set; }
}