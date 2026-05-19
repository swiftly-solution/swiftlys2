namespace SwiftlyS2.Shared.Permissions;

public interface IPermissionManager
{

    /// <summary>
    /// Checks if a player has a permission.
    /// Support 'xxx.*' for wildcard permissions.
    /// </summary>
    /// <param name="steamId"> The Steam ID of the player. </param>
    /// <param name="permission"> The permission to check. </param>
    /// <returns> True if the player has the permission, false otherwise. </returns>
    public bool PlayerHasPermission( ulong steamId, string permission );

    /// <summary>
    /// Checks if a player has all permissions in the list.
    /// Support 'xxx.*' for wildcard permissions.
    /// </summary>
    /// <param name="steamId"> The Steam ID of the player. </param>
    /// <param name="permissions"> The list of permissions to check. </param>
    /// <returns> True if the player has all the permissions, false otherwise. </returns>
    public bool PlayerHasPermissions( ulong steamId, IEnumerable<string> permissions );

    /// <summary>
    /// Adds a permission to a player.
    /// </summary>
    /// <param name="steamId"> The Steam ID of the player. </param>
    /// <param name="permission"> The permission to add. </param>
    public void AddPermission( ulong steamId, string permission );

    /// <summary>
    /// Removes a permission from a player.
    /// </summary>
    /// <param name="steamId"> The Steam ID of the player. </param>
    /// <param name="permission"> The permission to remove. </param>
    public void RemovePermission( ulong steamId, string permission );

    /// <summary>
    /// Adds a sub-permission to a permission.
    /// </summary>
    /// <param name="permission"> The permission to add the sub-permission to. </param>
    /// <param name="subPermission"> The sub-permission to add. </param>
    public void AddSubPermission( string permission, string subPermission );

    /// <summary>
    /// Removes a sub-permission from a permission.
    /// </summary>
    /// <param name="permission"> The permission to remove the sub-permission from. </param>
    /// <param name="subPermission"> The sub-permission to remove. </param>
    public void RemoveSubPermission( string permission, string subPermission );

    /// <summary>
    /// Clears all permissions from a player (both base and temporary).
    /// </summary>
    /// <param name="steamId"> The Steam ID of the player. </param>
    public void ClearPermissions( ulong steamId );

    /// <inheritdoc cref="ClearPermissions"/>
    [Obsolete("Use ClearPermissions instead.")]
    public void ClearPermission( ulong steamId ) => ClearPermissions(steamId);

    /// <summary>
    /// Gets all permissions of a player, including inherited permissions from sub-permissions.
    /// </summary>
    /// <param name="steamId"> The Steam ID of the player. </param>
    public IEnumerable<string> GetPlayerPermissions( ulong steamId );
}
