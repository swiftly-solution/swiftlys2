namespace SwiftlyS2.Shared.Natives;

public enum RoundEndReason : uint
{
    Unknown = 0x0u,
    TargetBombed = 0x1u,
    TerroristsEscaped = 0x4u,
    CTsPreventEscape = 0x5u,
    EscapingTerroristsNeutralized = 0x6u,
    BombDefused = 0x7u,
    CTsWin = 0x8u,
    TerroristsWin = 0x9u,
    RoundDraw = 0xAu,
    AllHostageRescued = 0xBu,
    TargetSaved = 0xCu,
    HostagesNotRescued = 0xDu,
    TerroristsNotEscaped = 0xEu,
    GameCommencing = 0x10u,

    TerroristsSurrender = 0x11u,
    CTsSurrender = 0x12u,

    TerroristsPlanted = 0x13u,
    CTsReachedHostage = 0x14u,
    SurvivalWin = 0x15u,
    SurvivalDraw = 0x16u,
}