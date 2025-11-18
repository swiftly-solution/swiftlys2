
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_SplitScreenImpl : NetMessage<CSVCMsg_SplitScreen>, CSVCMsg_SplitScreen
{
  public CSVCMsg_SplitScreenImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public ESplitScreenMessageType Type
  { get => (ESplitScreenMessageType)Accessor.GetInt32("type"); set => Accessor.SetInt32("type", (int)value); }


  public int Slot
  { get => Accessor.GetInt32("slot"); set => Accessor.SetInt32("slot", value); }


  public int PlayerIndex
  { get => Accessor.GetInt32("player_index"); set => Accessor.SetInt32("player_index", value); }

}
