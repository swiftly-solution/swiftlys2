
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_HltvFixupOperatorStatusImpl : NetMessage<CSVCMsg_HltvFixupOperatorStatus>, CSVCMsg_HltvFixupOperatorStatus
{
  public CSVCMsg_HltvFixupOperatorStatusImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public uint Mode
  { get => Accessor.GetUInt32("mode"); set => Accessor.SetUInt32("mode", value); }


  public string OverrideOperatorName
  { get => Accessor.GetString("override_operator_name"); set => Accessor.SetString("override_operator_name", value); }

}
