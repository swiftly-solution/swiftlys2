
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCommunity_GetGamePersonalDataEntries_ResponseImpl : TypedProtobuf<CCommunity_GetGamePersonalDataEntries_Response>, CCommunity_GetGamePersonalDataEntries_Response
{
  public CCommunity_GetGamePersonalDataEntries_ResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Gceresult
  { get => Accessor.GetUInt32("gceresult"); set => Accessor.SetUInt32("gceresult", value); }


  public IProtobufRepeatedFieldValueType<string> Entries
  { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "entries"); }


  public string ContinueToken
  { get => Accessor.GetString("continue_token"); set => Accessor.SetString("continue_token", value); }


  public string ContinueText
  { get => Accessor.GetString("continue_text"); set => Accessor.SetString("continue_text", value); }

}
