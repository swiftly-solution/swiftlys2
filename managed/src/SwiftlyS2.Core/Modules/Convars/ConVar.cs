using System.Globalization;
using System.Runtime.CompilerServices;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Scheduler;
using SwiftlyS2.Shared.Convars;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Convars;

internal class ConVar : IConVar
{
    private readonly IConVarService conVarService;

    private nint minValuePtrPtr;
    private nint maxValuePtrPtr;

    protected nint MinValuePtrPtr => minValuePtrPtr != 0 ? minValuePtrPtr : (minValuePtrPtr = NativeConvars.GetMinValuePtrPtr(Name));
    protected nint MaxValuePtrPtr => maxValuePtrPtr != 0 ? maxValuePtrPtr : (maxValuePtrPtr = NativeConvars.GetMaxValuePtrPtr(Name));

    public EConVarType Type { get; }
    public nint ValuePtr { get; }
    public string Name { get; set; }
    public string HelpText => NativeConvars.GetDescription(Name);
    public bool HasDefaultValue => NativeConvars.HasDefaultValue(Name);
    public bool HasMinValue => MinValuePtrPtr.Read<nint>() != 0;
    public bool HasMaxValue => MaxValuePtrPtr.Read<nint>() != 0;

    public string ValueAsString {
        get => NativeConvars.GetValueAsString(Name);
        set => EnsureSet(NativeConvars.SetValueAsString(Name, value), "value", value);
    }

    public string MaxValueAsString {
        get => GetMaxValueAsString();
        set => EnsureSet(NativeConvars.SetMaxValueAsString(Name, value), "max value", value);
    }

    public string MinValueAsString {
        get => GetMinValueAsString();
        set => EnsureSet(NativeConvars.SetMinValueAsString(Name, value), "min value", value);
    }

    public string DefaultValueAsString {
        get => GetDefaultValueAsString();
        set => EnsureSet(NativeConvars.SetDefaultValueAsString(Name, value), "default value", value);
    }

    public ConvarFlags Flags {
        get => (ConvarFlags)NativeConvars.GetFlags(Name);
        set => NativeConvars.SetFlags(Name, (ulong)value);
    }

    internal ConVar( string name, IConVarService conVarService )
    {
        Name = name;
        Type = (EConVarType)NativeConvars.GetConvarType(Name);

        if (Type == EConVarType.EConVarType_Invalid)
        {
            throw new Exception($"Convar {Name} is of invalid type.");
        }

        ValuePtr = NativeConvars.GetValuePtr(Name);
        this.conVarService = conVarService;
    }

    public void SetInternalAsString( string value )
    {
        NativeConvars.SetValueInternalAsString(Name, value);
    }

    public void QueryClient( int clientId, Action<string> callback )
    {
        conVarService.QueryClient(clientId, Name, callback);
    }

    public void ReplicateToClientAsString( int clientId, string value )
    {
        conVarService.ReplicateToClient(clientId, Name, value);
    }

    public bool TryGetDefaultValueAsString( out string defaultValue )
    {
        var hasDefaultValue = HasDefaultValue;
        defaultValue = hasDefaultValue ? GetDefaultValueAsString() : string.Empty;
        return hasDefaultValue;
    }

    public bool TryGetMinValueAsString( out string minValue )
    {
        var hasMinValue = HasMinValue;
        minValue = hasMinValue ? GetMinValueAsString() : string.Empty;
        return hasMinValue;
    }

    public bool TryGetMaxValueAsString( out string maxValue )
    {
        var hasMaxValue = HasMaxValue;
        maxValue = hasMaxValue ? GetMaxValueAsString() : string.Empty;
        return hasMaxValue;
    }

    private string GetDefaultValueAsString() => NativeConvars.GetDefaultValueAsString(Name);

    private string GetMinValueAsString() => NativeConvars.GetMinValueAsString(Name);

    private string GetMaxValueAsString() => NativeConvars.GetMaxValueAsString(Name);

