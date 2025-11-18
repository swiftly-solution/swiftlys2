
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_SurvivalStats_FactImpl : TypedProtobuf<CCSUsrMsg_SurvivalStats_Fact>, CCSUsrMsg_SurvivalStats_Fact
{
  public CCSUsrMsg_SurvivalStats_FactImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Type
  { get => Accessor.GetInt32("type"); set => Accessor.SetInt32("type", value); }


  public int Display
  { get => Accessor.GetInt32("display"); set => Accessor.SetInt32("display", value); }


  public int Value
  { get => Accessor.GetInt32("value"); set => Accessor.SetInt32("value", value); }


  public float Interestingness
  { get => Accessor.GetFloat("interestingness"); set => Accessor.SetFloat("interestingness", value); }

}
