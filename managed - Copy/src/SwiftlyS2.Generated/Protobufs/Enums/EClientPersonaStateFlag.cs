
namespace SwiftlyS2.Shared.ProtobufDefinitions;

public enum EClientPersonaStateFlag
{
  k_EClientPersonaStateFlagStatus = 1,
  k_EClientPersonaStateFlagPlayerName = 2,
  k_EClientPersonaStateFlagQueryPort = 4,
  k_EClientPersonaStateFlagSourceID = 8,
  k_EClientPersonaStateFlagPresence = 16,
  k_EClientPersonaStateFlagLastSeen = 64,
  k_EClientPersonaStateFlagUserClanRank = 128,
  k_EClientPersonaStateGameExtraInfo = 256,
  k_EClientPersonaStateGameDataBlob = 512,
  k_EClientPersonaStateFlagClanData = 1024,
  k_EClientPersonaStateFlagFacebook = 2048,
  k_EClientPersonaStateFlagRichPresence = 4096,
  k_EClientPersonaStateFlagBroadcast = 8192,
  k_EClientPersonaStateFlagWatching = 16384,
}
