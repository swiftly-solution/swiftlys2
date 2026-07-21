using System.Runtime.InteropServices;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Extensions;
using System.Text;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential, Size = 8)]
public struct CGameSoundEventName
{

    private nint _ptr;

    public string Value {
        get => Get();
        set => Set(value);
    }

    public CGameSoundEventName( string str )
    {
        Set(str);
    }

    private void Purge()
    {
        if (_ptr.IsValidPtr())
        {
            NativeAllocator.Free(_ptr);
            _ptr = 0;
        }
    }

    private string Get()
    {
        if (!_ptr.IsValidPtr()) return string.Empty;
        return Marshal.PtrToStringUTF8(_ptr)!;
    }

    private void Set( string str )
    {
        Purge();
        unsafe
        {
            var byteCount = Encoding.UTF8.GetByteCount(str);
            var neededSize = byteCount + 1;
            var ptr = NativeAllocator.Alloc((ulong)neededSize);
            var span = new Span<byte>(ptr.ToPointer(), neededSize);
            _ = Encoding.UTF8.GetBytes(str, span);
            span[byteCount] = 0;
            _ptr = ptr;
        }

    }

    public static implicit operator string( CGameSoundEventName str ) => str.Value;
    public static implicit operator CGameSoundEventName( string str ) => new(str);
}