
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageRequestUtilActionImpl : NetMessage<CUserMessageRequestUtilAction>, CUserMessageRequestUtilAction
{
  public CUserMessageRequestUtilActionImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Util1
  { get => Accessor.GetInt32("util1"); set => Accessor.SetInt32("util1", value); }


  public int Util2
  { get => Accessor.GetInt32("util2"); set => Accessor.SetInt32("util2", value); }


  public int Util3
  { get => Accessor.GetInt32("util3"); set => Accessor.SetInt32("util3", value); }


  public int Util4
  { get => Accessor.GetInt32("util4"); set => Accessor.SetInt32("util4", value); }


  public int Util5
  { get => Accessor.GetInt32("util5"); set => Accessor.SetInt32("util5", value); }

}
