
using SwiftlyS2.Core.ProtobufDefinitions;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageAmmoDenied : ITypedProtobuf<CUserMessageAmmoDenied>, INetMessage<CUserMessageAmmoDenied>, IDisposable
{
    static int INetMessage<CUserMessageAmmoDenied>.MessageId => 132;

    static string INetMessage<CUserMessageAmmoDenied>.MessageName => "CUserMessageAmmoDenied";

    static CUserMessageAmmoDenied ITypedProtobuf<CUserMessageAmmoDenied>.Wrap( nint handle, bool isManuallyAllocated ) => new CUserMessageAmmoDeniedImpl(handle, isManuallyAllocated);


    public uint AmmoId { get; set; }

}
