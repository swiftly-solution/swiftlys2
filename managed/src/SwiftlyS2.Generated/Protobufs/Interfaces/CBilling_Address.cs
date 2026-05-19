using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CBilling_Address : ITypedProtobuf<CBilling_Address>
{
    static CBilling_Address ITypedProtobuf<CBilling_Address>.Wrap(nint handle, bool isManuallyAllocated) => new CBilling_AddressImpl(handle, isManuallyAllocated);

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string UsState { get; set; }
    public string CountryCode { get; set; }
    public string Postcode { get; set; }
    public int ZipPlus4 { get; set; }
    public string Phone { get; set; }
}