
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgCsgoSteamUserStatChangeImpl : TypedProtobuf<CMsgCsgoSteamUserStatChange>, CMsgCsgoSteamUserStatChange
{
  public CMsgCsgoSteamUserStatChangeImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Ecsgosteamuserstat
  { get => Accessor.GetInt32("ecsgosteamuserstat"); set => Accessor.SetInt32("ecsgosteamuserstat", value); }


  public int Delta
  { get => Accessor.GetInt32("delta"); set => Accessor.SetInt32("delta", value); }


  public bool Absolute
  { get => Accessor.GetBool("absolute"); set => Accessor.SetBool("absolute", value); }

}
