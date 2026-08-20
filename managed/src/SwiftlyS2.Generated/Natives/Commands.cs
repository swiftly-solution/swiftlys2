#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeCommands
{

    private unsafe static delegate* unmanaged<byte*, byte, byte*, ulong> _RegisterCommand;

    /// <summary>
    /// if registerRaw is false, it will not put "sw_" before the command name
    /// </summary>
    public unsafe static ulong RegisterCommand(string commandName, bool registerRaw, string helpText)
    {
        return StringAlloc.CreateCString(commandName, commandNameBufferPtr =>
        {
            return StringAlloc.CreateCString(helpText, helpTextBufferPtr =>
            {
                var ret = _RegisterCommand((byte*)commandNameBufferPtr, registerRaw ? (byte)1 : (byte)0, (byte*)helpTextBufferPtr);
                return ret;
            });
        });
    }

    private unsafe static delegate* unmanaged<ulong, void> _UnregisterCommand;

    public unsafe static void UnregisterCommand(ulong callbackID)
    {
        _UnregisterCommand(callbackID);
    }

    private unsafe static delegate* unmanaged<nint, void> _SetCommandHandler;

    /// <summary>
    /// the callback should receive (nint commandName, int playerid, nint impodedArgs (\x01), nint originalCommandName, nint prefix, byte silent)
    /// </summary>
    public unsafe static void SetCommandHandler(nint callback)
    {
        _SetCommandHandler(callback);
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

    private unsafe static delegate* unmanaged<nint, void> _SetClientCommandHandler;

    /// <summary>
    /// callback should receive: int32 playerid, string commandline
    /// </summary>
    public unsafe static void SetClientCommandHandler(nint callback)
    {
        _SetClientCommandHandler(callback);
    }

    private unsafe static delegate* unmanaged<nint, void> _SetClientChatHandler;

    /// <summary>
    /// callback should receive: int32 playerid, string text, bool teamonly, return HookResult result
    /// </summary>
    public unsafe static void SetClientChatHandler(nint callback)
    {
        _SetClientChatHandler(callback);
    }
}