    private void EnsureSet( bool succeeded, string what, string value )
    {
        if (!succeeded)
        {
            throw new ArgumentException($"Failed to set {what} of convar {Name} (type {Type}) to {value}.");
        }
    }
}

internal class ConVar<T> : ConVar, IConVar<T>
{
    private bool IsValidType => Type is > EConVarType.EConVarType_Invalid and < EConVarType.EConVarType_MAX;

    // im not sure
    private bool IsMinMaxType => IsValidType && Type is not (EConVarType.EConVarType_String or EConVarType.EConVarType_Color);

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

    public T Value {
        get => GetValue();
        set => SetValue(value);
    }

    internal ConVar( string name, IConVarService conVarService ) : base(name, conVarService)
    {
        ValidateType();
    }

    public void ValidateType()
    {
        if (!IsCompatibleWith(Type))
        {
            throw new Exception($"Type mismatch for convar {Name}. The real type is {Type}.");
        }
    }

    public void ReplicateToClient( int clientId, T value )
    {
        var text = value switch {
            bool v => v ? "1" : "0",
            short v => Format(v),
            ushort v => Format(v),
            int v => Format(v),
            uint v => Format(v),
            float v => Format(v),
            long v => Format(v),
            ulong v => Format(v),
            double v => Format(v),
            Color v => $"{v.R},{v.G},{v.B}",
            QAngle v => string.Create(CultureInfo.InvariantCulture, $"{v.Pitch},{v.Yaw},{v.Roll}"),
            Vector v => string.Create(CultureInfo.InvariantCulture, $"{v.X},{v.Y},{v.Z}"),
            Vector2D v => string.Create(CultureInfo.InvariantCulture, $"{v.X},{v.Y}"),
            Vector4D v => string.Create(CultureInfo.InvariantCulture, $"{v.X},{v.Y},{v.Z},{v.W}"),
            string v => v,
            _ => throw new ArgumentException($"Invalid type {typeof(T).Name}")
        };

        _ = SchedulerManager.QueueOrNow(() => ReplicateToClientAsString(clientId, text));
    }

    public unsafe T GetValue()
    {
        return Type != EConVarType.EConVarType_String ? Unsafe.Read<T>((void*)ValuePtr) : (T)(object)ValueAsString;
    }

    public unsafe void SetValue( T value )
    {
        if (Type != EConVarType.EConVarType_String)
        {
            NativeConvars.SetValuePtr(Name, (nint)Unsafe.AsPointer(ref value));
        }
        else if (value is string text)
        {
            CUtlString str = new() { Value = text };
            NativeConvars.SetValuePtr(Name, (nint)(&str));
        }
    }

    public unsafe void SetInternal( T value )
    {
        if (Type != EConVarType.EConVarType_String)
        {
            NativeConvars.SetValueInternalPtr(Name, (nint)Unsafe.AsPointer(ref value));
        }
        else
        {
            CUtlString str = new() { Value = (string)(object)value! };
            NativeConvars.SetValueInternalPtr(Name, (nint)(&str));
        }
    }

    public T GetMinValue()
    {
        EnsureMinMaxType();

        if (!HasMinValue)
        {
            throw new InvalidOperationException($"Convar {Name} doesn't have a min value.");
        }

        return ReadThroughPointer(MinValuePtrPtr);
    }

    public T GetMaxValue()
    {
        EnsureMinMaxType();

        if (!HasMaxValue)
        {
            throw new InvalidOperationException($"Convar {Name} doesn't have a max value.");
        }

        return ReadThroughPointer(MaxValuePtrPtr);
    }

    public void SetMinValue( T minValue )
    {
        EnsureMinMaxType();
        WriteThroughPointer(MinValuePtrPtr, minValue);
    }

    public void SetMaxValue( T maxValue )
    {
        EnsureMinMaxType();
        WriteThroughPointer(MaxValuePtrPtr, maxValue);
    }

