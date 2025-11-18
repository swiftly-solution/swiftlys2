
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CWorkshop_PopulateItemDescriptions_Request_SingleItemDescriptionImpl : TypedProtobuf<CWorkshop_PopulateItemDescriptions_Request_SingleItemDescription>, CWorkshop_PopulateItemDescriptions_Request_SingleItemDescription
{
  public CWorkshop_PopulateItemDescriptions_Request_SingleItemDescriptionImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Gameitemid
  { get => Accessor.GetUInt32("gameitemid"); set => Accessor.SetUInt32("gameitemid", value); }


  public string ItemDescription
  { get => Accessor.GetString("item_description"); set => Accessor.SetString("item_description", value); }


  public bool OnePerAccount
  { get => Accessor.GetBool("one_per_account"); set => Accessor.SetBool("one_per_account", value); }

}
