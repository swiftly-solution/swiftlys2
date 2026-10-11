using System.Runtime.CompilerServices;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Scheduler;
using SwiftlyS2.Shared.Convars;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.Convars;

internal enum EConVarType : int
{
    EConVarType_Invalid = -1,
    EConVarType_Bool,
    EConVarType_Int16,
    EConVarType_UInt16,
    EConVarType_Int32,
    EConVarType_UInt32,
    EConVarType_Int64,
    EConVarType_UInt64,
    EConVarType_Float32,
    EConVarType_Float64,
    EConVarType_String,
    EConVarType_Color,
    EConVarType_Vector2,
    EConVarType_Vector3,
    EConVarType_Vector4,
    EConVarType_Qangle,
    EConVarType_VectorWS,
    EConVarType_MAX
};

internal class ConVarService : IConVarService
{
    private static readonly Dictionary<(int ClientId, string Name), Queue<Action<string>>> _pendingQueries = [];
    private static readonly Lock _pendingQueriesLock = new();
    private INetMessageService _netMessageService;

    public ConVarService( INetMessageService netMessageService )
    {
        _netMessageService = netMessageService;
    }

    public IConVar<T>? Find<T>( string name )
    {
        return !NativeConvars.ExistsConvar(name) ? null : (IConVar<T>)new ConVar<T>(name, this);
    }

    public IConVar? FindAsString( string name )
    {
        return !NativeConvars.ExistsConvar(name) ? null : (IConVar)new ConVar(name, this);
    }

    public unsafe IConVar<T> Create<T>( string name, string helpMessage, T defaultValue, ConvarFlags flags = ConvarFlags.NONE )
    {
        if (NativeConvars.ExistsConvar(name))
        {
            throw new Exception($"Convar {name} already exists.");
        }

        var type = GetConVarType<T>();

        if (defaultValue is string stringValue)
        {
            CreateStringConVar(name, helpMessage, stringValue, flags);
        }
        else
        {
            NativeConvars.CreateConvar(name, (int)type, (ulong)flags, helpMessage, (nint)Unsafe.AsPointer(ref defaultValue), 0, 0);
        }

        return new ConVar<T>(name, this);
    }

    public unsafe IConVar<T> Create<T>( string name, string helpMessage, T defaultValue, T? minValue, T? maxValue, ConvarFlags flags = ConvarFlags.NONE ) where T : unmanaged
    {
        if (NativeConvars.ExistsConvar(name))
        {
            throw new Exception($"Convar {name} already exists.");
        }

        var type = GetConVarType<T>();

        var min = minValue.GetValueOrDefault();
        var max = maxValue.GetValueOrDefault();

        NativeConvars.CreateConvar(name, (int)type, (ulong)flags, helpMessage, (nint)(&defaultValue), minValue.HasValue ? (nint)(&min) : 0, maxValue.HasValue ? (nint)(&max) : 0);

        return new ConVar<T>(name, this);
    }

    public IConVar<T> CreateOrFind<T>( string name, string helpMessage, T defaultValue, ConvarFlags flags = ConvarFlags.NONE )
    {
        return NativeConvars.ExistsConvar(name) ? new ConVar<T>(name, this) : Create(name, helpMessage, defaultValue, flags);
    }

    public IConVar<T> CreateOrFind<T>( string name, string helpMessage, T defaultValue, T? minValue, T? maxValue, ConvarFlags flags = ConvarFlags.NONE ) where T : unmanaged
    {
        return NativeConvars.ExistsConvar(name) ? new ConVar<T>(name, this) : Create(name, helpMessage, defaultValue, minValue, maxValue, flags);
    }

    public void ReplicateToClient( int clientId, string name, string value )
    {
        _netMessageService.Send<CNETMsg_SetConVar>(msg =>
        {
            var cvar = msg.Convars.Cvars.Add();
            cvar.Name = name;
            cvar.Value = value;
            msg.Recipients.AddRecipient(clientId);
        });
    }

    public void ReplicateToAll( string name, string value )
    {
        _netMessageService.Send<CNETMsg_SetConVar>(msg =>
        {
            var cvar = msg.Convars.Cvars.Add();
            cvar.Name = name;
            cvar.Value = value;
            msg.Recipients.AddAllPlayers();
        });
    }

    private static unsafe void CreateStringConVar( string name, string helpMessage, string value, ConvarFlags flags )
    {
        using var valueString = new ScopedCString(value);
        fixed (byte* valuePtr = valueString)
        {
            NativeConvars.CreateConvar(name, (int)EConVarType.EConVarType_String, (ulong)flags, helpMessage, (nint)valuePtr, 0, 0);
        }
    }

    private static EConVarType GetConVarType<T>()
    {
        if (typeof(T) == typeof(bool)) return EConVarType.EConVarType_Bool;
        if (typeof(T) == typeof(short)) return EConVarType.EConVarType_Int16;
        if (typeof(T) == typeof(ushort)) return EConVarType.EConVarType_UInt16;
        if (typeof(T) == typeof(int)) return EConVarType.EConVarType_Int32;
        if (typeof(T) == typeof(uint)) return EConVarType.EConVarType_UInt32;
        if (typeof(T) == typeof(long)) return EConVarType.EConVarType_Int64;
        if (typeof(T) == typeof(ulong)) return EConVarType.EConVarType_UInt64;
        if (typeof(T) == typeof(float)) return EConVarType.EConVarType_Float32;
        if (typeof(T) == typeof(double)) return EConVarType.EConVarType_Float64;
        if (typeof(T) == typeof(string)) return EConVarType.EConVarType_String;
        if (typeof(T) == typeof(Color)) return EConVarType.EConVarType_Color;
        if (typeof(T) == typeof(Vector2D)) return EConVarType.EConVarType_Vector2;
        if (typeof(T) == typeof(Vector)) return EConVarType.EConVarType_Vector3;
        if (typeof(T) == typeof(Vector4D)) return EConVarType.EConVarType_Vector4;
        if (typeof(T) == typeof(QAngle)) return EConVarType.EConVarType_Qangle;

        throw new Exception($"Unsupported type {typeof(T)}.");
    }

    public void QueryClient( int clientId, string name, Action<string> callback )
    {
        var key = (clientId, name);
        var shouldQuery = false;

        lock (_pendingQueriesLock)
        {
            if (!_pendingQueries.TryGetValue(key, out var queue))
            {
                queue = new Queue<Action<string>>();
                _pendingQueries[key] = queue;
                shouldQuery = true;
            }
            else
            {
                shouldQuery = false;
            }

            queue.Enqueue(callback);
        }

        if (shouldQuery)
        {
            _ = SchedulerManager.QueueOrNow(() => NativeConvars.QueryClientConvar(clientId, name));
        }
    }

    public static void ProcessConVarQueryCallback(int playerid, string convarName, string convarValue)
    {
        var key = (playerid, convarName);
        Queue<Action<string>>? queue;

        lock (_pendingQueriesLock)
        {
            if (!_pendingQueries.Remove(key, out queue))
            {
                return;
            }
        }

        while (queue.Count > 0)
        {
            queue.Dequeue()(convarValue);
        }
    }
}