
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CGameInfo : ITypedProtobuf<CGameInfo>
{
  static CGameInfo ITypedProtobuf<CGameInfo>.Wrap(nint handle, bool isManuallyAllocated) => new CGameInfoImpl(handle, isManuallyAllocated);


  public CGameInfo_CDotaGameInfo Dota { get; }


  public CGameInfo_CCSGameInfo Cs { get; }

}