    public unsafe T GetDefaultValue()
    {
        var ptr = GetDefaultValuePtr();

        return Type != EConVarType.EConVarType_String ? Unsafe.Read<T>((void*)ptr) : (T)(object)(*(CUtlString*)ptr).Value;
    }

    public unsafe void SetDefaultValue( T defaultValue )
    {
        var ptr = GetDefaultValuePtr();

        if (Type != EConVarType.EConVarType_String)
        {
            Unsafe.Write((void*)ptr, defaultValue);
        }
        else
        {
            ptr.Write(StringPool.Allocate((string)(object)defaultValue!));
        }
    }

    public bool TryGetMinValue( out T minValue )
    {
        if (IsMinMaxType && HasMinValue)
        {
            minValue = GetMinValue();
            return true;
        }

        minValue = default!;
        return false;
    }

    public bool TryGetMaxValue( out T maxValue )
    {
        if (IsMinMaxType && HasMaxValue)
        {
            maxValue = GetMaxValue();
            return true;
        }

        maxValue = default!;
        return false;
    }

    public bool TryGetDefaultValue( out T defaultValue )
    {
        if (HasDefaultValue)
        {
            defaultValue = GetDefaultValue();
            return true;
        }

        defaultValue = default!;
        return false;
    }

    private static bool IsCompatibleWith( EConVarType type )
    {
        if (typeof(T) == typeof(bool)) return type == EConVarType.EConVarType_Bool;
        if (typeof(T) == typeof(short)) return type == EConVarType.EConVarType_Int16;
        if (typeof(T) == typeof(ushort)) return type == EConVarType.EConVarType_UInt16;
        if (typeof(T) == typeof(int)) return type == EConVarType.EConVarType_Int32;
        if (typeof(T) == typeof(uint)) return type == EConVarType.EConVarType_UInt32;
        if (typeof(T) == typeof(float)) return type == EConVarType.EConVarType_Float32;
        if (typeof(T) == typeof(long)) return type == EConVarType.EConVarType_Int64;
        if (typeof(T) == typeof(ulong)) return type == EConVarType.EConVarType_UInt64;
        if (typeof(T) == typeof(double)) return type == EConVarType.EConVarType_Float64;
        if (typeof(T) == typeof(Color)) return type == EConVarType.EConVarType_Color;
        if (typeof(T) == typeof(QAngle)) return type == EConVarType.EConVarType_Qangle;
        if (typeof(T) == typeof(Vector)) return type is EConVarType.EConVarType_Vector3 or EConVarType.EConVarType_VectorWS;
        if (typeof(T) == typeof(Vector2D)) return type == EConVarType.EConVarType_Vector2;
        if (typeof(T) == typeof(Vector4D)) return type == EConVarType.EConVarType_Vector4;
        if (typeof(T) == typeof(string)) return type == EConVarType.EConVarType_String;

        return true;
    }

    private static string Format<TNumber>( TNumber value ) where TNumber : IFormattable =>
        value.ToString(null, CultureInfo.InvariantCulture);

    private void EnsureMinMaxType()
    {
        if (!IsMinMaxType)
        {
            throw new InvalidOperationException($"Convar {Name} is not a min/max type.");
        }
    }

    private nint GetDefaultValuePtr()
    {
        var ptr = NativeConvars.GetDefaultValuePtr(Name);
        return ptr != nint.Zero ? ptr : throw new Exception($"Convar {Name} doesn't have a default value.");
    }

    private static unsafe T ReadThroughPointer( nint pointerAddress ) =>
        Unsafe.Read<T>((void*)pointerAddress.Read<nint>());

    private static unsafe void WriteThroughPointer( nint pointerAddress, T value )
    {
        if (pointerAddress.Read<nint>() == nint.Zero)
        {
            pointerAddress.Write(NativeAllocator.Alloc(16));
        }

        Unsafe.Write((void*)pointerAddress.Read<nint>(), value);
    }
}
