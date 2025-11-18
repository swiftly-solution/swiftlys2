
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_GC2ClientTournamentInfo : ITypedProtobuf<CMsgGCCStrike15_v2_GC2ClientTournamentInfo>
{
  static CMsgGCCStrike15_v2_GC2ClientTournamentInfo ITypedProtobuf<CMsgGCCStrike15_v2_GC2ClientTournamentInfo>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_GC2ClientTournamentInfoImpl(handle, isManuallyAllocated);


  public uint Eventid { get; set; }


  public uint Stageid { get; set; }


  public uint GameType { get; set; }


  public IProtobufRepeatedFieldValueType<uint> Teamids { get; }

}
