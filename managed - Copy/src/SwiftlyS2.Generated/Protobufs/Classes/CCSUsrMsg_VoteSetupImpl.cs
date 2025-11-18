
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_VoteSetupImpl : NetMessage<CCSUsrMsg_VoteSetup>, CCSUsrMsg_VoteSetup
{
  public CCSUsrMsg_VoteSetupImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public IProtobufRepeatedFieldValueType<string> PotentialIssues
  { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "potential_issues"); }

}
