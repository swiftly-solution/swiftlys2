
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ServerVarValueNotificationInfo : ITypedProtobuf<CMsgGCCStrike15_v2_ServerVarValueNotificationInfo>
{
  static CMsgGCCStrike15_v2_ServerVarValueNotificationInfo ITypedProtobuf<CMsgGCCStrike15_v2_ServerVarValueNotificationInfo>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ServerVarValueNotificationInfoImpl(handle, isManuallyAllocated);


  public uint Accountid { get; set; }


  public IProtobufRepeatedFieldValueType<uint> Viewangles { get; }


  public uint Type { get; set; }


  public IProtobufRepeatedFieldValueType<uint> Userdata { get; }

}
