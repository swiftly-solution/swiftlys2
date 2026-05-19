using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCloud_UserFile : ITypedProtobuf<CCloud_UserFile>
{
    static CCloud_UserFile ITypedProtobuf<CCloud_UserFile>.Wrap(nint handle, bool isManuallyAllocated) => new CCloud_UserFileImpl(handle, isManuallyAllocated);

    public uint Appid { get; set; }
    public ulong Ugcid { get; set; }
    public string Filename { get; set; }
    public ulong Timestamp { get; set; }
    public uint FileSize { get; set; }
    public string Url { get; set; }
    public ulong SteamidCreator { get; set; }
}