namespace SwiftlyS2.Shared.Services;

public interface IGameDataService {

  /// <summary>
  /// Check if a signature exists.
  /// </summary>
  /// <param name="signatureName">Signature name defined in `signatures.jsonc` file.</param>
  /// <returns>Whether the signature exists.</returns>
  bool HasSignature(string signatureName);

  /// <summary>
  /// Get a signature by name.
  /// </summary>
  /// <param name="signatureName">Signature name defined in `signatures.jsonc` file.</param>
  /// <returns>The signature.</returns>
  nint GetSignature(string signatureName);

  /// <summary>
  /// Try to get a signature by name.
  /// </summary>
  /// <param name="signatureName">Signature name defined in `signatures.jsonc` file.</param>
  /// <param name="signature">The signature.</param>
  /// <returns>Whether the signature exists.</returns>
  bool TryGetSignature(string signatureName, out nint signature);

  /// <summary>
  /// Check if an offset exists.
  /// </summary>
  /// <param name="offsetName">Offset name defined in `offsets.jsonc` file.</param>
  /// <returns>Whether the offset exists.</returns>
  bool HasOffset(string offsetName);

  /// <summary>
  /// Get an offset by name.
  /// </summary>
  /// <param name="offsetName">Offset name defined in `offsets.jsonc` file.</param>
  /// <returns>The offset.</returns>
  int GetOffset(string offsetName);

  /// <summary>
  /// Try to get an offset by name.
  /// </summary>
  /// <param name="offsetName">Offset name defined in `offsets.jsonc` file.</param>
  /// <param name="offset">The offset.</param>
  /// <returns>Whether the offset exists.</returns>
  bool TryGetOffset(string offsetName, out nint offset);

  /// <summary>
  /// Check if a patch exists.
  /// </summary>
  /// <param name="patchName">Patch name defined in `patchs.jsonc` file.</param>
  /// <returns>Whether the patch exists.</returns>
  bool HasPatch(string patchName);

  /// <summary>
  /// Apply a patch by name.
  /// </summary>
  /// <param name="patchName">Patch name defined in `patchs.jsonc` file.</param>
  void ApplyPatch(string patchName);

}