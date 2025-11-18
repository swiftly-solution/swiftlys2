using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Convars;
using SwiftlyS2.Shared.Natives;

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
  EConVarType_MAX
};

internal class ConVarService : IConVarService
{

  public IConVar<T>? Find<T>( string name )
  {

    if (!NativeConvars.ExistsConvar(name))
    {
      return null;
    }

    return new ConVar<T>(name);

  }

  public IConVar<T> Create<T>( string name, string helpMessage, T defaultValue, ConvarFlags flags = ConvarFlags.NONE )
  {
    if (NativeConvars.ExistsConvar(name))
    {
      throw new Exception($"Convar {name} already exists.");
    }

    if (defaultValue is bool boolValue)
    {
      NativeConvars.CreateConvarBool(name, (int)EConVarType.EConVarType_Bool, (ulong)flags, helpMessage, boolValue, 0, 0);
    }
    else if (defaultValue is short shortValue)
    {
      NativeConvars.CreateConvarInt16(name, (int)EConVarType.EConVarType_Int16, (ulong)flags, helpMessage, shortValue, 0, 0);
    }
    else if (defaultValue is ushort ushortValue)
    {
      NativeConvars.CreateConvarUInt16(name, (int)EConVarType.EConVarType_UInt16, (ulong)flags, helpMessage, ushortValue, 0, 0);
    }
    else if (defaultValue is int intValue)
    {
      NativeConvars.CreateConvarInt32(name, (int)EConVarType.EConVarType_Int32, (ulong)flags, helpMessage, intValue, 0, 0);
    }
    else if (defaultValue is uint uintValue)
    {
      NativeConvars.CreateConvarUInt32(name, (int)EConVarType.EConVarType_UInt32, (ulong)flags, helpMessage, uintValue, 0, 0);
    }
    else if (defaultValue is long longValue)
    {
      NativeConvars.CreateConvarInt64(name, (int)EConVarType.EConVarType_Int64, (ulong)flags, helpMessage, longValue, 0, 0);
    }
    else if (defaultValue is ulong ulongValue)
    {
      NativeConvars.CreateConvarUInt64(name, (int)EConVarType.EConVarType_UInt64, (ulong)flags, helpMessage, ulongValue, 0, 0);
    }
    else if (defaultValue is float floatValue)
    {
      NativeConvars.CreateConvarFloat(name, (int)EConVarType.EConVarType_Float32, (ulong)flags, helpMessage, floatValue, 0, 0);
    }
    else if (defaultValue is double doubleValue)
    {
      NativeConvars.CreateConvarDouble(name, (int)EConVarType.EConVarType_Float64, (ulong)flags, helpMessage, doubleValue, 0, 0);
    }
    else if (defaultValue is Vector2D vector2Value)
    {
      NativeConvars.CreateConvarVector2D(name, (int)EConVarType.EConVarType_Vector2, (ulong)flags, helpMessage, vector2Value, 0, 0);
    }
    else if (defaultValue is Vector vector3Value)
    {
      NativeConvars.CreateConvarVector(name, (int)EConVarType.EConVarType_Vector3, (ulong)flags, helpMessage, vector3Value, 0, 0);
    }
    else if (defaultValue is Vector4D vector4Value)
    {
      NativeConvars.CreateConvarVector4D(name, (int)EConVarType.EConVarType_Vector4, (ulong)flags, helpMessage, vector4Value, 0, 0);
    }
    else if (defaultValue is QAngle qAngleValue)
    {
      NativeConvars.CreateConvarQAngle(name, (int)EConVarType.EConVarType_Qangle, (ulong)flags, helpMessage, qAngleValue, 0, 0);
    }
    else if (defaultValue is Color colorValue)
    {
      NativeConvars.CreateConvarColor(name, (int)EConVarType.EConVarType_Color, (ulong)flags, helpMessage, colorValue, 0, 0);
    }
    else if (defaultValue is string stringValue)
    {
      NativeConvars.CreateConvarString(name, (int)EConVarType.EConVarType_String, (ulong)flags, helpMessage, stringValue, 0, 0);
    }
    else
    {
      throw new Exception($"Unsupported type {typeof(T)}.");
    }

    return new ConVar<T>(name);
  }

