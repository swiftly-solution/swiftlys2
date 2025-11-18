
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CClientHeaderOverwatchEvidenceImpl : TypedProtobuf<CClientHeaderOverwatchEvidence>, CClientHeaderOverwatchEvidence
{
  public CClientHeaderOverwatchEvidenceImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Accountid
  { get => Accessor.GetUInt32("accountid"); set => Accessor.SetUInt32("accountid", value); }


  public ulong Caseid
  { get => Accessor.GetUInt64("caseid"); set => Accessor.SetUInt64("caseid", value); }

}
