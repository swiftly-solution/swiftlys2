using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class NetMessageSplitscreenUserChangedImpl : TypedProtobuf<NetMessageSplitscreenUserChanged>, NetMessageSplitscreenUserChanged
{
    public NetMessageSplitscreenUserChangedImpl( nint handle, bool isManuallyAllocated ) : base(handle)
    {
    }


    public uint Slot { get => Accessor.GetUInt32("slot"); set => Accessor.SetUInt32("slot", value); }

}
