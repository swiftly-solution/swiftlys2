#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeCommands {
  private static int _MainThreadID;

  private unsafe static delegate* unmanaged<int, byte*, int> _HandleCommandForPlayer;

  /// <summary>
  /// 1 -> not silent, 2 -> silent, -1 -> invalid player, 0 -> no command
  /// </summary>
  public unsafe static int HandleCommandForPlayer(int playerid, string command) {
    var pool = ArrayPool<byte>.Shared;
    var commandLength = Encoding.UTF8.GetByteCount(command);
    var commandBuffer = pool.Rent(commandLength + 1);
    Encoding.UTF8.GetBytes(command, commandBuffer);
    commandBuffer[commandLength] = 0;
    fixed (byte* commandBufferPtr = commandBuffer) {
      var ret = _HandleCommandForPlayer(playerid, commandBufferPtr);
      pool.Return(commandBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<byte*, nint, byte, ulong> _RegisterCommand;

  /// <summary>
  /// callback should receive (int32 playerid, string arguments_list (separated by \x01), string commandName, string prefix, bool silent), if registerRaw is false, it will not put "sw_" before the command name
  /// </summary>
  public unsafe static ulong RegisterCommand(string commandName, nint callback, bool registerRaw) {
    var pool = ArrayPool<byte>.Shared;
    var commandNameLength = Encoding.UTF8.GetByteCount(commandName);
    var commandNameBuffer = pool.Rent(commandNameLength + 1);
    Encoding.UTF8.GetBytes(commandName, commandNameBuffer);
    commandNameBuffer[commandNameLength] = 0;
    fixed (byte* commandNameBufferPtr = commandNameBuffer) {
      var ret = _RegisterCommand(commandNameBufferPtr, callback, registerRaw ? (byte)1 : (byte)0);
      pool.Return(commandNameBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<ulong, void> _UnregisterCommand;

  public unsafe static void UnregisterCommand(ulong callbackID) {
    _UnregisterCommand(callbackID);
  }

  private unsafe static delegate* unmanaged<byte*, byte*, byte, ulong> _RegisterAlias;

  /// <summary>
  /// registerRaw behaves the same as on RegisterCommand, for commandName you need to also put the "sw_" prefix if the command is registered without raw mode
  /// </summary>
  public unsafe static ulong RegisterAlias(string aliasName, string commandName, bool registerRaw) {
    var pool = ArrayPool<byte>.Shared;
    var aliasNameLength = Encoding.UTF8.GetByteCount(aliasName);
    var aliasNameBuffer = pool.Rent(aliasNameLength + 1);
    Encoding.UTF8.GetBytes(aliasName, aliasNameBuffer);
    aliasNameBuffer[aliasNameLength] = 0;
    var commandNameLength = Encoding.UTF8.GetByteCount(commandName);
    var commandNameBuffer = pool.Rent(commandNameLength + 1);
    Encoding.UTF8.GetBytes(commandName, commandNameBuffer);
    commandNameBuffer[commandNameLength] = 0;
    fixed (byte* aliasNameBufferPtr = aliasNameBuffer) {
      fixed (byte* commandNameBufferPtr = commandNameBuffer) {
        var ret = _RegisterAlias(aliasNameBufferPtr, commandNameBufferPtr, registerRaw ? (byte)1 : (byte)0);
        pool.Return(aliasNameBuffer);
        pool.Return(commandNameBuffer);
        return ret;
      }
    }
  }

  private unsafe static delegate* unmanaged<ulong, void> _UnregisterAlias;

  public unsafe static void UnregisterAlias(ulong callbackID) {
    _UnregisterAlias(callbackID);
  }

  private unsafe static delegate* unmanaged<nint, ulong> _RegisterClientCommandsListener;

  /// <summary>
  /// callback should receive: int32 playerid, string commandline, return true -> ignored, return false -> supercede
  /// </summary>
  public unsafe static ulong RegisterClientCommandsListener(nint callback) {
    var ret = _RegisterClientCommandsListener(callback);
    return ret;
  }

  private unsafe static delegate* unmanaged<ulong, void> _UnregisterClientCommandsListener;

  public unsafe static void UnregisterClientCommandsListener(ulong callbackID) {
    _UnregisterClientCommandsListener(callbackID);
  }

  private unsafe static delegate* unmanaged<nint, ulong> _RegisterClientChatListener;

  /// <summary>
  /// callback should receive: int32 playerid, string text, bool teamonly, return true -> ignored, return false -> supercede, when superceded it's not gonna send the message
  /// </summary>
  public unsafe static ulong RegisterClientChatListener(nint callback) {
    var ret = _RegisterClientChatListener(callback);
    return ret;
  }

  private unsafe static delegate* unmanaged<ulong, void> _UnregisterClientChatListener;

  public unsafe static void UnregisterClientChatListener(ulong callbackID) {
    _UnregisterClientChatListener(callbackID);
  }
}