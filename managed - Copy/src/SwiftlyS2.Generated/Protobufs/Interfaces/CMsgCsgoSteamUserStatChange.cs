
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgCsgoSteamUserStatChange : ITypedProtobuf<CMsgCsgoSteamUserStatChange>
{
  static CMsgCsgoSteamUserStatChange ITypedProtobuf<CMsgCsgoSteamUserStatChange>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgCsgoSteamUserStatChangeImpl(handle, isManuallyAllocated);


  public int Ecsgosteamuserstat { get; set; }


  public int Delta { get; set; }


  public bool Absolute { get; set; }

}
