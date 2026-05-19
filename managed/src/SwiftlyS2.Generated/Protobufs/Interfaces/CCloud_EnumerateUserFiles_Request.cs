using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCloud_EnumerateUserFiles_Request : ITypedProtobuf<CCloud_EnumerateUserFiles_Request>
{
    static CCloud_EnumerateUserFiles_Request ITypedProtobuf<CCloud_EnumerateUserFiles_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CCloud_EnumerateUserFiles_RequestImpl(handle, isManuallyAllocated);

    public uint Appid { get; set; }
    public bool ExtendedDetails { get; set; }
    public uint Count { get; set; }
    public uint StartIndex { get; set; }
}