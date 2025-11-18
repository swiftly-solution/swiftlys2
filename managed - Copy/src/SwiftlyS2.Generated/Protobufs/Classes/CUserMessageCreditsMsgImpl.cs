
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageCreditsMsgImpl : NetMessage<CUserMessageCreditsMsg>, CUserMessageCreditsMsg
{
  public CUserMessageCreditsMsgImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public eRollType Rolltype
  { get => (eRollType)Accessor.GetInt32("rolltype"); set => Accessor.SetInt32("rolltype", (int)value); }


  public float LogoLength
  { get => Accessor.GetFloat("logo_length"); set => Accessor.SetFloat("logo_length", value); }

}
