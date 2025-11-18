using System.Runtime.CompilerServices;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Core.Extensions;

internal static class PtrExtensions
{

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Read<T>( this nint ptr ) where T : unmanaged
  {
    unsafe { return Unsafe.Read<T>((void*)ptr); }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Read<T>( this nint ptr, int offset ) where T : unmanaged
  {
    unsafe { return Unsafe.Read<T>((void*)(ptr + offset)); }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Read<T>( this nint ptr, nint offset ) where T : unmanaged
  {
    unsafe { return Unsafe.Read<T>((void*)(ptr + offset)); }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ref T AsRef<T>( this nint ptr ) where T : unmanaged
  {
    unsafe { return ref Unsafe.AsRef<T>((void*)ptr); }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ref T AsRef<T>( this nint ptr, int offset ) where T : unmanaged
  {
    unsafe { return ref Unsafe.AsRef<T>((void*)(ptr + offset)); }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ref T AsRef<T>( this nint ptr, nint offset ) where T : unmanaged
  {
    unsafe { return ref Unsafe.AsRef<T>((void*)(ptr + offset)); }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ref T Deref<T>( this nint ptr ) where T : unmanaged
  {
    return ref ptr.Read<nint>().AsRef<T>();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ref T Deref<T>( this nint ptr, int offset ) where T : unmanaged
  {
    return ref ptr.Read<nint>(offset).AsRef<T>();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ref T Deref<T>( this nint ptr, nint offset ) where T : unmanaged
  {
    return ref ptr.Read<nint>(offset).AsRef<T>();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Write<T>( this nint ptr, T value ) where T : unmanaged
  {
    unsafe { Unsafe.Write((void*)ptr, value); }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Write<T>( this nint ptr, int offset, T value ) where T : unmanaged
  {
    unsafe { Unsafe.Write((void*)(ptr + offset), value); }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Write<T>( this nint ptr, nint offset, T value ) where T : unmanaged
  {
    unsafe { Unsafe.Write((void*)(ptr + offset), value); }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void CopyFrom( this nint ptr, byte[] source )
  {
    unsafe
    {
      fixed (byte* sourcePtr = source)
      {
        Unsafe.CopyBlockUnaligned((void*)ptr, sourcePtr, (uint)source.Length);
      }
    }
  }


  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void CopyFrom( this nint ptr, nint source, int size )
  {
    unsafe { Unsafe.CopyBlockUnaligned((void*)ptr, (void*)source, (uint)size); }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void CopyFrom( this nint ptr, int offset, nint source, int size )
  {
    unsafe { Unsafe.CopyBlockUnaligned((void*)(ptr + offset), (void*)source, (uint)size); }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsValidPtr( this nint ptr )
  {
    return ptr != 0 && ptr != IntPtr.MaxValue;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T AsHandle<T>( this nint ptr ) where T : INativeHandle, ISchemaClass<T>
  {
    return T.From(ptr);
  }
}