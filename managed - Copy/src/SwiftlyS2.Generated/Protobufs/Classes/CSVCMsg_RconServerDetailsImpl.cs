
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_RconServerDetailsImpl : NetMessage<CSVCMsg_RconServerDetails>, CSVCMsg_RconServerDetails
{
  public CSVCMsg_RconServerDetailsImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public byte[] Token
  { get => Accessor.GetBytes("token"); set => Accessor.SetBytes("token", value); }


  public string Details
  { get => Accessor.GetString("details"); set => Accessor.SetString("details", value); }

}
