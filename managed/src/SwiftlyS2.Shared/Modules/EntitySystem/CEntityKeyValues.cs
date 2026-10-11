using System.Runtime.CompilerServices;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Shared.EntitySystem;

internal enum CEntityKeyValuesValueKind
{
  Bool,
  Int32,
  UInt32,
  Int64,
  UInt64,
  Float,
  Double,
  Ptr,
  StringToken,
  Color,
  Vector,
  Vector2D,
  Vector4D,
  QAngle
}

public class CEntityKeyValues : IDisposable
{

  private CEntityKeyValuesSafeHandle _handle;

  public CEntityKeyValues()
  {
    _handle = new CEntityKeyValuesSafeHandle(NativeCEntityKeyValues.Allocate());
  }

  public void Dispose()
  {
    _handle.Dispose();
  }

  public nint Address => _handle.Address;

  private unsafe T Read<T>( string key, CEntityKeyValuesValueKind kind ) where T : unmanaged
  {
    T value = default;
    NativeCEntityKeyValues.GetValue(Address, key, (int)kind, (nint)(&value));
    return value;
  }

  private unsafe void Write<T>( string key, CEntityKeyValuesValueKind kind, T value ) where T : unmanaged
  {
    NativeCEntityKeyValues.SetValue(Address, key, (int)kind, (nint)(&value));
  }

  public void SetBool( string key, bool value )
  {
    Write(key, CEntityKeyValuesValueKind.Bool, value);
  }

  public void SetInt32( string key, int value )
  {
    Write(key, CEntityKeyValuesValueKind.Int32, value);
  }

  public void SetUInt32( string key, uint value )
  {
    Write(key, CEntityKeyValuesValueKind.UInt32, value);
  }

  public void SetInt64( string key, long value )
  {
    Write(key, CEntityKeyValuesValueKind.Int64, value);
  }

  public void SetUInt64( string key, ulong value )
  {
    Write(key, CEntityKeyValuesValueKind.UInt64, value);
  }

  public void SetFloat( string key, float value )
  {
    Write(key, CEntityKeyValuesValueKind.Float, value);
  }

  public void SetDouble( string key, double value )
  {
    Write(key, CEntityKeyValuesValueKind.Double, value);
  }

  public void SetString( string key, string value )
  {
    NativeCEntityKeyValues.SetString(Address, key, value);
  }

  public void SetPtr( string key, nint value )
  {
    Write(key, CEntityKeyValuesValueKind.Ptr, value);
  }

  public void SetStringToken( string key, CUtlStringToken value )
  {
    Write(key, CEntityKeyValuesValueKind.StringToken, value);
  }

  public void SetColor( string key, Color value )
  {
    Write(key, CEntityKeyValuesValueKind.Color, value);
  }

  public void SetVector( string key, Vector value )
  {
    Write(key, CEntityKeyValuesValueKind.Vector, value);
  }

  public void SetVector2D( string key, Vector2D value )
  {
    Write(key, CEntityKeyValuesValueKind.Vector2D, value);
  }

  public void SetVector4D( string key, Vector4D value )
  {
    Write(key, CEntityKeyValuesValueKind.Vector4D, value);
  }

  public void SetQAngle( string key, QAngle value )
  {
    Write(key, CEntityKeyValuesValueKind.QAngle, value);
  }

  public bool GetBool( string key )
  {
    return Read<bool>(key, CEntityKeyValuesValueKind.Bool);
  }

  public int GetInt32( string key )
  {
    return Read<int>(key, CEntityKeyValuesValueKind.Int32);
  }

  public uint GetUInt32( string key )
  {
    return Read<uint>(key, CEntityKeyValuesValueKind.UInt32);
  }

  public long GetInt64( string key )
  {
    return Read<long>(key, CEntityKeyValuesValueKind.Int64);
  }

  public ulong GetUInt64( string key )
  {
    return Read<ulong>(key, CEntityKeyValuesValueKind.UInt64);
  }

  public float GetFloat( string key )
  {
    return Read<float>(key, CEntityKeyValuesValueKind.Float);
  }

  public double GetDouble( string key )
  {
    return Read<double>(key, CEntityKeyValuesValueKind.Double);
  }

  public string GetString( string key )
  {
    return NativeCEntityKeyValues.GetString(Address, key);
  }

  public nint GetPtr( string key )
  {
    return Read<nint>(key, CEntityKeyValuesValueKind.Ptr);
  }

