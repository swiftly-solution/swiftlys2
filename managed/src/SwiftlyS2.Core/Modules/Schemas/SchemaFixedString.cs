using System.Runtime.InteropServices;
using System.Text;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Core.Schemas;

internal class SchemaFixedString : SchemaFixedArray<byte>, ISchemaFixedString, IFormattable
{

    public SchemaFixedString( nint handle, ulong hash, int elementCount, int elementSize, int elementAlignment ) : base(handle, hash, elementCount, elementSize, elementAlignment)
    {
    }

    public string Value {
        get {
            return StringAlloc.CreateCSharpString(_Handle);
        }
        set {
            unsafe
            {
                var bytes = Encoding.UTF8.GetBytes(value);
                if (bytes.Length + 1 > ElementCount)
                {
                    throw new ArgumentException("Value is too long for the fixed string.");
                }
                fixed (byte* p = bytes)
                {
                    NativeMemory.Copy(p, (byte*)_Handle, (nuint)bytes.Length);
                }
            }
        }
    }

    public static implicit operator string( SchemaFixedString str ) => str.Value;

    public string ToString( string? format, IFormatProvider? formatProvider )
    {
        return Value;
    }
}