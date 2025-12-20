using System.Reflection;
using System.Runtime.InteropServices;
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

[StructLayout(LayoutKind.Sequential, Size = 0x8)]
public unsafe struct CVariantData
{
    private fixed byte data[8];

    public bool Bool {
        readonly get => As<byte>() != 0;
        set => AsRef<byte>() = value ? (byte)1 : (byte)0;
    }

    public byte Byte {
        readonly get => As<byte>();
        set => AsRef<byte>() = value;
    }

    public short Int16 {
        readonly get => As<short>();
        set => AsRef<short>() = value;
    }

    public ushort UInt16 {
        readonly get => As<ushort>();
        set => AsRef<ushort>() = value;
    }

    public int Int32 {
        readonly get => As<int>();
        set => AsRef<int>() = value;
    }

    public uint UInt32 {
        readonly get => As<uint>();
        set => AsRef<uint>() = value;
    }

    public long Int64 {
        readonly get => As<long>();
        set => AsRef<long>() = value;
    }

    public ulong UInt64 {
        readonly get => As<ulong>();
        set => AsRef<ulong>() = value;
    }

    public float Float {
        readonly get => As<float>();
        set => AsRef<float>() = value;
    }

    public double Double {
        readonly get => As<double>();
        set => AsRef<double>() = value;
    }

    public nint Raw {
        readonly get => As<nint>();
        set => AsRef<nint>() = value;
    }

    public CHandle<CEntityInstance> EntityInstanceHandle {
        readonly get => As<CHandle<CEntityInstance>>();
        set => AsRef<CHandle<CEntityInstance>>() = value;
    }

    public CString String {
        readonly get => As<CString>();
        set => AsRef<CString>() = value;
    }

    public CUtlStringToken StringToken {
        readonly get => As<CUtlStringToken>();
        set => AsRef<CUtlStringToken>() = value;
    }

    public ResourceHandle* ResourceHandle {
        readonly get => As<nint>() == 0 ? null : (ResourceHandle*)As<nint>();
        set => AsRef<nint>() = (nint)value;
    }

    public Vector* Vector {
        readonly get => As<nint>() == 0 ? null : (Vector*)As<nint>();
        set => AsRef<nint>() = (nint)value;
    }

    public Vector2D* Vector2D {
        readonly get => As<nint>() == 0 ? null : (Vector2D*)As<nint>();
        set => AsRef<nint>() = (nint)value;
    }

    public Vector4D* Vector4D {
        readonly get => As<nint>() == 0 ? null : (Vector4D*)As<nint>();
        set => AsRef<nint>() = (nint)value;
    }

    public QAngle* QAngle {
        readonly get => As<nint>() == 0 ? null : (QAngle*)As<nint>();
        set => AsRef<nint>() = (nint)value;
    }

    public Quaternion* Quaternion {
        readonly get => As<nint>() == 0 ? null : (Quaternion*)As<nint>();
        set => AsRef<nint>() = (nint)value;
    }

    public Color* Color {
        readonly get => As<nint>() == 0 ? null : (Color*)As<nint>();
        set => AsRef<nint>() = (nint)value;
    }

    private ref T AsRef<T>() where T : unmanaged
    {
        fixed (byte* ptr = data)
        {
            return ref *(T*)ptr;
        }
    }

    private readonly T As<T>() where T : unmanaged
    {
        fixed (byte* ptr = data)
        {
            return *(T*)ptr;
        }
    }

    public readonly bool TryGet<T>( out T result ) where T : unmanaged
    {
        if (sizeof(T) <= 8)
        {
            fixed (byte* ptr = data)
            {
                result = *(T*)ptr;
                return true;
            }
        }
        result = default;
        return false;
    }

