using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SwiftlyS2.Core.Natives;

using System.Text;

namespace SwiftlyS2.Shared.SteamAPI;

/// <summary>
/// <para>Native <c>SteamParamStringArray_t</c> built from managed strings. Dispose it after the native call.</para>
/// <para>One block holds the <c>SteamParamStringArray_t</c> header, the string pointer table and the UTF-8 string data.</para>
/// </summary>
public unsafe ref struct SteamParamStringArray
{
	private void* _block;

	/// <summary>
	/// <para>Pointer to the native <c>SteamParamStringArray_t</c>, or null for a null list.</para>
	/// </summary>
	public readonly SteamParamStringArray_t* Pointer => (SteamParamStringArray_t*)_block;

	public SteamParamStringArray( IList<string>? strings )
	{
		if (strings is null)
		{
			_block = null;
			return;
		}

		var count = strings.Count;

		// Header is 12 bytes with Pack = 4, so align the pointer table to 8 bytes
		var tableOffset = (sizeof(SteamParamStringArray_t) + sizeof(nint) - 1) & ~(sizeof(nint) - 1);
		var tableSize = count * sizeof(nint);
		var dataSize = 0;
		for (var i = 0; i < count; i++)
		{
			dataSize += Encoding.UTF8.GetByteCount(strings[i]) + 1;
		}

		var block = (byte*)NativeMemory.Alloc((nuint)(tableOffset + tableSize + dataSize));
		var table = (nint*)(block + tableOffset);
		var data = block + tableOffset + tableSize;

		for (var i = 0; i < count; i++)
		{
			var written = Encoding.UTF8.GetBytes(strings[i], new Span<byte>(data, Encoding.UTF8.GetByteCount(strings[i])));
			data[written] = 0;
			table[i] = (nint)data;
			data += written + 1;
		}

		*(SteamParamStringArray_t*)block = new SteamParamStringArray_t
		{
			m_ppStrings = (nint)table,
			m_nNumStrings = count,
		};

		_block = block;
	}

	public static implicit operator nint( SteamParamStringArray that ) => (nint)that._block;

	public void Dispose()
	{
		NativeMemory.Free(_block);
		_block = null;
	}
}
