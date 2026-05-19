using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CBilling_AddressImpl : TypedProtobuf<CBilling_Address>, CBilling_Address
{
    public CBilling_AddressImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public string FirstName
    { get => Accessor.GetString("first_name"); set => Accessor.SetString("first_name", value); }
    public string LastName
    { get => Accessor.GetString("last_name"); set => Accessor.SetString("last_name", value); }
    public string Address1
    { get => Accessor.GetString("address1"); set => Accessor.SetString("address1", value); }
    public string Address2
    { get => Accessor.GetString("address2"); set => Accessor.SetString("address2", value); }
    public string City
    { get => Accessor.GetString("city"); set => Accessor.SetString("city", value); }
    public string UsState
    { get => Accessor.GetString("us_state"); set => Accessor.SetString("us_state", value); }
    public string CountryCode
    { get => Accessor.GetString("country_code"); set => Accessor.SetString("country_code", value); }
    public string Postcode
    { get => Accessor.GetString("postcode"); set => Accessor.SetString("postcode", value); }
    public int ZipPlus4
    { get => Accessor.GetInt32("zip_plus4"); set => Accessor.SetInt32("zip_plus4", value); }
    public string Phone
    { get => Accessor.GetString("phone"); set => Accessor.SetString("phone", value); }
}