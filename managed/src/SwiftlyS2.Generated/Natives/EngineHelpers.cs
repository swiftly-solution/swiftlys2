#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeEngineHelpers
{

    private unsafe static delegate* unmanaged<int*, byte*> _GetIP;

    public unsafe static string GetIP()
    {
        var length = 0;
        var returnedPtr = _GetIP(&length);
        var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
        NativeAllocator.Free((nint)returnedPtr);
        return outString;
    }

    private unsafe static delegate* unmanaged<byte*, byte> _IsMapValid;

    /// <summary>
    /// it can be map name, or workshop id
    /// </summary>
    public unsafe static bool IsMapValid(string map_name)
    {
        using var map_nameStr = new ScopedCString(map_name);
        fixed (byte* map_nameBufferPtr = map_nameStr)
        {
            var ret = _IsMapValid(map_nameBufferPtr);
            return ret == 1;
        }
    }

    private unsafe static delegate* unmanaged<byte*, void> _ExecuteCommand;

    public unsafe static void ExecuteCommand(string command)
    {
        if (!NativeBinding.IsMainThread)
        {
            throw new InvalidOperationException("This method can only be called from the main thread.");
        }
        using var commandStr = new ScopedCString(command);
        fixed (byte* commandBufferPtr = commandStr)
        {
            _ExecuteCommand(commandBufferPtr);
        }
    }

    private unsafe static delegate* unmanaged<byte*, nint> _FindGameSystemByName;

    public unsafe static nint FindGameSystemByName(string name)
    {
        using var nameStr = new ScopedCString(name);
        fixed (byte* nameBufferPtr = nameStr)
        {
            var ret = _FindGameSystemByName(nameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<byte*, void> _SendMessageToConsole;

    public unsafe static void SendMessageToConsole(string msg)
    {
        using var msgStr = new ScopedCString(msg);
        fixed (byte* msgBufferPtr = msgStr)
        {
            _SendMessageToConsole(msgBufferPtr);
        }
    }

    private unsafe static delegate* unmanaged<nint> _GetTraceManager;

    public unsafe static nint GetTraceManager()
    {
        var ret = _GetTraceManager();
        return ret;
    }

    private unsafe static delegate* unmanaged<int*, byte*> _GetCurrentGame;

    public unsafe static string GetCurrentGame()
    {
        var length = 0;
        var returnedPtr = _GetCurrentGame(&length);
        var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
        NativeAllocator.Free((nint)returnedPtr);
        return outString;
    }

    private unsafe static delegate* unmanaged<int*, byte*> _GetNativeVersion;

    public unsafe static string GetNativeVersion()
    {
        var length = 0;
        var returnedPtr = _GetNativeVersion(&length);
        var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
        NativeAllocator.Free((nint)returnedPtr);
        return outString;
    }

    private unsafe static delegate* unmanaged<int*, byte*> _GetMenuSettings;

    public unsafe static string GetMenuSettings()
    {
        var length = 0;
        var returnedPtr = _GetMenuSettings(&length);
        var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
        NativeAllocator.Free((nint)returnedPtr);
        return outString;
    }

    private unsafe static delegate* unmanaged<nint> _GetGlobalVars;

    public unsafe static nint GetGlobalVars()
    {
        var ret = _GetGlobalVars();
        return ret;
    }

    private unsafe static delegate* unmanaged<nint> _GetNetworkGameServer;

    public unsafe static nint GetNetworkGameServer()
    {
        var ret = _GetNetworkGameServer();
        return ret;
    }

    private unsafe static delegate* unmanaged<int*, byte*> _GetGameDirectoryPath;

    public unsafe static string GetGameDirectoryPath()
    {
        var length = 0;
        var returnedPtr = _GetGameDirectoryPath(&length);
        var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
        NativeAllocator.Free((nint)returnedPtr);
        return outString;
    }

    private unsafe static delegate* unmanaged<int*, byte*> _GetWorkshopId;

    public unsafe static string GetWorkshopId()
    {
        var length = 0;
        var returnedPtr = _GetWorkshopId(&length);
        var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
        NativeAllocator.Free((nint)returnedPtr);
        return outString;
    }

    private unsafe static delegate* unmanaged<byte*, uint, nint, byte, nint, byte, int, ulong, void> _DispatchParticleEffect;

    public unsafe static void DispatchParticleEffect(string particleName, uint attachmentType, nint entity, byte attachmentPoint, nint attachmentName, bool resetAllParticlesOnEntity, int splitScreenSlot, ulong filtermask)
    {
        using var particleNameStr = new ScopedCString(particleName);
        fixed (byte* particleNameBufferPtr = particleNameStr)
        {
            _DispatchParticleEffect(particleNameBufferPtr, attachmentType, entity, attachmentPoint, attachmentName, resetAllParticlesOnEntity ? (byte)1 : (byte)0, splitScreenSlot, filtermask);
        }
    }
}