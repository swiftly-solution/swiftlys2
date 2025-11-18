
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCLCMsg_RconServerDetailsImpl : NetMessage<CCLCMsg_RconServerDetails>, CCLCMsg_RconServerDetails
{
  public CCLCMsg_RconServerDetailsImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public byte[] Token
  { get => Accessor.GetBytes("token"); set => Accessor.SetBytes("token", value); }

}
