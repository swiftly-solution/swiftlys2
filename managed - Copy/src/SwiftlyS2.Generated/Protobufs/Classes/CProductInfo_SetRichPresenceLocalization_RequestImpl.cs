
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CProductInfo_SetRichPresenceLocalization_RequestImpl : TypedProtobuf<CProductInfo_SetRichPresenceLocalization_Request>, CProductInfo_SetRichPresenceLocalization_Request
{
  public CProductInfo_SetRichPresenceLocalization_RequestImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Appid
  { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }


  public IProtobufRepeatedFieldSubMessageType<CProductInfo_SetRichPresenceLocalization_Request_LanguageSection> Languages
  { get => new ProtobufRepeatedFieldSubMessageType<CProductInfo_SetRichPresenceLocalization_Request_LanguageSection>(Accessor, "languages"); }


  public ulong Steamid
  { get => Accessor.GetUInt64("steamid"); set => Accessor.SetUInt64("steamid", value); }

}
