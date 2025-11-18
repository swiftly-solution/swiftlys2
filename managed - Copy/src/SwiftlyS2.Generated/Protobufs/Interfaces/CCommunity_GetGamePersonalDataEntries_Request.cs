
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCommunity_GetGamePersonalDataEntries_Request : ITypedProtobuf<CCommunity_GetGamePersonalDataEntries_Request>
{
  static CCommunity_GetGamePersonalDataEntries_Request ITypedProtobuf<CCommunity_GetGamePersonalDataEntries_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CCommunity_GetGamePersonalDataEntries_RequestImpl(handle, isManuallyAllocated);


  public uint Appid { get; set; }


  public ulong Steamid { get; set; }


  public string Type { get; set; }


  public string ContinueToken { get; set; }

}
