using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CLocalizationToken : ITypedProtobuf<CLocalizationToken>
{
    static CLocalizationToken ITypedProtobuf<CLocalizationToken>.Wrap(nint handle, bool isManuallyAllocated) => new CLocalizationTokenImpl(handle, isManuallyAllocated);

    public uint Language { get; set; }
    public string LocalizedString { get; set; }
}