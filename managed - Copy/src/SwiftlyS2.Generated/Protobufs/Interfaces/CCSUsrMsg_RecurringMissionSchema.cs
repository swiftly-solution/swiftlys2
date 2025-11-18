
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_RecurringMissionSchema : ITypedProtobuf<CCSUsrMsg_RecurringMissionSchema>, INetMessage<CCSUsrMsg_RecurringMissionSchema>, IDisposable
{
  static int INetMessage<CCSUsrMsg_RecurringMissionSchema>.MessageId => 387;
  
  static string INetMessage<CCSUsrMsg_RecurringMissionSchema>.MessageName => "CCSUsrMsg_RecurringMissionSchema";

  static CCSUsrMsg_RecurringMissionSchema ITypedProtobuf<CCSUsrMsg_RecurringMissionSchema>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_RecurringMissionSchemaImpl(handle, isManuallyAllocated);


  public uint Period { get; set; }


  public byte[] MissionSchema { get; set; }

}
