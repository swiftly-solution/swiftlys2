
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgVDebugGameSessionIDEventImpl : NetMessage<CMsgVDebugGameSessionIDEvent>, CMsgVDebugGameSessionIDEvent
{
  public CMsgVDebugGameSessionIDEventImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Clientid
  { get => Accessor.GetInt32("clientid"); set => Accessor.SetInt32("clientid", value); }


  public string Gamesessionid
  { get => Accessor.GetString("gamesessionid"); set => Accessor.SetString("gamesessionid", value); }

}
