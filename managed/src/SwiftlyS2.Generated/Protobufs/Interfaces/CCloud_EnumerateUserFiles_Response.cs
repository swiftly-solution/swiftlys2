using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCloud_EnumerateUserFiles_Response : ITypedProtobuf<CCloud_EnumerateUserFiles_Response>
{
    static CCloud_EnumerateUserFiles_Response ITypedProtobuf<CCloud_EnumerateUserFiles_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CCloud_EnumerateUserFiles_ResponseImpl(handle, isManuallyAllocated);

    public IProtobufRepeatedFieldSubMessageType<CCloud_UserFile> Files { get; }
    public uint TotalFiles { get; set; }
}