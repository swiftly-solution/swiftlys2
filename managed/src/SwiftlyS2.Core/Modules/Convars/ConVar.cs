#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Scheduler;
using SwiftlyS2.Shared.Convars;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Core.Extensions;

namespace SwiftlyS2.Core.Convars;

internal delegate void ConVarCallbackDelegate( int playerId, nint name, nint value );

internal class ConVar : IConVar
{
    private readonly ConcurrentDictionary<int, ConVarCallbackDelegate> callbacks = new();
    protected nint MinValuePtrPtr => NativeConvars.GetMinValuePtrPtr(Name);
    protected nint MaxValuePtrPtr => NativeConvars.GetMaxValuePtrPtr(Name);

    public EConVarType Type => (EConVarType)NativeConvars.GetConvarType(Name);
    public string Name { get; set; }
    public string HelpText => NativeConvars.GetDescription(Name);
    public bool HasDefaultValue => NativeConvars.HasDefaultValue(Name);
    public bool HasMinValue => MinValuePtrPtr.Read<nint>() != 0;
    public bool HasMaxValue => MaxValuePtrPtr.Read<nint>() != 0;

    public string ValueAsString {
        get => GetValueAsString();
        set => SetValueAsString(value);
    }

    public string MaxValueAsString {
        get => GetMaxValueAsString();
        set => SetMaxValueAsString(value);
    }

    public string MinValueAsString {
        get => GetMinValueAsString();
        set => SetMinValueAsString(value);
    }

    public string DefaultValueAsString {
        get => GetDefaultValueAsString();
        set => SetDefaultValueAsString(value);
    }

    public ConvarFlags Flags {
        get => (ConvarFlags)NativeConvars.GetFlags(Name);
        set => NativeConvars.SetFlags(Name, (ulong)value);
    }

    internal ConVar( string name )
    {
        callbacks.Clear();
        Name = name;
    }

    public void SetInternalAsString( string value )
    {
        NativeConvars.SetValueInternalAsString(Name, value);
    }

    private void SetValueAsString( string value )
    {
        if (!NativeConvars.SetValueAsString(Name, value))
        {
            throw new ArgumentException($"Failed to set value of convar {Name} (type {Type}) to {value}.");
        }
    }

    private string GetValueAsString()
    {
        return NativeConvars.GetValueAsString(Name);
    }

    private void SetDefaultValueAsString( string value )
    {
        if (!NativeConvars.SetDefaultValueAsString(Name, value))
        {
            throw new ArgumentException($"Failed to set default value of convar {Name} (type {Type}) to {value}.");
        }
    }


    private string GetDefaultValueAsString()
    {
        return NativeConvars.GetDefaultValueAsString(Name);
    }

    private void SetMinValueAsString( string value )
    {
        if (!NativeConvars.SetMinValueAsString(Name, value))
        {
            throw new ArgumentException($"Failed to set min value of convar {Name} (type {Type}) to {value}.");
        }
    }

    private string GetMinValueAsString()
    {
        return NativeConvars.GetMinValueAsString(Name);
    }

    private void SetMaxValueAsString( string value )
    {
        if (!NativeConvars.SetMaxValueAsString(Name, value))
        {
            throw new ArgumentException($"Failed to set max value of convar {Name} (type {Type}) to {value}.");
        }
    }

    private string GetMaxValueAsString()
    {
        return NativeConvars.GetMaxValueAsString(Name);
    }

    public void QueryClient( int clientId, Action<string> callback )
    {
        Action? removeSelf = null;
        void nativeCallback( int playerId, nint namePtr, nint valuePtr )
        {
            if (clientId != playerId)
            {
                return;
            }
            var name = Marshal.PtrToStringAnsi(namePtr);

            if (name != Name)
            {
                return;
            }
            var value = Marshal.PtrToStringAnsi(valuePtr)!;

            // var convertedValue = (T)Convert.ChangeType(value, typeof(T))!;
            callback(value);
            removeSelf?.Invoke();
        }

        var callbackPtr = Marshal.GetFunctionPointerForDelegate((ConVarCallbackDelegate)nativeCallback);
        var listenerId = NativeConvars.AddQueryClientCvarCallback(callbackPtr);
        _ = callbacks.AddOrUpdate(listenerId, nativeCallback, ( key, oldValue ) => nativeCallback);

        removeSelf = () =>
        {
            _ = callbacks.TryRemove(listenerId, out _);
            NativeConvars.RemoveQueryClientCvarCallback(listenerId);
        };

        _ = SchedulerManager.QueueOrNow(() => NativeConvars.QueryClientConvar(clientId, Name));
    }

    public void ReplicateToClientAsString( int clientId, string value )
    {
        _ = SchedulerManager.QueueOrNow(() => NativeConvars.SetClientConvarValueString(clientId, Name, value));
    }

