using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CLocalizationTokenImpl : TypedProtobuf<CLocalizationToken>, CLocalizationToken
{
    public CLocalizationTokenImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint Language
    { get => Accessor.GetUInt32("language"); set => Accessor.SetUInt32("language", value); }
    public string LocalizedString
    { get => Accessor.GetString("localized_string"); set => Accessor.SetString("localized_string", value); }
}