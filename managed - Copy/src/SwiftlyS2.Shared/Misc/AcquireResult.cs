public enum AcquireResult : int
{
  Allowed = 0,
  InvalidItem,
  AlreadyOwned,
  AlreadyPurchased,
  ReachedGrenadeTypeLimit,
  ReachedGrenadeTotalLimit,
  NotAllowedByTeam,
  NotAllowedByMap,
  NotAllowedByMode,
  NotAllowedForPurchase,
  NotAllowedByProhibition,
};