    public bool TryGetDefaultValueAsString( out string defaultValue )
    {
        defaultValue = string.Empty;

        if (!HasDefaultValue)
        {
            return false;
        }

        defaultValue = GetDefaultValueAsString();
        return true;
    }

    public bool TryGetMinValueAsString( out string minValue )
    {
        minValue = string.Empty;

        if (!HasMinValue)
        {
            return false;
        }

        minValue = GetMinValueAsString();
        return true;
    }

    public bool TryGetMaxValueAsString( out string maxValue )
    {
        maxValue = string.Empty;

        if (!HasMaxValue)
        {
            return false;
        }

        maxValue = GetMaxValueAsString();
        return true;
    }
}

internal class ConVar<T> : ConVar, IConVar<T>
{
    private bool IsValidType => Type > EConVarType.EConVarType_Invalid && Type < EConVarType.EConVarType_MAX;

    // im not sure
    private bool IsMinMaxType => IsValidType && Type != EConVarType.EConVarType_String && Type != EConVarType.EConVarType_Color;

    public T MinValue {
        get => GetMinValue();
        set => SetMinValue(value);
    }

    public T MaxValue {
        get => GetMaxValue();
        set => SetMaxValue(value);
    }

    public T DefaultValue {
        get => GetDefaultValue();
        set => SetDefaultValue(value);
    }

    internal ConVar( string name ) : base(name)
    {
        ValidateType();
    }

    public void ValidateType()
    {
        if (
            (typeof(T) == typeof(bool) && Type != EConVarType.EConVarType_Bool) ||
            (typeof(T) == typeof(short) && Type != EConVarType.EConVarType_Int16) ||
            (typeof(T) == typeof(ushort) && Type != EConVarType.EConVarType_UInt16) ||
            (typeof(T) == typeof(int) && Type != EConVarType.EConVarType_Int32) ||
            (typeof(T) == typeof(uint) && Type != EConVarType.EConVarType_UInt32) ||
            (typeof(T) == typeof(float) && Type != EConVarType.EConVarType_Float32) ||
            (typeof(T) == typeof(long) && Type != EConVarType.EConVarType_Int64) ||
            (typeof(T) == typeof(ulong) && Type != EConVarType.EConVarType_UInt64) ||
            (typeof(T) == typeof(double) && Type != EConVarType.EConVarType_Float64) ||
            (typeof(T) == typeof(Color) && Type != EConVarType.EConVarType_Color) ||
            (typeof(T) == typeof(QAngle) && Type != EConVarType.EConVarType_Qangle) ||
            (typeof(T) == typeof(Vector) && Type != EConVarType.EConVarType_Vector3) ||
            (typeof(T) == typeof(Vector2D) && Type != EConVarType.EConVarType_Vector2) ||
            (typeof(T) == typeof(Vector4D) && Type != EConVarType.EConVarType_Vector4) ||
            (typeof(T) == typeof(string) && Type != EConVarType.EConVarType_String)
        )
        {
            throw new Exception($"Type mismatch for convar {Name}. The real type is {Type}.");
        }
    }

    public T Value {
        get => GetValue();
        set => SetValue(value);
    }

    public void ReplicateToClient( int clientId, T value )
    {
        var val = string.Empty;
        if (value is bool boolValue)
        {
            val = boolValue ? "1" : "0";
        }
        else if (value is short shortValue)
        {
            val = shortValue.ToString();
        }
        else if (value is ushort ushortValue)
        {
            val = ushortValue.ToString();
        }
        else if (value is int intValue)
        {
            val = intValue.ToString();
        }
        else if (value is uint uintValue)
        {
            val = uintValue.ToString();
        }
        else if (value is float floatValue)
        {
            val = floatValue.ToString();
        }
        else if (value is long longValue)
        {
            val = longValue.ToString();
        }
        else if (value is ulong ulongValue)
        {
            val = ulongValue.ToString();
        }
        else if (value is double doubleValue)
        {
            val = doubleValue.ToString();
        }
        else if (value is Color colorValue)
        {
            val = $"{colorValue.R},{colorValue.G},{colorValue.B}";
        }
        else if (value is QAngle qAngleValue)
        {
            val = $"{qAngleValue.Pitch},{qAngleValue.Yaw},{qAngleValue.Roll}";
        }
        else if (value is Vector vectorValue)
        {
            val = $"{vectorValue.X},{vectorValue.Y},{vectorValue.Z}";
        }
        else if (value is Vector2D vector2DValue)
        {
            val = $"{vector2DValue.X},{vector2DValue.Y}";
        }
        else if (value is Vector4D vector4DValue)
        {
            val = $"{vector4DValue.X},{vector4DValue.Y},{vector4DValue.Z},{vector4DValue.W}";
        }
        else if (value is string stringValue)
        {
            val = stringValue;
        }
        else
        {
            throw new ArgumentException($"Invalid type {typeof(T).Name}");
        }

        _ = SchedulerManager.QueueOrNow(() => NativeConvars.SetClientConvarValueString(clientId, Name, val));
    }


