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
internal sealed class AppliedPatch
{
    public nint Address { get; init; }
    public byte[] OriginalBytes { get; init; } = [];
}

internal class GameDataService : IGameDataService
{

    private CoreContext _Context { get; init; }

    private ConcurrentDictionary<string, nint> _Signatures = [];
    private ConcurrentDictionary<string, int> _Offsets = [];
    private ConcurrentDictionary<string, Patch> _Patches = [];
    private static readonly ConcurrentDictionary<string, AppliedPatch> _AppliedPatches = [];
    private static readonly object _AppliedPatchesLock = new();

    private static readonly OSPlatform _Platform = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? OSPlatform.Windows : OSPlatform.Linux;

    public GameDataService( CoreContext context, MemoryService memoryService, ILogger<GameDataService> logger )
    {
        _Context = context;

        var signaturePath = Path.Combine(_Context.BaseDirectory, "resources", "gamedata", "signatures.jsonc");
        var offsetPath = Path.Combine(_Context.BaseDirectory, "resources", "gamedata", "offsets.jsonc");
        var patchPath = Path.Combine(_Context.BaseDirectory, "resources", "gamedata", "patches.jsonc");

        var options = new JsonSerializerOptions() {
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
        };

        try
        {

            if (File.Exists(signaturePath))
            {
                var signatures = JsonSerializer.Deserialize<Dictionary<string, Signature>>(File.ReadAllText(signaturePath), options)!;
                foreach (var signature in signatures)
                {
                    var sig = _Platform == OSPlatform.Windows ? signature.Value.windows : signature.Value.linux;
                    if(sig.Trim() == "") continue;
                    
                    var value = memoryService.GetAddressBySignature(signature.Value.lib, sig);
                    if (value == null)
                    {
                        logger.LogError("Failed to load signature {Signature}!", signature.Key);
                        continue;
                    }
                    _ = _Signatures.TryAdd(signature.Key, value.Value);
                }
            }

            if (File.Exists(offsetPath))
            {
                var offsets = JsonSerializer.Deserialize<Dictionary<string, Offset>>(File.ReadAllText(offsetPath), options)!;
                foreach (var offset in offsets)
                {
                    _ = _Offsets.TryAdd(offset.Key, _Platform == OSPlatform.Windows ? offset.Value.windows : offset.Value.linux);
                }
            }

            if (File.Exists(patchPath))
            {
                var patches = JsonSerializer.Deserialize<Dictionary<string, Patch>>(File.ReadAllText(patchPath), options)!;
                foreach (var patch in patches)
                {
                    _ = _Patches.TryAdd(patch.Key, patch.Value);
                }
            }

        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e)) return;
            logger.LogError(e, "Failed to load game data.");
        }
    }

    public bool HasSignature( string signatureName )
    {
        return _Signatures.ContainsKey(signatureName) || NativeSignatures.Exists(signatureName);
    }

    public nint GetSignature( string signatureName )
    {
        return _Signatures.TryGetValue(signatureName, out var signature) ? signature : NativeSignatures.Fetch(signatureName);
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
        return _Offsets.ContainsKey(offsetName) || NativeOffsets.Exists(offsetName);
    }
    public int GetOffset( string offsetName )
    {
        return _Offsets.TryGetValue(offsetName, out var offset) ? offset : NativeOffsets.Fetch(offsetName);
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
        return _Patches.ContainsKey(patchName) || NativePatches.Exists(patchName);
    }

    public void ApplyPatch( string patchName )
    {
        if (_Patches.TryGetValue(patchName, out var patch))
        {
            var address = GetSignature(patch.signature);
            if (address == nint.Zero)
            {
                throw new Exception($"Failed to apply patch {patchName}, cannot find signature {patch.signature}.");
            }

            var bytes = (_Platform == OSPlatform.Windows ? patch.windows : patch.linux)
                  .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                  .Select(x => byte.Parse(x, NumberStyles.HexNumber, CultureInfo.InvariantCulture))
                  .ToArray();

            lock (_AppliedPatchesLock)
            {
                if (_AppliedPatches.ContainsKey(patchName))
                {
                    return;
                }

                var oldBytes = new byte[bytes.Length];
                Marshal.Copy(address, oldBytes, 0, oldBytes.Length);
                _AppliedPatches[patchName] = new AppliedPatch {
                    Address = address,
                    OriginalBytes = oldBytes,
                };

                _ = MemoryPatch.SetMemAccess(address, bytes.Length);
                address.CopyFrom(bytes);
            }

            return;
        }
        NativePatches.Apply(patchName);
    }

    public void RevertPatch( string patchName )
    {
        if (_Patches.ContainsKey(patchName))
        {
            lock (_AppliedPatchesLock)
            {
                if (_AppliedPatches.TryRemove(patchName, out var appliedPatch))
                {
                    _ = MemoryPatch.SetMemAccess(appliedPatch.Address, appliedPatch.OriginalBytes.Length);
                    appliedPatch.Address.CopyFrom(appliedPatch.OriginalBytes);
                }
            }
            return;
        }

        NativePatches.Revert(patchName);
    }
}
