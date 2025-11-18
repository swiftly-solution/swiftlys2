
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPreMatchInfoData_TeamStats : ITypedProtobuf<CPreMatchInfoData_TeamStats>
{
  static CPreMatchInfoData_TeamStats ITypedProtobuf<CPreMatchInfoData_TeamStats>.Wrap(nint handle, bool isManuallyAllocated) => new CPreMatchInfoData_TeamStatsImpl(handle, isManuallyAllocated);


  public int MatchInfoIdxtxt { get; set; }


  public string MatchInfoTxt { get; set; }


  public IProtobufRepeatedFieldValueType<string> MatchInfoTeams { get; }

}
