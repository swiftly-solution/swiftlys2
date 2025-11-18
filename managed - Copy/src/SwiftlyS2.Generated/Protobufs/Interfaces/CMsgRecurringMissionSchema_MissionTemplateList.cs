
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgRecurringMissionSchema_MissionTemplateList : ITypedProtobuf<CMsgRecurringMissionSchema_MissionTemplateList>
{
  static CMsgRecurringMissionSchema_MissionTemplateList ITypedProtobuf<CMsgRecurringMissionSchema_MissionTemplateList>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgRecurringMissionSchema_MissionTemplateListImpl(handle, isManuallyAllocated);


  public uint Period { get; set; }


  public IProtobufRepeatedFieldValueType<byte[]> MissionTemplates { get; }

}
