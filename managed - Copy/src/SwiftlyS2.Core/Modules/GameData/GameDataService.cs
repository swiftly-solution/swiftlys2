using System.Collections.Concurrent;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Core.Memory;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Services;

namespace SwiftlyS2.Core.Services;

internal record Offset( int windows, int linux );
internal record Signature( string lib, string windows, string linux );
internal record Patch( string signature, string windows, string linux );

internal class GameDataService : IGameDataService
{

  private CoreContext _Context { get; init; }

  private ConcurrentDictionary<string, nint> _Signatures = new();
  private ConcurrentDictionary<string, int> _Offsets = new();
  private ConcurrentDictionary<string, Patch> _Patches = new();

  private static OSPlatform _Platform = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? OSPlatform.Windows : OSPlatform.Linux;

  public GameDataService( CoreContext context, MemoryService memoryService, ILogger<GameDataService> logger )
  {
    _Context = context;

    var signaturePath = Path.Combine(_Context.BaseDirectory, "resources", "gamedata", "signatures.jsonc");
    var offsetPath = Path.Combine(_Context.BaseDirectory, "resources", "gamedata", "offsets.jsonc");
    var patchPath = Path.Combine(_Context.BaseDirectory, "resources", "gamedata", "patches.jsonc");

    try
    {

      if (File.Exists(signaturePath))
      {
        var signatures = JsonSerializer.Deserialize<Dictionary<string, Signature>>(File.ReadAllText(signaturePath))!;
        foreach (var signature in signatures)
        {
          nint? value = null;
          if (_Platform == OSPlatform.Windows)
          {
            value = memoryService.GetAddressBySignature(signature.Value.lib, signature.Value.windows);
          }
          else
          {
            value = memoryService.GetAddressBySignature(signature.Value.lib, signature.Value.linux);
          }
          if (value is null)
          {
            logger.LogError("Failed to load signature {Signature}!", signature.Key);
            continue;
          }
          _Signatures.TryAdd(signature.Key, value.Value);
        }
      }

      if (File.Exists(offsetPath))
      {
        var offsets = JsonSerializer.Deserialize<Dictionary<string, Offset>>(File.ReadAllText(offsetPath))!;
        foreach (var offset in offsets)
        {
          if (_Platform == OSPlatform.Windows)
          {
            _Offsets.TryAdd(offset.Key, offset.Value.windows);
          }
          else
          {
            _Offsets.TryAdd(offset.Key, offset.Value.linux);
          }
        }
      }

      if (File.Exists(patchPath))
      {
        var patches = JsonSerializer.Deserialize<Dictionary<string, Patch>>(File.ReadAllText(patchPath))!;
        foreach (var patch in patches)
        {
          _Patches.TryAdd(patch.Key, patch.Value);
        }
      }

    }
    catch (Exception e)
    {
      if (!GlobalExceptionHandler.Handle(e)) return;
      logger.LogError(e, "Failed to load game data.");
    }
  }

  public bool HasSignature( string signatureName )
  {
    if (_Signatures.ContainsKey(signatureName))
    {
      return true;
    }

    return NativeSignatures.Exists(signatureName);
  }

  public nint GetSignature( string signatureName )
  {
    if (_Signatures.TryGetValue(signatureName, out var signature))
    {
      return signature;
    }
    return NativeSignatures.Fetch(signatureName);
  }

  public bool TryGetSignature( string signatureName, out nint signature )
  {
    if (_Signatures.TryGetValue(signatureName, out var _signature))
    {
      signature = _signature;
      return true;
    }
    signature = NativeSignatures.Fetch(signatureName);
    return signature != nint.Zero;
  }

  public bool HasOffset( string offsetName )
  {
    if (_Offsets.ContainsKey(offsetName))
    {
      return true;
    }
    return NativeOffsets.Exists(offsetName);
  }

  public int GetOffset( string offsetName )
  {
    if (_Offsets.TryGetValue(offsetName, out var offset))
    {
      return offset;
    }
    return NativeOffsets.Fetch(offsetName);
  }

  public bool TryGetOffset( string offsetName, out nint offset )
  {
    if (_Offsets.TryGetValue(offsetName, out var _offset))
    {
      offset = _offset;
      return true;
    }
    offset = NativeOffsets.Fetch(offsetName);
    return offset != nint.Zero;
  }

  public bool HasPatch( string patchName )
  {
    if (_Patches.ContainsKey(patchName))
    {
      return true;
    }
    return NativePatches.Exists(patchName);
  }

  public void ApplyPatch( string patchName )
  {
    if (_Patches.TryGetValue(patchName, out var patch))
    {
      nint address = GetSignature(patch.signature);
      if (address == nint.Zero)
      {
        throw new Exception($"Failed to apply patch {patchName}, cannot find signature {patch.signature}.");
      }

      byte[] bytes;

      if (_Platform == OSPlatform.Windows)
      {
        bytes = patch.windows
          .Split(' ', StringSplitOptions.RemoveEmptyEntries)
          .Select(x => byte.Parse(x, NumberStyles.HexNumber, CultureInfo.InvariantCulture))
          .ToArray();
      }
      else
      {
        bytes = patch.linux
          .Split(' ', StringSplitOptions.RemoveEmptyEntries)
          .Select(x => byte.Parse(x, NumberStyles.HexNumber, CultureInfo.InvariantCulture))
          .ToArray();
      }
      MemoryPatch.SetMemAccess(address, bytes.Length);
      address.CopyFrom(bytes);
      return;
    }
    NativePatches.Apply(patchName);
  }

}
