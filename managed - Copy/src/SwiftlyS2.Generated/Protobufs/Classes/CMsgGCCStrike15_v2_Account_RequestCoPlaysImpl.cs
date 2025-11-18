
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_Account_RequestCoPlaysImpl : TypedProtobuf<CMsgGCCStrike15_v2_Account_RequestCoPlays>, CMsgGCCStrike15_v2_Account_RequestCoPlays
{
  public CMsgGCCStrike15_v2_Account_RequestCoPlaysImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_Account_RequestCoPlays_Player> Players
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_Account_RequestCoPlays_Player>(Accessor, "players"); }


  public uint Servertime
  { get => Accessor.GetUInt32("servertime"); set => Accessor.SetUInt32("servertime", value); }

}
