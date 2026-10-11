using System.Runtime.CompilerServices;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Natives.NativeObjects;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Core.NetMessages;

internal enum ProtobufValueKind
{
    Int32,
    Int64,
    UInt32,
    UInt64,
    Bool,
    Float,
    Double,
    Vector2D,
    Vector,
    Color,
    QAngle
}

internal class ProtobufAccessor : NativeHandle, IProtobufAccessor
{
    private const int NotRepeated = -1;

    public ProtobufAccessor( nint handle ) : base(handle)
    {
    }

    public bool HasField( string fieldName ) => NativeNetMessages.HasField(Address, fieldName);

    public bool GetBool( string fieldName ) => Read<bool>(fieldName, NotRepeated, ProtobufValueKind.Bool);
    public bool GetRepeatedBool( string fieldName, int index ) => Read<bool>(fieldName, index, ProtobufValueKind.Bool);
    public void SetBool( string fieldName, bool value ) => Write(fieldName, NotRepeated, ProtobufValueKind.Bool, value);
    public void SetRepeatedBool( string fieldName, int index, bool value ) => Write(fieldName, index, ProtobufValueKind.Bool, value);
    public void AddBool( string fieldName, bool value ) => Append(fieldName, ProtobufValueKind.Bool, value);

    public int GetInt32( string fieldName ) => Read<int>(fieldName, NotRepeated, ProtobufValueKind.Int32);
    public int GetRepeatedInt32( string fieldName, int index ) => Read<int>(fieldName, index, ProtobufValueKind.Int32);
    public void SetInt32( string fieldName, int value ) => Write(fieldName, NotRepeated, ProtobufValueKind.Int32, value);
    public void SetRepeatedInt32( string fieldName, int index, int value ) => Write(fieldName, index, ProtobufValueKind.Int32, value);
    public void AddInt32( string fieldName, int value ) => Append(fieldName, ProtobufValueKind.Int32, value);

    public uint GetUInt32( string fieldName ) => Read<uint>(fieldName, NotRepeated, ProtobufValueKind.UInt32);
    public uint GetRepeatedUInt32( string fieldName, int index ) => Read<uint>(fieldName, index, ProtobufValueKind.UInt32);
    public void SetUInt32( string fieldName, uint value ) => Write(fieldName, NotRepeated, ProtobufValueKind.UInt32, value);
    public void SetRepeatedUInt32( string fieldName, int index, uint value ) => Write(fieldName, index, ProtobufValueKind.UInt32, value);
    public void AddUInt32( string fieldName, uint value ) => Append(fieldName, ProtobufValueKind.UInt32, value);

    public long GetInt64( string fieldName ) => Read<long>(fieldName, NotRepeated, ProtobufValueKind.Int64);
    public long GetRepeatedInt64( string fieldName, int index ) => Read<long>(fieldName, index, ProtobufValueKind.Int64);
    public void SetInt64( string fieldName, long value ) => Write(fieldName, NotRepeated, ProtobufValueKind.Int64, value);
    public void SetRepeatedInt64( string fieldName, int index, long value ) => Write(fieldName, index, ProtobufValueKind.Int64, value);
    public void AddInt64( string fieldName, long value ) => Append(fieldName, ProtobufValueKind.Int64, value);

    public ulong GetUInt64( string fieldName ) => Read<ulong>(fieldName, NotRepeated, ProtobufValueKind.UInt64);
    public ulong GetRepeatedUInt64( string fieldName, int index ) => Read<ulong>(fieldName, index, ProtobufValueKind.UInt64);
    public void SetUInt64( string fieldName, ulong value ) => Write(fieldName, NotRepeated, ProtobufValueKind.UInt64, value);
    public void SetRepeatedUInt64( string fieldName, int index, ulong value ) => Write(fieldName, index, ProtobufValueKind.UInt64, value);
    public void AddUInt64( string fieldName, ulong value ) => Append(fieldName, ProtobufValueKind.UInt64, value);

    public float GetFloat( string fieldName ) => Read<float>(fieldName, NotRepeated, ProtobufValueKind.Float);
    public float GetRepeatedFloat( string fieldName, int index ) => Read<float>(fieldName, index, ProtobufValueKind.Float);
    public void SetFloat( string fieldName, float value ) => Write(fieldName, NotRepeated, ProtobufValueKind.Float, value);
    public void SetRepeatedFloat( string fieldName, int index, float value ) => Write(fieldName, index, ProtobufValueKind.Float, value);
    public void AddFloat( string fieldName, float value ) => Append(fieldName, ProtobufValueKind.Float, value);

