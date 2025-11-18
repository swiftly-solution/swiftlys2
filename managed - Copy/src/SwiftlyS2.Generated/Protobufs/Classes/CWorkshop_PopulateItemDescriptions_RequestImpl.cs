
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CWorkshop_PopulateItemDescriptions_RequestImpl : TypedProtobuf<CWorkshop_PopulateItemDescriptions_Request>, CWorkshop_PopulateItemDescriptions_Request
{
  public CWorkshop_PopulateItemDescriptions_RequestImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Appid
  { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }


  public IProtobufRepeatedFieldSubMessageType<CWorkshop_PopulateItemDescriptions_Request_ItemDescriptionsLanguageBlock> Languages
  { get => new ProtobufRepeatedFieldSubMessageType<CWorkshop_PopulateItemDescriptions_Request_ItemDescriptionsLanguageBlock>(Accessor, "languages"); }

}
