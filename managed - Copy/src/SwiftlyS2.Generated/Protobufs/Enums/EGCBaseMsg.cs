
namespace SwiftlyS2.Shared.ProtobufDefinitions;

public enum EGCBaseMsg
{
  k_EMsgGCSystemMessage = 4001,
  k_EMsgGCReplicateConVars = 4002,
  k_EMsgGCConVarUpdated = 4003,
  k_EMsgGCInQueue = 4008,
  k_EMsgGCInviteToParty = 4501,
  k_EMsgGCInvitationCreated = 4502,
  k_EMsgGCPartyInviteResponse = 4503,
  k_EMsgGCKickFromParty = 4504,
  k_EMsgGCLeaveParty = 4505,
  k_EMsgGCServerAvailable = 4506,
  k_EMsgGCClientConnectToServer = 4507,
  k_EMsgGCGameServerInfo = 4508,
  k_EMsgGCError = 4509,
  k_EMsgGCReplay_UploadedToYouTube = 4510,
  k_EMsgGCLANServerAvailable = 4511,
}
