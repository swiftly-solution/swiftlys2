// Reference: https://github.com/KZGlobalTeam/cs2kz-metamod/blob/8d4038394173f1c10d763346d45cd3ccbc0091aa/src/sdk/datatypes.h#L252-L278

using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct CMoveData
{
    public CMoveDataBase Base; // class CMoveData : public CMoveDataBase

    public Vector OutWishVel;
    public QAngle OldAngles;
    public Vector2D WalkWishedVelocity;

    public Vector Acceleration;

    public Vector ContinuousAcceleration;

    public float MaxSpeed;
    public float ClientMaxSpeed;
    public float FrictionDecel;
    private unsafe fixed byte _padding[3 * 4];

    [MarshalAs(UnmanagedType.U1)]
    public bool InAir;
    [MarshalAs(UnmanagedType.U1)]
    public bool GameCodeMovedPlayer; // true if usercmd cmd number == (m_nGameCodeHasMovedPlayerAfterCommand + 1)
}
