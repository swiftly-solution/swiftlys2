using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CHelpRequestLogs_UploadUserApplicationLog_ResponseImpl : TypedProtobuf<CHelpRequestLogs_UploadUserApplicationLog_Response>, CHelpRequestLogs_UploadUserApplicationLog_Response
{
    public CHelpRequestLogs_UploadUserApplicationLog_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public ulong Id
    { get => Accessor.GetUInt64("id"); set => Accessor.SetUInt64("id", value); }
}