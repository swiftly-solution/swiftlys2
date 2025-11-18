namespace SwiftlyS2.Shared.Permissions;

public interface IPermissionManager {

  /// <summary>
  /// Checks if a player has a permission.
  /// Support 'xxx.*' for wildcard permissions.
  /// </summary>
  /// <param name="steamId"> The Steam ID of the player. </param>
  /// <param name="permission"> The permission to check. </param>
  /// <returns> True if the player has the permission, false otherwise. </returns>
  bool PlayerHasPermission(ulong steamId, string permission);

  /// <summary>
  /// Adds a permission to a player.
  /// </summary>
  /// <param name="steamId"> The Steam ID of the player. </param>
  /// <param name="permission"> The permission to add. </param>
  void AddPermission(ulong steamId, string permission);

  /// <summary>
  /// Removes a permission from a player.
  /// </summary>
  /// <param name="steamId"> The Steam ID of the player. </param>
  /// <param name="permission"> The permission to remove. </param>
  void RemovePermission(ulong steamId, string permission);

  /// <summary>
  /// Adds a sub-permission to a permission.
  /// </summary>
  /// <param name="permission"> The permission to add the sub-permission to. </param>
  /// <param name="subPermission"> The sub-permission to add. </param>
  void AddSubPermission(string permission, string subPermission);

  /// <summary>
  /// Removes a sub-permission from a permission.
  /// </summary>
  /// <param name="permission"> The permission to remove the sub-permission from. </param>
  /// <param name="subPermission"> The sub-permission to remove. </param>
  void RemoveSubPermission(string permission, string subPermission);
}