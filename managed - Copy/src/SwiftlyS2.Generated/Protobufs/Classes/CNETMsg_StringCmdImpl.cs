
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CNETMsg_StringCmdImpl : NetMessage<CNETMsg_StringCmd>, CNETMsg_StringCmd
{
  public CNETMsg_StringCmdImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public string Command
  { get => Accessor.GetString("command"); set => Accessor.SetString("command", value); }


  public uint PredictionSync
  { get => Accessor.GetUInt32("prediction_sync"); set => Accessor.SetUInt32("prediction_sync", value); }

}
