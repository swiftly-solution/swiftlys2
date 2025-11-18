
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPredictionEvent_DiagnosticImpl : TypedProtobuf<CPredictionEvent_Diagnostic>, CPredictionEvent_Diagnostic
{
  public CPredictionEvent_DiagnosticImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Id
  { get => Accessor.GetUInt32("id"); set => Accessor.SetUInt32("id", value); }


  public uint RequestedSync
  { get => Accessor.GetUInt32("requested_sync"); set => Accessor.SetUInt32("requested_sync", value); }


  public uint RequestedPlayerIndex
  { get => Accessor.GetUInt32("requested_player_index"); set => Accessor.SetUInt32("requested_player_index", value); }


  public IProtobufRepeatedFieldValueType<uint> ExecutionSync
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "execution_sync"); }

}
