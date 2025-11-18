using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Memory;

[StructLayout(LayoutKind.Explicit, Size = 16)]
public struct Xmm
{
    [FieldOffset(0)]
    public unsafe fixed byte U8[16];

    [FieldOffset(0)]
    public unsafe fixed ushort U16[8];

    [FieldOffset(0)]
    public unsafe fixed uint U32[4];

    [FieldOffset(0)]
    public unsafe fixed ulong U64[2];

    [FieldOffset(0)]
    public unsafe fixed float F32[4];

    [FieldOffset(0)]
    public unsafe fixed double F64[2];
}

[StructLayout(LayoutKind.Sequential)]
public struct MidHookContext
{
    public Xmm XMM0, XMM1, XMM2, XMM3, XMM4, XMM5, XMM6, XMM7, XMM8, XMM9, XMM10, XMM11, XMM12, XMM13, XMM14, XMM15;
    public nuint RFLAGS, R15, R14, R13, R12, R11, R10, R9, R8, RDI, RSI, RDX, RCX, RBX, RAX, RBP, RSP, TRAMPOLINE_RSP, RIP;
}

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void MidHookDelegate(ref MidHookContext context);

public interface IUnmanagedMemory
{
    /// <summary>
    /// The address of the unmanaged pointer.
    /// </summary>
    public nint Address { get; }

    /// <summary>
    /// Hook a native function at the specified address with a managed callback.
    /// The callback receives a context structure that allows reading and modifying CPU registers.
    /// </summary>
    /// <param name="callback">The callback to call when the code reaches that address.</param>
    public Guid AddHook(MidHookDelegate callback);

    /// <summary>
    /// Unhook a hook by its id.
    /// </summary>
    /// <param name="id">The id of the hook to unhook.</param>
    public void RemoveHook(Guid id);
}