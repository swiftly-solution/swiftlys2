using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CHelpRequestLogs_UploadUserApplicationLog_Response : ITypedProtobuf<CHelpRequestLogs_UploadUserApplicationLog_Response>
{
    static CHelpRequestLogs_UploadUserApplicationLog_Response ITypedProtobuf<CHelpRequestLogs_UploadUserApplicationLog_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CHelpRequestLogs_UploadUserApplicationLog_ResponseImpl(handle, isManuallyAllocated);

    public ulong Id { get; set; }
}