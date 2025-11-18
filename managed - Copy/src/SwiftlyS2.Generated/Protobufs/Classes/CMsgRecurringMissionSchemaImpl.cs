
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgRecurringMissionSchemaImpl : TypedProtobuf<CMsgRecurringMissionSchema>, CMsgRecurringMissionSchema
{
  public CMsgRecurringMissionSchemaImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CMsgRecurringMissionSchema_MissionTemplateList> Missions
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgRecurringMissionSchema_MissionTemplateList>(Accessor, "missions"); }

}
