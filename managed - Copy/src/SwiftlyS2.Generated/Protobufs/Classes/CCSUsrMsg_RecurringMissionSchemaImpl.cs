
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_RecurringMissionSchemaImpl : NetMessage<CCSUsrMsg_RecurringMissionSchema>, CCSUsrMsg_RecurringMissionSchema
{
  public CCSUsrMsg_RecurringMissionSchemaImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public uint Period
  { get => Accessor.GetUInt32("period"); set => Accessor.SetUInt32("period", value); }


  public byte[] MissionSchema
  { get => Accessor.GetBytes("mission_schema"); set => Accessor.SetBytes("mission_schema", value); }

}
