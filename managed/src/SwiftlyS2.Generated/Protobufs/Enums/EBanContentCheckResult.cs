namespace SwiftlyS2.Shared.ProtobufDefinitions;

public enum EBanContentCheckResult
{
    k_EBanContentCheckResult_NotScanned = 0,
    k_EBanContentCheckResult_Reset = 1,
    k_EBanContentCheckResult_NeedsChecking = 2,
    k_EBanContentCheckResult_VeryUnlikely = 5,
    k_EBanContentCheckResult_Unlikely = 30,
    k_EBanContentCheckResult_Possible = 50,
    k_EBanContentCheckResult_Likely = 75,
    k_EBanContentCheckResult_VeryLikely = 100,
}