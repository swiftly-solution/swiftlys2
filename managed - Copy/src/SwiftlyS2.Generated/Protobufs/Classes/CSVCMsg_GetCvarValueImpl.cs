
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_GetCvarValueImpl : NetMessage<CSVCMsg_GetCvarValue>, CSVCMsg_GetCvarValue
{
  public CSVCMsg_GetCvarValueImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Cookie
  { get => Accessor.GetInt32("cookie"); set => Accessor.SetInt32("cookie", value); }


  public string CvarName
  { get => Accessor.GetString("cvar_name"); set => Accessor.SetString("cvar_name", value); }

}
