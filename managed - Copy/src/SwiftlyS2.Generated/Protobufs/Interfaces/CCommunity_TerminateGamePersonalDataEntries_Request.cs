
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCommunity_TerminateGamePersonalDataEntries_Request : ITypedProtobuf<CCommunity_TerminateGamePersonalDataEntries_Request>
{
  static CCommunity_TerminateGamePersonalDataEntries_Request ITypedProtobuf<CCommunity_TerminateGamePersonalDataEntries_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CCommunity_TerminateGamePersonalDataEntries_RequestImpl(handle, isManuallyAllocated);


  public uint Appid { get; set; }


  public ulong Steamid { get; set; }

}
