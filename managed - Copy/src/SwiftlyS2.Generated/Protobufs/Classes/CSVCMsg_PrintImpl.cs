
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_PrintImpl : NetMessage<CSVCMsg_Print>, CSVCMsg_Print
{
  public CSVCMsg_PrintImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public string Text
  { get => Accessor.GetString("text"); set => Accessor.SetString("text", value); }

}
