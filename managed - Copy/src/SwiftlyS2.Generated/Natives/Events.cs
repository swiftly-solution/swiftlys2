#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeEvents {
  private static int _MainThreadID;

  private unsafe static delegate* unmanaged<nint, void> _RegisterOnGameTickCallback;

  /// <summary>
  /// bool simulating, bool first, bool last -> void
  /// </summary>
  public unsafe static void RegisterOnGameTickCallback(nint callback) {
    _RegisterOnGameTickCallback(callback);
  }

  private unsafe static delegate* unmanaged<nint, void> _RegisterOnClientConnectCallback;

  /// <summary>
  /// int32 playerid -> bool (true -> ignored, false -> supercede)
  /// </summary>
  public unsafe static void RegisterOnClientConnectCallback(nint callback) {
    _RegisterOnClientConnectCallback(callback);
  }

  private unsafe static delegate* unmanaged<nint, void> _RegisterOnClientDisconnectCallback;

  /// <summary>
  /// int32 playerid, ENetworkDisconnectReason (int32) reason -> void
  /// </summary>
  public unsafe static void RegisterOnClientDisconnectCallback(nint callback) {
    _RegisterOnClientDisconnectCallback(callback);
  }

  private unsafe static delegate* unmanaged<nint, void> _RegisterOnClientKeyStateChangedCallback;

  /// <summary>
  /// int32 playerid, string key, bool pressed -> void
  /// </summary>
  public unsafe static void RegisterOnClientKeyStateChangedCallback(nint callback) {
    _RegisterOnClientKeyStateChangedCallback(callback);
  }

  private unsafe static delegate* unmanaged<nint, void> _RegisterOnClientProcessUsercmdsCallback;

  /// <summary>
  /// int32 playerid, ptr* usercmds, int numcmds, bool paused, float margin -> void
  /// </summary>
  public unsafe static void RegisterOnClientProcessUsercmdsCallback(nint callback) {
    _RegisterOnClientProcessUsercmdsCallback(callback);
  }

  private unsafe static delegate* unmanaged<nint, void> _RegisterOnClientPutInServerCallback;

  /// <summary>
  /// int32 playerid, int32 client_kind (0 -> player, 1 -> bot, 2 -> unknown) -> void
  /// </summary>
  public unsafe static void RegisterOnClientPutInServerCallback(nint callback) {
    _RegisterOnClientPutInServerCallback(callback);
  }

  private unsafe static delegate* unmanaged<nint, void> _RegisterOnClientSteamAuthorizeCallback;

  /// <summary>
  /// int32 playerid -> void
  /// </summary>
  public unsafe static void RegisterOnClientSteamAuthorizeCallback(nint callback) {
    _RegisterOnClientSteamAuthorizeCallback(callback);
  }

  private unsafe static delegate* unmanaged<nint, void> _RegisterOnClientSteamAuthorizeFailCallback;

  /// <summary>
  /// int32 playerid -> void
  /// </summary>
  public unsafe static void RegisterOnClientSteamAuthorizeFailCallback(nint callback) {
    _RegisterOnClientSteamAuthorizeFailCallback(callback);
  }

  private unsafe static delegate* unmanaged<nint, void> _RegisterOnEntityCreatedCallback;

  /// <summary>
  /// CEntityInstance* entity
  /// </summary>
  public unsafe static void RegisterOnEntityCreatedCallback(nint callback) {
    _RegisterOnEntityCreatedCallback(callback);
  }

  private unsafe static delegate* unmanaged<nint, void> _RegisterOnEntityDeletedCallback;

  /// <summary>
  /// CEntityInstance* entity
  /// </summary>
  public unsafe static void RegisterOnEntityDeletedCallback(nint callback) {
    _RegisterOnEntityDeletedCallback(callback);
  }

  private unsafe static delegate* unmanaged<nint, void> _RegisterOnEntityParentChangedCallback;

  /// <summary>
  /// CEntityInstance* entity, CEntityInstance* newparent
  /// </summary>
  public unsafe static void RegisterOnEntityParentChangedCallback(nint callback) {
    _RegisterOnEntityParentChangedCallback(callback);
  }

  private unsafe static delegate* unmanaged<nint, void> _RegisterOnEntitySpawnedCallback;

  /// <summary>
  /// CEntityInstance* entity
  /// </summary>
  public unsafe static void RegisterOnEntitySpawnedCallback(nint callback) {
    _RegisterOnEntitySpawnedCallback(callback);
  }

  private unsafe static delegate* unmanaged<nint, void> _RegisterOnMapLoadCallback;

  /// <summary>
  /// string mapname
  /// </summary>
  public unsafe static void RegisterOnMapLoadCallback(nint callback) {
    _RegisterOnMapLoadCallback(callback);
  }

  private unsafe static delegate* unmanaged<nint, void> _RegisterOnMapUnloadCallback;

  /// <summary>
  /// string mapname
  /// </summary>
  public unsafe static void RegisterOnMapUnloadCallback(nint callback) {
    _RegisterOnMapUnloadCallback(callback);
  }

  private unsafe static delegate* unmanaged<nint, void> _RegisterOnEntityTakeDamageCallback;

  /// <summary>
  /// CBaseEntity* entity, CTakeDamageInfo* info -> bool (true -> ignored, false -> supercede)
  /// </summary>
  public unsafe static void RegisterOnEntityTakeDamageCallback(nint callback) {
    _RegisterOnEntityTakeDamageCallback(callback);
  }

  private unsafe static delegate* unmanaged<nint, void> _RegisterOnPrecacheResourceCallback;

  /// <summary>
  /// IEntityResourceManifest* pResourceManifest
  /// </summary>
  public unsafe static void RegisterOnPrecacheResourceCallback(nint callback) {
    _RegisterOnPrecacheResourceCallback(callback);
  }

  private unsafe static delegate* unmanaged<nint, void> _RegisterOnPreworldUpdateCallback;

  /// <summary>
  /// bool simulating
  /// </summary>
  public unsafe static void RegisterOnPreworldUpdateCallback(nint callback) {
    _RegisterOnPreworldUpdateCallback(callback);
  }
}