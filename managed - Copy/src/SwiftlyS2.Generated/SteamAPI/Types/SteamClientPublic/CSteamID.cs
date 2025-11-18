using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace SwiftlyS2.Shared.SteamAPI;

public partial class SteamIdParser
{
	private const ulong STEAM_ID_BASE = 76561197960265728;

	private static readonly Regex SteamId64Regex = MyRegex();
	private static readonly Regex SteamIdRegex = MyRegex1();
	private static readonly Regex SteamId3Regex = MyRegex2();
	private static readonly Regex SteamIdOnlineRegex = MyRegex3();

	public static ulong? ParseToSteamId64( string input )
	{
		if (string.IsNullOrWhiteSpace(input))
			return null;

		input = input.Trim();

		if (TryParseSteamId64(input, out var steamId64))
			return steamId64;

		if (TryParseSteamIdOnline(input, out steamId64))
			return steamId64;

		if (TryParseSteamId(input, out steamId64))
			return steamId64;

		if (TryParseSteamId3(input, out steamId64))
			return steamId64;

		return null;
	}

	private static bool TryParseSteamId64( string input, out ulong steamId64 )
	{
		steamId64 = 0;

		if (!SteamId64Regex.IsMatch(input))
			return false;

		return ulong.TryParse(input, out steamId64) && steamId64 >= STEAM_ID_BASE;
	}

	private static bool TryParseSteamId( string input, out ulong steamId64 )
	{
		steamId64 = 0;

		var match = SteamIdRegex.Match(input);
		if (!match.Success)
			return false;

		if (!uint.TryParse(match.Groups[1].Value, out uint y))
			return false;

		if (!uint.TryParse(match.Groups[2].Value, out uint z))
			return false;

		if (y > 1)
			return false;

		steamId64 = STEAM_ID_BASE + (z * 2) + y;
		return true;
	}

	public static bool TryParseSteamIdOnline( string input, out ulong steamId64 )
	{
		steamId64 = 0;

		var match = SteamIdOnlineRegex.Match(input);
		if (!match.Success)
			return false;

		if (!uint.TryParse(match.Groups[1].Value, out uint y))
			return false;

		if (!uint.TryParse(match.Groups[2].Value, out uint z))
			return false;

		if (y > 1)
			return false;

		steamId64 = STEAM_ID_BASE + (z * 2) + y;
		return true;
	}

	private static bool TryParseSteamId3( string input, out ulong steamId64 )
	{
		steamId64 = 0;

		var match = SteamId3Regex.Match(input);
		if (!match.Success)
			return false;

		if (!uint.TryParse(match.Groups[1].Value, out uint accountId))
			return false;

		steamId64 = STEAM_ID_BASE + accountId;
		return true;
	}

	public static bool IsValidSteamId64( ulong steamId64 )
	{
		return steamId64 >= STEAM_ID_BASE;
	}

	public static string ToSteamId( ulong steamId64 )
	{
		if (!IsValidSteamId64(steamId64))
			return null;

		var accountId = steamId64 - STEAM_ID_BASE;
		var y = accountId % 2;
		var z = accountId / 2;

		return $"STEAM_0:{y}:{z}";
	}

	public static string ToSteamId3( ulong steamId64 )
	{
		if (!IsValidSteamId64(steamId64))
			return null;

		var accountId = steamId64 - STEAM_ID_BASE;
		return $"[U:1:{accountId}]";
	}

	public static string ToSteamIdOnline( ulong steamId64 )
	{
		if (!IsValidSteamId64(steamId64))
			return null;

		var accountId = steamId64 - STEAM_ID_BASE;
		var y = accountId % 2;
		var z = accountId / 2;
		return $"STEAM_1:{y}:{z}";
	}

	[GeneratedRegex(@"^7656119[0-9]{10}$")]
	private static partial Regex MyRegex();

	[GeneratedRegex(@"^STEAM_[0-5]:([0-1]):([0-9]+)$")]
	private static partial Regex MyRegex1();

	[GeneratedRegex(@"^\[U:1:([0-9]+)\]$")]
	private static partial Regex MyRegex2();

	[GeneratedRegex(@"^STEAM_1:([0-1]):([0-9]+)$")]
	private static partial Regex MyRegex3();
}


