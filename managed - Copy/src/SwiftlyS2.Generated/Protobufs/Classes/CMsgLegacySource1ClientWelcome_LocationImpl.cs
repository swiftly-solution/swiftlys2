
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgLegacySource1ClientWelcome_LocationImpl : TypedProtobuf<CMsgLegacySource1ClientWelcome_Location>, CMsgLegacySource1ClientWelcome_Location
{
  public CMsgLegacySource1ClientWelcome_LocationImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public float Latitude
  { get => Accessor.GetFloat("latitude"); set => Accessor.SetFloat("latitude", value); }


  public float Longitude
  { get => Accessor.GetFloat("longitude"); set => Accessor.SetFloat("longitude", value); }


  public string Country
  { get => Accessor.GetString("country"); set => Accessor.SetString("country", value); }

}
