using System.Runtime.InteropServices;
using System.Text;
using IntPtr = nint;

namespace SwiftlyS2.Shared.SteamAPI;

public class InteropHelp
{
    public class UTF8StringHandle : Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid
    {
        public UTF8StringHandle( string str )
            : base(true)
        {
            if (str == null)
            {
                SetHandle(IntPtr.Zero);
                return;
            }

            // +1 for '\0'
            var strbuf = new byte[Encoding.UTF8.GetByteCount(str) + 1];
            _ = Encoding.UTF8.GetBytes(str, 0, str.Length, strbuf, 0);
            var buffer = Marshal.AllocHGlobal(strbuf.Length);
            Marshal.Copy(strbuf, 0, buffer, strbuf.Length);

            SetHandle(buffer);
        }

        protected override bool ReleaseHandle()
        {
            if (!IsInvalid)
            {
                Marshal.FreeHGlobal(handle);
            }
            return true;
        }
    }

    public static void TestIfPlatformSupported()
    {
    }

    public static void TestIfAvailableClient()
    {
        TestIfPlatformSupported();
        throw new InvalidOperationException("Steamworks Client is not available.");
    }

    public static void TestIfAvailableGameServer()
    {
        TestIfPlatformSupported();
        if (CSteamGameServerAPIContext.GetSteamClient() == IntPtr.Zero)
        {
            if (!CSteamGameServerAPIContext.Init())
            {
                throw new InvalidOperationException("Steamworks GameServer is not initialized.");
            }
        }
    }

    // This continues to exist for both 'out string' and strings returned by Steamworks functions.
    public static string PtrToStringUTF8( IntPtr nativeUtf8 )
    {
        if (nativeUtf8 == IntPtr.Zero)
        {
            return null;
        }

        var len = 0;

        while (Marshal.ReadByte(nativeUtf8, len) != 0)
        {
            ++len;
        }

        if (len == 0)
        {
            return string.Empty;
        }

        var buffer = new byte[len];
        Marshal.Copy(nativeUtf8, buffer, 0, buffer.Length);
        return Encoding.UTF8.GetString(buffer);
    }

    public static string ByteArrayToStringUTF8( byte[] buffer )
    {
        var length = 0;
        while (length < buffer.Length && buffer[length] != 0)
        {
            length++;
        }

        return Encoding.UTF8.GetString(buffer, 0, length);
    }

    public static void StringToByteArrayUTF8( string str, byte[] outArrayBuffer, int outArrayBufferSize )
    {
        outArrayBuffer = new byte[outArrayBufferSize];
        var length = Encoding.UTF8.GetBytes(str, 0, str.Length, outArrayBuffer, 0);
        outArrayBuffer[length] = 0;
    }

    // TODO - Should be IDisposable
    // We can't use an ICustomMarshaler because Unity dies when MarshalManagedToNative() gets called with a generic type.
    public class SteamParamStringArray
    {
        // The pointer to each AllocHGlobal() string
        private readonly IntPtr[] m_Strings;
        // The pointer to the condensed version of m_Strings
        private readonly IntPtr m_ptrStrings;
        // The pointer to the StructureToPtr version of SteamParamStringArray_t that will get marshaled
        private readonly IntPtr m_pSteamParamStringArray;

        public SteamParamStringArray( IList<string> strings )
        {
            if (strings == null)
            {
                m_pSteamParamStringArray = IntPtr.Zero;
                return;
            }

            m_Strings = new IntPtr[strings.Count];
            for (var i = 0; i < strings.Count; ++i)
            {
                var strbuf = new byte[Encoding.UTF8.GetByteCount(strings[i]) + 1];
                _ = Encoding.UTF8.GetBytes(strings[i], 0, strings[i].Length, strbuf, 0);
                m_Strings[i] = Marshal.AllocHGlobal(strbuf.Length);
                Marshal.Copy(strbuf, 0, m_Strings[i], strbuf.Length);
            }

            m_ptrStrings = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)) * m_Strings.Length);
            var stringArray = new SteamParamStringArray_t() {
                m_ppStrings = m_ptrStrings,
                m_nNumStrings = m_Strings.Length
            };
            Marshal.Copy(m_Strings, 0, stringArray.m_ppStrings, m_Strings.Length);

            m_pSteamParamStringArray = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SteamParamStringArray_t)));
            Marshal.StructureToPtr(stringArray, m_pSteamParamStringArray, false);
        }

        ~SteamParamStringArray()
        {
            if (m_Strings != null)
            {
                foreach (var ptr in m_Strings)
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }

            if (m_ptrStrings != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(m_ptrStrings);
            }

            if (m_pSteamParamStringArray != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(m_pSteamParamStringArray);
            }
        }

        public static implicit operator IntPtr( SteamParamStringArray that )
        {
            return that.m_pSteamParamStringArray;
        }
    }
}

// TODO - Should be IDisposable
// MatchMaking Key-Value Pair Marshaller
public class MMKVPMarshaller
{
    private readonly IntPtr m_pNativeArray;
    private readonly IntPtr m_pArrayEntries;

    public MMKVPMarshaller( MatchMakingKeyValuePair_t[] filters )
    {
        if (filters == null)
        {
            return;
        }

        var sizeOfMMKVP = Marshal.SizeOf(typeof(MatchMakingKeyValuePair_t));

        m_pNativeArray = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)) * filters.Length);
        m_pArrayEntries = Marshal.AllocHGlobal(sizeOfMMKVP * filters.Length);
        for (var i = 0; i < filters.Length; ++i)
        {
            Marshal.StructureToPtr(filters[i], new IntPtr(m_pArrayEntries.ToInt64() + (i * sizeOfMMKVP)), false);
        }

        Marshal.WriteIntPtr(m_pNativeArray, m_pArrayEntries);
    }

    ~MMKVPMarshaller()
    {
        if (m_pArrayEntries != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(m_pArrayEntries);
        }
        if (m_pNativeArray != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(m_pNativeArray);
        }
    }

    public static implicit operator IntPtr( MMKVPMarshaller that )
    {
        return that.m_pNativeArray;
    }
}