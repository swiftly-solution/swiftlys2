using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

public enum MapLoadType_t
{
    MapLoad_NewMap = 0,
    MapLoad_LoadGame,
    MapLoad_Transition,
    MapLoad_Background,
}

[StructLayout(LayoutKind.Sequential)]
public struct CGlobalVars
{
    public float RealTime;
    public int FrameCount;
    public float AbsoluteFrameTime;
    public float AbsoluteFrameStartTimeStdDev;
    public int MaxClients;
    private int _unk01;
    private int _unk02;
    private int _unk03;
    private int _unk04;
    private int _unk05;
    private nint _unk06;
    public float CurrentTime;
    public float FrameTime;
    private float _unk07;
    private float _unk08;
    public bool InSimulation;
    public bool EnableAssertions;
    public int TickCount;
    private int _unk09;
    private int _unk10;
    public float SubtickFraction;
    public CString MapName;
    public CString StartSpot;
    public MapLoadType_t MapLoadType;
    public bool TeamPlay;
    public int MaxEntities;
    public int ServerCount;
}