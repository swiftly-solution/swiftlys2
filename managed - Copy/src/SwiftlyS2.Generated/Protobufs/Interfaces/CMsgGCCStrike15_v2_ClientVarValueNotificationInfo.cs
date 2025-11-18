
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ClientVarValueNotificationInfo : ITypedProtobuf<CMsgGCCStrike15_v2_ClientVarValueNotificationInfo>
{
  static CMsgGCCStrike15_v2_ClientVarValueNotificationInfo ITypedProtobuf<CMsgGCCStrike15_v2_ClientVarValueNotificationInfo>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ClientVarValueNotificationInfoImpl(handle, isManuallyAllocated);


  public string ValueName { get; set; }


  public int ValueInt { get; set; }


  public uint ServerAddr { get; set; }


  public uint ServerPort { get; set; }


  public IProtobufRepeatedFieldValueType<string> ChokedBlocks { get; }

}
