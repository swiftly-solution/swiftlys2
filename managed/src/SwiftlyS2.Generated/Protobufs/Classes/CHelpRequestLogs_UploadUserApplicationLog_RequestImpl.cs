using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CHelpRequestLogs_UploadUserApplicationLog_RequestImpl : TypedProtobuf<CHelpRequestLogs_UploadUserApplicationLog_Request>, CHelpRequestLogs_UploadUserApplicationLog_Request
{
    public CHelpRequestLogs_UploadUserApplicationLog_RequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint Appid
    { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }
    public string LogType
    { get => Accessor.GetString("log_type"); set => Accessor.SetString("log_type", value); }
    public string VersionString
    { get => Accessor.GetString("version_string"); set => Accessor.SetString("version_string", value); }
    public string LogContents
    { get => Accessor.GetString("log_contents"); set => Accessor.SetString("log_contents", value); }
}