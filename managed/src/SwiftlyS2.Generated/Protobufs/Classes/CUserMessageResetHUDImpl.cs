using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageResetHUDImpl : NetMessage<CUserMessageResetHUD>, CUserMessageResetHUD
{
    public CUserMessageResetHUDImpl( nint handle, bool isManuallyAllocated ) : base(handle, isManuallyAllocated)
    {
    }


}
