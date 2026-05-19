namespace SwiftlyS2.Shared.ProtobufDefinitions;

public enum CMsgSteamDatagramConnectionStatsP2PClientToRouter_Flags
{
    ACK_REQUEST_RELAY = 1,
    ACK_REQUEST_E2E = 2,
    ACK_REQUEST_IMMEDIATE = 4,
    NOT_PRIMARY_SESSION = 8,
    NOT_PRIMARY_TRANSPORT_E2E = 16,
    CLIENT_RELAY_OVERRIDE = 32,
}