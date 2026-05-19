using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_GetNicknameList_Request : ITypedProtobuf<CPlayer_GetNicknameList_Request>
{
    static CPlayer_GetNicknameList_Request ITypedProtobuf<CPlayer_GetNicknameList_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_GetNicknameList_RequestImpl(handle, isManuallyAllocated);

}