
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_PacketReliableImpl : NetMessage<CSVCMsg_PacketReliable>, CSVCMsg_PacketReliable
{
  public CSVCMsg_PacketReliableImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Tick
  { get => Accessor.GetInt32("tick"); set => Accessor.SetInt32("tick", value); }


  public int Messagessize
  { get => Accessor.GetInt32("messagessize"); set => Accessor.SetInt32("messagessize", value); }


  public bool State
  { get => Accessor.GetBool("state"); set => Accessor.SetBool("state", value); }

}
