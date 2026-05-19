using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCloud_Delete_Request : ITypedProtobuf<CCloud_Delete_Request>
{
    static CCloud_Delete_Request ITypedProtobuf<CCloud_Delete_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CCloud_Delete_RequestImpl(handle, isManuallyAllocated);

    public string Filename { get; set; }
    public uint Appid { get; set; }
}