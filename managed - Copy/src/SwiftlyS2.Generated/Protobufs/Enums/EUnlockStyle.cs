
namespace SwiftlyS2.Shared.ProtobufDefinitions;

public enum EUnlockStyle
{
  k_UnlockStyle_Succeeded = 0,
  k_UnlockStyle_Failed_PreReq = 1,
  k_UnlockStyle_Failed_CantAfford = 2,
  k_UnlockStyle_Failed_CantCommit = 3,
  k_UnlockStyle_Failed_CantLockCache = 4,
  k_UnlockStyle_Failed_CantAffordAttrib = 5,
}
