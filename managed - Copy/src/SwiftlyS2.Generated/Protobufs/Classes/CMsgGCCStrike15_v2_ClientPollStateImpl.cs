
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientPollStateImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientPollState>, CMsgGCCStrike15_v2_ClientPollState
{
  public CMsgGCCStrike15_v2_ClientPollStateImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Pollid
  { get => Accessor.GetUInt32("pollid"); set => Accessor.SetUInt32("pollid", value); }


  public IProtobufRepeatedFieldValueType<string> Names
  { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "names"); }


  public IProtobufRepeatedFieldValueType<int> Values
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "values"); }

}
