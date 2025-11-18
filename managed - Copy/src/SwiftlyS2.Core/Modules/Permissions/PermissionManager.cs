using System.Collections.Concurrent;
using System.Collections.Immutable;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Spectre.Console;
using SwiftlyS2.Core.Models;
using SwiftlyS2.Shared.Permissions;

namespace SwiftlyS2.Core.Permissions;

internal class PermissionManager : IPermissionManager
{

  private Dictionary<ulong, List<string>> _playerPermissions = new();
  private Dictionary<ulong, List<string>> _temporaryPlayerPermissions = new();
  private Dictionary<string, List<string>> _subPermissions = new();
  private Dictionary<string, List<string>> _temporarySubPermissions = new();
  private List<string> _defaultPermissions = new();
  private ImmutableDictionary<PermissionCacheKey, bool> _queryCache = ImmutableDictionary.Create<PermissionCacheKey, bool>();
  private object _lock = new();

  public PermissionManager( IOptionsMonitor<PermissionConfig> options, ILogger<PermissionManager> logger )
  {
    LoadPermissions(options.CurrentValue);

    options.OnChange(( config ) =>
    {
      try
      {
        logger.LogInformation("Permission config changed, reloading...");
        LoadPermissions(config);
        logger.LogInformation("Permission config reloaded.");
      }
      catch (Exception e)
      {
        if (!GlobalExceptionHandler.Handle(e)) return;
        logger.LogError(e, "Error reloading permission config.");
      }
    });
  }

  private void LoadPermissions( PermissionConfig config )
  {
    lock (_lock)
    {
      _queryCache = _queryCache.Clear();
      _defaultPermissions = config.PermissionGroups.ContainsKey("__default") ? config.PermissionGroups["__default"] : [];
      _playerPermissions = config.Players.ToDictionary(x => ulong.Parse(x.Key), x => x.Value);
      _subPermissions = config.PermissionGroups;
    }
  }

  private List<string> GetPlayerPermissions( ulong playerId )
  {
    List<string> permissions = new();

    if (_playerPermissions.TryGetValue(playerId, out var playerPermission))
    {
      permissions.AddRange(playerPermission);
    }

    if (_temporaryPlayerPermissions.TryGetValue(playerId, out var temporaryPermissions))
    {
      permissions.AddRange(temporaryPermissions);
    }

    permissions.AddRange(_defaultPermissions);

    return permissions;
  }

  private Dictionary<string, List<string>> GetSubPermissions()
  {
    var result = new Dictionary<string, List<string>>();

    foreach (var kvp in _subPermissions)
    {
      result[kvp.Key] = [.. kvp.Value];
    }

    foreach (var kvp in _temporarySubPermissions)
    {
      if (result.TryGetValue(kvp.Key, out var existingPermissions))
      {
        var permissionSet = new HashSet<string>(existingPermissions);
        foreach (var perm in kvp.Value)
        {
          permissionSet.Add(perm);
        }
        result[kvp.Key] = permissionSet.ToList();
      }
      else
      {
        result[kvp.Key] = [.. kvp.Value];
      }
    }

    return result;
  }

  private bool IsEqual( string from, string target )
  {
    if (from == "*")
    {
      return true;
    }
    if (!from.Contains("*"))
    {
      return string.Equals(from, target, StringComparison.OrdinalIgnoreCase);
    }

    var prefix = from[..^2];
    return target.StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
  }

  private bool HasNestedPermission( string rootPermission, string targetPermission, HashSet<string> visitedPermissions )
  {
    if (visitedPermissions.Contains(rootPermission))
    {
      AnsiConsole.WriteLine("Loop detected for permission: " + rootPermission);
      return false;
    }

    visitedPermissions.Add(rootPermission);

    if (IsEqual(rootPermission, targetPermission))
    {
      return true;
    }

    if (GetSubPermissions().TryGetValue(rootPermission, out var subPermissions))
    {
      foreach (var subPermission in subPermissions)
      {
        if (HasNestedPermission(subPermission, targetPermission, visitedPermissions))
        {
          return true;
        }
      }
    }

    return false;
  }

  public bool PlayerHasPermission( ulong playerId, string permission )
  {
    var key = new PermissionCacheKey { PlayerId = playerId, Permission = permission };
    if (_queryCache.TryGetValue(key, out var result))
    {
      return result;
    }

    lock (_lock)
    {
      var permissions = GetPlayerPermissions(playerId);

      if (permissions.Count == 0)
      {
        _queryCache = _queryCache.Add(key, false);
        return false;
      }

      if (permissions.Any(p => IsEqual(p, permission)))
      {
        _queryCache = _queryCache.Add(key, true);
        return true;
      }

      foreach (var perm in permissions)
      {
        if (HasNestedPermission(perm, permission, new HashSet<string>()))
        {
          _queryCache = _queryCache.Add(key, true);
          return true;
        }
      }

      _queryCache = _queryCache.Add(key, false);
      return false;
    }

  }

  public void AddPermission( ulong playerId, string permission )
  {
    lock (_lock)
    {
      if (_temporaryPlayerPermissions.TryGetValue(playerId, out var permissions))
      {
        if (!permissions.Contains(permission))
        {
          permissions.Add(permission);
        }
      }
      else
      {
        _temporaryPlayerPermissions[playerId] = [permission];
      }

      _queryCache = _queryCache.Clear();
    }
  }

  public void RemovePermission( ulong playerId, string permission )
  {
    lock (_lock)
    {
      if (_temporaryPlayerPermissions.TryGetValue(playerId, out var permissions))
      {
        if (permissions.Contains(permission))
        {
          permissions.Remove(permission);
        }
      }
    }

    _queryCache = _queryCache.Clear();
  }

  public void AddSubPermission( string permission, string subPermission )
  {
    lock (_lock)
    {
      if (_temporarySubPermissions.TryGetValue(permission, out var subPermissions))
      {
        if (!subPermissions.Contains(subPermission))
        {
          subPermissions.Add(subPermission);
        }
      }
      else
      {
        _temporarySubPermissions[permission] = [subPermission];
      }
    }
    _queryCache = _queryCache.Clear();
  }

  public void RemoveSubPermission( string permission, string subPermission )
  {
    lock (_lock)
    {
      if (_temporarySubPermissions.TryGetValue(permission, out var subPermissions))
      {
        if (subPermissions.Contains(subPermission))
        {
          subPermissions.Remove(subPermission);
        }
      }
    }

    _queryCache = _queryCache.Clear();
  }
}