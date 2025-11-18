namespace SwiftlyS2.Core.Permissions;

internal readonly struct PermissionCacheKey : IEquatable<PermissionCacheKey> {
  public required ulong PlayerId { get; init; }
  public required string Permission { get; init; }
  private static readonly StringComparer StrCmp = StringComparer.OrdinalIgnoreCase;

  public bool Equals(PermissionCacheKey other) {
    return PlayerId == other.PlayerId && StrCmp.Equals(Permission, other.Permission);
  }

  public override int GetHashCode() {
    return HashCode.Combine(PlayerId, Permission.GetHashCode(StringComparison.OrdinalIgnoreCase));
  }
}