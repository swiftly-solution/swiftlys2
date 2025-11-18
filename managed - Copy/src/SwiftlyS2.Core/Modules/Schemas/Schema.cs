using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Extensions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Buffers;
using System.Runtime.InteropServices;

namespace SwiftlyS2.Core.Schemas;

internal static class Schema
{
  private static readonly HashSet<ulong> dangerousFields = new() {
    0x509D90A88DFCB984, // CMaterialAttributeAnimTag.m_flValue
    0xCB1D2D708DFCB984, // CNmConstFloatNode__CDefinition.m_flValue
    0xB6A452E28DFCB984, // MaterialParamFloat_t.m_flValue
    0x37D4D7108DFCB984, // CAudioEmphasisSample.m_flValue
    0xF12966B68DFCB984, // NmPercent_t.m_flValue
    0x5EC3BE448DFCB984, // CSeqPoseSetting.m_flValue
    0xF4BEF78E8DFCB984, // ConstantInfo_t.m_flValue
    0x6295CF65B14BF634, // CCSGameRules.m_bIsValveDS
    0x6295CF65814483B8, // CCSGameRules.m_bIsQuestEligible
    0xBB0F80FC8DAFCD73, // CEconItemAttribute.m_iAttributeDefinitionIndex
    0xBB0F80FC8DFCB984, // CEconItemAttribute.m_flValue
    0xBB0F80FCE2DBFFF2, // CEconItemAttribute.m_flInitialValue
    0xBB0F80FC1021E694, // CEconItemAttribute.m_nRefundableCurrency
    0xBB0F80FCA5E9EA96, // CEconItemAttribute.m_bSetBonus
    0x28ECD7A1D82CC087, // CCSPlayerController.m_iCompetitiveRanking
    0x28ECD7A192776C10, // CCSPlayerController.m_iCompetitiveWins
    0x28ECD7A15803DF71, // CCSPlayerController.m_iCompetitiveRankType
    0x28ECD7A1C32AD2BC, // CCSPlayerController.m_iCompetitiveRankingPredicted_Win
    0x28ECD7A1BDCCE5ED, // CCSPlayerController.m_iCompetitiveRankingPredicted_Loss
    0x28ECD7A138CA4C74, // CCSPlayerController.m_iCompetitiveRankingPredicted_Tie
    0xE1A93F256A67D4C4, // CEconItemView.m_iEntityQuality
    0xE1A93F2555EF3B5F, // CEconItemView.m_iEntityLevel
    0xE1A93F25373EE446, // CEconItemView.m_iItemIDHigh
    0xE1A93F250DF29C2C, // CEconItemView.m_iItemIDLow
    0xE1A93F25C65DE986, // CEconItemView.m_iAccountID
    0xE1A93F250710ABDD, // CEconItemView.m_bInitialized
    0xE1A93F25AFD12EE8, // CEconItemView.m_szCustomName
    0xCD91F6843C990CE3, // CEconEntity.m_OriginalOwnerXuidLow
    0xCD91F6842628947F, // CEconEntity.m_OriginalOwnerXuidHigh
    0xCD91F6840A12D48F, // CEconEntity.m_nFallbackPaintKit
    0xCD91F684A1B165B2, // CEconEntity.m_nFallbackSeed
    0xCD91F68486253266, // CEconEntity.m_flFallbackWear
    0xCD91F68467ECC1E7, // CEconEntity.m_nFallbackStatTrak
  };

  private static readonly bool isFollowingServerGuidelines = NativeServerHelpers.IsFollowingServerGuidelines();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static nint GetOffset( ulong hash )
  {
    if (isFollowingServerGuidelines && dangerousFields.Contains(hash))
    {
      throw new InvalidOperationException($"Cannot get or set 0x{hash:X16} while \"FollowCS2ServerGuidelines\" is enabled.\n\tTo use this operation, disable the option in core.jsonc.");
    }
    return NativeSchema.GetOffset(hash);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Update( nint handle, ulong hash )
  {
    if (isFollowingServerGuidelines && dangerousFields.Contains(hash))
    {
      throw new InvalidOperationException($"Cannot get or set 0x{hash:X16} while \"FollowCS2ServerGuidelines\" is enabled.\n\tTo use this operation, disable the option in core.jsonc.");
    }
    NativeSchema.SetStateChanged(handle, hash);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void SetString( nint handle, nint offset, string value )
  {
    handle.Write(offset, StringPool.Allocate(value));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void SetFixedString( nint handle, nint offset, string value, int maxSize )
  {
    var pool = ArrayPool<byte>.Shared;
    var size = Encoding.UTF8.GetByteCount(value);
    if (size + 1 > maxSize)
    {
      throw new ArgumentException("Value is too long. Max size is " + maxSize);
    }
    var bytes = pool.Rent(size + 1);
    Encoding.UTF8.GetBytes(value, bytes);
    bytes[size] = 0;
    Unsafe.CopyBlockUnaligned(
      ref handle.AsRef<byte>(offset),
      ref bytes[0],
      (uint)(size + 1)
    );
    pool.Return(bytes);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetString( nint handle )
  {
    return Marshal.PtrToStringUTF8(handle) ?? string.Empty;
  }

}