    public T GetValue()
    {
        unsafe
        {
            return Type != EConVarType.EConVarType_String ? *(T*)NativeConvars.GetValuePtr(Name) : (T)(object)(*(CUtlString*)NativeConvars.GetValuePtr(Name)).Value;
        }
    }

    public void SetValue( T value )
    {
        unsafe
        {
            if (Type != EConVarType.EConVarType_String)
            {
                NativeConvars.SetValuePtr(Name, (nint)(&value));
            }
            else
            {

                CUtlString str = new() { Value = (string)(object)value };
                NativeConvars.SetValuePtr(Name, (nint)(&str));
            }
        }
    }


    public void SetInternal( T value )
    {
        unsafe
        {
            if (Type != EConVarType.EConVarType_String)
            {
                NativeConvars.SetValueInternalPtr(Name, (nint)(&value));
            }
            else
            {
                CUtlString str = new() { Value = (string)(object)value };
                NativeConvars.SetValueInternalPtr(Name, (nint)(&str));
            }
        }
    }


    public T GetMinValue()
    {
        if (!IsMinMaxType)
        {
            throw new Exception($"Convar {Name} is not a min/max type.");
        }

        if (!HasMinValue)
        {
            throw new Exception($"Convar {Name} doesn't have a min value.");
        }

        unsafe
        {
            return **(T**)MinValuePtrPtr;
        }
    }

    public T GetMaxValue()
    {
        if (!IsMinMaxType)
        {
            throw new Exception($"Convar {Name} is not a min/max type.");
        }

        if (!HasMaxValue)
        {
            throw new Exception($"Convar {Name} doesn't have a max value.");
        }

        unsafe
        {
            return **(T**)MaxValuePtrPtr;
        }
    }

    public void SetMinValue( T minValue )
    {
        if (!IsMinMaxType)
        {
            throw new Exception($"Convar {Name} is not a min/max type.");
        }

        unsafe
        {
            if (MinValuePtrPtr.Read<nint>() == nint.Zero)
            {
                MinValuePtrPtr.Write(NativeAllocator.Alloc(16));
            }

            **(T**)MinValuePtrPtr = minValue;
        }
    }

    public void SetMaxValue( T maxValue )
    {
        if (!IsMinMaxType)
        {
            throw new Exception($"Convar {Name} is not a min/max type.");
        }

        unsafe
        {
            if (MaxValuePtrPtr.Read<nint>() == nint.Zero)
            {
                MaxValuePtrPtr.Write(NativeAllocator.Alloc(16));
            }

            **(T**)MaxValuePtrPtr = maxValue;
        }
    }

    public T GetDefaultValue()
    {
        unsafe
        {
            var ptr = NativeConvars.GetDefaultValuePtr(Name);
            if (ptr == nint.Zero)
            {
                throw new Exception($"Convar {Name} doesn't have a default value.");
            }

            return Type != EConVarType.EConVarType_String ? *(T*)ptr : (T)(object)(*(CUtlString*)ptr).Value;
        }
    }

    public void SetDefaultValue( T defaultValue )
    {
        unsafe
        {
            var ptr = NativeConvars.GetDefaultValuePtr(Name);
            if (ptr == nint.Zero)
            {
                throw new Exception($"Convar {Name} doesn't have a default value.");
            }

            if (Type != EConVarType.EConVarType_String)
            {
                *(T*)NativeConvars.GetDefaultValuePtr(Name) = defaultValue;
            }
            else
            {
                NativeConvars.GetDefaultValuePtr(Name).Write(StringPool.Allocate((string)(object)defaultValue));
            }
        }
    }

    public bool TryGetMinValue( out T minValue )
    {
        if (!IsMinMaxType)
        {
            minValue = default;
            return false;
        }

        if (!HasMinValue)
        {
            minValue = default;
            return false;
        }

        minValue = GetMinValue();
        return true;
    }

    public bool TryGetMaxValue( out T maxValue )
    {
        if (!IsMinMaxType)
        {
            maxValue = default;
            return false;
        }

        if (!HasMaxValue)
        {
            maxValue = default;
            return false;
        }

        maxValue = GetMaxValue();
        return true;
    }

    public bool TryGetDefaultValue( out T defaultValue )
    {
        if (!HasDefaultValue)
        {

            defaultValue = default;
            return false;
        }

        defaultValue = GetDefaultValue();
        return true;
    }
}
#pragma warning restore CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning restore CS8604 // Possible null reference argument.