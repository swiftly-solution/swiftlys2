
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_GC2ClientInitSystem_ResponseImpl : TypedProtobuf<CMsgGCCStrike15_v2_GC2ClientInitSystem_Response>, CMsgGCCStrike15_v2_GC2ClientInitSystem_Response
{
  public CMsgGCCStrike15_v2_GC2ClientInitSystem_ResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool Success
  { get => Accessor.GetBool("success"); set => Accessor.SetBool("success", value); }


  public string Diagnostic
  { get => Accessor.GetString("diagnostic"); set => Accessor.SetString("diagnostic", value); }


  public byte[] ShaHash
  { get => Accessor.GetBytes("sha_hash"); set => Accessor.SetBytes("sha_hash", value); }


  public int Response
  { get => Accessor.GetInt32("response"); set => Accessor.SetInt32("response", value); }


  public int ErrorCode1
  { get => Accessor.GetInt32("error_code1"); set => Accessor.SetInt32("error_code1", value); }


  public int ErrorCode2
  { get => Accessor.GetInt32("error_code2"); set => Accessor.SetInt32("error_code2", value); }


  public long Handle
  { get => Accessor.GetInt64("handle"); set => Accessor.SetInt64("handle", value); }


  public EInitSystemResult EinitResult
  { get => (EInitSystemResult)Accessor.GetInt32("einit_result"); set => Accessor.SetInt32("einit_result", (int)value); }


  public int AuxSystem1
  { get => Accessor.GetInt32("aux_system1"); set => Accessor.SetInt32("aux_system1", value); }


  public int AuxSystem2
  { get => Accessor.GetInt32("aux_system2"); set => Accessor.SetInt32("aux_system2", value); }

}
