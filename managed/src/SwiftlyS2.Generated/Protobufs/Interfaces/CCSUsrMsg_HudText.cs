
using SwiftlyS2.Core.ProtobufDefinitions;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_HudText : ITypedProtobuf<CCSUsrMsg_HudText>, INetMessage<CCSUsrMsg_HudText>, IDisposable
{
    static int INetMessage<CCSUsrMsg_HudText>.MessageId => 304;

    static string INetMessage<CCSUsrMsg_HudText>.MessageName => "CCSUsrMsg_HudText";

    static CCSUsrMsg_HudText ITypedProtobuf<CCSUsrMsg_HudText>.Wrap( nint handle, bool isManuallyAllocated ) => new CCSUsrMsg_HudTextImpl(handle, isManuallyAllocated);


    public string Text { get; set; }

}
