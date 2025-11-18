
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgRecurringMissionSchema_MissionTemplateListImpl : TypedProtobuf<CMsgRecurringMissionSchema_MissionTemplateList>, CMsgRecurringMissionSchema_MissionTemplateList
{
  public CMsgRecurringMissionSchema_MissionTemplateListImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Period
  { get => Accessor.GetUInt32("period"); set => Accessor.SetUInt32("period", value); }


  public IProtobufRepeatedFieldValueType<byte[]> MissionTemplates
  { get => new ProtobufRepeatedFieldValueType<byte[]>(Accessor, "mission_templates"); }

}
