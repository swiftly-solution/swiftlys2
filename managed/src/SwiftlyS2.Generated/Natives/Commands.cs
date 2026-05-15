#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeCommands
{

    private unsafe static delegate* unmanaged<int, byte*, int> _HandleCommandForPlayer;

    /// <summary>
    /// 1 -> not silent, 2 -> silent, -1 -> invalid player, 0 -> no command
    /// </summary>
    public unsafe static int HandleCommandForPlayer(int playerid, string command)
    {
        if (!NativeBinding.IsMainThread)
        {
            throw new InvalidOperationException("This method can only be called from the main thread.");
        }
        return StringAlloc.CreateCString(command, commandBufferPtr =>
        {
            var ret = _HandleCommandForPlayer(playerid, (byte*)commandBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<byte*, nint, byte, byte*, ulong> _RegisterCommand;

    /// <summary>
    /// callback should receive (int32 playerid, string arguments_list (separated by \x01), string commandName, string prefix, bool silent), if registerRaw is false, it will not put "sw_" before the command name
    /// </summary>
    public unsafe static ulong RegisterCommand(string commandName, nint callback, bool registerRaw, string helpText)
    {
        return StringAlloc.CreateCString(commandName, commandNameBufferPtr =>
        {
            return StringAlloc.CreateCString(helpText, helpTextBufferPtr =>
            {
                var ret = _RegisterCommand((byte*)commandNameBufferPtr, callback, registerRaw ? (byte)1 : (byte)0, (byte*)helpTextBufferPtr);
                return ret;
            });
        });
    }

    private unsafe static delegate* unmanaged<ulong, void> _UnregisterCommand;

    public unsafe static void UnregisterCommand(ulong callbackID)
    {
        _UnregisterCommand(callbackID);
    }

    private unsafe static delegate* unmanaged<byte*, byte> _IsCommandRegistered;

    public unsafe static bool IsCommandRegistered(string commandName)
    {
        return StringAlloc.CreateCString(commandName, commandNameBufferPtr =>
        {
            var ret = _IsCommandRegistered((byte*)commandNameBufferPtr);
            return ret == 1;
        });
    }

    private unsafe static delegate* unmanaged<byte*, byte*, byte, ulong> _RegisterAlias;

    /// <summary>
    /// registerRaw behaves the same as on RegisterCommand, for commandName you need to also put the "sw_" prefix if the command is registered without raw mode
    /// </summary>
    public unsafe static ulong RegisterAlias(string aliasName, string commandName, bool registerRaw)
    {
        return StringAlloc.CreateCString(aliasName, aliasNameBufferPtr =>
        {
            return StringAlloc.CreateCString(commandName, commandNameBufferPtr =>
            {
                var ret = _RegisterAlias((byte*)aliasNameBufferPtr, (byte*)commandNameBufferPtr, registerRaw ? (byte)1 : (byte)0);
                return ret;
            });
        });
    }

    private unsafe static delegate* unmanaged<ulong, void> _UnregisterAlias;

    public unsafe static void UnregisterAlias(ulong callbackID)
    {
        _UnregisterAlias(callbackID);
    }

    private unsafe static delegate* unmanaged<nint, ulong> _RegisterClientCommandsListener;

    /// <summary>
    /// callback should receive: int32 playerid, string commandline, return true -> ignored, return false -> supercede
    /// </summary>
    public unsafe static ulong RegisterClientCommandsListener(nint callback)
    {
        var ret = _RegisterClientCommandsListener(callback);
        return ret;
    }

    private unsafe static delegate* unmanaged<ulong, void> _UnregisterClientCommandsListener;

    public unsafe static void UnregisterClientCommandsListener(ulong callbackID)
    {
        _UnregisterClientCommandsListener(callbackID);
    }

    private unsafe static delegate* unmanaged<nint, ulong> _RegisterClientChatListener;

    /// <summary>
    /// callback should receive: int32 playerid, string text, bool teamonly, return true -> ignored, return false -> supercede, when superceded it's not gonna send the message
    /// </summary>
    public unsafe static ulong RegisterClientChatListener(nint callback)
    {
        var ret = _RegisterClientChatListener(callback);
        return ret;
    }

    private unsafe static delegate* unmanaged<ulong, void> _UnregisterClientChatListener;

    public unsafe static void UnregisterClientChatListener(ulong callbackID)
    {
        _UnregisterClientChatListener(callbackID);
    }

    private unsafe static delegate* unmanaged<int*, nint> _EnumerateAll;

    public unsafe static (nint ptr, int count) EnumerateAll()
    {
        var count = 0;
        var ptr = _EnumerateAll(&count);
        return (ptr, count);
    }

    private unsafe static delegate* unmanaged<ulong, int> _UnlockAll;

    public unsafe static int UnlockAll(ulong flagsToRemove)
    {
        var ret = _UnlockAll(flagsToRemove);
        return ret;
    }
}