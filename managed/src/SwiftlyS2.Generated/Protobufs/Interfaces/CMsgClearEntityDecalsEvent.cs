
using SwiftlyS2.Core.ProtobufDefinitions;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

using SwiftlyS2.Shared.NetMessages;

public interface CMsgClearEntityDecalsEvent : ITypedProtobuf<CMsgClearEntityDecalsEvent>, INetMessage<CMsgClearEntityDecalsEvent>, IDisposable
{
    static int INetMessage<CMsgClearEntityDecalsEvent>.MessageId => 203;

    static string INetMessage<CMsgClearEntityDecalsEvent>.MessageName => "CMsgClearEntityDecalsEvent";

    static CMsgClearEntityDecalsEvent ITypedProtobuf<CMsgClearEntityDecalsEvent>.Wrap( nint handle, bool isManuallyAllocated ) => new CMsgClearEntityDecalsEventImpl(handle, isManuallyAllocated);


    public uint Flagstoclear { get; set; }

}
