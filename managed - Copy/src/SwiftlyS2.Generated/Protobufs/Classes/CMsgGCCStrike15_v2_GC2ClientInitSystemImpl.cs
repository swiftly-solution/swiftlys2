
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_GC2ClientInitSystemImpl : TypedProtobuf<CMsgGCCStrike15_v2_GC2ClientInitSystem>, CMsgGCCStrike15_v2_GC2ClientInitSystem
{
  public CMsgGCCStrike15_v2_GC2ClientInitSystemImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool Load
  { get => Accessor.GetBool("load"); set => Accessor.SetBool("load", value); }


  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }


  public string Outputname
  { get => Accessor.GetString("outputname"); set => Accessor.SetString("outputname", value); }


  public byte[] KeyData
  { get => Accessor.GetBytes("key_data"); set => Accessor.SetBytes("key_data", value); }


  public byte[] ShaHash
  { get => Accessor.GetBytes("sha_hash"); set => Accessor.SetBytes("sha_hash", value); }


  public int Cookie
  { get => Accessor.GetInt32("cookie"); set => Accessor.SetInt32("cookie", value); }


  public string Manifest
  { get => Accessor.GetString("manifest"); set => Accessor.SetString("manifest", value); }


  public byte[] SystemPackage
  { get => Accessor.GetBytes("system_package"); set => Accessor.SetBytes("system_package", value); }


  public bool LoadSystem
  { get => Accessor.GetBool("load_system"); set => Accessor.SetBool("load_system", value); }

}
