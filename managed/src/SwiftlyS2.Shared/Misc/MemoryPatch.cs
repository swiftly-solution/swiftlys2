using System.Runtime.InteropServices;

internal static class MemoryPatch
{
    private const int PAGE = 4096;
    private const int PROT_RWX = 0x1 | 0x2 | 0x4;  // 7
    private const int PAGE_EXECUTE_READWRITE = 0x40;

    [DllImport("libc", EntryPoint = "mprotect")]
    private static extern int mprotect( nint addr, nint len, int prot );

    [DllImport("kernel32.dll", SetLastError = true)]
    private static     unsafe extern bool VirtualProtect( nint addr, int size, int newProt, int* oldProt );

    public unsafe static bool SetMemAccess( nint addr, int size )
    {
        if (addr == 0) throw new ArgumentNullException(nameof(addr));

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            var start = addr & ~(PAGE - 1);
            var span = size + (addr - start); // size + LALDIF
            return mprotect(start, span, PROT_RWX) == 0;
        }
        else
        {
            int old;
            return VirtualProtect(addr, size, PAGE_EXECUTE_READWRITE, &old);
        }
    }
}