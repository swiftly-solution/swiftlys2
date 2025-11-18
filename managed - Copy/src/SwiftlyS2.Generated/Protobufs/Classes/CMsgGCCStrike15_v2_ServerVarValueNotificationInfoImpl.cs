
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ServerVarValueNotificationInfoImpl : TypedProtobuf<CMsgGCCStrike15_v2_ServerVarValueNotificationInfo>, CMsgGCCStrike15_v2_ServerVarValueNotificationInfo
{
  public CMsgGCCStrike15_v2_ServerVarValueNotificationInfoImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Accountid
  { get => Accessor.GetUInt32("accountid"); set => Accessor.SetUInt32("accountid", value); }


  public IProtobufRepeatedFieldValueType<uint> Viewangles
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "viewangles"); }


  public uint Type
  { get => Accessor.GetUInt32("type"); set => Accessor.SetUInt32("type", value); }


  public IProtobufRepeatedFieldValueType<uint> Userdata
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "userdata"); }

}