  public IConVar<T> Create<T>( string name, string helpMessage, T defaultValue, T? minValue, T? maxValue, ConvarFlags flags = ConvarFlags.NONE ) where T : unmanaged
  {

    if (NativeConvars.ExistsConvar(name))
    {
      throw new Exception($"Convar {name} already exists.");
    }
    unsafe
    {

      if (defaultValue is short shortValue)
      {
        short* pMin = stackalloc short[1];
        if (minValue.HasValue)
        {
          pMin[0] = (short)(object)minValue.Value;
        }

        short* pMax = stackalloc short[1];
        if (maxValue.HasValue)
        {
          pMax[0] = (short)(object)maxValue.Value;
        }

        NativeConvars.CreateConvarInt16(name, (int)EConVarType.EConVarType_Int16, (ulong)flags, helpMessage, shortValue, minValue.HasValue ? (nint)pMin : 0, maxValue.HasValue ? (nint)pMax : 0);
      }
      else if (defaultValue is ushort ushortValue)
      {
        ushort* pMin = stackalloc ushort[1];
        if (minValue.HasValue)
        {
          pMin[0] = (ushort)(object)minValue.Value;
        }

        ushort* pMax = stackalloc ushort[1];
        if (maxValue.HasValue)
        {
          pMax[0] = (ushort)(object)maxValue.Value;
        }

        NativeConvars.CreateConvarUInt16(name, (int)EConVarType.EConVarType_UInt16, (ulong)flags, helpMessage, ushortValue, minValue.HasValue ? (nint)pMin : 0, maxValue.HasValue ? (nint)pMax : 0);
      }
      else if (defaultValue is int intValue)
      {
        int* pMin = stackalloc int[1];
        if (minValue.HasValue)
        {
          pMin[0] = (int)(object)minValue.Value;
        }

        int* pMax = stackalloc int[1];
        if (maxValue.HasValue)
        {
          pMax[0] = (int)(object)maxValue.Value;
        }

        NativeConvars.CreateConvarInt32(name, (int)EConVarType.EConVarType_Int32, (ulong)flags, helpMessage, intValue, minValue.HasValue ? (nint)pMin : 0, maxValue.HasValue ? (nint)pMax : 0);
      }
      else if (defaultValue is uint uintValue)
      {
        uint* pMin = stackalloc uint[1];
        if (minValue.HasValue)
        {
          pMin[0] = (uint)(object)minValue.Value;
        }

        uint* pMax = stackalloc uint[1];
        if (maxValue.HasValue)
        {
          pMax[0] = (uint)(object)maxValue.Value;
        }

        NativeConvars.CreateConvarUInt32(name, (int)EConVarType.EConVarType_UInt32, (ulong)flags, helpMessage, uintValue, minValue.HasValue ? (nint)pMin : 0, maxValue.HasValue ? (nint)pMax : 0);
      }
      else if (defaultValue is long longValue)
      {
        long* pMin = stackalloc long[1];
        if (minValue.HasValue)
        {
          pMin[0] = (long)(object)minValue.Value;
        }

        long* pMax = stackalloc long[1];
        if (maxValue.HasValue)
        {
          pMax[0] = (long)(object)maxValue.Value;
        }

        NativeConvars.CreateConvarInt64(name, (int)EConVarType.EConVarType_Int64, (ulong)flags, helpMessage, longValue, minValue.HasValue ? (nint)pMin : 0, maxValue.HasValue ? (nint)pMax : 0);
      }
      else if (defaultValue is ulong ulongValue)
      {
        ulong* pMin = stackalloc ulong[1];
        if (minValue.HasValue)
        {
          pMin[0] = (ulong)(object)minValue.Value;
        }

        ulong* pMax = stackalloc ulong[1];
        if (maxValue.HasValue)
        {
          pMax[0] = (ulong)(object)maxValue.Value;
        }

        NativeConvars.CreateConvarUInt64(name, (int)EConVarType.EConVarType_UInt64, (ulong)flags, helpMessage, ulongValue, minValue.HasValue ? (nint)pMin : 0, maxValue.HasValue ? (nint)pMax : 0);
      }
      else if (defaultValue is float floatValue)
      {
        float* pMin = stackalloc float[1];
        if (minValue.HasValue)
        {
          pMin[0] = (float)(object)minValue.Value;
        }

        float* pMax = stackalloc float[1];
        if (maxValue.HasValue)
        {
          pMax[0] = (float)(object)maxValue.Value;
        }

        NativeConvars.CreateConvarFloat(name, (int)EConVarType.EConVarType_Float32, (ulong)flags, helpMessage, floatValue, minValue.HasValue ? (nint)pMin : 0, maxValue.HasValue ? (nint)pMax : 0);
      }
      else if (defaultValue is double doubleValue)
      {
        double* pMin = stackalloc double[1];
        if (minValue.HasValue)
        {
          pMin[0] = (double)(object)minValue.Value;
        }

        double* pMax = stackalloc double[1];
        if (maxValue.HasValue)
        {
          pMax[0] = (double)(object)maxValue.Value;
        }

        NativeConvars.CreateConvarDouble(name, (int)EConVarType.EConVarType_Float64, (ulong)flags, helpMessage, doubleValue, minValue.HasValue ? (nint)pMin : 0, maxValue.HasValue ? (nint)pMax : 0);
      }
      else
      {
        throw new Exception($"You can't assign min and max values to {typeof(T)}.");
      }
    }

    return new ConVar<T>(name);
  }

  public IConVar<T> CreateOrFind<T>( string name, string helpMessage, T defaultValue, ConvarFlags flags = ConvarFlags.NONE )
  {
    return NativeConvars.ExistsConvar(name) ? new ConVar<T>(name) : Create(name, helpMessage, defaultValue, flags);
  }

  public IConVar<T> CreateOrFind<T>( string name, string helpMessage, T defaultValue, T? minValue, T? maxValue, ConvarFlags flags = ConvarFlags.NONE ) where T : unmanaged
  {
    return NativeConvars.ExistsConvar(name) ? new ConVar<T>(name) : Create(name, helpMessage, defaultValue, minValue, maxValue, flags);
  }
}