    public double GetDouble( string fieldName ) => Read<double>(fieldName, NotRepeated, ProtobufValueKind.Double);
    public double GetRepeatedDouble( string fieldName, int index ) => Read<double>(fieldName, index, ProtobufValueKind.Double);
    public void SetDouble( string fieldName, double value ) => Write(fieldName, NotRepeated, ProtobufValueKind.Double, value);
    public void SetRepeatedDouble( string fieldName, int index, double value ) => Write(fieldName, index, ProtobufValueKind.Double, value);
    public void AddDouble( string fieldName, double value ) => Append(fieldName, ProtobufValueKind.Double, value);

    public Vector2D GetVector2D( string fieldName ) => Read<Vector2D>(fieldName, NotRepeated, ProtobufValueKind.Vector2D);
    public Vector2D GetRepeatedVector2D( string fieldName, int index ) => Read<Vector2D>(fieldName, index, ProtobufValueKind.Vector2D);
    public void SetVector2D( string fieldName, Vector2D value ) => Write(fieldName, NotRepeated, ProtobufValueKind.Vector2D, value);
    public void SetRepeatedVector2D( string fieldName, int index, Vector2D value ) => Write(fieldName, index, ProtobufValueKind.Vector2D, value);
    public void AddVector2D( string fieldName, Vector2D value ) => Append(fieldName, ProtobufValueKind.Vector2D, value);

    public Vector GetVector( string fieldName ) => Read<Vector>(fieldName, NotRepeated, ProtobufValueKind.Vector);
    public Vector GetRepeatedVector( string fieldName, int index ) => Read<Vector>(fieldName, index, ProtobufValueKind.Vector);
    public void SetVector( string fieldName, Vector value ) => Write(fieldName, NotRepeated, ProtobufValueKind.Vector, value);
    public void SetRepeatedVector( string fieldName, int index, Vector value ) => Write(fieldName, index, ProtobufValueKind.Vector, value);
    public void AddVector( string fieldName, Vector value ) => Append(fieldName, ProtobufValueKind.Vector, value);

    public Color GetColor( string fieldName ) => Read<Color>(fieldName, NotRepeated, ProtobufValueKind.Color);
    public Color GetRepeatedColor( string fieldName, int index ) => Read<Color>(fieldName, index, ProtobufValueKind.Color);
    public void SetColor( string fieldName, Color value ) => Write(fieldName, NotRepeated, ProtobufValueKind.Color, value);
    public void SetRepeatedColor( string fieldName, int index, Color value ) => Write(fieldName, index, ProtobufValueKind.Color, value);
    public void AddColor( string fieldName, Color value ) => Append(fieldName, ProtobufValueKind.Color, value);

    public QAngle GetQAngle( string fieldName ) => Read<QAngle>(fieldName, NotRepeated, ProtobufValueKind.QAngle);
    public QAngle GetRepeatedQAngle( string fieldName, int index ) => Read<QAngle>(fieldName, index, ProtobufValueKind.QAngle);
    public void SetQAngle( string fieldName, QAngle value ) => Write(fieldName, NotRepeated, ProtobufValueKind.QAngle, value);
    public void SetRepeatedQAngle( string fieldName, int index, QAngle value ) => Write(fieldName, index, ProtobufValueKind.QAngle, value);
    public void AddQAngle( string fieldName, QAngle value ) => Append(fieldName, ProtobufValueKind.QAngle, value);

    public string GetString( string fieldName ) => NativeNetMessages.GetString(Address, fieldName, NotRepeated);
    public string GetRepeatedString( string fieldName, int index ) => NativeNetMessages.GetString(Address, fieldName, index);
    public void SetString( string fieldName, string value ) => NativeNetMessages.SetString(Address, fieldName, NotRepeated, value);
    public void SetRepeatedString( string fieldName, int index, string value ) => NativeNetMessages.SetString(Address, fieldName, index, value);
    public void AddString( string fieldName, string value ) => NativeNetMessages.AddString(Address, fieldName, value);

    public byte[] GetBytes( string fieldName ) => NativeNetMessages.GetBytes(Address, fieldName, NotRepeated);
    public byte[] GetRepeatedBytes( string fieldName, int index ) => NativeNetMessages.GetBytes(Address, fieldName, index);
    public void SetBytes( string fieldName, byte[] value ) => NativeNetMessages.SetBytes(Address, fieldName, NotRepeated, value);
    public void SetRepeatedBytes( string fieldName, int index, byte[] value ) => NativeNetMessages.SetBytes(Address, fieldName, index, value);
    public void AddBytes( string fieldName, byte[] value ) => NativeNetMessages.AddBytes(Address, fieldName, value);

