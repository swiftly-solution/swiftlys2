#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativePlayer {
  private static int _MainThreadID;

  private unsafe static delegate* unmanaged<int, int, byte*, int, void> _SendMessage;

  public unsafe static void SendMessage(int playerid, int kind, string message, int htmlDuration) {
    var pool = ArrayPool<byte>.Shared;
    var messageLength = Encoding.UTF8.GetByteCount(message);
    var messageBuffer = pool.Rent(messageLength + 1);
    Encoding.UTF8.GetBytes(message, messageBuffer);
    messageBuffer[messageLength] = 0;
    fixed (byte* messageBufferPtr = messageBuffer) {
      _SendMessage(playerid, kind, messageBufferPtr, htmlDuration);
      pool.Return(messageBuffer);
    }
  }

  private unsafe static delegate* unmanaged<int, byte> _IsFakeClient;

  public unsafe static bool IsFakeClient(int playerid) {
    var ret = _IsFakeClient(playerid);
    return ret == 1;
  }

  private unsafe static delegate* unmanaged<int, byte> _IsAuthorized;

  public unsafe static bool IsAuthorized(int playerid) {
    var ret = _IsAuthorized(playerid);
    return ret == 1;
  }

  private unsafe static delegate* unmanaged<int, uint> _GetConnectedTime;

  public unsafe static uint GetConnectedTime(int playerid) {
    var ret = _GetConnectedTime(playerid);
    return ret;
  }

  private unsafe static delegate* unmanaged<int, ulong> _GetUnauthorizedSteamID;

  public unsafe static ulong GetUnauthorizedSteamID(int playerid) {
    var ret = _GetUnauthorizedSteamID(playerid);
    return ret;
  }

  private unsafe static delegate* unmanaged<int, ulong> _GetSteamID;

  public unsafe static ulong GetSteamID(int playerid) {
    var ret = _GetSteamID(playerid);
    return ret;
  }

  private unsafe static delegate* unmanaged<int, nint> _GetController;

  public unsafe static nint GetController(int playerid) {
    var ret = _GetController(playerid);
    return ret;
  }

  private unsafe static delegate* unmanaged<int, nint> _GetPawn;

  public unsafe static nint GetPawn(int playerid) {
    var ret = _GetPawn(playerid);
    return ret;
  }

  private unsafe static delegate* unmanaged<int, nint> _GetPlayerPawn;

  public unsafe static nint GetPlayerPawn(int playerid) {
    var ret = _GetPlayerPawn(playerid);
    return ret;
  }

  private unsafe static delegate* unmanaged<int, ulong> _GetPressedButtons;

  public unsafe static ulong GetPressedButtons(int playerid) {
    var ret = _GetPressedButtons(playerid);
    return ret;
  }

  private unsafe static delegate* unmanaged<int, byte*, void> _PerformCommand;

  public unsafe static void PerformCommand(int playerid, string command) {
    var pool = ArrayPool<byte>.Shared;
    var commandLength = Encoding.UTF8.GetByteCount(command);
    var commandBuffer = pool.Rent(commandLength + 1);
    Encoding.UTF8.GetBytes(command, commandBuffer);
    commandBuffer[commandLength] = 0;
    fixed (byte* commandBufferPtr = commandBuffer) {
      _PerformCommand(playerid, commandBufferPtr);
      pool.Return(commandBuffer);
    }
  }

  private unsafe static delegate* unmanaged<byte*, int, int> _GetIPAddress;

  public unsafe static string GetIPAddress(int playerid) {
    var ret = _GetIPAddress(null, playerid);
    var pool = ArrayPool<byte>.Shared;
    var retBuffer = pool.Rent(ret + 1);
    fixed (byte* retBufferPtr = retBuffer) {
      ret = _GetIPAddress(retBufferPtr, playerid);
      var retString = Encoding.UTF8.GetString(retBufferPtr, ret);
      pool.Return(retBuffer);
      return retString;
    }
  }

  private unsafe static delegate* unmanaged<int, byte*, int, void> _Kick;

  public unsafe static void Kick(int playerid, string reason, int gamereason) {
    var pool = ArrayPool<byte>.Shared;
    var reasonLength = Encoding.UTF8.GetByteCount(reason);
    var reasonBuffer = pool.Rent(reasonLength + 1);
    Encoding.UTF8.GetBytes(reason, reasonBuffer);
    reasonBuffer[reasonLength] = 0;
    fixed (byte* reasonBufferPtr = reasonBuffer) {
      _Kick(playerid, reasonBufferPtr, gamereason);
      pool.Return(reasonBuffer);
    }
  }

  private unsafe static delegate* unmanaged<int, int, byte, void> _ShouldBlockTransmitEntity;

  public unsafe static void ShouldBlockTransmitEntity(int playerid, int entityidx, bool shouldBlockTransmit) {
    _ShouldBlockTransmitEntity(playerid, entityidx, shouldBlockTransmit ? (byte)1 : (byte)0);
  }

  private unsafe static delegate* unmanaged<int, int, byte> _IsTransmitEntityBlocked;

  public unsafe static bool IsTransmitEntityBlocked(int playerid, int entityidx) {
    var ret = _IsTransmitEntityBlocked(playerid, entityidx);
    return ret == 1;
  }

  private unsafe static delegate* unmanaged<int, void> _ClearTransmitEntityBlocked;

  public unsafe static void ClearTransmitEntityBlocked(int playerid) {
    _ClearTransmitEntityBlocked(playerid);
  }

  private unsafe static delegate* unmanaged<int, int, void> _ChangeTeam;

  public unsafe static void ChangeTeam(int playerid, int newteam) {
    _ChangeTeam(playerid, newteam);
  }

  private unsafe static delegate* unmanaged<int, int, void> _SwitchTeam;

  public unsafe static void SwitchTeam(int playerid, int newteam) {
    _SwitchTeam(playerid, newteam);
  }

  private unsafe static delegate* unmanaged<int, nint, void> _TakeDamage;

  public unsafe static void TakeDamage(int playerid, nint dmginfo) {
    _TakeDamage(playerid, dmginfo);
  }

  private unsafe static delegate* unmanaged<int, Vector, QAngle, Vector, void> _Teleport;

  public unsafe static void Teleport(int playerid, Vector pos, QAngle angle, Vector velocity) {
    _Teleport(playerid, pos, angle, velocity);
  }

  private unsafe static delegate* unmanaged<byte*, int, int> _GetLanguage;

  public unsafe static string GetLanguage(int playerid) {
    var ret = _GetLanguage(null, playerid);
    var pool = ArrayPool<byte>.Shared;
    var retBuffer = pool.Rent(ret + 1);
    fixed (byte* retBufferPtr = retBuffer) {
      ret = _GetLanguage(retBufferPtr, playerid);
      var retString = Encoding.UTF8.GetString(retBufferPtr, ret);
      pool.Return(retBuffer);
      return retString;
    }
  }

  private unsafe static delegate* unmanaged<int, byte*, void> _SetCenterMenuRender;

  public unsafe static void SetCenterMenuRender(int playerid, string text) {
    var pool = ArrayPool<byte>.Shared;
    var textLength = Encoding.UTF8.GetByteCount(text);
    var textBuffer = pool.Rent(textLength + 1);
    Encoding.UTF8.GetBytes(text, textBuffer);
    textBuffer[textLength] = 0;
    fixed (byte* textBufferPtr = textBuffer) {
      _SetCenterMenuRender(playerid, textBufferPtr);
      pool.Return(textBuffer);
    }
  }

  private unsafe static delegate* unmanaged<int, void> _ClearCenterMenuRender;

  public unsafe static void ClearCenterMenuRender(int playerid) {
    _ClearCenterMenuRender(playerid);
  }

  private unsafe static delegate* unmanaged<int, byte> _HasMenuShown;

  public unsafe static bool HasMenuShown(int playerid) {
    var ret = _HasMenuShown(playerid);
    return ret == 1;
  }

  private unsafe static delegate* unmanaged<int, byte*, void> _ExecuteCommand;

  public unsafe static void ExecuteCommand(int playerid, string command) {
    var pool = ArrayPool<byte>.Shared;
    var commandLength = Encoding.UTF8.GetByteCount(command);
    var commandBuffer = pool.Rent(commandLength + 1);
    Encoding.UTF8.GetBytes(command, commandBuffer);
    commandBuffer[commandLength] = 0;
    fixed (byte* commandBufferPtr = commandBuffer) {
      _ExecuteCommand(playerid, commandBufferPtr);
      pool.Return(commandBuffer);
    }
  }
}