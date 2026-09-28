#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativePlayer
{

    private unsafe static delegate* unmanaged<int, int, byte*, int, void> _SendMessage;

    public unsafe static void SendMessage(int playerid, int kind, string message, int htmlDuration)
    {
        if (!NativeBinding.IsMainThread)
        {
            throw new InvalidOperationException("This method can only be called from the main thread.");
        }
        using var messageStr = new ScopedCString(message);
        fixed (byte* messageBufferPtr = messageStr)
        {
            _SendMessage(playerid, kind, messageBufferPtr, htmlDuration);
        }
    }

    private unsafe static delegate* unmanaged<int, byte> _IsFakeClient;

    public unsafe static bool IsFakeClient(int playerid)
    {
        var ret = _IsFakeClient(playerid);
        return ret == 1;
    }

    private unsafe static delegate* unmanaged<int, byte> _IsAuthorized;

    public unsafe static bool IsAuthorized(int playerid)
    {
        var ret = _IsAuthorized(playerid);
        return ret == 1;
    }

    private unsafe static delegate* unmanaged<int, uint> _GetConnectedTime;

    public unsafe static uint GetConnectedTime(int playerid)
    {
        var ret = _GetConnectedTime(playerid);
        return ret;
    }

    private unsafe static delegate* unmanaged<int, ulong> _GetUnauthorizedSteamID;

    public unsafe static ulong GetUnauthorizedSteamID(int playerid)
    {
        var ret = _GetUnauthorizedSteamID(playerid);
        return ret;
    }

    private unsafe static delegate* unmanaged<int, ulong> _GetSteamID;

    public unsafe static ulong GetSteamID(int playerid)
    {
        var ret = _GetSteamID(playerid);
        return ret;
    }

    private unsafe static delegate* unmanaged<int*, int, byte*> _GetIPAddress;

    public unsafe static string GetIPAddress(int playerid)
    {
        var length = 0;
        var returnedPtr = _GetIPAddress(&length, playerid);
        var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
        NativeAllocator.Free((nint)returnedPtr);
        return outString;
    }

    private unsafe static delegate* unmanaged<int, byte*, int, void> _Kick;

    public unsafe static void Kick(int playerid, string reason, int gamereason)
    {
        if (!NativeBinding.IsMainThread)
        {
            throw new InvalidOperationException("This method can only be called from the main thread.");
        }
        using var reasonStr = new ScopedCString(reason);
        fixed (byte* reasonBufferPtr = reasonStr)
        {
            _Kick(playerid, reasonBufferPtr, gamereason);
        }
    }

    private unsafe static delegate* unmanaged<int, int, byte, void> _ShouldBlockTransmitEntity;

    public unsafe static void ShouldBlockTransmitEntity(int playerid, int entityidx, bool shouldBlockTransmit)
    {
        _ShouldBlockTransmitEntity(playerid, entityidx, shouldBlockTransmit ? (byte)1 : (byte)0);
    }

    private unsafe static delegate* unmanaged<int, int, byte> _IsTransmitEntityBlocked;

    public unsafe static bool IsTransmitEntityBlocked(int playerid, int entityidx)
    {
        var ret = _IsTransmitEntityBlocked(playerid, entityidx);
        return ret == 1;
    }

    private unsafe static delegate* unmanaged<int, void> _ClearTransmitEntityBlocked;

    public unsafe static void ClearTransmitEntityBlocked(int playerid)
    {
        _ClearTransmitEntityBlocked(playerid);
    }

    private unsafe static delegate* unmanaged<int*, int, byte*> _GetLanguage;

    public unsafe static string GetLanguage(int playerid)
    {
        var length = 0;
        var returnedPtr = _GetLanguage(&length, playerid);
        var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
        NativeAllocator.Free((nint)returnedPtr);
        return outString;
    }

    private unsafe static delegate* unmanaged<int, byte*, void> _SetCenterMenuRender;

    public unsafe static void SetCenterMenuRender(int playerid, string text)
    {
        using var textStr = new ScopedCString(text);
        fixed (byte* textBufferPtr = textStr)
        {
            _SetCenterMenuRender(playerid, textBufferPtr);
        }
    }

    private unsafe static delegate* unmanaged<int, void> _ClearCenterMenuRender;

    public unsafe static void ClearCenterMenuRender(int playerid)
    {
        _ClearCenterMenuRender(playerid);
    }

    private unsafe static delegate* unmanaged<int, byte> _HasMenuShown;

    public unsafe static bool HasMenuShown(int playerid)
    {
        var ret = _HasMenuShown(playerid);
        return ret == 1;
    }

    private unsafe static delegate* unmanaged<int, byte*, void> _ExecuteCommand;

    public unsafe static void ExecuteCommand(int playerid, string command)
    {
        if (!NativeBinding.IsMainThread)
        {
            throw new InvalidOperationException("This method can only be called from the main thread.");
        }
        using var commandStr = new ScopedCString(command);
        fixed (byte* commandBufferPtr = commandStr)
        {
            _ExecuteCommand(playerid, commandBufferPtr);
        }
    }

    private unsafe static delegate* unmanaged<int, byte> _IsFirstSpawn;

    public unsafe static bool IsFirstSpawn(int playerid)
    {
        var ret = _IsFirstSpawn(playerid);
        return ret == 1;
    }

    private unsafe static delegate* unmanaged<int, int> _GetUserID;

    public unsafe static int GetUserID(int playerid)
    {
        var ret = _GetUserID(playerid);
        return ret;
    }

    private unsafe static delegate* unmanaged<int, ulong> _GetSessionID;

    public unsafe static ulong GetSessionID(int playerid)
    {
        var ret = _GetSessionID(playerid);
        return ret;
    }

    private unsafe static delegate* unmanaged<int*, int, byte*, byte*> _GetClientConvarValue;

    public unsafe static string GetClientConvarValue(int playerid, string convarName)
    {
        using var convarNameStr = new ScopedCString(convarName);
        fixed (byte* convarNameBufferPtr = convarNameStr)
        {
            var length = 0;
            var returnedPtr = _GetClientConvarValue(&length, playerid, convarNameBufferPtr);
            var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
            NativeAllocator.Free((nint)returnedPtr);
            return outString;
        }
    }

    private unsafe static delegate* unmanaged<int, nint> _GetServerSideClient;

    public unsafe static nint GetServerSideClient(int playerid)
    {
        var ret = _GetServerSideClient(playerid);
        return ret;
    }
}