    public unsafe nint GetNestedMessage( string fieldName ) => NativeNetMessages.GetNestedMessage(Address, fieldName);
    public unsafe nint GetRepeatedNestedMessage( string fieldName, int index ) => NativeNetMessages.GetRepeatedNestedMessage(Address, fieldName, index);
    public unsafe nint AddNestedMessage( string fieldName ) => NativeNetMessages.AddNestedMessage(Address, fieldName);

    public int GetRepeatedFieldSize( string fieldName ) => NativeNetMessages.GetRepeatedFieldSize(Address, fieldName);

    public void ClearRepeatedField( string fieldName ) => NativeNetMessages.ClearRepeatedField(Address, fieldName);

    public void Clear() => NativeNetMessages.Clear(Address);

    public T Get<T>( string fieldName ) => ReadAny<T>(fieldName, NotRepeated);

    public T GetRepeated<T>( string fieldName, int index ) => ReadAny<T>(fieldName, index);

    public void Set<T>( string fieldName, T value ) => WriteAny(fieldName, NotRepeated, value);

    public void SetRepeated<T>( string fieldName, int index, T value ) => WriteAny(fieldName, index, value);

    public void Add<T>( string fieldName, T value )
    {
        if (value is string text)
        {
            AddString(fieldName, text);
        }
        else if (value is byte[] bytes)
        {
            AddBytes(fieldName, bytes);
        }
        else
        {
            Append(fieldName, KindOf<T>(), value);
        }
    }

    private T ReadAny<T>( string fieldName, int index )
    {
        if (typeof(T) == typeof(string))
        {
            return (T)(object)NativeNetMessages.GetString(Address, fieldName, index);
        }

        if (typeof(T) == typeof(byte[]))
        {
            return (T)(object)NativeNetMessages.GetBytes(Address, fieldName, index);
        }

        return Read<T>(fieldName, index, KindOf<T>());
    }

    private void WriteAny<T>( string fieldName, int index, T value )
    {
        if (value is string text)
        {
            NativeNetMessages.SetString(Address, fieldName, index, text);
        }
        else if (value is byte[] bytes)
        {
            NativeNetMessages.SetBytes(Address, fieldName, index, bytes);
        }
        else
        {
            Write(fieldName, index, KindOf<T>(), value);
        }
    }

    private unsafe T Read<T>( string fieldName, int index, ProtobufValueKind kind )
    {
        T value = default!;
        _ = NativeNetMessages.GetValue(Address, fieldName, index, (int)kind, (nint)Unsafe.AsPointer(ref value));
        return value;
    }

    private unsafe void Write<T>( string fieldName, int index, ProtobufValueKind kind, T value )
    {
        _ = NativeNetMessages.SetValue(Address, fieldName, index, (int)kind, (nint)Unsafe.AsPointer(ref value));
    }

    private unsafe void Append<T>( string fieldName, ProtobufValueKind kind, T value )
    {
        _ = NativeNetMessages.AddValue(Address, fieldName, (int)kind, (nint)Unsafe.AsPointer(ref value));
    }

    private static ProtobufValueKind KindOf<T>()
    {
        if (typeof(T) == typeof(bool)) return ProtobufValueKind.Bool;
        if (typeof(T) == typeof(int)) return ProtobufValueKind.Int32;
        if (typeof(T) == typeof(uint)) return ProtobufValueKind.UInt32;
        if (typeof(T) == typeof(long)) return ProtobufValueKind.Int64;
        if (typeof(T) == typeof(ulong)) return ProtobufValueKind.UInt64;
        if (typeof(T) == typeof(float)) return ProtobufValueKind.Float;
        if (typeof(T) == typeof(double)) return ProtobufValueKind.Double;
        if (typeof(T) == typeof(Vector2D)) return ProtobufValueKind.Vector2D;
        if (typeof(T) == typeof(Vector)) return ProtobufValueKind.Vector;
        if (typeof(T) == typeof(Color)) return ProtobufValueKind.Color;
        if (typeof(T) == typeof(QAngle)) return ProtobufValueKind.QAngle;

        throw new InvalidOperationException($"Invalid type: {typeof(T)}");
    }
}
