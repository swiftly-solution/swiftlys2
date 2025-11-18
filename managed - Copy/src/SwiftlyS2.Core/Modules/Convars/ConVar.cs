using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Convars;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Core.Extensions;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using SwiftlyS2.Core.Services;

namespace SwiftlyS2.Core.Convars;

internal delegate void ConVarCallbackDelegate( int playerId, nint name, nint value );

internal class ConVar<T> : IConVar<T>
{

  private Dictionary<int, ConVarCallbackDelegate> _callbacks = new();
  private Lock _lock = new();

  private nint _minValuePtrPtr => NativeConvars.GetMinValuePtrPtr(Name);
  private nint _maxValuePtrPtr => NativeConvars.GetMaxValuePtrPtr(Name);

  public EConVarType Type => (EConVarType)NativeConvars.GetConvarType(Name);

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

  public ConvarFlags Flags {
    get => (ConvarFlags)NativeConvars.GetFlags(Name);
    set => NativeConvars.SetFlags(Name, (ulong)value);
  }

  public bool HasDefaultValue => NativeConvars.HasDefaultValue(Name);

  public bool HasMinValue => _minValuePtrPtr.Read<nint>() != 0;
  public bool HasMaxValue => _maxValuePtrPtr.Read<nint>() != 0;

  public string Name { get; set; }

  internal ConVar( string name )
  {
    Name = name;

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
    var val = "";
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
    else throw new ArgumentException($"Invalid type {typeof(T).Name}");

    NativeConvars.SetClientConvarValueString(clientId, Name, val);
  }

  public void QueryClient( int clientId, Action<string> callback )
  {

    Action? removeSelf = null;
    ConVarCallbackDelegate nativeCallback = ( playerId, namePtr, valuePtr ) =>
    {
      if (clientId != playerId) return;
      var name = Marshal.PtrToStringAnsi(namePtr);
      if (name != Name) return;
      var value = Marshal.PtrToStringAnsi(valuePtr)!;

      // var convertedValue = (T)Convert.ChangeType(value, typeof(T))!;
      callback(value);
      if (removeSelf != null) removeSelf();
    };


    var callbackPtr = Marshal.GetFunctionPointerForDelegate(nativeCallback);

    var listenerId = NativeConvars.AddQueryClientCvarCallback(callbackPtr);
    lock (_lock)
    {
      _callbacks[listenerId] = nativeCallback;
    }

    removeSelf = () =>
    {
      lock (_lock)
      {
        _callbacks.Remove(listenerId);
        NativeConvars.RemoveQueryClientCvarCallback(listenerId);
      }
    };

    NativeConvars.QueryClientConvar(clientId, Name);
  }

  public T GetValue()
  {
    unsafe
    {
      if (Type != EConVarType.EConVarType_String)
      {
        return *(T*)NativeConvars.GetValuePtr(Name);
      }
      else
      {
        return (T)(object)(*(CUtlString*)NativeConvars.GetValuePtr(Name)).Value;
      }
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
        CUtlString str = new();
        str.Value = (string)(object)value;
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
        CUtlString str = new();
        str.Value = (string)(object)value;
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
      return **(T**)_minValuePtrPtr;
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
      return **(T**)_maxValuePtrPtr;
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
      if (_minValuePtrPtr.Read<nint>() == nint.Zero)
      {
        _minValuePtrPtr.Write(NativeAllocator.Alloc(16));
      }
      **(T**)_minValuePtrPtr = minValue;
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
      if (_maxValuePtrPtr.Read<nint>() == nint.Zero)
      {
        _maxValuePtrPtr.Write(NativeAllocator.Alloc(16));
      }
      **(T**)_maxValuePtrPtr = maxValue;
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
      if (Type != EConVarType.EConVarType_String)
      {
        return *(T*)ptr;
      }
      else
      {
        return (T)(object)(*(CUtlString*)ptr).Value;
      }
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
