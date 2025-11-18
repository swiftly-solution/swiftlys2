
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_SetViewImpl : NetMessage<CSVCMsg_SetView>, CSVCMsg_SetView
{
  public CSVCMsg_SetViewImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int EntityIndex
  { get => Accessor.GetInt32("entity_index"); set => Accessor.SetInt32("entity_index", value); }


  public int Slot
  { get => Accessor.GetInt32("slot"); set => Accessor.SetInt32("slot", value); }

}
