
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSource1LegacyListenEventsImpl : NetMessage<CMsgSource1LegacyListenEvents>, CMsgSource1LegacyListenEvents
{
  public CMsgSource1LegacyListenEventsImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Playerslot
  { get => Accessor.GetInt32("playerslot"); set => Accessor.SetInt32("playerslot", value); }


  public IProtobufRepeatedFieldValueType<uint> Eventarraybits
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "eventarraybits"); }

}