[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct CSteamID : IEquatable<CSteamID>, IComparable<CSteamID>
{
	public static readonly CSteamID Nil = new();
	public static readonly CSteamID OutofDateGS = new(new AccountID_t(0), 0, EUniverse.k_EUniverseInvalid, EAccountType.k_EAccountTypeInvalid);
	public static readonly CSteamID LanModeGS = new(new AccountID_t(0), 0, EUniverse.k_EUniversePublic, EAccountType.k_EAccountTypeInvalid);
	public static readonly CSteamID NotInitYetGS = new(new AccountID_t(1), 0, EUniverse.k_EUniverseInvalid, EAccountType.k_EAccountTypeInvalid);
	public static readonly CSteamID NonSteamGS = new(new AccountID_t(2), 0, EUniverse.k_EUniverseInvalid, EAccountType.k_EAccountTypeInvalid);
	public ulong m_SteamID;

	public CSteamID( AccountID_t unAccountID, EUniverse eUniverse, EAccountType eAccountType )
	{
		m_SteamID = 0;
		Set(unAccountID, eUniverse, eAccountType);
	}

	public CSteamID( AccountID_t unAccountID, uint unAccountInstance, EUniverse eUniverse, EAccountType eAccountType )
	{
		m_SteamID = 0;
#if _SERVER && Assert
		Assert( ! ( ( EAccountType.k_EAccountTypeIndividual == eAccountType ) && ( unAccountInstance > k_unSteamUserWebInstance ) ) );	// enforce that for individual accounts, instance is always 1
#endif // _SERVER
		InstancedSet(unAccountID, unAccountInstance, eUniverse, eAccountType);
	}

	public CSteamID( ulong ulSteamID )
	{
		m_SteamID = ulSteamID;
	}

	public CSteamID( string sSteamID )
	{
		m_SteamID = SteamIdParser.ParseToSteamId64(sSteamID) ?? 0;
	}

	public void Set( AccountID_t unAccountID, EUniverse eUniverse, EAccountType eAccountType )
	{
		SetAccountID(unAccountID);
		SetEUniverse(eUniverse);
		SetEAccountType(eAccountType);

		if (eAccountType == EAccountType.k_EAccountTypeClan || eAccountType == EAccountType.k_EAccountTypeGameServer)
		{
			SetAccountInstance(0);
		}
		else
		{
			SetAccountInstance(Constants.k_unSteamUserDefaultInstance);
		}
	}

	public void InstancedSet( AccountID_t unAccountID, uint unInstance, EUniverse eUniverse, EAccountType eAccountType )
	{
		SetAccountID(unAccountID);
		SetEUniverse(eUniverse);
		SetEAccountType(eAccountType);
		SetAccountInstance(unInstance);
	}

	public void Clear()
	{
		m_SteamID = 0;
	}

	public void CreateBlankAnonLogon( EUniverse eUniverse )
	{
		SetAccountID(new AccountID_t(0));
		SetEUniverse(eUniverse);
		SetEAccountType(EAccountType.k_EAccountTypeAnonGameServer);
		SetAccountInstance(0);
	}

	public void CreateBlankAnonUserLogon( EUniverse eUniverse )
	{
		SetAccountID(new AccountID_t(0));
		SetEUniverse(eUniverse);
		SetEAccountType(EAccountType.k_EAccountTypeAnonUser);
		SetAccountInstance(0);
	}

	//-----------------------------------------------------------------------------
	// Purpose: Is this an anonymous game server login that will be filled in?
	//-----------------------------------------------------------------------------
	public bool BBlankAnonAccount()
	{
		return GetAccountID() == new AccountID_t(0) && BAnonAccount() && GetUnAccountInstance() == 0;
	}

	//-----------------------------------------------------------------------------
	// Purpose: Is this a game server account id?  (Either persistent or anonymous)
	//-----------------------------------------------------------------------------
	public bool BGameServerAccount()
	{
		return GetEAccountType() == EAccountType.k_EAccountTypeGameServer || GetEAccountType() == EAccountType.k_EAccountTypeAnonGameServer;
	}

	//-----------------------------------------------------------------------------
	// Purpose: Is this a persistent (not anonymous) game server account id?
	//-----------------------------------------------------------------------------
	public bool BPersistentGameServerAccount()
	{
		return GetEAccountType() == EAccountType.k_EAccountTypeGameServer;
	}

	//-----------------------------------------------------------------------------
	// Purpose: Is this an anonymous game server account id?
	//-----------------------------------------------------------------------------
	public bool BAnonGameServerAccount()
	{
		return GetEAccountType() == EAccountType.k_EAccountTypeAnonGameServer;
	}

	//-----------------------------------------------------------------------------
	// Purpose: Is this a content server account id?
	//-----------------------------------------------------------------------------
	public bool BContentServerAccount()
	{
		return GetEAccountType() == EAccountType.k_EAccountTypeContentServer;
	}


	//-----------------------------------------------------------------------------
	// Purpose: Is this a clan account id?
	//-----------------------------------------------------------------------------
	public bool BClanAccount()
	{
		return GetEAccountType() == EAccountType.k_EAccountTypeClan;
	}


	//-----------------------------------------------------------------------------
	// Purpose: Is this a chat account id?
	//-----------------------------------------------------------------------------
	public bool BChatAccount()
	{
		return GetEAccountType() == EAccountType.k_EAccountTypeChat;
	}

	//-----------------------------------------------------------------------------
	// Purpose: Is this a chat account id?
	//-----------------------------------------------------------------------------
	public bool IsLobby()
	{
		return (GetEAccountType() == EAccountType.k_EAccountTypeChat)
			&& (GetUnAccountInstance() & (int)EChatSteamIDInstanceFlags.k_EChatInstanceFlagLobby) != 0;
	}


	//-----------------------------------------------------------------------------
	// Purpose: Is this an individual user account id?
	//-----------------------------------------------------------------------------
	public bool BIndividualAccount()
	{
		return GetEAccountType() == EAccountType.k_EAccountTypeIndividual || GetEAccountType() == EAccountType.k_EAccountTypeConsoleUser;
	}


	//-----------------------------------------------------------------------------
	// Purpose: Is this an anonymous account?
	//-----------------------------------------------------------------------------
	public bool BAnonAccount()
	{
		return GetEAccountType() == EAccountType.k_EAccountTypeAnonUser || GetEAccountType() == EAccountType.k_EAccountTypeAnonGameServer;
	}

	//-----------------------------------------------------------------------------
	// Purpose: Is this an anonymous user account? ( used to create an account or reset a password )
	//-----------------------------------------------------------------------------
	public bool BAnonUserAccount()
	{
		return GetEAccountType() == EAccountType.k_EAccountTypeAnonUser;
	}

	//-----------------------------------------------------------------------------
	// Purpose: Is this a faked up Steam ID for a PSN friend account?
	//-----------------------------------------------------------------------------
	public bool BConsoleUserAccount()
	{
		return GetEAccountType() == EAccountType.k_EAccountTypeConsoleUser;
	}

	public void SetAccountID( AccountID_t other )
	{
		m_SteamID = (m_SteamID & ~(0xFFFFFFFFul << (ushort)0)) | (((ulong)(other) & 0xFFFFFFFFul) << (ushort)0);
	}

	public void SetAccountInstance( uint other )
	{
		m_SteamID = (m_SteamID & ~(0xFFFFFul << (ushort)32)) | (((ulong)(other) & 0xFFFFFul) << (ushort)32);
	}

	// This is a non standard/custom function not found in C++ Steamworks
	public void SetEAccountType( EAccountType other )
	{
		m_SteamID = (m_SteamID & ~(0xFul << (ushort)52)) | (((ulong)(other) & 0xFul) << (ushort)52);
	}

	public void SetEUniverse( EUniverse other )
	{
		m_SteamID = (m_SteamID & ~(0xFFul << (ushort)56)) | (((ulong)(other) & 0xFFul) << (ushort)56);
	}

	public AccountID_t GetAccountID()
	{
		return new AccountID_t((uint)(m_SteamID & 0xFFFFFFFFul));
	}

	public uint GetUnAccountInstance()
	{
		return (uint)((m_SteamID >> 32) & 0xFFFFFul);
	}

	public EAccountType GetEAccountType()
	{
		return (EAccountType)((m_SteamID >> 52) & 0xFul);
	}

	public EUniverse GetEUniverse()
	{
		return (EUniverse)((m_SteamID >> 56) & 0xFFul);
	}


	public bool IsValid()
	{
		if (GetEAccountType() <= EAccountType.k_EAccountTypeInvalid || GetEAccountType() >= EAccountType.k_EAccountTypeMax)
			return false;

		if (GetEUniverse() <= EUniverse.k_EUniverseInvalid || GetEUniverse() >= EUniverse.k_EUniverseMax)
			return false;

		if (GetEAccountType() == EAccountType.k_EAccountTypeIndividual)
		{
			if (GetAccountID() == new AccountID_t(0) || GetUnAccountInstance() > Constants.k_unSteamUserDefaultInstance)
				return false;
		}

		if (GetEAccountType() == EAccountType.k_EAccountTypeClan)
		{
			if (GetAccountID() == new AccountID_t(0) || GetUnAccountInstance() != 0)
				return false;
		}

		if (GetEAccountType() == EAccountType.k_EAccountTypeGameServer)
		{
			if (GetAccountID() == new AccountID_t(0))
				return false;
			// Any limit on instances?  We use them for local users and bots
		}
		return true;
	}

	public ulong GetSteamID64()
	{
		return m_SteamID;
	}

	public string GetSteamID()
	{
		return SteamIdParser.ToSteamId(m_SteamID);
	}

	public string GetSteamID3()
	{
		return SteamIdParser.ToSteamId3(m_SteamID);
	}

	public uint GetSteamID32()
	{
		return GetAccountID().m_AccountID;
	}

	public string GetSteamIDOnline()
	{
		return SteamIdParser.ToSteamIdOnline(m_SteamID);
	}

	#region Overrides
	public override string ToString()
	{
		return m_SteamID.ToString();
	}

	public override bool Equals( object other )
	{
		return other is CSteamID && this == (CSteamID)other;
	}

	public override int GetHashCode()
	{
		return m_SteamID.GetHashCode();
	}

	public static bool operator ==( CSteamID x, CSteamID y )
	{
		return x.m_SteamID == y.m_SteamID;
	}

	public static bool operator !=( CSteamID x, CSteamID y )
	{
		return !(x == y);
	}

	public static explicit operator CSteamID( ulong value )
	{
		return new CSteamID(value);
	}
	public static explicit operator ulong( CSteamID that )
	{
		return that.m_SteamID;
	}

	public bool Equals( CSteamID other )
	{
		return m_SteamID == other.m_SteamID;
	}

	public int CompareTo( CSteamID other )
	{
		return m_SteamID.CompareTo(other.m_SteamID);
	}
	#endregion
}