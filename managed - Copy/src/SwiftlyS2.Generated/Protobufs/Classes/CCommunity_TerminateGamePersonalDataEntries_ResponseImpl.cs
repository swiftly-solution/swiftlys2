
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCommunity_TerminateGamePersonalDataEntries_ResponseImpl : TypedProtobuf<CCommunity_TerminateGamePersonalDataEntries_Response>, CCommunity_TerminateGamePersonalDataEntries_Response
{
  public CCommunity_TerminateGamePersonalDataEntries_ResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Gceresult
  { get => Accessor.GetUInt32("gceresult"); set => Accessor.SetUInt32("gceresult", value); }

}