  public CUtlStringToken GetStringToken( string key )
  {
    return Read<CUtlStringToken>(key, CEntityKeyValuesValueKind.StringToken);
  }

  public Color GetColor( string key )
  {
    return Read<Color>(key, CEntityKeyValuesValueKind.Color);
  }

  public Vector GetVector( string key )
  {
    return Read<Vector>(key, CEntityKeyValuesValueKind.Vector);
  }

  public Vector2D GetVector2D( string key )
  {
    return Read<Vector2D>(key, CEntityKeyValuesValueKind.Vector2D);
  }

  public Vector4D GetVector4D( string key )
  {
    return Read<Vector4D>(key, CEntityKeyValuesValueKind.Vector4D);
  }

  public QAngle GetQAngle( string key )
  {
    return Read<QAngle>(key, CEntityKeyValuesValueKind.QAngle);
  }

  public void Set<T>( string key, T value )
  {
    if (value is bool boolValue)
    {
      SetBool(key, boolValue);
    }
    else if (value is int intValue)
    {
      SetInt32(key, intValue);
    }
    else if (value is uint uintValue)
    {
      SetUInt32(key, uintValue);
    }
    else if (value is long longValue)
    {
      SetInt64(key, longValue);
    }
    else if (value is ulong ulongValue)
    {
      SetUInt64(key, ulongValue);
    }
    else if (value is float floatValue)
    {
      SetFloat(key, floatValue);
    }
    else if (value is double doubleValue)
    {
      SetDouble(key, doubleValue);
    }
    else if (value is string stringValue)
    {
      SetString(key, stringValue);
    }
    else if (value is nint ptrValue)
    {
      SetPtr(key, ptrValue);
    }
    else if (value is CUtlStringToken stringTokenValue)
    {
      SetStringToken(key, stringTokenValue);
    }
    else if (value is Color colorValue)
    {
      SetColor(key, colorValue);
    }
    else if (value is Vector vectorValue)
    {
      SetVector(key, vectorValue);
    }
    else if (value is Vector2D vector2DValue)
    {
      SetVector2D(key, vector2DValue);
    }
    else if (value is Vector4D vector4DValue)
    {
      SetVector4D(key, vector4DValue);
    }
    else if (value is QAngle qAngleValue)
    {
      SetQAngle(key, qAngleValue);
    }
    else
    {
      throw new InvalidOperationException($"Unsupported type: {typeof(T).Name}");
    }
  }

  public T Get<T>( string key )
  {
    if (typeof(T) == typeof(bool))
    {
      return (T)(object)GetBool(key);
    }
    else if (typeof(T) == typeof(int))
    {
      return (T)(object)GetInt32(key);
    }
    else if (typeof(T) == typeof(uint))
    {
      return (T)(object)GetUInt32(key);
    }
    else if (typeof(T) == typeof(long))
    {
      return (T)(object)GetInt64(key);
    }
    else if (typeof(T) == typeof(ulong))
    {
      return (T)(object)GetUInt64(key);
    }
    else if (typeof(T) == typeof(float))
    {
      return (T)(object)GetFloat(key);
    }
    else if (typeof(T) == typeof(double))
    {
      return (T)(object)GetDouble(key);
    }
    else if (typeof(T) == typeof(string))
    {
      return (T)(object)GetString(key);
    }
    else if (typeof(T) == typeof(nint))
    {
      return (T)(object)GetPtr(key);
    }
    else if (typeof(T) == typeof(CUtlStringToken))
    {
      return (T)(object)GetStringToken(key);
    }
    else if (typeof(T) == typeof(Color))
    {
      return (T)(object)GetColor(key);
    }
    else if (typeof(T) == typeof(Vector))
    {
      return (T)(object)GetVector(key);
    }
    else if (typeof(T) == typeof(Vector2D))
    {
      return (T)(object)GetVector2D(key);
    }
    else if (typeof(T) == typeof(Vector4D))
    {
      return (T)(object)GetVector4D(key);
    }
    else if (typeof(T) == typeof(QAngle))
    {
      return (T)(object)GetQAngle(key);
    }
    else
    {
      throw new InvalidOperationException($"Unsupported type: {typeof(T).Name}");
    }
  }

  public bool Has( string key )
  {
    return NativeCEntityKeyValues.HasKey(Address, key);
  }
}
