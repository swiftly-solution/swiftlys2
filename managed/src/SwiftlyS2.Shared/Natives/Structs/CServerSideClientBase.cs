using System.Runtime.InteropServices;
using SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.SteamAPI;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct Spike_t
{
    public CUtlString Desc;
    public int Bits;
}

[StructLayout(LayoutKind.Sequential)]
public struct CNetworkStatTrace
{
    public CUtlVector<Spike_t> Records;
    public int MinWarningBytes;
    public int StartBit;
    public int CurrentBit;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct CServerSideClientBase
{
    public nint _pVTableServerSideClient;
    public nint _pVTableSlot;
    public CUtlSlot Slot;
    public CUtlString UserIDString;
    public CUtlString Name;
    public int PlayerSlot;
    public int EntityIndex;
    public nint Server;
    public nint NetworkChannel;
    public ushort ConnectionTypeFlags;
    public byte MarkedToKick;
    public SignonState_t SignonState;
    public byte SplitScreenUser;
    public byte SplitAllowFastDisconnect;
    public int SplitScreenPlayerSlot;
    public CServerSideClientBase** SplitScreenUsers;
    public CServerSideClientBase* AttachedTo;
    public byte SplitPlayerDisconnecting;
    public int UnkVar;
    public byte FakeClient;
    public byte SendingSnapshot;
    public fixed byte Padding[0x5];
    public ushort UserID;
    public byte ReceivedPacket;
    public CSteamID SteamID;
    public CSteamID UnknownSteamID;
    public CSteamID UnknownSteamID2;
    public CSteamID FriendsID;
    public NSAddress Address;
    public NSAddress Address2;
    public KeyValues* ConVars;
    public byte Unk0;
    public fixed byte Padding2[0x28];
    public byte ConVarsChanged;
    public byte IsHLTV;
    public fixed byte Padding3[0xB];
    public uint SendtableCRC;
    public uint ChallengeNumber;
    public int SignonTick;
    public int DeltaTick;
    public int UnkVariable3;
    public int StringTableAckTick;
    public int UnkVar4;
    public nint LastSnapshot;
    public CUtlVector<uint> LoadedSpawnGroups;
    public fixed byte PlayerInfo[0x38];
    public nint BaselineSnapshot;
    public int BaselineUpdateTick;
    public CBitVec16384 BaselinesSent;
    public int BaselineUsed;
    public int LoadingProgress;
    public int ForceWaitForTick;
    public CCircularBuffer UnkBuffer;
    public byte LowViolence;
    public byte SomethingWithAddressType;
    public byte FullyAuthenticated;
    public byte Unk1;
    public int Unk;
    public float NextMessageTime;
    public float AuthenticatedTime;
    public float SnapshotInterval;
    public fixed byte PaddingPacketEntitiesMsg[0x138];
    public CNetworkStatTrace Trace;
    public int SpamCommandsCount;
    public int Unknown;
    public double LastExecutedCommand;
    public fixed byte Padding4[0x20];
}

[StructLayout(LayoutKind.Sequential)]
public struct VisInfo
{
    public uint VisBitsBufSize;
    public uint SpawnGroupHandle;
    public CBitVec4096 VisBits;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct CCheckTransmitInfo
{
    public CBitVec16384* TransmitEntities;
    public CBitVec16384* TransmitNonPlayers;
    public CBitVec16384* UnkBitVec2;
    public CBitVec16384* UnkBitVec3;
    public CBitVec16384* TransmitAlways;
    public CUtlLeanVector<int, int> TargetSlots;
    public VisInfo VisInfo;
    public int PlayerSlot;
    public byte FullUpdate;
}

public enum FailEnum_t
{
    FAILURE_ALREADY_IN_REPLAY,
    FAILURE_TOO_FREQUENT,
    FAILURE_NO_FRAME,
    FAILURE_NO_FRAME2,
    FAILURE_CANNOT_MATCH_DELAY,
    FAILURE_FRAME_NOT_READY,
    NUM_FAILURES
};

[StructLayout(LayoutKind.Sequential)]
public unsafe struct HltvReplayStats_t
{
    public uint Clients;
    public uint StartRequests;
    public uint SuccessfulStarts;
    public uint StopRequests;
    public uint AbortStopRequests;
    public uint UserCancels;
    public uint FullReplays;
    public uint NetAbortReplays;
    public fixed uint FailedReplays[(int)FailEnum_t.NUM_FAILURES];
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct CServerSideClient
{
    public CServerSideClientBase Base;
    public CBitVec64 VoiceStreams;
    public CBitVec64 VoiceProximity;
    public CCheckTransmitInfo TransmitInfo;
    public fixed byte FrameManager[0x120];
    public fixed byte Padding[0x8];
    public float LastClinetCommandQuotaStart;
    public float TimeClientBecameFullyConnected;
    public byte VoiceLoopback;
    public byte Unk10;
    public int HLTVReplayDelay;
    public nint HLTVReplayServer;
    public int HLTVReplayStopAt;
    public int HLTVReplayStartAt;
    public int HLTVReplaySlowdownBeginAt;
    public int HLTVReplaySlowdownEndAt;
    public float HLTVReplaySlowdownRate;
    public int HLTVLastSendTick;
    public float HLTVLastReplayRequestTime;
    public CUtlVector<nint> HLTVQueuedMessages;
    public HltvReplayStats_t hltvReplayStats;
    public nint LastJob;
    public fixed byte Padding2[0x8];
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct CHLTVClient
{
    public CServerSideClientBase Base;
    public CUtlString Password;
    public CUtlString ChatGroup;
    public double LastSendTime;
    public double LastChatTime;
    public int LastSendTick;
    public int Unknown2;
    public int FullFrameTime;
    public int Unknown3;
    public byte NoChat;
    public byte UnkBool;
    public byte UnkBool2;
    public byte UnkBool3;
    public fixed byte Padding[0x24];
}