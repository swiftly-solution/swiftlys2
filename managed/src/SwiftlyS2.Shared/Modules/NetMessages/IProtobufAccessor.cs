using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Shared.NetMessages;

public interface IProtobufAccessor : INativeHandle
{
    public bool HasField( string fieldName );
    public void SetBool( string fieldName, bool value );
    public void AddBool( string fieldName, bool value );
    public void SetRepeatedBool( string fieldName, int index, bool value );
    public bool GetRepeatedBool( string fieldName, int index );
    public bool GetBool( string fieldName );

    public void SetInt32( string fieldName, int value );
    public void AddInt32( string fieldName, int value );
    public void SetRepeatedInt32( string fieldName, int index, int value );
    public int GetRepeatedInt32( string fieldName, int index );
    public int GetInt32( string fieldName );

    public void SetUInt32( string fieldName, uint value );
    public void AddUInt32( string fieldName, uint value );
    public void SetRepeatedUInt32( string fieldName, int index, uint value );
    public uint GetRepeatedUInt32( string fieldName, int index );
    public uint GetUInt32( string fieldName );

    public void SetInt64( string fieldName, long value );
    public void AddInt64( string fieldName, long value );
    public void SetRepeatedInt64( string fieldName, int index, long value );
    public long GetRepeatedInt64( string fieldName, int index );
    public long GetInt64( string fieldName );

    public void SetUInt64( string fieldName, ulong value );
    public void AddUInt64( string fieldName, ulong value );
    public void SetRepeatedUInt64( string fieldName, int index, ulong value );
    public ulong GetRepeatedUInt64( string fieldName, int index );
    public ulong GetUInt64( string fieldName );

    public void SetFloat( string fieldName, float value );
    public void AddFloat( string fieldName, float value );
    public void SetRepeatedFloat( string fieldName, int index, float value );
    public float GetRepeatedFloat( string fieldName, int index );
    public float GetFloat( string fieldName );

    public void SetDouble( string fieldName, double value );
    public void AddDouble( string fieldName, double value );
    public void SetRepeatedDouble( string fieldName, int index, double value );
    public double GetRepeatedDouble( string fieldName, int index );
    public double GetDouble( string fieldName );

    public void SetString( string fieldName, string value );
    public void AddString( string fieldName, string value );
    public void SetRepeatedString( string fieldName, int index, string value );
    public string GetRepeatedString( string fieldName, int index );
    public string GetString( string fieldName );

    public void SetBytes( string fieldName, byte[] value );
    public void AddBytes( string fieldName, byte[] value );
    public void SetRepeatedBytes( string fieldName, int index, byte[] value );
    public byte[] GetRepeatedBytes( string fieldName, int index );
    public byte[] GetBytes( string fieldName );

    public void SetVector2D( string fieldName, Vector2D value );
    public void AddVector2D( string fieldName, Vector2D value );
    public void SetRepeatedVector2D( string fieldName, int index, Vector2D value );
    public Vector2D GetRepeatedVector2D( string fieldName, int index );
    public Vector2D GetVector2D( string fieldName );

    public void SetVector( string fieldName, Vector value );
    public void AddVector( string fieldName, Vector value );
    public void SetRepeatedVector( string fieldName, int index, Vector value );
    public Vector GetRepeatedVector( string fieldName, int index );
    public Vector GetVector( string fieldName );

    public void SetColor( string fieldName, Color value );
    public void AddColor( string fieldName, Color value );
    public void SetRepeatedColor( string fieldName, int index, Color value );
    public Color GetRepeatedColor( string fieldName, int index );
    public Color GetColor( string fieldName );

    public void SetQAngle( string fieldName, QAngle value );
    public void AddQAngle( string fieldName, QAngle value );
    public void SetRepeatedQAngle( string fieldName, int index, QAngle value );
    public QAngle GetRepeatedQAngle( string fieldName, int index );
    public QAngle GetQAngle( string fieldName );

    public unsafe nint GetNestedMessage( string fieldName );
    public unsafe nint GetRepeatedNestedMessage( string fieldName, int index );
    public unsafe nint AddNestedMessage( string fieldName );

    public int GetRepeatedFieldSize( string fieldName );
    public void ClearRepeatedField( string fieldName );
    public void Clear();

    public void Set<T>( string fieldName, T value );
    public void Add<T>( string fieldName, T value );
    public void SetRepeated<T>( string fieldName, int index, T value );
    public T GetRepeated<T>( string fieldName, int index );
    public T Get<T>( string fieldName );
}