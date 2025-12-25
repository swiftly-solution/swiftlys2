
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Schemas;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Natives;

public enum VariantFieldType : byte
{
    FIELD_VOID = 0,         // No type or value
    FIELD_FLOAT32,          // Any floating point value
    FIELD_STRING,           // A string ID (return from ALLOC_STRING)
    FIELD_VECTOR,           // Any vector, QAngle, or AngularImpulse
    FIELD_QUATERNION,       // A quaternion
    FIELD_INT32,            // Any integer or enum
    FIELD_BOOLEAN,          // boolean, implemented as an int, I may use this as a hint for compression
    FIELD_INT16,            // 2 byte integer
    FIELD_CHARACTER,        // a byte
    FIELD_COLOR32,          // 8-bit per channel r,g,b,a (32bit color)
    FIELD_EMBEDDED,         // an embedded object with a datadesc, recursively traverse and embedded class/structure based on an additional typedescription
    FIELD_CUSTOM,           // special type that contains function pointers to it's read/write/parse functions
    FIELD_CLASSPTR,         // CBaseEntity *
    FIELD_EHANDLE,          // Entity handle
    FIELD_POSITION_VECTOR,  // A world coordinate (these are fixed up across level transitions automagically)
    FIELD_TIME,             // a floating point time (these are fixed up automatically too!)
    FIELD_TICK,             // an integer tick count( fixed up similarly to time)
    FIELD_SOUNDNAME,        // Engine string that is a sound name (needs precache)
    FIELD_INPUT,            // a list of inputed data fields (all derived from CMultiInputVar)
    FIELD_FUNCTION,         // A class function pointer (Think, Use, etc)
    FIELD_VMATRIX,          // a vmatrix (output coords are NOT worldspace)
    // NOTE: Use float arrays for local transformations that don't need to be fixed up.
    FIELD_VMATRIX_WORLDSPACE,   // A VMatrix that maps some local space to world space (translation is fixed up on level transitions)
    FIELD_MATRIX3X4_WORLDSPACE, // matrix3x4_t that maps some local space to world space (translation is fixed up on level transitions)
    FIELD_INTERVAL,         // a start and range floating point interval ( e.g., 3.2->3.6 == 3.2 and 0.4 )
    FIELD_UNUSED,
    FIELD_VECTOR2D,         // 2 floats
    FIELD_INT64,            // 64bit integer
    FIELD_VECTOR4D,         // 4 floats
    FIELD_RESOURCE,
    FIELD_TYPEUNKNOWN,
    FIELD_CSTRING,
    FIELD_HSCRIPT,
    FIELD_VARIANT,
    FIELD_UINT64,
    FIELD_FLOAT64,
    FIELD_POSITIVEINTEGER_OR_NULL,
    FIELD_HSCRIPT_NEW_INSTANCE,
    FIELD_UINT32,
    FIELD_UTLSTRINGTOKEN,
    FIELD_QANGLE,
    FIELD_NETWORK_ORIGIN_CELL_QUANTIZED_VECTOR,
    FIELD_HMATERIAL,
    FIELD_HMODEL,
    FIELD_NETWORK_QUANTIZED_VECTOR,
    FIELD_NETWORK_QUANTIZED_FLOAT,
    FIELD_DIRECTION_VECTOR_WORLDSPACE,
    FIELD_QANGLE_WORLDSPACE,
    FIELD_QUATERNION_WORLDSPACE,
    FIELD_HSCRIPT_LIGHTBINDING,
    FIELD_V8_VALUE,
    FIELD_V8_OBJECT,
    FIELD_V8_ARRAY,
    FIELD_V8_CALLBACK_INFO,
    FIELD_UTLSTRING,
    FIELD_NETWORK_ORIGIN_CELL_QUANTIZED_POSITION_VECTOR,
    FIELD_HRENDERTEXTURE,
    FIELD_HPARTICLESYSTEMDEFINITION,
    FIELD_UINT8,
    FIELD_UINT16,
    FIELD_CTRANSFORM,
    FIELD_CTRANSFORM_WORLDSPACE,
    FIELD_HPOSTPROCESSING,
    FIELD_MATRIX3X4,
    FIELD_SHIM,
    FIELD_CMOTIONTRANSFORM,
    FIELD_CMOTIONTRANSFORM_WORLDSPACE,
    FIELD_ATTACHMENT_HANDLE,
    FIELD_AMMO_INDEX,
    FIELD_CONDITION_ID,
    FIELD_AI_SCHEDULE_BITS,
    FIELD_MODIFIER_HANDLE,
    FIELD_ROTATION_VECTOR,
    FIELD_ROTATION_VECTOR_WORLDSPACE,
    FIELD_HVDATA,
    FIELD_SCALE32,
    FIELD_STRING_AND_TOKEN,
    FIELD_ENGINE_TIME,
    FIELD_ENGINE_TICK,
    FIELD_WORLD_GROUP_ID,
    FIELD_GLOBALSYMBOL,
    FIELD_HNMGRAPHDEFINITION,
    FIELD_TYPECOUNT
}