    public VariantFieldType Set( object? value )
    {
        if (value is bool boolValue)
        {
            Byte = boolValue ? (byte)1 : (byte)0;
            return VariantFieldType.FIELD_BOOLEAN;
        }
        else if (value is byte || value is sbyte)
        {
            Byte = Convert.ToByte(value);
            return VariantFieldType.FIELD_CHARACTER;
        }
        else if (value is short shortVal)
        {
            Int16 = shortVal;
            return VariantFieldType.FIELD_INT16;
        }
        else if (value is ushort ushortVal)
        {
            UInt16 = ushortVal;
            return VariantFieldType.FIELD_UINT16;
        }
        else if (value is int intVal)
        {
            Int32 = intVal;
            return VariantFieldType.FIELD_INT32;
        }
        else if (value is uint uintVal)
        {
            UInt32 = uintVal;
            return VariantFieldType.FIELD_UINT32;
        }
        else if (value is long || value is nint)
        {
            Int64 = Convert.ToInt64(value);
            return VariantFieldType.FIELD_INT64;
        }
        else if (value is ulong || value is nuint)
        {
            UInt64 = Convert.ToUInt64(value);
            return VariantFieldType.FIELD_UINT64;
        }
        else if (value is float floatVal)
        {
            Float = floatVal;
            return VariantFieldType.FIELD_FLOAT32;
        }
        else if (value is double doubleVal)
        {
            Double = doubleVal;
            return VariantFieldType.FIELD_FLOAT64;
        }
        else if (value is string stringVal)
        {
            String = new CString { Value = stringVal };
            return VariantFieldType.FIELD_CSTRING;
        }
        else if (value is ResourceHandle resourceHandleVal)
        {
            ResourceHandle = &resourceHandleVal;
            return VariantFieldType.FIELD_RESOURCE;
        }
        else if (value != null && value.GetType() == typeof(ResourceHandle*))
        {
            ResourceHandle = (ResourceHandle*)Pointer.Unbox(value);
            return VariantFieldType.FIELD_RESOURCE;
        }
        else if (value is Vector vectorVal)
        {
            Vector = &vectorVal;
            return VariantFieldType.FIELD_VECTOR;
        }
        else if (value != null && value.GetType() == typeof(Vector*))
        {
            Vector = (Vector*)Pointer.Unbox(value);
            return VariantFieldType.FIELD_VECTOR;
        }
        else if (value is QAngle qangleVal)
        {
            QAngle = &qangleVal;
            return VariantFieldType.FIELD_QANGLE;
        }
        else if (value != null && value.GetType() == typeof(QAngle*))
        {
            QAngle = (QAngle*)Pointer.Unbox(value);
            return VariantFieldType.FIELD_QANGLE;
        }
        else if (value is Vector2D vector2DVal)
        {
            Vector2D = &vector2DVal;
            return VariantFieldType.FIELD_VECTOR2D;
        }
        else if (value != null && value.GetType() == typeof(Vector2D*))
        {
            Vector2D = (Vector2D*)Pointer.Unbox(value);
            return VariantFieldType.FIELD_VECTOR2D;
        }
        else if (value is Vector4D vector4DVal)
        {
            Vector4D = &vector4DVal;
            return VariantFieldType.FIELD_VECTOR4D;
        }
        else if (value != null && value.GetType() == typeof(Vector4D*))
        {
            Vector4D = (Vector4D*)Pointer.Unbox(value);
            return VariantFieldType.FIELD_VECTOR4D;
        }
        else if (value is Quaternion quaternionVal)
        {
            Quaternion = &quaternionVal;
            return VariantFieldType.FIELD_QUATERNION;
        }
        else if (value != null && value.GetType() == typeof(Quaternion*))
        {
            Quaternion = (Quaternion*)Pointer.Unbox(value);
            return VariantFieldType.FIELD_QUATERNION;
        }
        else if (value is Color colorVal)
        {
            Color = &colorVal;
            return VariantFieldType.FIELD_COLOR32;
        }
        else if (value != null && value.GetType() == typeof(Color*))
        {
            Color = (Color*)Pointer.Unbox(value);
            return VariantFieldType.FIELD_COLOR32;
        }
        else if (value is CHandle<CEntityInstance> handleVal)
        {
            EntityInstanceHandle = handleVal;
            return VariantFieldType.FIELD_EHANDLE;
        }
        else if (value is CUtlStringToken tokenVal)
        {
            StringToken = tokenVal;
            return VariantFieldType.FIELD_UTLSTRINGTOKEN;
        }
        else if (value != null && value.GetType().IsPointer)
        {
            Int64 = (long)Pointer.Unbox(value);
            return VariantFieldType.FIELD_INT64;
        }
        else
        {
            String = new CString { Value = string.Empty };
            return VariantFieldType.FIELD_CSTRING;
        }
    }
}

[StructLayout(LayoutKind.Explicit, Size = 0x10)]
public struct CVariant
{
    [FieldOffset(0x0)] public CVariantData Data;            // 8 bytes (union)
    [FieldOffset(0x8)] public VariantFieldType DataType;    // 1 byte (uint8 enum)
    // 1 byte padding
    [FieldOffset(0xA)] public ushort Flags;                 // 2 bytes
    // 4 bytes padding for alignment

    public CVariant() : this(null)
    {
    }

    public CVariant( object? value )
    {
        Set(value);
    }

    public void Set( object? value )
    {
        DataType = Data.Set(value);
    }
}