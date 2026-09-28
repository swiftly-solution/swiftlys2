namespace SwiftlyS2.Shared.ProtobufDefinitions;

public enum CUserMessageRemoteServerResponse_ECommandResult
{
    EResultSuccess = 1,
    EResultServerDoesntAllow = 2,
    EResultClientNotAuthenticated = 3,
    EResultClientNotAllowed = 4,
    EResultCommandNotAllowed = 5,
}