[Flags]
public enum CVFlags
{
    FREE = 0x01,
}

public interface IVariantAllocator
{
    public static abstract nint Alloc( ulong size );
    public static abstract void Free( nint ptr );
}

public interface CVariantDefaultAllocator : IVariantAllocator
{
    static nint IVariantAllocator.Alloc( ulong size )
    {
        return NativeAllocator.Alloc(size);
    }
    static void IVariantAllocator.Free( nint ptr )
    {
        NativeAllocator.Free(ptr);
    }
}

[StructLayout(LayoutKind.Sequential, Size = 0x8)]
internal unsafe struct CVariantData
{
    public nint data;

    public nint ThisPointer => (nint)Unsafe.AsPointer(ref this);

    public bool TryGetInnerPointer( out nint ptr )
    {
        ptr = data;
        return ptr.IsValidPtr();
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CVariant<TAllocator> where TAllocator : IVariantAllocator
{
    private CVariantData Data;          // 8 bytes (union)
    public VariantFieldType DataType;   // 1 byte (uint8 enum)
    private readonly byte padding1;
    public CVFlags Flags;               // 2 bytes
    // 4 bytes padding for alignment
    private unsafe fixed byte padding2[4];

    public CVariant()
    {
        Data = new();
        DataType = VariantFieldType.FIELD_VOID;
    }

    public CVariant( object? value ) : this()
    {
        Set(value);
    }

    public readonly bool IsVoid()
    {
        return DataType == VariantFieldType.FIELD_VOID;
    }

    public void SetBool( bool value )
    {
        Free();
        DataType = VariantFieldType.FIELD_BOOLEAN;
        Data.ThisPointer.Write(value);
    }
    public void SetChar( char value )
    {
        Free();
        DataType = VariantFieldType.FIELD_CHARACTER;
        Data.ThisPointer.Write(value);
    }
    public void SetShort( short value )
    {
        Free();
        DataType = VariantFieldType.FIELD_INT16;
        Data.ThisPointer.Write(value);
    }
    public void SetUShort( ushort value )
    {
        Free();
        DataType = VariantFieldType.FIELD_UINT16;
        Data.ThisPointer.Write(value);
    }
    public void SetInt( int value )
    {
        Free();
        DataType = VariantFieldType.FIELD_INT32;
        Data.ThisPointer.Write(value);
    }
    public void SetUInt( uint value )
    {
        Free();
        DataType = VariantFieldType.FIELD_UINT32;
        Data.ThisPointer.Write(value);
    }
    public void SetLong( long value )
    {
        Free();
        DataType = VariantFieldType.FIELD_INT64;
        Data.ThisPointer.Write(value);
    }
    public void SetULong( ulong value )
    {
        Free();
        DataType = VariantFieldType.FIELD_UINT64;
        Data.ThisPointer.Write(value);
    }
    public void SetFloat( float value )
    {
        Free();
        DataType = VariantFieldType.FIELD_FLOAT32;
        Data.ThisPointer.Write(value);
    }
    public void SetDouble( double value )
    {
        Free();
        DataType = VariantFieldType.FIELD_FLOAT64;
        Data.ThisPointer.Write(value);
    }
    public void SetResourceHandle( ResourceHandle value )
    {
        Free();
        DataType = VariantFieldType.FIELD_RESOURCE;
        Data.ThisPointer.Write(value);
    }
    public void SetUtlStringToken( CUtlStringToken value )
    {
        Free();
        DataType = VariantFieldType.FIELD_UTLSTRINGTOKEN;
        Data.ThisPointer.Write(value);
    }
    public void SetHScript( HSCRIPT value )
    {
        Free();
        DataType = VariantFieldType.FIELD_HSCRIPT;
        Data.ThisPointer.Write(value);
    }
    public void SetHandle( ICHandle value )
    {
        Free();
        DataType = VariantFieldType.FIELD_EHANDLE;
        Data.ThisPointer.Write(value.Raw);
    }
    public void SetString( string value )
    {
        Free();
        DataType = VariantFieldType.FIELD_CSTRING;
        var len = Encoding.UTF8.GetByteCount(value);
        var buffer = TAllocator.Alloc((ulong)(len + 1));
        buffer.CopyFrom(Encoding.UTF8.GetBytes(value));
        buffer.Write(len, 0);
        Data.ThisPointer.Write(buffer);
        SetAllocated();
    }
    public void SetVector2D( Vector2D value )
    {
        Free();
        DataType = VariantFieldType.FIELD_VECTOR2D;
        CopyData(value);
    }
    public void SetVector( Vector value )
    {
        Free();
        DataType = VariantFieldType.FIELD_VECTOR;
        CopyData(value);
    }
    public void SetVector4D( Vector4D value )
    {
        Free();
        DataType = VariantFieldType.FIELD_VECTOR4D;
        CopyData(value);
    }
    public void SetQAngle( QAngle value )
    {
        Free();
        DataType = VariantFieldType.FIELD_QANGLE;
        CopyData(value);
    }
    public void SetQuaternion( Quaternion value )
    {
        Free();
        DataType = VariantFieldType.FIELD_QUATERNION;
        CopyData(value);
    }
    public void SetColor( Color value )
    {
        Free();
        DataType = VariantFieldType.FIELD_COLOR32;
        CopyData(value);
    }

    public void Set<T>( T value )
    {
        unsafe
        {
            if (value is bool boolValue)
            {
                SetBool(boolValue);
            }
            else if (value is char charValue)
            {
                SetChar(charValue);
            }
            else if (value is short shortValue)
            {
                SetShort(shortValue);
            }
            else if (value is ushort ushortValue)
            {
                SetUShort(ushortValue);
            }
            else if (value is int intValue)
            {
                SetInt(intValue);
            }
            else if (value is uint uintValue)
            {
                SetUInt(uintValue);
            }
            else if (value is long longValue)
            {
                SetLong(longValue);
            }
            else if (value is ulong ulongValue)
            {
                SetULong(ulongValue);
            }
            else if (value is float floatValue)
            {
                SetFloat(floatValue);
            }
            else if (value is double doubleValue)
            {
                SetDouble(doubleValue);
            }
            else if (value is string stringValue)
            {
                SetString(stringValue);
            }
            else if (value is ResourceHandle resourceHandle)
            {
                SetResourceHandle(resourceHandle);
            }
            else if (value is CUtlStringToken utlStringToken)
            {
                SetUtlStringToken(utlStringToken);
            }
            else if (value is HSCRIPT hscript)
            {
                SetHScript(hscript);
            }
            else if (value is ICHandle handle)
            {
                SetHandle(handle);
            }
            else if (value is Vector2D vector2D)
            {
                SetVector2D(vector2D);
            }
            else if (value is Vector vector)
            {
                SetVector(vector);
            }
            else if (value is Vector4D vector4D)
            {
                SetVector4D(vector4D);
            }
            else if (value is QAngle qAngle)
            {
                SetQAngle(qAngle);
            }
            else if (value is Quaternion quaternion)
            {
                SetQuaternion(quaternion);
            }
            else if (value is Color color)
            {
                SetColor(color);
            }
            else
            {
                throw new InvalidOperationException($"Unsupported type: {typeof(T).Name}");
            }
        }
    }

    public bool TryGetBool( [MaybeNullWhen(false)] out bool value )
    {
        return TryGetUnmanaged(out value, VariantFieldType.FIELD_BOOLEAN);
    }
    public bool TryGetChar( [MaybeNullWhen(false)] out char value )
    {
        return TryGetUnmanaged(out value, VariantFieldType.FIELD_CHARACTER);
    }
    public bool TryGetInt16( [MaybeNullWhen(false)] out short value )
    {
        return TryGetUnmanaged(out value, VariantFieldType.FIELD_INT16);
    }
    public bool TryGetUInt16( [MaybeNullWhen(false)] out ushort value )
    {
        return TryGetUnmanaged(out value, VariantFieldType.FIELD_UINT16);
    }
    public bool TryGetInt32( [MaybeNullWhen(false)] out int value )
    {
        return TryGetUnmanaged(out value, VariantFieldType.FIELD_INT32);
    }
    public bool TryGetUInt32( [MaybeNullWhen(false)] out uint value )
    {
        return TryGetUnmanaged(out value, VariantFieldType.FIELD_UINT32);
    }
    public bool TryGetInt64( [MaybeNullWhen(false)] out long value )
    {
        return TryGetUnmanaged(out value, VariantFieldType.FIELD_INT64);
    }
    public bool TryGetUInt64( [MaybeNullWhen(false)] out ulong value )
    {
        return TryGetUnmanaged(out value, VariantFieldType.FIELD_UINT64);
    }
    public bool TryGetFloat( [MaybeNullWhen(false)] out float value )
    {
        return TryGetUnmanaged(out value, VariantFieldType.FIELD_FLOAT32);
    }
    public bool TryGetDouble( [MaybeNullWhen(false)] out double value )
    {
        return TryGetUnmanaged(out value, VariantFieldType.FIELD_FLOAT64);
    }
    public bool TryGetResourceHandle( [MaybeNullWhen(false)] out ResourceHandle value )
    {
        return TryGetUnmanaged(out value, VariantFieldType.FIELD_RESOURCE);
    }
    public bool TryGetUtlStringToken( [MaybeNullWhen(false)] out CUtlStringToken value )
    {
        return TryGetUnmanaged(out value, VariantFieldType.FIELD_UTLSTRINGTOKEN);
    }
    public bool TryGetHScript( [MaybeNullWhen(false)] out HSCRIPT value )
    {
        return TryGetUnmanaged(out value, VariantFieldType.FIELD_HSCRIPT);
    }
    public bool TryGetCHandle<T>( [MaybeNullWhen(false)] out CHandle<T> value ) where T : class, ISchemaClass<T>
    {
        if (IsVoid())
        {
            value = default;
            return false;
        }

        if (DataType != VariantFieldType.FIELD_EHANDLE)
        {
            value = default;
            return false;
        }
        else
        {
            value = new CHandle<T>(Data.ThisPointer.Read<uint>());
            return true;
        }
    }
    public bool TryGetVector2D( [MaybeNullWhen(false)] out Vector2D value )
    {
        return TryGetAllocated(out value, VariantFieldType.FIELD_VECTOR2D);
    }
    public bool TryGetVector( [MaybeNullWhen(false)] out Vector value )
    {
        return TryGetAllocated(out value, VariantFieldType.FIELD_VECTOR);
    }
    public bool TryGetVector4D( [MaybeNullWhen(false)] out Vector4D value )
    {
        return TryGetAllocated(out value, VariantFieldType.FIELD_VECTOR4D);
    }
    public bool TryGetQAngle( [MaybeNullWhen(false)] out QAngle value )
    {
        return TryGetAllocated(out value, VariantFieldType.FIELD_QANGLE);
    }
    public bool TryGetQuaternion( [MaybeNullWhen(false)] out Quaternion value )
    {
        return TryGetAllocated(out value, VariantFieldType.FIELD_QUATERNION);
    }
    public bool TryGetColor( [MaybeNullWhen(false)] out Color value )
    {
        return TryGetAllocated(out value, VariantFieldType.FIELD_COLOR32);
    }
    public bool TryGetString( [MaybeNullWhen(false)] out string value )
    {
        if (TryGetUnmanaged(out nint ptr, VariantFieldType.FIELD_CSTRING))
        {
            value = Marshal.PtrToStringUTF8(ptr)!;
            return true;
        }
        else
        {
            value = default;
            return false;
        }
    }

    public override string? ToString()
    {
        if (TryGetBool(out var boolValue))
        {
            return $"CVariant(Type={DataType}, Value={boolValue}, Flags={Flags})";
        }
        else if (TryGetChar(out var charValue))
        {
            return $"CVariant(Type={DataType}, Value={charValue}, Flags={Flags})";
        }
        else if (TryGetInt16(out var int16Value))
        {
            return $"CVariant(Type={DataType}, Value={int16Value}, Flags={Flags})";
        }
        else if (TryGetUInt16(out var uint16Value))
        {
            return $"CVariant(Type={DataType}, Value={uint16Value}, Flags={Flags})";
        }
        else if (TryGetInt32(out var int32Value))
        {
            return $"CVariant(Type={DataType}, Value={int32Value}, Flags={Flags})";
        }
        else if (TryGetUInt32(out var uint32Value))
        {
            return $"CVariant(Type={DataType}, Value={uint32Value}, Flags={Flags})";
        }
        else if (TryGetInt64(out var int64Value))
        {
            return $"CVariant(Type={DataType}, Value={int64Value}, Flags={Flags})";
        }
        else if (TryGetUInt64(out var uint64Value))
        {
            return $"CVariant(Type={DataType}, Value={uint64Value}, Flags={Flags})";
        }
        else if (TryGetFloat(out var floatValue))
        {
            return $"CVariant(Type={DataType}, Value={floatValue}, Flags={Flags})";
        }
        else if (TryGetDouble(out var doubleValue))
        {
            return $"CVariant(Type={DataType}, Value={doubleValue}, Flags={Flags})";
        }
        else if (TryGetResourceHandle(out var resourceHandleValue))
        {
            return $"CVariant(Type={DataType}, Value={resourceHandleValue}, Flags={Flags})";
        }
        else if (TryGetUtlStringToken(out var utlStringTokenValue))
        {
            return $"CVariant(Type={DataType}, Value={utlStringTokenValue}, Flags={Flags})";
        }
        else if (TryGetHScript(out var hscriptValue))
        {
            return $"CVariant(Type={DataType}, Value={hscriptValue}, Flags={Flags})";
        }
        else if (TryGetCHandle<CBaseEntity>(out var handleValue))
        {
            return $"CVariant(Type={DataType}, Value={handleValue.Raw}, Flags={Flags})";
        }
        else if (TryGetVector2D(out var vector2DValue))
        {
            return $"CVariant(Type={DataType}, Value={vector2DValue}, Flags={Flags})";
        }
        else if (TryGetVector(out var vectorValue))
        {
            return $"CVariant(Type={DataType}, Value={vectorValue}, Flags={Flags})";
        }
        else if (TryGetVector4D(out var vector4DValue))
        {
            return $"CVariant(Type={DataType}, Value={vector4DValue}, Flags={Flags})";
        }
        else if (TryGetQAngle(out var qAngleValue))
        {
            return $"CVariant(Type={DataType}, Value={qAngleValue}, Flags={Flags})";
        }
        else if (TryGetQuaternion(out var quaternionValue))
        {
            return $"CVariant(Type={DataType}, Value={quaternionValue}, Flags={Flags})";
        }
        else if (TryGetColor(out var colorValue))
        {
            return $"CVariant(Type={DataType}, Value={colorValue}, Flags={Flags})";
        }
        else if (TryGetString(out var stringValue))
        {
            return $"CVariant(Type={DataType}, Value={stringValue}, Flags={Flags})";
        }
        return $"CVariant(Type={DataType}, Flags={Flags}, UnknownValue)";
    }

    private void Free()
    {
        if (Flags.HasFlag(CVFlags.FREE))
        {
            if (Data.TryGetInnerPointer(out var ptr))
            {
                TAllocator.Free(ptr);
            }
        }
        Data.ThisPointer.Write(0);
        Flags &= ~CVFlags.FREE;
        DataType = VariantFieldType.FIELD_VOID;
    }

    private void SetAllocated()
    {
        Flags |= CVFlags.FREE;
    }

    private void CopyData<T>( T value ) where T : unmanaged
    {
        unsafe
        {
            var memory = TAllocator.Alloc((ulong)sizeof(T));
            memory.Write(value);
            Data.ThisPointer.Write(memory);
            SetAllocated();
        }
    }

    private bool TryGetUnmanaged<T>( [MaybeNullWhen(false)] out T value, VariantFieldType targetType ) where T : unmanaged
    {
        if (IsVoid())
        {
            value = default;
            return false;
        }

        if (DataType != targetType)
        {
            value = default;
            return false;
        }
        else
        {
            value = Data.ThisPointer.Read<T>();
            return true;
        }
    }

    private bool TryGetAllocated<T>( [MaybeNullWhen(false)] out T value, VariantFieldType targetType ) where T : unmanaged
    {
        if (IsVoid())
        {
            value = default;
            return false;
        }

        if (DataType != targetType)
        {
            value = default;
            return false;
        }

        if (Data.TryGetInnerPointer(out var ptr))
        {
            value = ptr.Read<T>();
            return true;
        }
        else
        {
            value = default;
            return false;
        }
    }
}